import { TranslationItem } from "src/orval/react-query";
import { setLocalStorageItem } from "@neolution-ch/javascript-utils";
import { useConstructor } from "./useConstructor";

interface TranslationContext {
  translations?: TranslationItem[];
  translationHash?: string;
}

const useAddTranslationsToLocalStorage = (props: TranslationContext) => {
  const { translations, translationHash } = props;

  useConstructor(() => {
    // We are on the client
    if (typeof document !== "undefined") {
      // We received new translations
      if (typeof localStorage !== "undefined" && translations !== undefined && translationHash !== undefined) {
        setLocalStorageItem("translations", translations);
        document.cookie = `translationHash=${translationHash}; path=/`;
      }
    }
  });
};

export { useAddTranslationsToLocalStorage };
