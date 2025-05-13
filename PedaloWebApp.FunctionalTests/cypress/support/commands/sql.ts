import type * as sql from "mssql";

export interface SQLResult<T> extends sql.IResult<T> {
  errorMessage?: string;
}

// add new command to the existing Cypress interface
declare global {
  namespace Cypress {
    interface Chainable {
      /**
       * Executed the given SQL statement and returns the result.
       * The result will contain the records of T and also the number of rows affected.
       * @param query The query to be executed.
       */
      sql: <T>(query: string) => Chainable<SQLResult<T>>;

      /**
       * Executed the given SQL statement and verifies the result against the provided expected Result
       * @param query The expected result. Only those properties will be checked on the result.
       */
      sqlVerify: <T>(query: string, expectedResult: T | T[]) => void;

      /**
       * Resets the db to the snapshot
       */
      resetDb: () => Chainable;
    }
  }
}

Cypress.Commands.add("sql", <T>(query: string): Cypress.Chainable<SQLResult<T>> => {
  const cyResult = cy.task("sql", query).then((result: unknown): Cypress.Chainable<SQLResult<T>> => {
    const sqlResult = result as SQLResult<T>;
    return cy
      .wrap(sqlResult.errorMessage, { timeout: 100 })
      .should("be.undefined")
      .then(() => sqlResult);
  });
  return cyResult;
});

Cypress.Commands.add("sqlVerify", (query: string, expectedResult: unknown): void => {
  cy.sql(query).then((result: SQLResult<unknown>): void => {
    window.console.log(result.recordset);
    const wrappedExpectedResult: unknown[] = Array.isArray(expectedResult) ? expectedResult : [expectedResult];

    wrappedExpectedResult.forEach((entry: unknown, i: number) => {
      cy.wrap(result.recordset[i]).should("deep.include", entry);

      /* Object.keys(entry).forEach((key) => {
        cy.wrap(entry[key]).should("equal", result.records[i][key]);
      }); */
    });
  });
});

Cypress.Commands.add("resetDb", () => {
  const dbName = Cypress.env("dbName") as string;
  const query = `USE master;
  ALTER DATABASE [${dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
  RESTORE DATABASE [${dbName}] from DATABASE_SNAPSHOT = '${dbName}_tests';
  ALTER DATABASE [${dbName}] SET MULTI_USER;`;
  return cy.sql(query);
});
