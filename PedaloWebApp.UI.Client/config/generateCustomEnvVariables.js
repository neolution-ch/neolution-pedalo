// This script will be executed during the build process to generate a custom-environment-variables.json file
// We do this so we can override the default config values with environment variables
// Unfortunately, the config module does not support this out of the box
// The generated file is gitignored, because the docker container will generate it at build time
// ref: https://github.com/node-config/node-config/wiki/Environment-Variables#custom-environment-variables

const fs = require("fs");

function transformObject(obj, path = "") {
  const transformedObj = {};

  for (const key in obj) {
    if (typeof obj[key] === "object" && obj[key] !== null) {
      transformedObj[key] = transformObject(obj[key], path + key + "__");
    } else {
      transformedObj[key] = path + key;
    }
  }

  return transformedObj;
}

// Read the input JSON file
const inputFile = "config/default.json";
const inputJSON = JSON.parse(fs.readFileSync(inputFile, "utf8"));

// Transform the JSON for all root-level keys
const transformedJSON = {};
for (const key in inputJSON) {
  transformedJSON[key] = transformObject(inputJSON[key], `${key}__`);
}

// Write the transformed JSON to the output file
const outputFile = "config/custom-environment-variables.json";
fs.writeFileSync(outputFile, JSON.stringify(transformedJSON, null, 2));

console.log(`Transformation complete. Result saved to ${outputFile}`);
