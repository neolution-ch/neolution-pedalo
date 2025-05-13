import type { CustomerModel } from "orval/axios";
import { customersCreate } from "orval/axios";
import CustomersPage from "../../pages/CustomerPage";

const page = new CustomersPage();

describe("Customer", () => {
  let customerModel: CustomerModel;

  beforeEach(() => {
    customerModel = page.generateFakeModel();
    cy.wrap(customersCreate(customerModel));
    cy.visit("/customers");
  });

  it("Navigation / read works", () => {
    cy.visit("/");
    cy.contains("Kunden").click();
    page.rowExists(customerModel);
    cy.reload();
    page.rowExists(customerModel);
  });

  it("Adding a customer works", () => {
    const model = page.generateFakeModel();
    page.clickNew();
    page.fillForm(model);
    cy.saveForm({ clickBack: true });
    page.rowExists(model);
  });

  it("Editing a customer works", () => {
    const updatedModel = page.generateFakeModel();
    page.openRow(customerModel);
    page.updateForm(customerModel, updatedModel);
    cy.saveForm({ clickBack: true });
    page.rowExists(updatedModel);
  });

  it("Deleting a customer works", () => {
    page.rowExists(customerModel);
    page.deleteRow(customerModel);
    cy.confirmDeleteDialog();
    page.rowNotExists(customerModel);
  });

  it("Filtering customer works", () => {
    cy.contains("th", "Vorname")
      .invoke("index")
      .then((i) => {
        cy.get("thead>tr")
          .eq(1)
          .find("th")
          .eq(i)
          .find("input")
          .then((x) => {
            cy.wrap(x).type(`${customerModel.firstName ?? ""}{enter}`, { force: true });
            cy.get("tbody>tr").should("have.length", 1);
            page.rowExists(customerModel);
            cy.get("thead .fa-xmark").click({ force: true });
            cy.get("tbody>tr").should("have.length.greaterThan", 1);
            cy.wrap(x).should("be.empty");
          });
      });
  });
});
