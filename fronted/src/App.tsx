import React, { ChangeEvent, SyntheticEvent, useState } from "react";
import logo from "./logo.svg";
import "./App.css";
import Card from "./components/Card/Card";
import CardList from "./components/CardList/CardList";
import Search from "./components/Search/Search";
import { CompanySearch } from "./company";
import { searchCompanies } from "./api";
import ListPortfolio from "./components/Portfolio/ListPortfolio/ListPortfolio";
import Navbar from "./components/Navbar/Navbar";
import Hero from "./components/Hero/Hero";

function App() {
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
    <div className="App">
      <Navbar />
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

      {/* <Card image="https://images.unsplash.com/photo-1620321023374-d1a68fbc720d?q=80&w=2994&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" /> */}
    </div>
  );
}

export default App;
