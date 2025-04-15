import React, { FC, useCallback } from "react";
import { DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown } from "reactstrap";
import { setCookie } from "cookies-next";
import { useRouter } from "next/router";
import { useTranslationsListSupportedLanguages, TranslationCodeId } from "src/orval/react-query";
import { useT } from "src/hooks/useT";
import { infiniteQueryOptions } from "src/queries/queryOptions";
import { Locale } from "nextjs-routes";

const NavbarUser: FC = () => {
  const t = useT();

  const { data: langs, isLoading: langsAreLoading } = useTranslationsListSupportedLanguages({
    query: infiniteQueryOptions,
  });
  const { push: pushRoute, pathname, query, asPath } = useRouter();

  const switchToLocale = useCallback(
    async (locale: Locale) => {
      setCookie("NEXT_LOCALE", locale);
      await pushRoute({ pathname, query }, asPath, { locale });
      window.location.reload();
    },
    [pushRoute, asPath, pathname, query],
  );

  return (
    <>
      <DropdownItem header>{t(TranslationCodeId.Label_Settings)}</DropdownItem>
      <DropdownItem divider />
      {!langsAreLoading && (
        <UncontrolledDropdown>
          <DropdownToggle tag="button" className="dropdown-item">
            {t(TranslationCodeId.Label_SwitchLanguage)}
          </DropdownToggle>
          <DropdownMenu>
            {langs?.map((x) => (
              <DropdownItem
                onClick={() => {
                  switchToLocale(x.code as Locale);
                }}
                key={x.code}
              >
                {x.displayName}
              </DropdownItem>
            ))}
          </DropdownMenu>
        </UncontrolledDropdown>
      )}
    </>
  );
};

export default NavbarUser;
