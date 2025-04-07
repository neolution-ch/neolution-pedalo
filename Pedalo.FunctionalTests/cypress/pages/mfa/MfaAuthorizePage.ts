import { authenticator } from "otplib";

class MfaAuthorizePage {
  public fillForm = (sharedKey: string): void => {
    cy.getByLabel("code").type(authenticator.generate(sharedKey));
    cy.get("button[type=submit]").click();
  };
}

export default MfaAuthorizePage;
