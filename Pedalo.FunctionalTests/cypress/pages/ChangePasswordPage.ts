import type { ChangePasswordModel } from "orval/axios";

class ChangePasswordPage {
  public fillForm = (model: ChangePasswordModel): void => {
    cy.getByLabel("Aktuelles Kennwort").type(model.existingPassword);
    cy.getByLabel("Neues Kennwort").type(model.newPassword);
    cy.getByLabel("Neues Kennwort bestätigen").type(model.newPasswordConfirmation);
  };
}

export default ChangePasswordPage;
