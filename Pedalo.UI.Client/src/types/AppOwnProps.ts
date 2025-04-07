import { Session } from "next-auth";
import { TranslationItem } from "src/orval/react-query";

export interface AppOwnProps {
  translations?: TranslationItem[];
  translationHash?: string;
  session?: Session | null;
}
