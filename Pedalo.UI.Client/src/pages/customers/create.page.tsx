import React from "react";
import { Card, CardBody, CardTitle, Row, Col } from "reactstrap";
import Head from "next/head";
import { CustomerModel, TranslationCodeId } from "src/orval/react-query";
import { SideBarLayout } from "@neolution-ch/react-pattern-ui";
import { CustomerForm } from "./CustomerForm";
import { emptyGuid } from "@neolution-ch/javascript-utils";
import { useT } from "src/hooks/useT";

const CreateCustomerPage = () => {
  const t = useT();

  const defaultValues: CustomerModel = {
    customerId: emptyGuid,
    firstName: "",
    lastName: "",
    birthdayDate: new Date(),
  };

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
                <CustomerForm defaultValues={defaultValues} />
              </Col>
            </Row>
          </CardBody>
        </Card>
      </SideBarLayout>
    </>
  );
};

export default CreateCustomerPage;
