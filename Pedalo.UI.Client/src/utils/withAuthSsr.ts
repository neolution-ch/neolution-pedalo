import { AlsContext } from "@neolution-ch/als-context";
import type { GetServerSidePropsContext, GetServerSidePropsResult } from "next";
import { GetServerSidePropsAlsContext } from "src/types/GetServerSidePropsAlsContext";

export function withAuthSsr<P extends { [key: string]: unknown } = { [key: string]: unknown }>(
  handler: (
    context: GetServerSidePropsContext,
  ) => GetServerSidePropsResult<P> | Promise<GetServerSidePropsResult<P>>,
) {
  return async function (context: GetServerSidePropsContext) {
    const { locale, defaultLocale } = context;

    const alsContext = new AlsContext<GetServerSidePropsAlsContext>();
    const result = await alsContext.run(async () => await handler(context), {
      locale: locale || defaultLocale,
      serverSidePropsContext: context,
    });

    return result;
  };
}
