/* eslint-disable */
const config = require("config");
const { IgnorePlugin, DefinePlugin } = require("webpack");
const withRoutes = require("nextjs-routes/config")();
const { createSecureHeaders } = require("next-secure-headers");

const withBundleAnalyzer = require("@next/bundle-analyzer")({
  enabled: process.env.ANALYZE === "true",
});

const { serverRuntimeConfig, publicRuntimeConfig } = config;

const { validateSslCertificats } = serverRuntimeConfig;

// write this value to the environment so that it can be used in the middleware:
// https://github.com/vercel/next.js/discussions/35690
process.env.SSL_DETECTOR = serverRuntimeConfig.sslDetector;

if (!validateSslCertificats) {
  process.env.NODE_TLS_REJECT_UNAUTHORIZED = 0;
}

/** @type {import('next').NextConfig} */
const nextConfig = {
  webpack: (config, { isServer }) => {
    if (!isServer) {
      config.plugins.push(
        new IgnorePlugin({
          checkResource(resource) {
            const ignoredModules = ["async_hooks", "@neolution/als-context"];

            return ignoredModules.some((m) => resource.includes(m));
          },
        }),
      );
    }

    return config;
  },
  serverRuntimeConfig,
  publicRuntimeConfig,
  pageExtensions: ["page.tsx", "page.ts"],
  i18n: {
    defaultLocale: "de-CH",
    locales: ["de-CH", "en-GB", "ru-RU"],
    localeDetection: true,
  },
  headers: async () => {
    //  https://github.com/vercel/next.js/discussions/17991
    const secureHeaders = ["/(.*?)", "/"].map((source) => ({
      source,
      headers: createSecureHeaders({
        forceHTTPSRedirect: serverRuntimeConfig.addHstsHeader
          ? [
              true,
              {
                maxAge: 63072000, // 2 years
                includeSubDomains: true,
                preload: true,
              },
            ]
          : false,
        contentSecurityPolicy: {
          directives: {
            defaultSrc: ["'self'"],
            styleSrc: ["'self'", "'unsafe-inline'"],
            scriptSrc: ["'self'", "'unsafe-eval'"],
            imgSrc: ["'self'", "authjs.dev", "data:"],
          },
        },
      }),
    }));

    const apiNoCacheHeaders = [
      {
        source: "/api(.*)",
        headers: [
          {
            key: "cache-control",
            value: "no-cache, no-store",
          },
          {
            key: "pragma",
            value: "no-cache",
          },
          {
            key: "expires",
            value: "0",
          },
        ],
      },
      {
        source: "/api/auth/signout",
        headers: [
          {
            key: "clear-site-data",
            value: '"*"',
          },
        ],
      },
    ];

    const result = secureHeaders.concat(apiNoCacheHeaders);
    return result;
  },
};

module.exports = async () => {
  return withRoutes(withBundleAnalyzer(nextConfig));
};
