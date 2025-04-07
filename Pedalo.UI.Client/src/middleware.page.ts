import { NextMiddleware, NextResponse } from "next/server";
import { env } from "process";

const middleware: NextMiddleware = async (req) => {
  const { locale, pathname } = req.nextUrl;
  const { cookies } = req;
  const cookieLocale = cookies.get("NEXT_LOCALE")?.value;

  const shouldRedirectToHttps = () => {
    const sslDetector = env.SSL_DETECTOR;
    const isServer = typeof window === "undefined";

    if (!sslDetector || !isServer) return false;

    if (sslDetector === "azure" && !!req.headers.get("x-arr-ssl")) return true;
    if (sslDetector === "googleCloud" && req.headers.get("x-forwarded-proto") === "https") return true;
  };

  if (shouldRedirectToHttps()) {
    return NextResponse.redirect(`https://${req.headers.get("host")}${pathname}`, 301);
  }

  const ignoredPaths = ["static", "images", "api"];

  // ignore next.js specific paths
  if (ignoredPaths.some((path) => pathname.startsWith(`/${path}`))) {
    return NextResponse.next();
  }

  // if the cookie is not set do nothing
  if (!cookieLocale) {
    return NextResponse.next();
  }

  // if the cookie has a different value than the locale in the url redirect to the cookie locale
  if (cookieLocale !== locale) {
    return NextResponse.redirect(new URL(`/${cookieLocale}${pathname}${req.nextUrl.search}`, req.url), 307);
  }

  return NextResponse.next();
};

// Removed withAuth wrapper and related logic

export default middleware;
