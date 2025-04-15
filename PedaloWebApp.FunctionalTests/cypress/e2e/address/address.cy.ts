import type { AddressModel } from "orval/axios";
import { addressesCreate } from "orval/axios";
import AddressPage from "../../pages/AddressPage";

const page = new AddressPage();

describe("Address", () => {
  let addressModel: AddressModel;

  beforeEach(() => {
    cy.signIn("tenant-admin@tenant1.ch", "Secret1!").then(() => {
      addressModel = page.generateFakeModel();
      cy.wrap(addressesCreate(addressModel));
    });

    cy.visit("/addresses");
  });

  it("Navigation / read works", () => {
    cy.visit("/");
    cy.contains("Adressen").click();
    page.rowExists(addressModel);
    cy.reload();
    page.rowExists(addressModel);
  });

  it("Adding an address works", () => {
    const model = page.generateFakeModel();
    page.clickNew();
    page.fillForm(model);
    cy.saveForm({ clickBack: true });
    page.rowExists(model);
  });

  it("Editing an address works", () => {
    const updatedModel = page.generateFakeModel();
    page.openRow(addressModel);
    page.updateForm(addressModel, updatedModel);
    cy.saveForm({ clickBack: true });
    page.rowExists(updatedModel);
  });

  it("Deleting an address works", () => {
    page.rowExists(addressModel);
    page.deleteRow(addressModel);
    cy.confirmDeleteDialog();
    page.rowNotExists(addressModel);
  });

  it("Filtering addresses works", () => {
    cy.contains("th", "Vorname")
      .invoke("index")
      .then((i) => {
        cy.get("thead>tr")
          .eq(1)
          .get("th")
          .eq(i)
          .get("input")
          .then((x) => {
            cy.wrap(x).type(`${addressModel.firstname ?? ""}{enter}`);
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
        await addressesCreate(page.generateFakeModel());
      }

      return 30;
    });
  });
});
