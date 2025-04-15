import { userAccountsCreate, type UserAccountModel } from "orval/axios";
import UserAccountPage from "pages/UserAccountPage";

const page = new UserAccountPage();

describe("User Account", () => {
  let userAccountModel: UserAccountModel;

  beforeEach(() => {
    cy.signIn("tenant-admin@tenant1.ch", "Secret1!").then(() => {
      userAccountModel = page.generateFakeModel();
      cy.wrap(userAccountsCreate(userAccountModel));
      cy.visit("/user-accounts");
    });
  });

  it("Navigation / read works", () => {
    cy.visit("/");
    cy.navigateToManageUsers();
    page.rowExists(userAccountModel);
    cy.reload();
    page.rowExists(userAccountModel);
  });

  it("Adding a user account works", () => {
    const fakeUserAccountModel = page.generateFakeModel();

    page.clickNew();
    page.fillForm(fakeUserAccountModel);
    cy.saveForm({ clickBack: true });

    page.rowExists(fakeUserAccountModel);
    page.openRow(fakeUserAccountModel);
    cy.getByLabel("Berechtigung Accounts verwalten").should("be.checked");

    cy.login(fakeUserAccountModel.email ?? "", fakeUserAccountModel.password ?? "");
  });

  it("Deleting a user works", () => {
    page.rowExists(userAccountModel);
    page.deleteRow(userAccountModel);
    cy.confirmDeleteDialog();
    page.rowNotExists(userAccountModel);
  });

  it("Editing a user works", () => {
    const updatedUserAccountModel = page.generateFakeModel();
    page.rowExists(userAccountModel);
    page.openRow(userAccountModel);
    page.updateForm(userAccountModel, updatedUserAccountModel);
    cy.saveForm({ clickBack: true });
    page.rowExists(updatedUserAccountModel);
    page.openRow(updatedUserAccountModel);
    cy.getByLabel("Berechtigung Accounts verwalten").should("be.checked");
    cy.login(updatedUserAccountModel.email ?? "", userAccountModel.password ?? "");
  });
});
