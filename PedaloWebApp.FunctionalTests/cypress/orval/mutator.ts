import type { AxiosRequestConfig } from "axios";
import axios from "axios";

// eslint-disable-next-line @typescript-eslint/no-unused-vars
const mutator = <T>(config: AxiosRequestConfig, _notUsed = undefined): Promise<T> => {
  const promise = axios({
    ...config,
    baseURL: "/api/proxy",
  }).then((response) => response.data as T);

  return promise;
};

export { mutator };
