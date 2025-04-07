import React from "react";
import Head from "next/head";
import { Card, CardBody, CardTitle, Row, Col } from "reactstrap";
import { SideBarLayout } from "@neolution-ch/react-pattern-ui";
import { CustomerForm } from "./CustomerForm";
import { useRouter } from "next/router";
import { useT } from "src/hooks/useT";
import { TranslationCodeId, useCustomersRead } from "src/orval/react-query";

const UpdateCustomerPage = () => {
  const t = useT();
  const router = useRouter();

  const { data, isLoading } = useCustomersRead(router.query.customerId as string);

  return (
    <>
      <Head>
        <title>{t(TranslationCodeId.Title_Address_Edit)}</title>
      </Head>
      <SideBarLayout>
        <Card>
          <CardBody>
            <CardTitle tag="h3">{t(TranslationCodeId.Title_Address_Edit)}</CardTitle>
            <Row>
              <Col lg="3">
                {!isLoading && data && <CustomerForm defaultValues={data} />}
              </Col>
            </Row>
          </CardBody>
        </Card>
      </SideBarLayout>
    </>
  );
};

export default UpdateCustomerPage;
