import { faker } from "@faker-js/faker";
import type { CustomerModel } from "orval/axios";

class CustomerPage {
  public rowExists = (customer: CustomerModel): Cypress.Chainable => cy.dataTableRowExists(customer.firstName ?? "", customer.lastName ?? "");

  public rowNotExists = (customer: CustomerModel): Cypress.Chainable =>
    cy.dataTableRowNotExists(customer.firstName ?? "", customer.lastName ?? "");

  public fillForm = (customer: CustomerModel): void => {
    cy.getByLabel("Vorname")
      .clear()
      .type(customer.firstName ?? "");
    cy.getByLabel("Nachname")
      .clear()
      .type(customer.lastName ?? "");
  };

  public updateForm = (expected: CustomerModel, newCustomer: CustomerModel): void => {
    cy.getByLabel("Vorname")
      .should("have.value", expected.firstName)
      .clear()
      .type(newCustomer.firstName ?? "");
    cy.getByLabel("Nachname")
      .should("have.value", expected.lastName)
      .clear()
      .type(newCustomer.lastName ?? "");
  };

  public openRow = (customer: CustomerModel): Cypress.Chainable =>
    cy
      .contains(customer.firstName ?? "")
      .parent("tr")
      .find(".fa-eye")
      .click({ force: true });

  public clickNew = (): Cypress.Chainable => cy.get(".fa-plus").parent("button").click();

  public deleteRow = (customer: CustomerModel): Cypress.Chainable =>
    cy
      .contains(customer.firstName ?? "")
      .parent("tr")
      .find(".fa-trash")
      .click({ force: true });

  public generateFakeModel = (): CustomerModel => ({
    firstName: faker.helpers.unique(() => faker.name.firstName()),
    lastName: faker.helpers.unique(() => faker.name.lastName()),
    customerId: faker.datatype.uuid(),
    birthdayDate: faker.date.past(),
  });
}

export default CustomerPage;
