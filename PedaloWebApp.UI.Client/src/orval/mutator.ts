import { AxiosInstance, AxiosRequestConfig } from "axios";
import { browserInstance } from "src/axios/instances/browserInstance";
import { getServerSidePropsInstance } from "src/axios/instances/getServerSidePropsInstance";

export interface MutatorOptions {
  overrideAxiosRequestConfig?: Partial<AxiosRequestConfig>;
  customAxiosRequestConfig?: Partial<CustomAxiosRequestConfig>;
  axiosInstance?: AxiosInstance;
}

export interface CustomAxiosRequestConfig {
  toastErrors: boolean;
  handleUnauthorized: boolean;
  handleForbidden: boolean;
}

const mutator = <T>(config: AxiosRequestConfig, mutatorOptions?: Partial<MutatorOptions>): Promise<T> => {
  const { customAxiosRequestConfig, overrideAxiosRequestConfig, axiosInstance } = mutatorOptions || {};
  const isServer = typeof localStorage === "undefined";
  const defaultInstance = isServer ? getServerSidePropsInstance : browserInstance;
  const instance = axiosInstance || defaultInstance;

  const defaultedCustomAxiosRequestConfig: CustomAxiosRequestConfig = {
    toastErrors: true,
    handleUnauthorized: true,
    handleForbidden: true,
    ...customAxiosRequestConfig,
  };

  const promise = instance({
    ...config,
    ...overrideAxiosRequestConfig,
    customAxiosRequestConfig: defaultedCustomAxiosRequestConfig,
  }).then((response) => response?.data);
  return promise;
};

export { mutator };
