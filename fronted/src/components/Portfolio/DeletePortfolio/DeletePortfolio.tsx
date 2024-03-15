import React, { SyntheticEvent } from "react";

interface Props {
  onDeletePortfolio: (e: SyntheticEvent) => void;
  portfolioValue: string;
}

const DeletePortfolio = ({ onDeletePortfolio, portfolioValue }: Props) => {
  return (
    <div>
      <form onSubmit={onDeletePortfolio}>
        <input hidden={true} value={portfolioValue} />
        <button type="submit">X</button>
      </form>
    </div>
  );
};

export default DeletePortfolio;
