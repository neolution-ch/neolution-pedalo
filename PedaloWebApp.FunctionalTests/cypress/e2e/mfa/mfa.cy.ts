import MfaAuthorizePage from "pages/mfa/MfaAuthorizePage";
import MfaRegisterPage from "pages/mfa/MfaRegisterPage";

const mfaRegisterPage = new MfaRegisterPage();
const mfaAuthorizePage = new MfaAuthorizePage();

describe("MFA", () => {
  beforeEach(() => {
    cy.resetDb();

    cy.signIn("tenant-admin@tenant1.ch", "Secret1!");
    cy.visit("/");
  });

  it("MFA Works as expected", () => {
    const protocol = Cypress.config().baseUrl?.includes("https") ? "https" : "http";
    cy.contains("MFA Only").click();
    cy.url().should("match", new RegExp(`/auth/mfa/register\\?redirectTo=${protocol}%3A%2F%2F.*%2Fmfa-only`));
    cy.get("#shared-key")
      .invoke("text")
      .then((sharedKey) => {
        mfaRegisterPage.fillForm(sharedKey);
        cy.url().should("match", new RegExp(`/auth/mfa/authorize\\?redirectTo=${protocol}%3A%2F%2F.*%2Fmfa-only`));

        mfaAuthorizePage.fillForm(sharedKey);
        cy.contains("pssst santa is real!");

        cy.contains("Home").click();
        cy.url().should("eq", `${Cypress.config().baseUrl ?? ""}/`);
        cy.get("h1").should("have.text", "Welcome to the Whitelabel ");
        cy.logout();
        cy.login("tenant-admin@tenant1.ch", "Secret1!");
        cy.contains("MFA Only").click();

        cy.url().should("match", new RegExp(`/auth/mfa/authorize\\?redirectTo=${protocol}%3A%2F%2F.*%2Fmfa-only`));
        mfaAuthorizePage.fillForm(sharedKey);
        cy.contains("pssst santa is real!");
      });
  });
});
