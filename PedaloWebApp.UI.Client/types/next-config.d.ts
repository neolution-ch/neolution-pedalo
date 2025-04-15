declare module "next/config" {
  type ConfigTypes = () => Config;

  export interface Config {
    publicRuntimeConfig: PublicRuntimeConfig;
    serverRuntimeConfig: ServerRuntimeConfig;
  }

  interface PublicRuntimeConfig {
    apiBaseUrl: string;
    showCopyErrorMessageButton: boolean;
  }

  interface ServerRuntimeConfig {
    sslDetector: string;
    addHstsHeader: boolean;
    googleSecrets: GoogleSecrets;
    validateSslCertificats: boolean;
  }

  interface GoogleSecrets {
    enabled: boolean;
    gcpProjectId: string;
  }

  declare const getConfig: ConfigTypes;

  export default getConfig;
}
