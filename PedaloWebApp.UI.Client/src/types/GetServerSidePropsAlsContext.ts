import { GetServerSidePropsContext } from "next";

export interface GetServerSidePropsAlsContext {
  locale?: string;
  serverSidePropsContext: GetServerSidePropsContext;
}
