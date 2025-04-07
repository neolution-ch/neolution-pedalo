module.exports = {
  env: {
    es6: true,
    node: true,
    browser: true,
  },
  extends: [
    "eslint:recommended",
    "plugin:import/recommended",
    "plugin:@typescript-eslint/eslint-recommended",
    "plugin:@typescript-eslint/recommended",
    "prettier",
  ],
  ignorePatterns: ["/cypress/orval/axios"],
  settings: {
    "import/resolver": {
      node: {
        extensions: [".js", ".jsx", ".ts", ".tsx"],
      },
      typescript: {
        alwaysTryTypes: true,
      },
    },
  },
  globals: {
    JSX: true,
    cy: true,
    Cypress: true,
    describe: true,
    it: true,
    beforeEach: true,
    before: true,
  },
  // rules we want to enforce or disable
  rules: {
    "@typescript-eslint/promise-function-async": "off",
    "@typescript-eslint/naming-convention": "off",
    "@typescript-eslint/no-namespace": "off",
  },
  overrides: [
    {
      files: ["cypress/client.ts"],
      rules: {
        "@typescript-eslint/ban-tslint-comment": "off",
      },
    },
    {
      files: ["cypress/support/e2e.ts"],
      rules: {
        "@typescript-eslint/method-signature-style": "off",
      },
    },
    {
      files: ["*.ts", "*.tsx"],
      rules: {
        "import/prefer-default-export": ["off"],
      },
    },
  ],
};
