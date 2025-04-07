import { faker } from "@faker-js/faker";
import type { UserAccountModel } from "orval/axios";
import { fakerValidPassword } from "api-helpers";

class UserAccountPage {
  public rowExists = (userAccount: UserAccountModel): Cypress.Chainable =>
    cy.dataTableRowExists(userAccount.firstname ?? "", userAccount.lastname ?? "");

  public rowNotExists = (userAccount: UserAccountModel): Cypress.Chainable =>
    cy.dataTableRowNotExists(userAccount.firstname ?? "", userAccount.lastname ?? "");

  public fillForm = (userAccount: UserAccountModel): void => {
    cy.getByLabel("Vorname").type(userAccount.firstname ?? "", { force: true });
    cy.getByLabel("Nachname").type(userAccount.lastname ?? "");
    cy.getByLabel("Email").type(userAccount.email ?? "");
    cy.getByLabel("Kennwort").type(userAccount.password ?? "");
    cy.getByLabel("Berechtigung Accounts verwalten").check();
  };

  public updateForm = (expected: UserAccountModel, newUserAccount: UserAccountModel): void => {
    cy.getByLabel("Vorname")
      .should("have.value", expected.firstname)
      .clear()
      .type(newUserAccount.firstname ?? "");
    cy.getByLabel("Nachname")
      .should("have.value", expected.lastname)
      .clear()
      .type(newUserAccount.lastname ?? "");
    cy.getByLabel("Email")
      .should("have.value", expected.email)
      .clear()
      .type(newUserAccount.email ?? "");
    cy.getByLabel("Berechtigung Accounts verwalten").check();
  };

  public openRow = (userAccount: UserAccountModel): Cypress.Chainable =>
    cy
      .contains(userAccount.firstname ?? "")
      .parent("tr")
      .find(".fa-eye")
      .click();

  public clickNew = (): Cypress.Chainable => cy.get(".fa-plus").parent("button").click();

  public deleteRow = (userAccount: UserAccountModel): Cypress.Chainable =>
    cy
      .contains(userAccount.firstname ?? "")
      .parent("tr")
      .find(".fa-trash")
      .click();

  public generateFakeModel = (): UserAccountModel => ({
    email: faker.internet.email(),
    firstname: faker.name.firstName(),
    lastname: faker.name.lastName(),
    password: fakerValidPassword(),
    userClaims: [],
    tenantId: window.sessionStorage.getItem("tenantId") || faker.datatype.uuid(),
    userAccountId: faker.datatype.uuid(),
  });
}

export default UserAccountPage;
