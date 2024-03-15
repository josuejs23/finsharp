import React, {
  ChangeEvent,
  MouseEvent,
  SyntheticEvent,
  useState,
} from "react";
import { searchCompanies } from "../../api";

interface Props {
  handleSearchChange: (e: ChangeEvent<HTMLInputElement>) => void;
  onSearchSubmit: (e: SyntheticEvent) => void;
  search: string | undefined;
}

const Search: React.FC<Props> = ({
  search,
  handleSearchChange,
  onSearchSubmit,
}: Props): JSX.Element => {
  return (
    <>
      <form onSubmit={onSearchSubmit}>
        <input value={search} onChange={handleSearchChange} />
      </form>
    </>
  );
};

export default Search;
