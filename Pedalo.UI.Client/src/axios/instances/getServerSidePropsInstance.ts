import axios from "axios";
import { registerDateResponseInterceptor } from "../interceptors/response/dates";
import { AlsContext } from "@neolution-ch/als-context";
import { GetServerSidePropsAlsContext } from "src/types/GetServerSidePropsAlsContext";
import getConfig from "next/config";
import { registerConvertNullToUndefinedResponseInterceptor } from "../interceptors/response/nullToUndefined";

const {
  publicRuntimeConfig: { apiBaseUrl },
} = getConfig();

const getServerSidePropsInstance = axios.create({
  baseURL: apiBaseUrl,
});

registerConvertNullToUndefinedResponseInterceptor(getServerSidePropsInstance);
registerDateResponseInterceptor(getServerSidePropsInstance);

getServerSidePropsInstance.interceptors.request.use((request) => {
  const isServer = typeof window === "undefined";

  if (!isServer) throw new Error("This interceptor should only be used on the server side.");

  const alsContext = new AlsContext<GetServerSidePropsAlsContext>();
  const store = alsContext.getStore();
  if (!store) return request;

  const { locale } = store;

  request.headers.Language = locale;

  return request;
});

getServerSidePropsInstance.interceptors.response.use(undefined, async (error) => Promise.reject(error));

export { getServerSidePropsInstance };
