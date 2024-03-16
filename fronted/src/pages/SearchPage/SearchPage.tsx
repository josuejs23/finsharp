import React, { ChangeEvent, SyntheticEvent, useState } from "react";
import { CompanySearch } from "../../company";
import { searchCompanies } from "../../api";
import Navbar from "../../components/Navbar/Navbar";
import Hero from "../../components/Hero/Hero";
import Search from "../../components/Search/Search";
import ListPortfolio from "../../components/Portfolio/ListPortfolio/ListPortfolio";
import CardList from "../../components/CardList/CardList";

interface Props {}

const SearchPage = (props: Props) => {
  const [search, setSearch] = useState<string>("");
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([]);
  const [portfolioValues, setPortfolioValues] = useState<string[]>([]);
  const [serverError, setServerError] = useState<string>("");

  const handleChangeSearch = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
  };

  const onPortfolioCreate = (e: any) => {
    e.preventDefault();
    if (portfolioValues.includes(e.target[0].value)) {
      return;
    }
    const values = [...portfolioValues, e.target[0].value];
    setPortfolioValues(values);
    console.log(portfolioValues);
  };

  const onSearchSubmit = async (e: SyntheticEvent) => {
    e.preventDefault();
    const result = await searchCompanies(search);
    if (typeof result === "string") {
      setServerError(result);
    } else if (Array.isArray(result.data)) {
      setSearchResult(result.data);
    }
  };

  const onDeletePortfolioValue = (e: any) => {
    e.preventDefault();
    const newPortfolio = portfolioValues.filter(
      (value) => value !== e.target[0].value
    );
    setPortfolioValues([...newPortfolio]);
    console.log(e.target[0].value);
  };
  return (
    <>
      {/* <Navbar /> */}
      <Hero />
      <Search
        handleSearchChange={handleChangeSearch}
        onSearchSubmit={onSearchSubmit}
        search={search}
      />
      <ListPortfolio
        portfolioValues={portfolioValues}
        onDeletePortfolioValue={onDeletePortfolioValue}
      />
      {serverError && <h1>There is an error</h1>}
      <CardList data={searchResult} onPortfolioCreate={onPortfolioCreate} />
    </>
  );
};

export default SearchPage;
