import { faker } from "@faker-js/faker";
import type { CustomerModel } from "orval/axios";

class CustomerPage {
  public rowExists = (address: CustomerModel): Cypress.Chainable => cy.dataTableRowExists(address.firstName ?? "", address.lastName ?? "");

  public rowNotExists = (address: CustomerModel): Cypress.Chainable =>
    cy.dataTableRowNotExists(address.firstName ?? "", address.lastName ?? "");

  public fillForm = (address: CustomerModel): void => {
    cy.getByLabel("Vorname")
      .clear()
      .type(address.firstName ?? "");
    cy.getByLabel("Nachname")
      .clear()
      .type(address.lastName ?? "");
  };

  public updateForm = (expected: CustomerModel, newAddress: CustomerModel): void => {
    cy.getByLabel("Vorname")
      .should("have.value", expected.firstName)
      .clear()
      .type(newAddress.firstName ?? "");
    cy.getByLabel("Nachname")
      .should("have.value", expected.lastName)
      .clear()
      .type(newAddress.lastName ?? "");
  };

  public openRow = (address: CustomerModel): Cypress.Chainable =>
    cy
      .contains(address.firstName ?? "")
      .parent("tr")
      .find(".fa-eye")
      .click();

  public clickNew = (): Cypress.Chainable => cy.get(".fa-plus").parent("button").click();

  public deleteRow = (address: CustomerModel): Cypress.Chainable =>
    cy
      .contains(address.firstName ?? "")
      .parent("tr")
      .find(".fa-trash")
      .click();

  public generateFakeModel = (): CustomerModel => ({
    firstName: faker.helpers.unique(() => faker.name.firstName()),
    lastName: faker.helpers.unique(() => faker.name.lastName()),
    customerId: faker.datatype.uuid(),
    birthdayDate: faker.date.past(),
  });
}

export default CustomerPage;
