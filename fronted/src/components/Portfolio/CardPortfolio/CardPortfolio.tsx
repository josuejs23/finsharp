import React from "react";
import DeletePortfolio from "../DeletePortfolio/DeletePortfolio";

interface Props {
  portfolioValue: string;
  onDeletePortfolioValue: any;
}

const CardPortfolio = ({ portfolioValue, onDeletePortfolioValue }: Props) => {
  return (
    <>
      <h4>{portfolioValue}</h4>
      <DeletePortfolio
        portfolioValue={portfolioValue}
        onDeletePortfolio={onDeletePortfolioValue}
      />
    </>
  );
};

export default CardPortfolio;
