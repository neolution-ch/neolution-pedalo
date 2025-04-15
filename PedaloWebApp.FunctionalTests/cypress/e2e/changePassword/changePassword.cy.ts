import type { ChangePasswordModel } from "orval/axios";
import { fakerValidPassword } from "api-helpers";
import ChangePasswordPage from "pages/ChangePasswordPage";

const changePasswordPage = new ChangePasswordPage();

describe("Change Password", () => {
  beforeEach(() => {
    cy.signIn("tenant-admin@tenant1.ch", "Secret1!").then(() => {
      cy.visit("/my-account/changepassword");
    });
  });

  it("Navigation / read works", () => {
    cy.visit("/");
    cy.navigateToChangePassword();
    cy.getByLabel("Aktuelles Kennwort").should("be.empty");
  });

  it("Updating the password with the correct password works", () => {
    cy.visit("/");
    cy.navigateToChangePassword();

    const generatedPassword = fakerValidPassword();
    const model: ChangePasswordModel = {
      existingPassword: "Secret1!",
      newPassword: generatedPassword,
      newPasswordConfirmation: generatedPassword,
    };

    changePasswordPage.fillForm(model);

    cy.saveForm();
    cy.clickUserBadge();
    cy.contains("abmelden").click();
    cy.login("tenant-admin@tenant1.ch", generatedPassword);

    // Password has been changed, reset the DB
    cy.resetDb();
  });

  it("Updating the password with the wrong password doesn't work", () => {
    cy.visit("/");
    cy.navigateToChangePassword();

    const generatedPassword = fakerValidPassword();

    const model: ChangePasswordModel = {
      existingPassword: fakerValidPassword(),
      newPassword: generatedPassword,
      newPasswordConfirmation: generatedPassword,
    };

    changePasswordPage.fillForm(model);
    cy.saveForm({ expectErrorModal: true });

    cy.logout();
    cy.login("tenant-admin@tenant1.ch", generatedPassword, false);
  });
});
