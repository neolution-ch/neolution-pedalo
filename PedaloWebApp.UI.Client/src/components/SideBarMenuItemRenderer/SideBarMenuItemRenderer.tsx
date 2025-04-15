import React, { ReactNode } from "react";
import Link from "next/link";
import { ISideBarMenuItemWithRoute } from "src/sidebar/sideBar";

interface SideBarMenuItemRendererProps {
  item: ISideBarMenuItemWithRoute;
  children: ReactNode;
}

export const SideBarMenuItemRenderer = (props: SideBarMenuItemRendererProps) => {
  const { children, item } = props;
  return (
    // eslint-disable-next-line react/jsx-no-useless-fragment -- false positive
    <>
      {item.route ? (
        <Link href={item.route}>{children}</Link>
      ) : (
        // eslint-disable-next-line react/jsx-no-useless-fragment -- false positive
        <>{children}</>
      )}
    </>
  );
};
