import { authenticator } from "otplib";

class MfaRegisterPage {
  public fillForm = (sharedKey: string): void => {
    cy.getByLabel("code").type(authenticator.generate(sharedKey));
    cy.contains("Register").click();
  };
}

export default MfaRegisterPage;
