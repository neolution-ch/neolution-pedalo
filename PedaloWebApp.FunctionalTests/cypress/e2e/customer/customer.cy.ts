import type { CustomerModel } from "orval/axios";
import { customersCreate } from "orval/axios";
import CustomersPage from "../../pages/CustomerPage";

const page = new CustomersPage();

describe("Customer", () => {
  let addressModel: CustomerModel;

  beforeEach(() => {
    addressModel = page.generateFakeModel();
    cy.wrap(customersCreate(addressModel));
    cy.visit("/customers");
  });

  it("Navigation / read works", () => {
    cy.visit("/");
    cy.contains("Kunden").click();
    page.rowExists(addressModel);
    cy.reload();
    page.rowExists(addressModel);
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
    page.openRow(addressModel);
    page.updateForm(addressModel, updatedModel);
    cy.saveForm({ clickBack: true });
    page.rowExists(updatedModel);
  });

  it("Deleting a customer works", () => {
    page.rowExists(addressModel);
    page.deleteRow(addressModel);
    cy.confirmDeleteDialog();
    page.rowNotExists(addressModel);
  });

  it("Filtering customer works", () => {
    cy.contains("th", "Vorname")
      .invoke("index")
      .then((i) => {
        cy.get("thead>tr")
          .eq(1)
          .get("th")
          .eq(i)
          .get("input")
          .then((x) => {
            cy.wrap(x).type(`${addressModel.firstName ?? ""}{enter}`);
            cy.get("tbody>tr").should("have.length", 1);
            page.rowExists(addressModel);

            cy.get("thead .fa-xmark").click();
            cy.get("tbody>tr").should("have.length.greaterThan", 1);
            cy.wrap(x).should("be.empty");
          });
      });
  });

  it("Paging works", () => {
    cy.testDataTablePaging(async () => {
      for (let index = 0; index < 30; index++) {
        await customersCreate(page.generateFakeModel());
      }

      return 30;
    });
  });
});
