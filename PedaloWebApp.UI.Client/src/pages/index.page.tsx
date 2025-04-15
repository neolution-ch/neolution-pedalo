/* eslint-disable max-lines */
import React from "react";
import Head from "next/head";
import { SideBarLayout } from "@neolution-ch/react-pattern-ui";
import { NextPage } from "next";

const HomePage: NextPage = () => (
  <>
    <Head>
      <title>Whitelabel</title>
    </Head>
    <SideBarLayout>
      <h1>Welcome to the Whitelabel </h1>
      <p>This template already includes various features which will be exaplained here.</p>
      <h2>Authentication</h2>
      <p>
        Authentication is handled by{" "}
        <a target="_blank" rel="noopener noreferrer" href="https://next-auth.js.org/">
          next-auth
        </a>
        .
      </p>
    </SideBarLayout>
  </>
);

export default HomePage;
