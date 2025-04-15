// add new command to the existing Cypress interface
declare global {
  namespace Cypress {
    interface Chainable {
      clickUserBadge: () => Chainable;
      dataTableRowExists: (...fields: string[]) => Chainable;
      dataTableRowNotExists: (...fields: string[]) => Chainable;
      confirmDeleteDialog: () => Chainable;
      testDataTablePaging: (generator: () => Promise<number>) => Chainable;
    }
  }
}

Cypress.Commands.add("clickUserBadge", () => cy.get(".user-dropdown.nav-link").click());

Cypress.Commands.add("dataTableRowExists", (...fields: string[]) => {
  fields.forEach((x) => {
    cy.contains("td", x);
  });
});
Cypress.Commands.add("dataTableRowNotExists", (...fields: string[]) => {
  fields.forEach((x) => {
    cy.contains("td", x).should("not.exist");
  });
});
Cypress.Commands.add("confirmDeleteDialog", () => cy.contains("Delete").click());

Cypress.Commands.add("testDataTablePaging", (generator: () => Promise<number>) =>
  cy.contains("span", "von insgesamt").then(async (x) => {
    const match = /.*([0-9]+) Resultat/.exec(x.text());
    const countBefore = match != null ? +match[1] : 0;
    const countCreate = await generator();
    const defaultPageSize = 25;
    const countSecondPage = countBefore + countCreate - defaultPageSize;

    cy.reload();

    // todo make this more dynamic...
    cy.get("tbody>tr").should("have.length", defaultPageSize);
    cy.contains("button", ">").click();
    cy.get("tbody>tr").should("have.length", countSecondPage);
  }),
);

export {};
