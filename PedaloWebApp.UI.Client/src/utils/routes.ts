// eslint-disable-next-line import/no-named-as-default -- only exported as default
import NextRouter from "next/router";
import { Query, Route, route } from "nextjs-routes";
import { GetServerSidePropsContext } from "next";
import { RouteParams } from "@neolution-ch/react-data-table";
export type Pathname = Route["pathname"];
export type CombinedRoute = Route | Exclude<Route, { query: unknown }>["pathname"] | Omit<Route, "pathname">;

/**
 * Cleans up the params object by removing all empty values.
 * @param params The params object to clean up.
 * @returns A new object without empty values.
 */
const cleanUpParams = (params?: RouteParams): Query | undefined => {
  if (!params) return params;

  const copy = { ...params };

  Object.keys(copy).forEach((key) => {
    if (!copy[key]) {
      delete copy[key];
    }
  });

  return copy as Query;
};

/**
 * A method to redirect to a strongly typed route.
 * :warning: Only use this method on the server side or outside of a React component.
 * In React components, use the `useRouter` hook instead.
 * @param target The target route to redirect to. If you want to redirect to a route with params, you can use the `{ pathname: string, query: { foo: "baz" } }` .
 * @param ctx The optional context object from `getServerSideProps`.
 * @param httpCode The optional HTTP code to use for the redirect. Defaults to 302.
 */
const redirectToRoute = async (target: CombinedRoute, ctx?: GetServerSidePropsContext, httpCode = 302) => {
  const url =
    typeof target === "string"
      ? route({
          pathname: target,
        })
      : route(target as Route);

  if (ctx?.res) {
    ctx.res.writeHead(httpCode, { location: url });
    ctx.res.end();
  } else {
    await NextRouter.push(url);
  }

  // we wait forever here so there is no code execution while the user gets redirected...
  await new Promise(() => {
    console.log("Waiting for redirect...");
  });
};

export { redirectToRoute, cleanUpParams };
