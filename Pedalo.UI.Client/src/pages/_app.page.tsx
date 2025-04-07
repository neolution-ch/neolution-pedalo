import { config } from "@fortawesome/fontawesome-svg-core";
import { SideBarMenuProvider } from "@neolution-ch/react-pattern-ui";
import App, { AppContext, AppInitialProps, AppProps } from "next/app";
import NextNProgress from "nextjs-progressbar";
import { QueryClient, QueryClientProvider } from "react-query";
import { ToastContainer } from "react-toastify";
import { SideBarMenuItemRenderer } from "src/components/SideBarMenuItemRenderer/SideBarMenuItemRenderer";
import { useAddTranslationsToLocalStorage } from "src/hooks/useAddTranslationsToLocalStorage";
import { useSetLocale } from "src/hooks/useSetLocale";
import { TranslationsContext, useGetT } from "src/hooks/useT";
import { CustomAppSideBarLayoutProvider } from "src/providers/CustomAppSideBarLayoutProvider";
import { getSideBarMenuItems } from "src/sidebar/sideBar";
import { handleTranslations } from "src/translations/translations";
import { AppOwnProps } from "src/types/AppOwnProps";

import "@fortawesome/fontawesome-svg-core/styles.css";
import "@neolution-ch/react-pattern-ui/styles.css";
import "nprogress/nprogress.css";
import "react-datepicker/dist/react-datepicker.css";
import "react-toastify/dist/ReactToastify.css";
import "../../styles/main.scss";

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      refetchOnMount: true,
      refetchOnReconnect: false,
      refetchOnWindowFocus: false,
      cacheTime: 0,
      retry: (failureCount, error) => {
        //If Error 404 happens, don't retry
        if (!error || (typeof error === "object" && "status" in error && error.status === 404)) {
          return false;
        }
        // Otherwise use default behaviour and retry 3 times
        return failureCount < 3;
      },
    },
  },
});

// Import the CSS
config.autoAddCss = false; // Tell Font Awesome to skip adding the CSS automatically since it's being imported above

interface CustomAppProps extends AppProps, AppOwnProps {}

const CustomApp = (props: CustomAppProps) => {
  const { Component, pageProps, translationHash, translations, router } = props;

  useAddTranslationsToLocalStorage({
    translationHash,
    translations,
  });

  useSetLocale();

  const t = useGetT();

  return (
    <>
      <NextNProgress />

      <ToastContainer position="top-center" />

      <TranslationsContext.Provider value={t}>
        <QueryClientProvider client={queryClient}>
          <CustomAppSideBarLayoutProvider>
            <SideBarMenuProvider items={getSideBarMenuItems(t)} LinkRenderer={SideBarMenuItemRenderer}>
              <Component {...pageProps} key={router.asPath} />
            </SideBarMenuProvider>
          </CustomAppSideBarLayoutProvider>
        </QueryClientProvider>
      </TranslationsContext.Provider>
    </>
  );
};

CustomApp.getInitialProps = async (appCtx: AppContext): Promise<AppOwnProps & AppInitialProps> => {
  const { ctx } = appCtx;

  const appOwnProps: AppOwnProps = {
    ...(await handleTranslations(ctx)),
  };

  const initalProps = await App.getInitialProps(appCtx);

  const result: AppOwnProps & AppInitialProps = {
    ...initalProps,
    ...appOwnProps,
  };

  return result;
};

export default CustomApp;
