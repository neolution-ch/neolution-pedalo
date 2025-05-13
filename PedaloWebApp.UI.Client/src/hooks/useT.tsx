import { useRouter } from "next/router";
import { createContext, useContext, useMemo } from "react";
import { getLocalStorageItem } from "@neolution-ch/javascript-utils";
import { CacheContainer, cacheProvider } from "src/cache/cache";
import { TranslationCodeId, TranslationItem, TranslationsGetTranslations200 } from "src/orval/react-query";

export type TFn = (code: TranslationCodeId, params?: Record<string, unknown>) => string;

const TranslationsContext = createContext<TFn | null>(null);

const useGetT = (): TFn => {
  const isServer = typeof localStorage === "undefined";
  const router = useRouter();
  const locale = router.locale || router.defaultLocale || "de-CH";

  const t = useMemo(() => {
    const translations = isServer
      ? cacheProvider.getObject<TranslationsGetTranslations200>(CacheContainer.Translations)?.[locale]?.translations || []
      : getLocalStorageItem<TranslationItem[]>("translations") ?? [];

    return (code: TranslationCodeId, params?: Record<string, unknown>): string => {
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      function getObjectKey(obj: any, value: any) {
        return Object.keys(obj).find((key) => obj[key] === value);
      }

      const key = getObjectKey(TranslationCodeId, code);
      const item = translations.find((t) => t.id === key)?.text;
      let match = `T_ID_${key}`;

      if (item) {
        match = item;
        if (params) {
          Object.entries(params).forEach(([paramKey, paramValue]) => {
            if (match) match = match.split(`{${paramKey}}`).join(String(paramValue));
          });
        }
      }

      return match;
    };
  }, [locale, isServer]);

  return t;
};

const useT = () => {
  const res = useContext(TranslationsContext);
  if (!res) throw new Error("TranslationsContext is not initialized");
  return res;
};

export { useGetT, useT, TranslationsContext };
