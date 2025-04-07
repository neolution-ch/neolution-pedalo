import { tenantsCreate, type TenantModel } from "orval/axios";
import TenantPage from "pages/TenantPage";

const page = new TenantPage();

describe("Tenant", () => {
  let tenantModel: TenantModel;

  beforeEach(() => {
    tenantModel = page.generateFakeModel();

    cy.signIn("tenant-admin@tenant1.ch", "Secret1!").then(() => {
      cy.wrap(tenantsCreate(tenantModel));
      cy.visit("/tenants");
    });
  });

  it("Navigation / read works", () => {
    cy.visit("/");
    cy.navigateToManageTenants();
    page.rowExists(tenantModel);
    cy.reload();
    page.rowExists(tenantModel);
  });

  it("Adding an tenant works", () => {
    const model = page.generateFakeModel();
    page.clickNew();
    page.fillForm(model);
    cy.saveForm();
    page.rowExists(model);
  });

  it("Editing an tenant works", () => {
    const updatedModel = page.generateFakeModel();
    page.openRow(tenantModel);
    page.updateForm(tenantModel, updatedModel);
    cy.saveForm({ clickBack: true });
    page.rowExists(updatedModel);
  });

  it("Deleting a tenant works", () => {
    page.rowExists(tenantModel);
    page.deleteRow(tenantModel);
    cy.confirmDeleteDialog();
    page.rowNotExists(tenantModel);
  });

  it("Paging works", () => {
    cy.testDataTablePaging(async () => {
      for (let index = 0; index < 30; index++) {
        await tenantsCreate(page.generateFakeModel());
      }

      return 30;
    });
  });
});
