import { CacheProvider } from "@neolution-ch/javascript-utils";

enum CacheContainer {
  Translations,
}

const cacheProvider = new CacheProvider(CacheContainer, "CacheContainer");

export { CacheContainer, cacheProvider };
