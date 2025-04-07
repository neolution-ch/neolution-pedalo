# Next.js + React Client

The frontend for this project is based on Next.js (for Server Side Rendering) together with React.

## Best Practices

- **Use functional components:** We use functional components with hooks instead of class components. This is the recommended way to write React components nowadays. More information: <https://reactjs.org/docs/hooks-intro.html>

- **Use TypeScript:** We use TypeScript to write type safe code. This means that we can define the type of each variable and function parameter. This will help us to avoid bugs and make the code more readable. More information: <https://www.typescriptlang.org/docs/handbook/typescript-in-5-minutes.html>

- **Use `NextPage`** for next js pages:

  ```jsx
  import { NextPage } from "next";

  const HomePage: NextPage = () => <>hello world</>;

  export default HomePage;
  ```

- **Use `FC` for functional components:**
  **If your component needs children you can use `FC<PropsWithChildren<MyComponentProps>>` instead.**

  ```jsx
  import { FC } from "react";

  interface MyComponentProps {
    name: string;
  }

  const MyComponent: FC<MyComponentProps> = ({ name }) => <div>{name}</div>;

  export { MyComponent };
  ```

- Because we use the pageExtensions option in next.config.js, we can use the `.page.tsx` extension for pages. This also means that we can have components directly in the pages folder. This is useful for components that are only used on one page. If you want to use a component on multiple pages, it's recommended to create a new folder in the `src/components` folder and put the component there. This way we can keep the pages folder clean and have a better overview of the components.

## Translations

This section describes how the translation system works. In general we will try to use as much as possible of native Next.js features.

- next.config.js contains the configuration for the i18n module. Here we define the languages we want to support and the default language.
- getInitialProps in \_app.page.tsx will be called on every page load. Here we are doing two things:
  - Check if the translation cache is empty. If it is, we will load the translation from the API and store it in memory. This is done to avoid loading the translation on every page load.
  - Check the `translationHash` cookie. If it is empty or is different from the hash of the translation we have in memory, we will inject the translation into the page props. That means that they will be sent to the browser via the `__NEXT_DATA__` object. This is done to avoid loading the translation on every page load.
- in the render method of \_app.page.tsx (only on the browser) we will check if the props contain any translations. If they do, we will add them to the localstorage and set the `translationHash` cookie to the hash of the translation.
- Finally to use the translations we use the `useGetT` hook which will return a function that can be used to translate a key. On the server the hook will load the translations from the memory cache and on the client it will load them from the localstorage.
  - To optimize the performance of the hook we are using a context to store this function. Every component can simply call `useT` to get the function. This way we don't need to pass the function down the component tree.

### Changing language

- We use the `NEXT_LOCALE` cookie to store the current language. This cookie will be set as soon as the user visits the page. The value of the cookie will be the language that Next.js determines based on the `Accept-Language` header, supported languages and default language.
- When we want to change the language we will simply set the `NEXT_LOCALE` cookie to the new language and redirect the user to the same page with the new locale. After this we will reload the page to make the translations hash invalid and force the page to load the new translations (and write them to the localstorage).
- For the API to also be aware of the language, we attach the `NEXT_LOCALE` cookie to the `Language` header. This way the API will know which language the user wants to use.

## Software required

To get a local instance of the whitelabel client running you will need the following software installed on your machine.

- **Visual Studio Code**
  We use this to edit all the typescript files, works better with TypeScript than VS and has more features. If you only want to run the client you don't need to install VS Code.
  <https://code.visualstudio.com/>

- **yarn**

  Package Manger comparable to npm
  <https://yarnpkg.com/lang/en/docs/install/#windows-stable>

- **Node JS:**

  JS Runtime used

  <https://nodejs.org/en/>

## How to get the application running

