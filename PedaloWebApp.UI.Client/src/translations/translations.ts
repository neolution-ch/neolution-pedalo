import { getCookie } from "cookies-next";
import { NextPageContext } from "next";
import { CacheContainer, cacheProvider } from "src/cache/cache";
import { translationsGetTranslations, TranslationsGetTranslations200 } from "src/orval/react-query";
import { AppOwnProps } from "src/types/AppOwnProps";

type ReturnType = Pick<AppOwnProps, "translations" | "translationHash">;

async function handleTranslations(ctx: NextPageContext): Promise<ReturnType> {
  const { defaultLocale, locale, req, res } = ctx;

  if (!req || !defaultLocale) {
    return {};
  }

  const effectiveLocale = locale || defaultLocale;
  const result: AppOwnProps = {};

  let cachedTranslations = cacheProvider.getObject<TranslationsGetTranslations200>(CacheContainer.Translations);

  if (!cachedTranslations) {
    cachedTranslations = await translationsGetTranslations();
    cacheProvider.setObject(CacheContainer.Translations, undefined, cachedTranslations);
  }

  const cookieTranslationHash = getCookie("translationHash", { req, res });

  // if the translation hashes do not match, we put the translations into the props
  // by doing this, they will be written to the __NEXT_DATA__ object in the html.
  // we can then read them from the client and add them to the localStorage
  if (cookieTranslationHash !== cachedTranslations[effectiveLocale]?.hash) {
    result.translations = cachedTranslations[effectiveLocale]?.translations;
    result.translationHash = cachedTranslations[effectiveLocale]?.hash;
  }

  return result;
}

export { handleTranslations };
