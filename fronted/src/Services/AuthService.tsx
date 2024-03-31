import axios from "axios";
import { errorHandler } from "../Helpers/ErrorHandler";
import { UserProfileToken } from "../Models/User";

const api = "http://localhost:5022/api";

export const loginApi = async (username: string, password: string) => {
  try {
    const data = await axios.post<UserProfileToken>(api + "account/login", {
      username,
      password,
    });
    return data;
  } catch (error) {
    errorHandler(error);
  }
};

export const registerApi = async (
  email: string,
  username: string,
  password: string
) => {
  try {
    const data = await axios.post<UserProfileToken>(api + "account/register", {
      email,
      username,
      password,
    });
    return data;
  } catch (error) {
    errorHandler(error);
  }
};
