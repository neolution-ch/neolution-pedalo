import { RouteParams } from "@neolution-ch/react-data-table";
import Link from "next/link";
import { PropsWithChildren } from "react";
import { Pathname, cleanUpParams } from "src/utils/routes";

interface ReactDataTableLinkRenderProps extends PropsWithChildren {
  route: Pathname;
  params?: RouteParams;
}

const ReactDataTableLinkRender = ({ route, params, children }: ReactDataTableLinkRenderProps) => (
  <Link href={{ pathname: route, query: cleanUpParams(params) }}>{children}</Link>
);

export { ReactDataTableLinkRender };
