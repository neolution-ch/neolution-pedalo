import { defineConfig } from "orval";
import { replaceInFileSync } from "replace-in-file";

export default defineConfig({
  "react-query": {
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
      client: "react-query",
      workspace: "./src/orval/react-query",
      target: "./client",
      mode: "tags-split",
      schemas: "./schemas",
      clean: true,
      prettier: true,
      mock: true,
      override: {
        useDates: true,
        formData: {
          path: "../customFormData.ts",
          name: "customFormData",
        },
        mutator: {
          path: "../mutator.ts",
          name: "mutator",
        },
      },
    },
  },
});
