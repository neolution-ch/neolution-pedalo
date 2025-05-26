import React, { useRef } from "react";
import { Card, CardBody, CardTitle } from "reactstrap";
import Head from "next/head";
import { ColumnFilterType, DataTable, DataTableColumnDescription, ListSortDirection } from "@neolution-ch/react-data-table";
import { SideBarLayout } from "@neolution-ch/react-pattern-ui";
import { useT } from "src/hooks/useT";
import { PedaloModel, TranslationCodeId, pedalosQuery, usePedalosQuery } from "src/orval/react-query";
import { PedaloColor } from "src/orval/react-query/schemas/pedaloColor";

type T = PedaloModel;
type TFilter = Pick<T, "name">;

const ListPage = () => {
  const t = useT();
  const reloadData = useRef<() => void>(() => void 0);

  const columns: DataTableColumnDescription<T>[] = [
    {
      dataField: "pedaloId",
      text: t(TranslationCodeId.Label_PedaloId),
    },
    {
      dataField: "name",
      text: t(TranslationCodeId.Label_Name),
      filter: {
        filterType: ColumnFilterType.String,
      },
    },
    {
      dataField: "color",
      text: t(TranslationCodeId.Label_Color),
      formatter: ({ value }: { value: PedaloColor }) =>
        (Object.keys(PedaloColor) as (keyof typeof PedaloColor)[]).find((key) => PedaloColor[key] === value) || "",
    },
    {
      dataField: "capacity",
      text: t(TranslationCodeId.Label_Capacity),
    },
    {
      dataField: "hourlyRate",
      text: t(TranslationCodeId.Label_HourlyRate),
    },
  ];

  const { isLoading, data } = usePedalosQuery();

  return (
    <>
      <Head>
        <title>{t(TranslationCodeId.Title_Pedalos)}</title>
      </Head>
      <SideBarLayout>
        <Card>
          <CardBody>
            <CardTitle tag="h3">{t(TranslationCodeId.Text_PedalosIntro)}</CardTitle>

            {isLoading && <div>Loading...</div>}

            {!isLoading && data && (
              <DataTable<T, TFilter>
                keyField="pedaloId"
                data={data}
                columns={columns}
                handlers={(handlers) => {
                  reloadData.current = handlers.reloadData;
                }}
                query={(filter, limit, page, orderBy, orderDirection) =>
                  pedalosQuery({
                    "Filter.Name": filter.name,
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
