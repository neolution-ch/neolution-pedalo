import { faker } from "@faker-js/faker";
import type { AddressModel } from "orval/axios";

class AddressPage {
  public rowExists = (address: AddressModel): Cypress.Chainable => cy.dataTableRowExists(address.firstname ?? "", address.lastname ?? "");

  public rowNotExists = (address: AddressModel): Cypress.Chainable =>
    cy.dataTableRowNotExists(address.firstname ?? "", address.lastname ?? "");

  public fillForm = (address: AddressModel): void => {
    cy.getByLabel("Vorname")
      .clear()
      .type(address.firstname ?? "");
    cy.getByLabel("Nachname")
      .clear()
      .type(address.lastname ?? "");
  };

  public updateForm = (expected: AddressModel, newAddress: AddressModel): void => {
    cy.getByLabel("Vorname")
      .should("have.value", expected.firstname)
      .clear()
      .type(newAddress.firstname ?? "");
    cy.getByLabel("Nachname")
      .should("have.value", expected.lastname)
      .clear()
      .type(newAddress.lastname ?? "");
  };

  public openRow = (address: AddressModel): Cypress.Chainable =>
    cy
      .contains(address.firstname ?? "")
      .parent("tr")
      .find(".fa-eye")
      .click();

  public clickNew = (): Cypress.Chainable => cy.get(".fa-plus").parent("button").click();

  public deleteRow = (address: AddressModel): Cypress.Chainable =>
    cy
      .contains(address.firstname ?? "")
      .parent("tr")
      .find(".fa-trash")
      .click();

  public generateFakeModel = (): AddressModel => ({
    firstname: faker.helpers.unique(() => faker.name.firstName()),
    lastname: faker.helpers.unique(() => faker.name.lastName()),
    addressId: faker.datatype.uuid(),
  });
}

export default AddressPage;
