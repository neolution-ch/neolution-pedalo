import axios from "axios";
import { registerDateResponseInterceptor } from "../interceptors/response/dates";
import getConfig from "next/config";
import { registerConvertNullToUndefinedResponseInterceptor } from "../interceptors/response/nullToUndefined";

const {
  publicRuntimeConfig: { apiBaseUrl },
} = getConfig();

const nextAuthInstance = axios.create({
  baseURL: apiBaseUrl,
});

registerConvertNullToUndefinedResponseInterceptor(nextAuthInstance);
registerDateResponseInterceptor(nextAuthInstance);

export { nextAuthInstance };
