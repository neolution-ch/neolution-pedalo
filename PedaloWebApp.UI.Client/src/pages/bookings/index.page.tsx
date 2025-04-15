import React, { useRef } from "react";
import { Card, CardBody, CardTitle } from "reactstrap";
import Head from "next/head";
import {
  ColumnFilterType,
  DataTable,
  DataTableColumnDescription,
  ListSortDirection,
} from "@neolution-ch/react-data-table";
import { SideBarLayout } from "@neolution-ch/react-pattern-ui";
import { useT } from "src/hooks/useT";
import { BookingModel, TranslationCodeId, bookingsQuery, useBookingsQuery } from "src/orval/react-query";

type T = BookingModel;
type TFilter = Pick<T, "pedaloName" | "customerName">;

const formatDateTime = (date: Date | null): string =>
  date
    ? date.toLocaleString("de-CH", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit",
      })
    : "";

const ListPage = () => {
  const t = useT();
  const reloadData = useRef<() => void>(() => void 0);

  const columns: DataTableColumnDescription<T>[] = [
    {
      dataField: "bookingId",
      text: t(TranslationCodeId.Label_BookingId),
    },
    {
      dataField: "pedaloName",
      text: t(TranslationCodeId.Label_PedaloName),
      filter: {
        filterType: ColumnFilterType.String,
      },
    },
    {
      dataField: "customerName",
      text: t(TranslationCodeId.Label_CustomerName),
      filter: {
        filterType: ColumnFilterType.String,
      },
    },
    {
      dataField: "startDate",
      text: t(TranslationCodeId.Label_BookingStartDate),
      formatter: ({ value }: { value: Date }) => formatDateTime(value),
    },
    {
      dataField: "endDate",
      text: t(TranslationCodeId.Label_BookingEndDate),
      formatter: ({ value }: { value: Date | null }) => formatDateTime(value),
    },
  ];

  const { isLoading, data } = useBookingsQuery();

  return (
    <>
      <Head>
        <title>{t(TranslationCodeId.Title_Bookings)}</title>
      </Head>
      <SideBarLayout>
        <Card>
          <CardBody>
            <CardTitle tag="h3">{t(TranslationCodeId.Text_BookingsIntro)}</CardTitle>

            {isLoading && <div>Loading...</div>}

            {!isLoading && data && (
              <DataTable<T, TFilter>
                keyField="bookingId"
                data={data}
                columns={columns}
                handlers={(handlers) => {
                  reloadData.current = handlers.reloadData;
                }}
                query={(filter, limit, page, orderBy, orderDirection) =>
                  bookingsQuery({
                    "Filter.PedaloName": filter.pedaloName,
                    "Filter.CustomerName": filter.customerName,
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
