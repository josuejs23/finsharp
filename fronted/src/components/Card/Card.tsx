import React, { SyntheticEvent } from "react";
import "./Card.css";
import { JsxElement, symbolName } from "typescript";
import { CompanySearch } from "../../company";
import AddPortfolio from "../Portfolio/AddPortfolio/AddPortfolio";
interface Props {
  id: string;
  searchResult: CompanySearch;
  onPortfolioCreate: (e: SyntheticEvent) => void;
}

const Card: React.FC<Props> = ({
  id,
  searchResult,
  onPortfolioCreate,
}: Props): JSX.Element => {
  return (
    <div className="card">
      <img
        src="https://images.unsplash.com/photo-1620321023374-d1a68fbc720d?q=80&w=2994&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
        alt="logo"
      />
      <div className="details">
        <h2>
          {searchResult.name} ({searchResult.symbol})
        </h2>
        <p>{searchResult.symbol}</p>
      </div>
      <p className="info">
        {searchResult.exchangeShortName} - {searchResult.stockExchange}
      </p>
      <AddPortfolio
        onPortfolioCreate={onPortfolioCreate}
        symbol={searchResult.symbol}
      />
    </div>
  );
};

export default Card;
