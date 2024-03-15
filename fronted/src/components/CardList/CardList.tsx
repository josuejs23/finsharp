import React, { SyntheticEvent } from "react";
import Card from "../Card/Card";
import { CompanySearch } from "../../company";
import { v4 as uuidv4 } from "uuid";

type Props = {
  data: CompanySearch[];
  onPortfolioCreate: (e: SyntheticEvent) => void;
};

const CardList: React.FC<Props> = ({
  data,
  onPortfolioCreate,
}: Props): JSX.Element => {
  return (
    <div>
      {data.length > 0 ? (
        data.map((c) => {
          return (
            <Card
              key={uuidv4()}
              searchResult={c}
              onPortfolioCreate={onPortfolioCreate}
              id=""
            />
          );
        })
      ) : (
        <h1>No data</h1>
      )}
    </div>
  );
};

export default CardList;
