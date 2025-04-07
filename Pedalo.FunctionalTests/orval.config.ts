import { defineConfig } from "orval";
import { replaceInFileSync } from "replace-in-file";

export default defineConfig({
  axios: {
    input: "../Pedalo.UI.Api/openapi-spec.json",
    hooks: {
      afterAllFilesWrite: async () => {
        replaceInFileSync({
          files: "./src/orval/react-query/**/*.ts",
          from: / \| null/g,
          to: "",
        });
      },
    },
    output: {
      workspace: "./cypress/orval/axios",
      client: "axios-functions",
      target: "./client",
      mode: "tags-split",
      schemas: "./schemas",
      clean: true,
      prettier: true,
      override: {
        formData: {
          path: "../customFormData.ts",
          name: "customFormData",
        },
        mutator: {
          path: "../mutator.ts",
          name: "mutator",
        },
        useDates: true,
      },
    },
  },
});
