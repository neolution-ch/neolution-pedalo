// ***********************************************************
// This example support/index.js is processed and
// loaded automatically before your test files.
//
// This is a great place to put global configuration and
// behavior that modifies Cypress.
//
// You can change the location of this file or turn off
// automatically serving support files with the
// 'supportFile' configuration option.
//
// You can read more here:
// https://on.cypress.io/configuration
// ***********************************************************

// Import commands.js using ES2015 syntax:
import "./commands/sql";
import "./commands/form";
import "./commands/helpers";
import * as installLogsCollector from "cypress-terminal-report/src/installLogsCollector";

/**
 * The wrap definition in cypress is not properly implemented, here is the suggested fix that
 * might be implemented directly in cypress in a future version
 * reference: https://github.com/cypress-io/cypress/issues/18182
 */
declare global {
  namespace Cypress {
    interface Chainable {
      wrap<S>(object: Promise<S> | S, options?: Partial<Loggable & Timeoutable>): Chainable<S>;
    }
  }
}

installLogsCollector();

before(() => {
  Cypress.Keyboard.defaults({
    keystrokeDelay: 0,
  });
  cy.resetDb();
  cy.log("DB RESETED");
});
