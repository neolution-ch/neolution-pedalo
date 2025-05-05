/* eslint-disable max-lines */
import React from "react";
import Head from "next/head";
import { SideBarLayout } from "@neolution-ch/react-pattern-ui";
import { NextPage } from "next";

const HomePage: NextPage = () => (
  <>
    <Head>
      <title>Rent-A-Pedalo</title>
    </Head>
    <SideBarLayout>
      <h1>Welcome to the Rent-A-Pedalo</h1>
      <p>This template already includes various features which will be exaplained here.</p>
      <ul>
        <li>Fully working <a href="https://en.wikipedia.org/wiki/Create,_read,_update_and_delete" className="link-primary" target="_blank" rel="noreferrer">CRUD</a> customer management</li>
        <li>Download a PDF fact sheet for each customer</li>
        <li>Overview of the pedalo fleet</li>
        <li>Overview of the past and current bookings</li>
        <li>Switch between German and English as the UI language</li>
      </ul>
      <p>Other features still need to be implemented, you can find some of them in README.</p>
      <h2>Before you start</h2>
      <p>Check the README to understand the architecture and tools involved to run and develop this software.</p>
    </SideBarLayout>
  </>
);

export default HomePage;
