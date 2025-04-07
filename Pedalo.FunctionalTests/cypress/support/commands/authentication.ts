import { authenticator } from "otplib";

// add new command to the existing Cypress interface
declare global {
  namespace Cypress {
    interface AddressModel {
      firstName: string;
      lastName: string;
    }

    interface Chainable {
      login: (username: string, password: string, shouldBeSuccessfull?: boolean) => void;
      logout: () => Chainable;
      navigateToChangePassword: () => Chainable;
      navigateToManageTenants: () => Chainable;
      navigateToManageUsers: () => Chainable;
      signIn: (userName: string, password: string) => Chainable<null>;
    }
  }
}

Cypress.Commands.add("login", (username, password, shouldBeSuccessfull = true) => {
  const signinPath = "/auth/signin";

  cy.location("pathname", { log: false }).then((currentPath) => {
    if (currentPath !== signinPath) {
      cy.visit(signinPath);
    }
  });

  cy.url().should("include", `${Cypress.config().baseUrl ?? ""}${signinPath}`);

  cy.get("#username").type(username).should("have.value", username);
  cy.get("#password").type(password).should("have.value", password);
  cy.get("button[type=submit]").click();

  if (shouldBeSuccessfull) {
    cy.url().then((url) => {
      if (url.endsWith("mfa/register")) {
        cy.url().should("equal", `${Cypress.config().baseUrl ?? ""}/mfa/register`);

        cy.get("#shared-key")
          .invoke("text")
          .then((sharedKeyText) => {
            cy.get("#code").type(authenticator.generate(sharedKeyText));
            cy.get("button[type=submit]").click();

            cy.url().should("equal", `${Cypress.config().baseUrl ?? ""}/mfa/authorize?`);
            cy.get("#code").type(authenticator.generate(sharedKeyText));
            cy.get("button[type=submit]").click();
          });
      }
      cy.url().should("not.include", "/auth");
    });
  } else {
    cy.contains("p", "Login failed, please check your credentials and try again.");
  }
});

Cypress.Commands.add("logout", () => {
  cy.clickUserBadge();
  cy.contains("abmelden").click();
});

Cypress.Commands.add("navigateToChangePassword", () => {
  cy.clickUserBadge();
  cy.contains("Kennwort ändern").click();
});

Cypress.Commands.add("navigateToManageTenants", () => {
  cy.clickUserBadge();
  cy.contains("Firmen verwalten").click();
});

Cypress.Commands.add("navigateToManageUsers", () => {
  cy.clickUserBadge();
  cy.contains("Account Übersicht").click();
});

Cypress.Commands.add("signIn", (userName, password) => {
  cy.session(userName, () => {
    cy.visit("/auth/signin");

    cy.get("#username").type(userName);
    cy.get("#password").type(password);

    cy.get("button[type=submit]").click();

    // urld should be / and the user should be logged in
    cy.url().should("equal", `${Cypress.config().baseUrl ?? ""}/`);

    cy.request("/api/auth/session").then((response) => {
      window.sessionStorage.setItem("tenantId", response.body.tenantId);
    });
  });
});
