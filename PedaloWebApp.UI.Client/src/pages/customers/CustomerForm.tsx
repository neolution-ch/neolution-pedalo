import { yupResolver } from "@hookform/resolvers/yup";
import React, { FC } from "react";
import { ButtonGroup, Button } from "reactstrap";
import { CustomerModel, TranslationCodeId, useCustomersCreate, useCustomersUpdate } from "src/orval/react-query";
import { toastSuccess } from "src/components/Misc/Toasts";
import { Form, Input, DatePickerInput } from "@neolution-ch/react-hook-form-components";
import * as yup from "yup";
import { useRouter } from "next/router";
import { emptyGuid } from "@neolution-ch/javascript-utils";
import { useT } from "src/hooks/useT";

interface CustomerFormProps {
  defaultValues: CustomerModel;
}

const CustomerForm: FC<CustomerFormProps> = ({ defaultValues }) => {
  const t = useT();

  const { push } = useRouter();

  const schema = yup.object().shape({
    firstName: yup.string().required(t(TranslationCodeId.Error_InputRequired)),
    lastName: yup.string().required(t(TranslationCodeId.Error_InputRequired)),
    birthdayDate: yup.date().required(t(TranslationCodeId.Error_InputRequired)),
  });

  const customersCreate = useCustomersCreate({
    mutation: {
      onSuccess: (customerId) => {
        toastSuccess(t(TranslationCodeId.Label_DataSavedSuccess));
        push({ pathname: "/customers/[customerId]", query: { customerId } });
      },
    },
  });

  const customersUpdate = useCustomersUpdate({
    mutation: {
      onSuccess: () => {
        toastSuccess(t(TranslationCodeId.Label_DataSavedSuccess));
      },
    },
  });

  return (
    <Form<CustomerModel>
      defaultValues={defaultValues}
      resolver={yupResolver(schema)}
      onSubmit={async (values) => {
        if (values.customerId === emptyGuid) {
          customersCreate.mutate({
            data: values,
          });
        } else {
          customersUpdate.mutate({
            id: values.customerId,
            data: values,
          });
        }
      }}
    >
      {({ formState: { isSubmitting } }) => (
        <>
          <Input<CustomerModel> name="firstName" label={t(TranslationCodeId.Label_Firstname)} />
          <Input<CustomerModel> name="lastName" label={t(TranslationCodeId.Label_Lastname)} />
          <DatePickerInput<CustomerModel> name="birthdayDate" label={t(TranslationCodeId.Label_DateOfBirth)} />

          <ButtonGroup>
            <Button disabled={isSubmitting} color="primary" type="submit">
              {t(TranslationCodeId.Button_Save)}
            </Button>
            <Button onClick={() => push("/customers")}>{t(TranslationCodeId.Button_Back)}</Button>
          </ButtonGroup>
        </>
      )}
    </Form>
  );
};

export { CustomerForm };
