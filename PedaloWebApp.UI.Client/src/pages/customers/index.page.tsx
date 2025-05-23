import React, { useRef } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus, faFilePdf } from "@fortawesome/free-solid-svg-icons";
import { Card, CardBody, CardTitle, ButtonGroup, Button } from "reactstrap";
import Head from "next/head";
import {
  ColumnFilterType,
  DataTable,
  DataTableColumnDescription,
  DataTableRoutedActions,
  ListSortDirection,
} from "@neolution-ch/react-data-table";
import { toastError, toastSuccess } from "src/components/Misc/Toasts";
import { SideBarLayout } from "@neolution-ch/react-pattern-ui";
import { useT } from "src/hooks/useT";
import {
  CustomerModel,
  TranslationCodeId,
  customersQuery,
  useCustomersDelete,
  useCustomersQuery,
  getCustomersPdfQueryKey,
} from "src/orval/react-query";
import { useRouter } from "next/router";
import { Pathname } from "src/utils/routes";
import { ReactDataTableLinkRender } from "src/components/ReactDataTableLinkRender/ReactDataTableLinkRender";

type T = CustomerModel;
type TFilter = Pick<T, "firstName" | "lastName">;

const ListPage = () => {
  const t = useT();
  const { push } = useRouter();
  const reloadData = useRef<() => void>(() => void 0);

  const { mutateAsync: deleteAsync } = useCustomersDelete({
    mutation: {
      onSuccess: () => {
        toastSuccess(t(TranslationCodeId.RecordDeleteSuccess));
        reloadData.current();
      },
      onError: () => {
        toastError("Can't delete");
      },
    },
  });

  const columns: DataTableColumnDescription<T>[] = [
    {
      dataField: "customerId",
      text: t(TranslationCodeId.Label_CustomerId),
    },
    {
      dataField: "firstName",
      text: t(TranslationCodeId.Label_Firstname),
      filter: {
        filterType: ColumnFilterType.String,
      },
    },
    {
      dataField: "lastName",
      text: t(TranslationCodeId.Label_Lastname),
      filter: {
        filterType: ColumnFilterType.String,
      },
    },
  ];

  const actions: DataTableRoutedActions<T, Pathname> = {
    view: {
      route: "/customers/[customerId]",
      getParams: ({ keyValue }) => ({ customerId: keyValue }),
      link: ReactDataTableLinkRender,
    },
    delete: {
      action: async ({ key }) => {
        await deleteAsync({
          id: key,
        });
      },
      title: t(TranslationCodeId.Title_DeleteEntry),
      text: t(TranslationCodeId.Text_ConfirmDeleteEntry),
    },
    others: [
      {
        formatter: ({ row }) => (
          <a href={`/api/proxy${getCustomersPdfQueryKey(row.customerId)}`} target="_blank" rel="noopener noreferrer">
            <FontAwesomeIcon icon={faFilePdf} style={{ cursor: "pointer" }} title={t(TranslationCodeId.Button_DownloadPdf)} />
          </a>
        ),
      },
    ],
  };

  const { isLoading, data } = useCustomersQuery();

  return (
    <>
      <Head>
        <title>{t(TranslationCodeId.Title_Customers)}</title>
      </Head>
      <SideBarLayout>
        <Card>
          <CardBody>
            <CardTitle tag="h3">{t(TranslationCodeId.Text_CustomersIntro)}</CardTitle>
            <div className="d-flex justify-content-end align-items-end mb-2">
              <div className="d-flex ml-auto" />
              <ButtonGroup>
                <Button
                  type="button"
                  color="primary"
                  onClick={() => {
                    push("/customers/create");
                  }}
                >
                  <FontAwesomeIcon icon={faPlus} />
                </Button>
              </ButtonGroup>
            </div>

            {isLoading && <div>Loading...</div>}

            {!isLoading && data && (
              <DataTable<T, TFilter>
                keyField="customerId"
                data={data}
                columns={columns}
                actions={actions}
                handlers={(handlers) => {
                  reloadData.current = handlers.reloadData;
                }}
                query={(filter, limit, page, orderBy, orderDirection) =>
                  customersQuery({
                    "Filter.FirstName": filter.firstName,
                    "Filter.LastName": filter.lastName,
                    "Options.Limit": limit,
                    "Options.Page": page,
                    "Options.OrderBy": orderBy as string,
                    "Options.SortDirection": orderDirection ? ListSortDirection.Ascending : ListSortDirection.Descending,
                  })
                }
              />
            )}
          </CardBody>
        </Card>
      </SideBarLayout>
    </>
  );
};

export default ListPage;
