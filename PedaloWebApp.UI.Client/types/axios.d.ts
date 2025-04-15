import "axios";
import { CustomAxiosRequestConfig } from "src/orval/mutator";

declare module "axios" {
  export interface AxiosRequestConfig {
    customAxiosRequestConfig?: CustomAxiosRequestConfig;
  }
}
