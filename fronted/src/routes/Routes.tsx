import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import HomePage from "../pages/HomePage/HomePage";
import SearchPage from "../pages/SearchPage/SearchPage";
import CompanyPage from "../pages/CompanyPage/CompanyPage";
import IncomeStatement from "../components/IncomeStatement/IncomeStatement";
import CompanyProfile from "../components/CompanyProfile/CompanyProfile";
import DesignPage from "../pages/DesignPage/DesignPage";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <HomePage /> },
      { path: "search", element: <SearchPage /> },
      { path: "design", element: <DesignPage /> },
      {
        path: "company/:ticker",
        element: <CompanyPage />,
        children: [
          { path: "company-profile", element: <CompanyProfile /> },
          { path: "income-statement", element: <IncomeStatement /> },
        ],
      },
    ],
  },
]);
