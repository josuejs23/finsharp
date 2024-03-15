import React, { SyntheticEvent } from "react";
import CardPortfolio from "../CardPortfolio/CardPortfolio";

interface Props {
  portfolioValues: string[];
  onDeletePortfolioValue: (e: SyntheticEvent) => void;
}

const ListPortfolio = ({ portfolioValues, onDeletePortfolioValue }: Props) => {
  return (
    <div>
      {portfolioValues.length > 0 ? (
        portfolioValues.map((value) => (
          <CardPortfolio
            key={value}
            onDeletePortfolioValue={onDeletePortfolioValue}
            portfolioValue={value}
          />
        ))
      ) : (
        <h2>No data</h2>
      )}
    </div>
  );
};

export default ListPortfolio;
