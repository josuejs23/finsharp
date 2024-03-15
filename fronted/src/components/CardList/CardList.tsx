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
        <p className="mb-3 mt-3 text-xl font-semibold text-center md:text-xl">
          No results!
        </p>
      )}
    </div>
  );
};

export default CardList;
