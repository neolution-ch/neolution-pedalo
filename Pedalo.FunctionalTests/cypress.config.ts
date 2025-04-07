import { defineConfig } from "cypress";
import * as sql from "mssql";
import * as installLogsPrinter from "cypress-terminal-report/src/installLogsPrinter";
import { writeFileSync, readFileSync, existsSync } from "fs";
import { join } from "path";

export default defineConfig({
  retries: {
    openMode: 0,
    runMode: 2,
  },
  videoUploadOnPasses: false,
  defaultCommandTimeout: 10000,
  video: true,
  reporter: "junit",
  reporterOptions: {
    mochaFile: "cypress/results/e2e-tests-[hash].xml",
  },
  env: {
    dbName: "pedalo",
    dbServer: "localhost",
    dbPort: 1433,
    dbUser: "sa",
    dbPassword: "Test1234!",
  },
  e2e: {
    setupNodeEvents: (on: Cypress.PluginEvents, config: Cypress.PluginConfigOptions): void => {
      installLogsPrinter(on);

      on("task", {
        /**
         * The sql task
         * @param query The query
         */
        sql: async (query: string) => {
          const pool: sql.ConnectionPool = new sql.ConnectionPool({
            database: config.env["dbName"] as string,
            server: config.env["dbServer"] as string,
            port: config.env["dbPort"] as number,
            user: config.env["dbUser"] as string,
            password: config.env["dbPassword"] as string,
            options: {
              trustedConnection: false,
              trustServerCertificate: true,
            },
          });

          const connection: sql.ConnectionPool = await pool.connect();
          const result: sql.IResult<unknown> = await connection.query(query);
          return result;
        },
      });

      on("after:spec", (_, results: CypressCommandLine.RunResult) => {
        if (results && results.tests) {
          // Loop through all passed tests and check if they had a failed try
          const exisitingCommentfile = join(process.cwd(), "retries_existing.txt");
          const newCommentfile = join(process.cwd(), "retries_new.txt");
          const defaultnew = "\r\n---\r\n";
          let failedTestString = existsSync(exisitingCommentfile)
            ? readFileSync(exisitingCommentfile, { encoding: "utf8" }) + "\r\n"
            : defaultnew;

          results.tests
            .filter((test) => test.state === "passed")
            .forEach((test) => {
              if (test.attempts.find((attempt) => attempt.state === "failed")) {
                failedTestString += `- Test "${test.title}" failed ${
                  test.attempts.filter((attempt) => attempt.state === "failed").length
                } time(s) before succeeding\r\n`;
              }
            });

          if (failedTestString == defaultnew) {
            failedTestString = "";
          }

          if (failedTestString.length > 0) {
            failedTestString = failedTestString.trimEnd();
          }

          writeFileSync(exisitingCommentfile, failedTestString, {
            flag: "w",
          });

          writeFileSync(
            newCommentfile,
            failedTestString.length > 0
              ? "# ⚠️ Flaky Tests detected\r\n\r\nThe following tests have been detected as flaky:" + failedTestString
              : "",
            {
              flag: "w",
            },
          );
        }
      });
    },
    baseUrl: "https://localhost:30349",
  },
});
