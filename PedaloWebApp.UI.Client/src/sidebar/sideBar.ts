import { ISideBarMenuItem } from "@neolution-ch/react-pattern-ui";
import { faHome, faPerson, faShip, faBook } from "@fortawesome/free-solid-svg-icons";
import { TFn } from "src/hooks/useT";
import { TranslationCodeId } from "src/orval/react-query";
import { CombinedRoute } from "src/utils/routes";

export interface ISideBarMenuItemWithRoute extends ISideBarMenuItem {
  route?: CombinedRoute;
}

const getSideBarMenuItems = (t: TFn): ISideBarMenuItemWithRoute[] => [
  { id: "home", title: "Home", icon: faHome, route: "/" },
  { id: "customers", title: t(TranslationCodeId.Nav_Customers), icon: faPerson, route: "/customers" },
  { id: "pedalos", title: t(TranslationCodeId.Nav_Pedalos), icon: faShip, route: "/pedalos" },
  { id: "bookings", title: t(TranslationCodeId.Nav_Bookings), icon: faBook, route: "/bookings" },
];

export { getSideBarMenuItems };
