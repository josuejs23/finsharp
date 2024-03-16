import React from "react";
import DeletePortfolio from "../DeletePortfolio/DeletePortfolio";
import { Link } from "react-router-dom";

interface Props {
  portfolioValue: string;
  onDeletePortfolioValue: any;
}

const CardPortfolio = ({ portfolioValue, onDeletePortfolioValue }: Props) => {
  return (
    <div className="flex flex-col w-full p-8 space-y-4 text-center rounded-lg shadow-lg md:w-1/3">
      <Link
        to={`/company/${portfolioValue}`}
        className="pt-6 text-xl font-bold"
      >
        {portfolioValue}
      </Link>
      <DeletePortfolio
        portfolioValue={portfolioValue}
        onDeletePortfolio={onDeletePortfolioValue}
      />
    </div>
  );
};

export default CardPortfolio;
