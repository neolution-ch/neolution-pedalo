import { convertNullToUndefined } from "@neolution-ch/javascript-utils";
import { AxiosInstance } from "axios";

const registerConvertNullToUndefinedResponseInterceptor = (instance: AxiosInstance): void => {
  instance.interceptors.response.use((response) => {
    convertNullToUndefined(response.data);
    return response;
  });
};

export { registerConvertNullToUndefinedResponseInterceptor };
