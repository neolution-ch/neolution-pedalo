import { tenantsCreate, type TenantModel, type UserAccountModel, userAccountsCreate } from "orval/axios";
import UserAccountPage from "pages/UserAccountPage";
import TenantPage from "pages/TenantPage";

const userAccountPage = new UserAccountPage();
const tenantPage = new TenantPage();

describe("Tenant", () => {
  let tenantModel: TenantModel;
  let userAccountModel: UserAccountModel;

  beforeEach(() => {
    tenantModel = tenantPage.generateFakeModel();
    userAccountModel = userAccountPage.generateFakeModel();

    cy.signIn("tenant-admin@tenant1.ch", "Secret1!").then(() => {
      cy.wrap(tenantsCreate(tenantModel)).then((tenantId) => {
        userAccountModel.tenantId = tenantId;
        cy.wrap(userAccountsCreate(userAccountModel));
        cy.visit(`/user-accounts?tenantId=${tenantId}`);
      });
    });
  });

  it("Navigation / read works", () => {
    cy.visit("/");
    cy.navigateToManageTenants();
    tenantPage.rowExists(tenantModel);
    tenantPage.openUsers(tenantModel);
    cy.url().should("contain", userAccountModel.tenantId);
    userAccountPage.rowExists(userAccountModel);
    cy.reload();
    userAccountPage.rowExists(userAccountModel);
  });

  it("Adding a user for a tenant works", () => {
    const newUserAccountModel: UserAccountModel = userAccountPage.generateFakeModel();

    userAccountPage.clickNew();
    userAccountPage.fillForm(newUserAccountModel);
    cy.saveForm({ clickBack: true });
    userAccountPage.rowExists(newUserAccountModel);
    userAccountPage.openRow(newUserAccountModel);
    cy.getByLabel("Berechtigung Accounts verwalten").should("be.checked");

    cy.login(newUserAccountModel.email ?? "", newUserAccountModel.password ?? "");
  });

  it("Deleting a user for a tenant works", () => {
    userAccountPage.rowExists(userAccountModel);
    userAccountPage.deleteRow(userAccountModel);
    cy.confirmDeleteDialog();
    userAccountPage.rowNotExists(userAccountModel);
  });

  it("Editing a user for a tenant", () => {
    const updatedUserAccountModel = userAccountPage.generateFakeModel();
    userAccountPage.rowExists(userAccountModel);
    userAccountPage.openRow(userAccountModel);
    userAccountPage.updateForm(userAccountModel, updatedUserAccountModel);
    cy.saveForm({ clickBack: true });
    userAccountPage.rowExists(updatedUserAccountModel);
    userAccountPage.openRow(updatedUserAccountModel);
    cy.getByLabel("Berechtigung Accounts verwalten").should("be.checked");
    cy.login(updatedUserAccountModel.email ?? "", userAccountModel.password ?? "");
  });
});
