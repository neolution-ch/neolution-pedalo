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
      footer={
        <>
          <a href="#" className="link-primary">
            Terms &amp; Conditions
          </a>
          <i className="mx-2">&#9679;</i>
          <a href="#" className="link-primary">
            Privacy Policy
          </a>
          <p>Copyright © Your Website {new Date().getUTCFullYear()}</p>
        </>
      }
      userDropDownMenuToggle={initials}
      userDropDownMenu={<NavbarUser />}
    >
      {children}
    </SideBarLayoutProvider>
  );
};

export { CustomAppSideBarLayoutProvider };
