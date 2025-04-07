import { faker } from "@faker-js/faker";
import type { TenantModel } from "orval/axios";

class TenantPage {
  public rowExists = (tenant: TenantModel): Cypress.Chainable => cy.dataTableRowExists(tenant.name ?? "");

  public rowNotExists = (tenant: TenantModel): Cypress.Chainable => cy.dataTableRowNotExists(tenant.name ?? "");

  public fillForm = (tenant: TenantModel): void => {
    cy.getByLabel("Firmenname")
      .clear()
      .type(tenant.name ?? "");
  };

  public updateForm = (expected: TenantModel, newTenant: TenantModel): void => {
    cy.getByLabel("Firmenname")
      .should("have.value", expected.name)
      .clear()
      .type(newTenant.name ?? "");
  };

  public openRow = (tenant: TenantModel): Cypress.Chainable =>
    cy
      .contains(tenant.name ?? "")
      .parent("tr")
      .find(".fa-eye")
      .click();

  public clickNew = (): Cypress.Chainable => cy.get(".fa-plus").parent("button").click();

  public deleteRow = (tenant: TenantModel): Cypress.Chainable =>
    cy
      .contains(tenant.name ?? "")
      .parent("tr")
      .find(".fa-trash")
      .click();

  public generateFakeModel = (): TenantModel => ({
    name: faker.company.name(),
    dateCreated: faker.date.past(),
    tenantId: faker.datatype.uuid(),
  });

  public openUsers = (tenant: TenantModel): void => {
    cy.contains("td", tenant.name ?? "")
      .parent("tr")
      .find(".fa-user")
      .click();
  };
}

export default TenantPage;
