import { useConstructor } from "./useConstructor";
import { setCookie } from "cookies-next";
import { useRouter } from "next/router";

const useSetLocale = () => {
  const { locale, defaultLocale } = useRouter();
  const effectiveLocale = locale ?? defaultLocale;

  useConstructor(() => {
    if (typeof document !== "undefined") {
      setCookie("NEXT_LOCALE", effectiveLocale);
    }
  });
};

export { useSetLocale };
