// add new command to the existing Cypress interface
declare global {
  namespace Cypress {
    interface SaveFormParams {
      clickBack?: boolean;
      waitForModal?: boolean;
      expectErrorModal?: boolean;
    }

    interface Chainable {
      getByLabel: (label: string) => Chainable;
      saveForm: (params?: SaveFormParams) => Chainable;
    }
  }
}

Cypress.Commands.add("getByLabel", (label: string) =>
  cy
    .contains("label", label)
    .invoke("attr", "for")
    .then((id) => {
      cy.get(`input[id='${id ?? ""}']`);
    }),
);

Cypress.Commands.add("saveForm", (params = {}) => {
  const { clickBack = false, waitForModal = true, expectErrorModal = false } = params;
  cy.contains("speichern").click();
  if (waitForModal) cy.get(`.Toastify__toast--${expectErrorModal ? "error" : "success"}`).should("be.visible");

  if (clickBack) {
    cy.contains("button", "zurück").click();
  }
});

export {};