- Set default terminal to cmd (<https://stackoverflow.com/questions/44435697/vscode-change-default-terminal>)
- Open the root folder in VS Code. So you can edit both the Web API and React Frontend at the same time.
- Right click on `Pedalo.UI.Client` and select `Open in Terminal`
- First press ctrl + F5 (or without ctrl if you want to debug) to launch the Web API. This will build the .NET Core Project and also generate the NSWag client.
- Enter `yarn` into terminal to install all the required packages
- Enter `yarn dev` into terminal. This will start a development version of the website. It will be ready on <http://localhost:30349>

## Scripts in package.json

- `yarn dev` will run the client in development mode. This means all pages will be compiled on the fly. This allows you to edit a page or component and will see the changes shortly after the hot reloading process is finished. The downside is that it can be quiet slow because every page / component will only be compiled once it's requested. If you only need to work on the API side, it's recommended to use `yarn buildandstart` instead.
- `yarn build` will compile an optimized version of the client. If you change something you will need to build the project again. Much faster than `yarn dev` because everything is precompiled. Use `yarn start` afterwards to start the project.
- `yarn start` will start an optimized build. Requires `yarn build` to be executed first.
- `yarn buildandstart` combination of `yarn build` and `yarn start`
- `yarn build:analyze` Same as `yarn build` but will analyze the build (will open two web pages when done with an interactive map of the packages used and their size)
- `yarn kill` will kill all `node.exe` instances (Warning: **Will kill all node.exe processes not only the one from your project**)
- `yarn lint` will run `eslint` and report all problems in the console
- `yarn pr` will first run `eslint` and after that build the project like it will be built on the build server. It's advised to execute this before creating a pull request so you don't end up fixing annoying errors after the build server took a while to report this to you. Most errors will be catched by eslint but there are some errors that will only appear in the TypeScript build.

## Software Stack

- **TypeScript:** Transpiller to write typesafe JS Code
- **Node.js:** JavaScript runtime for WebServers
- **Next.js:** Used for Server Side Rendering
- **ESLint**: Pluggable linting utility (Code Analyzer)

## Used Packages

- **@next/bundle-analyzer**: Provides functionality to run the analyze build to analyze the size of each package. Used in `next.config.js`
- **cookies-next:** Small helper library to read cookies from the context easily. We could also do it manually but this just makes things easier.
- **reactstrap**: This library is used to provide us with all the standard bootstrap components, so we don't need to write the HTML ourself.
- **[config](https://github.com/node-config/node-config)**: Library to handle the loading and merging of configs files and environment variable to have the final configuration available.

## How Orval TypeScript client generation works

- In the `Pedalo.UI.Api.csproj` file there is a Target defined to run after build.
- `$(NSwagExe_Core31)` is made available through the `NSwag.MSBuild` nuget.
- The `config.nswag.json` file contains the whole config. Here are the most important parameters:
  - **documentGenerator.aspNetCoreToOpenApi.assemblyPaths**: Contains the path to the .dll which the client will be generated according to.
  - **documentGenerator.aspNetCoreToOpenApi.output**: Contains the path where the generated open api spec will be saved to.
- Once the open api spec is generated, orval can be used to generate the typescript clients. Run `yarn orval` to generate the client based on the `orval.config.ts` file.

## Configuration files for the client

The Configuration files are located in the `config` folder. By default the `default.json` file will be loaded. The config file contains two main sections: `serverRuntimeConfig` and `publicRuntimeConfig`. The difference is that `serverRuntimeConfig` will only be available on the node server (not accessible by the user) whereas `publicRuntimeConfig` will be available on the node server and the browser client. More information: <https://nextjs.org/docs/api-reference/next.config.js/runtime-configuration>

### Using a different config file

To use a different config file than the default `default.json` you have to set the environment variable `NODE_CONFIG_ENV`. For example if you want to load the file `test.json` you could alter the `dev` script in the packages.json file to the following `"dev": "cross-env NODE_CONFIG_ENV=test yarn orval && next dev -p 30349"`. The `default.json` will always be loaded first and then the config file specified in the `NODE_CONFIG_ENV` variable will be loaded and merged with the default config.

### Overriding settings with environment variables

To override certain configuration values you can assign environment variables, they have to be configured in the file `custom-environment-variables.json` in the `config` folder like:

```json
{
  "serverRuntimeConfig": {
    "googleClientSecret": "serverRuntimeConfig__googleClientSecret"
  },
  "publicRuntimeConfig": {
    "environment": "NODE_ENV"
  }
}
```

You can override `googleClientSecret` by setting the environment variable `serverRuntimeConfig__googleClientSecret` to whatever you like. Values are only overwritten if an environment variable is set, if there is none the value remains untouched.

You can test this on your local machine by using a `cross-env`. For example altering the `dev` script in the `packages.json` file to `"dev": "cross-env serverRuntimeConfig__googleClientSecret=overridden yarn orval && next dev -p 30349",` will override the `googleClientSecret` setting in the `serverRuntimeConfig`.

**If a value need to be overwritten without having the mapping done in the json file (for testing or debuging purposes), you can do that over the `NODE_CONFIG` variable: <https://github.com/node-config/node-config/wiki/Environment-Variables#node_config>**

### next-config.d.ts file

The `next-config.d.ts` file is simply a strongly typed representation of the json file so it's more convenient for you to read settings and makes them strongy typed. **You should never print a config from the `serverRuntimeConfig` to the website, otherwise it will be visible in the server HTML**

## Useful Tutorials / Links

- **React Tic Tac Toe Tutorial**
  Very good introduction to tic tac toe. Unfortunately it's in JS and not TS. But google finds multiple TS implementations.

  <https://reactjs.org/tutorial/tutorial.html>

- **useRef vs useState**
  <https://www.codebeast.dev/usestate-vs-useref-re-render-or-not/>

- **8 React Conditional Rendering Methods**
  <https://blog.logrocket.com/conditional-rendering-in-react-c6b0e5af381e/>

- **Learn Map, Filter and Reduce**
  <https://medium.com/@joomiguelcunha/learn-map-filter-and-reduce-in-javascript-ea59009593c4>
