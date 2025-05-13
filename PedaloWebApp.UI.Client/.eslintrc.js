module.exports = {
  env: {
    es6: true,
    node: true,
    browser: true,
  },
  extends: [
    "eslint:recommended",
    "plugin:react/recommended",
    "plugin:react-hooks/recommended",
    "plugin:@next/next/recommended",
    "plugin:import/recommended",
    "plugin:@typescript-eslint/eslint-recommended",
    "plugin:@typescript-eslint/recommended",
    "prettier",
  ],
  ignorePatterns: ["src/orval/react-query"],
  parser: "@typescript-eslint/parser",
  parserOptions: {
    jsx: true,
    useJSXTextNode: true,
    project: "tsconfig.eslint.json",
    ecmaFeatures: {
      jsx: true,
    },
    ecmaVersion: 2018,
    sourceType: "module",
  },
  plugins: ["@typescript-eslint", "react", "react-hooks", "import"],
  settings: {
    react: {
      version: "detect",
    },
    "import/parsers": {
      "@typescript-eslint/parser": [".ts", ".tsx"],
    },
    "import/resolver": {
      typescript: {
        alwaysTryTypes: true,
        project: ["tsconfig.json"],
      },
    },
  },
  globals: {
    JSX: true,
  },
  // rules we want to enforce or disable
  rules: {
    // https://github.com/typescript-eslint/typescript-eslint/issues/2621
    "no-unused-vars": "off",
    "@typescript-eslint/no-unused-vars": "error",

    // Doesn't work with our neolution packages: DataTableColumnDescription not found in '@neolution-ch/react-data-table'
    "import/named": "off",

    // Enforce double quotes
    quotes: ["error", "double", { avoidEscape: true }],

    // Prefer string interpolation
    "prefer-template": "error",

    // Prefer arrow functions for components
    "react/function-component-definition": [
      "error",
      {
        namedComponents: "arrow-function",
        unnamedComponents: "arrow-function",
      },
    ],

    // https://github.com/vercel/next.js/issues/24421
    "@next/next/no-img-element": "off",

    "max-lines": ["error", { max: 200 }],
    complexity: ["error", { max: 12 }],
    "react-hooks/rules-of-hooks": "error",
    "react/jsx-filename-extension": ["error", { extensions: [".tsx", ".jsx"] }],
    "prefer-destructuring": "error",
    "no-empty-function": "error",
    "arrow-body-style": ["error", "as-needed"],

    // React
    "react/jsx-no-useless-fragment": ["error"],
    "react/prop-types": "off",

    "react/react-in-jsx-scope": "off",
    "react/jsx-uses-react": "off",

    eqeqeq: ["error", "always"],
  },
  overrides: [
    {
      files: ["src/providers/tokenProvider.ts"],
      rules: {
        "@typescript-eslint/no-var-requires": ["off"],
      },
    },
    {
      files: ["src/utils/misc.ts"],
      rules: {
        "import/no-named-as-default": ["off"],
      },
    },
    {
      files: ["src/axios/interceptors/response/dates.ts", "src/axios/interceptors/response/nullToUndefined.ts"],
      rules: {
        "@typescript-eslint/no-explicit-any": ["off"],
      },
    },
  ],
};
