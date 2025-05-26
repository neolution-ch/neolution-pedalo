/* eslint-disable max-lines */
import React from "react";
import Head from "next/head";
import { SideBarLayout } from "@neolution-ch/react-pattern-ui";
import { NextPage } from "next";

const ForbiddenPage: NextPage = () => (
  <>
    <Head>
      <title>Forbidden</title>
    </Head>
    <SideBarLayout>
      <h1>Forbidden</h1>
      <p>You are not authorized to read this content.</p>
    </SideBarLayout>
  </>
);

export default ForbiddenPage;
