import React from "react";
import Table from "../../components/Table/Table";
import RatioList from "../../components/RatioList/RatioList";
import { CompanyKeyMetrics } from "../../company";
import { testIncomeStatementData } from "../../components/Table/testData";

type Props = {};
const tableConfig = [
  {
    label: "Market Cap",
    render: (company: any) => company.marketCapTTM,
    subTitle: "Total value of all a company's shares of stock",
  },
];

const DesignPage = (props: Props) => {
  return (
    <>
      <h1>FinShark Design page</h1>
      <h2>This is the app design page</h2>
      <RatioList data={testIncomeStatementData} config={tableConfig} />
      <Table config={tableConfig} data={testIncomeStatementData} />
    </>
  );
};

export default DesignPage;
