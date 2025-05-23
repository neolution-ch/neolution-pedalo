import { SideBarLayoutProvider } from "@neolution-ch/react-pattern-ui";
import NavbarUser from "src/components/Layout/NavbarUser";
import React, { FC, PropsWithChildren } from "react";

const CustomAppSideBarLayoutProvider: FC<PropsWithChildren> = ({ children }) => {
  const initials = "";

  return (
    <SideBarLayoutProvider
      brand={
        <a href="/">
          <img alt="brand logo" src="/images/neolution.png" />
        </a>
      }
      footer={<p>Copyright © Neolution AG {new Date().getUTCFullYear()}</p>}
      userDropDownMenuToggle={initials}
      userDropDownMenu={<NavbarUser />}
    >
      {children}
    </SideBarLayoutProvider>
  );
};

export { CustomAppSideBarLayoutProvider };
