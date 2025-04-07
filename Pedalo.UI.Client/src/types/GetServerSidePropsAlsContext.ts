import { GetServerSidePropsContext } from "next";

export interface GetServerSidePropsAlsContext {
  token?: string;
  locale?: string;
  serverSidePropsContext: GetServerSidePropsContext;
}
