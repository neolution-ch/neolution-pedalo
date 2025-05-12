import { getCookie } from "cookies-next";
import httpProxy from "http-proxy";
import { NextApiRequest, NextApiResponse } from "next";
import getNextConfig from "next/config";

const {
  serverRuntimeConfig: { validateSslCertificats },
} = getNextConfig();

const proxy = httpProxy.createProxyServer({ secure: validateSslCertificats });

// Make sure that we don't parse JSON bodies on this route:
export const config = {
  api: {
    bodyParser: false,
    externalResolver: true,
  },
};

export default async (req: NextApiRequest, res: NextApiResponse) => {
  const {
    publicRuntimeConfig: { apiBaseUrl },
  } = getNextConfig();
  const target = `${apiBaseUrl}/${req.url?.replace("/api/proxy/", "")}`;
  const headers: { [header: string]: string } = {};

  // Remove the Authorization header if no token is found,
  // to mitigate attacks directly on the API
  headers.Authorization = "";

  const locale = getCookie("NEXT_LOCALE", { req })?.toString();
  if (locale) {
    headers.Language = locale;
  }

  // Remove the cookie header to prevent leaking sensitive information
  headers["cookie"] = "";

  new Promise<void>((resolve, reject) => {
    proxy.web(
      req,
      res,
      {
        target,
        changeOrigin: true,
        ignorePath: true,
        headers,
        xfwd: true,
      },
      (err) => {
        if (err) {
          console.error("Error proxying request", err);
          return reject(err);
        }
        resolve();
      },
    );
  });
};
