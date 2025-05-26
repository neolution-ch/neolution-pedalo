import axios, { AxiosRequestConfig } from "axios";
import { registerDateResponseInterceptor } from "../interceptors/response/dates";
import { toastError } from "src/components/Misc/Toasts";
import { registerConvertNullToUndefinedResponseInterceptor } from "../interceptors/response/nullToUndefined";

const browserInstance = axios.create({
  baseURL: "/api/proxy",
});

browserInstance.interceptors.request.use((request) => {
  const isServer = typeof window === "undefined";

  if (isServer) throw new Error("This interceptor should only be used on the client side.");

  return request;
});

registerConvertNullToUndefinedResponseInterceptor(browserInstance);
registerDateResponseInterceptor(browserInstance);

browserInstance.interceptors.response.use(undefined, async (error) => {
  if (axios.isAxiosError(error)) {
    const { customAxiosRequestConfig } = error.config as AxiosRequestConfig;
    const { toastErrors } = customAxiosRequestConfig || {};

    const status = error.response?.status || 0;

    if ((toastErrors && status >= 400 && status < 600) || status === 402) {
      toastError(`Error calling ${error?.config?.url} ${error.message}`);
      return;
    }
  }

  return Promise.reject(error);
});

export { browserInstance };
