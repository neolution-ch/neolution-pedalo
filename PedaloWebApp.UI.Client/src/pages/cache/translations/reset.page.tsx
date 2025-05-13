import { GetServerSideProps, NextPage } from "next";
import { CacheContainer, cacheProvider } from "src/cache/cache";

const Reset: NextPage = () => null;

export const getServerSideProps: GetServerSideProps = async (context) => {
  const { res } = context;

  cacheProvider.reset(CacheContainer.Translations);

  res.statusCode = 200;
  res.setHeader("Content-Type", "application/json");
  res.end(JSON.stringify({ ok: true }));

  return {
    props: {},
  };
};

export default Reset;
