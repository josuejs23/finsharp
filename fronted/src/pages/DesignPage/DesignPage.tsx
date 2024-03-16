import React from "react";
import Table from "../../components/Table/Table";
import RatioList from "../../components/RatioList/RatioList";

type Props = {};

const DesignPage = (props: Props) => {
  return (
    <>
      <h1>FinShark Design page</h1>
      <h2>This is the app design page</h2>
      <RatioList />
      <Table />
    </>
  );
};

export default DesignPage;
