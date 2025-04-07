import jwtDecode from "jwt-decode";
import MfaAuthorizePage from "pages/mfa/MfaAuthorizePage";
import MfaRegisterPage from "pages/mfa/MfaRegisterPage";

interface JwtToken {
  exp: number;
  amr: string;
}

const mfaRegisterPage = new MfaRegisterPage();
const mfaAuthorizePage = new MfaAuthorizePage();

describe("Refresh Token", () => {
  beforeEach(() => {
    Cypress.session.clearAllSavedSessions();
  });

  it("The refresh token works", () => {
    cy.signIn("tenant-admin@tenant1.ch", "Secret1!").then(() => {
      cy.intercept("POST", "/api/auth/session").as("session");
      cy.visit("/debug");

      cy.get("pre.session").then(($el) => {
        const text = $el.text();
        const json = JSON.parse(text);
        const accessTokenBeforeRefresh = jwtDecode<JwtToken>(json.apiJwtToken);
        const refreshTokenBeforeRefresh = jwtDecode<JwtToken>(json.apiRefreshToken);

        cy.get("button").contains("Refresh Token").click();

        cy.wait("@session").then(({ response }) => {
          const body = response?.body;
          const accessTokenAfterRefresh = jwtDecode<JwtToken>(body.apiJwtToken);
          const refreshTokenAfterRefresh = jwtDecode<JwtToken>(body.apiRefreshToken);

          expect(accessTokenBeforeRefresh).not.to.be.deep.equal(accessTokenAfterRefresh);
          expect(refreshTokenBeforeRefresh).not.to.be.deep.equal(refreshTokenAfterRefresh);

          expect(accessTokenBeforeRefresh.amr).to.be.equal(accessTokenAfterRefresh.amr);
          expect(refreshTokenBeforeRefresh.amr).to.be.equal(refreshTokenAfterRefresh.amr);
        });
      });
    });
  });

  it("The amr claim is the same after refreshing the token and being mfa authenticated", () => {
    cy.signIn("tenant-admin@tenant1.ch", "Secret1!").then(() => {
      cy.visit("/auth/mfa/register");
      cy.get("#shared-key")
        .invoke("text")
        .then((sharedKey) => {
          mfaRegisterPage.fillForm(sharedKey);
          cy.url().should("match", /\/auth\/mfa\/authorize\?redirectTo=%2F/);
          mfaAuthorizePage.fillForm(sharedKey);
          cy.url().should("match", /\/$/);
        });

      cy.intercept("POST", "/api/auth/session").as("session");
      cy.visit("/debug");

      cy.get("pre.session").then(($el) => {
        const text = $el.text();
        const json = JSON.parse(text);
        const accessTokenBeforeRefresh = jwtDecode<JwtToken>(json.apiJwtToken);
        const refreshTokenBeforeRefresh = jwtDecode<JwtToken>(json.apiRefreshToken);

        expect(accessTokenBeforeRefresh.amr).to.be.equal("mfa");
        expect(refreshTokenBeforeRefresh.amr).to.be.equal("mfa");

        cy.get("button").contains("Refresh Token").click();

        cy.wait("@session").then(({ response }) => {
          const body = response?.body;
          const accessTokenAfterRefresh = jwtDecode<JwtToken>(body.apiJwtToken);
          const refreshTokenAfterRefresh = jwtDecode<JwtToken>(body.apiRefreshToken);

          expect(accessTokenBeforeRefresh).not.to.be.deep.equal(accessTokenAfterRefresh);
          expect(refreshTokenBeforeRefresh).not.to.be.deep.equal(refreshTokenAfterRefresh);

          expect(accessTokenBeforeRefresh.amr).to.be.equal(accessTokenAfterRefresh.amr);
          expect(refreshTokenBeforeRefresh.amr).to.be.equal(refreshTokenAfterRefresh.amr);
        });
      });
    });
  });
});
