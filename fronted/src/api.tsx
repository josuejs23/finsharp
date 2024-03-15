import axios from "axios";
import { CompanySearch } from "./company";

interface SearchResponse {
  data: CompanySearch[];
}
export const searchCompanies = async (query: string) => {
  console.log(process.env);
  try {
    const data = await axios.get<SearchResponse>(
      `https://financialmodelingprep.com/api/v3/search-name?query=${query}&apikey=${process.env.REACT_APP_API_KEY}`
    );
    console.log(data);
    return data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.log("Axios error: ", error.message);
      return error.message;
    } else {
      console.log("Unexpected error: ", error);
      return "Unexpected error has occur";
    }
  }
};
