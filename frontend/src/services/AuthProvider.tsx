import { useContext, createContext, useState } from "react";
import axios from "axios";
import TokenModel from "../interfaces/tokens/TokenModel";
import CredentialsModel from "../interfaces/tokens/CredentialsModel";
import RefreshTokenModel from "../interfaces/tokens/RefreshTokenModel";
import AccountModel from "../interfaces/AccountModel";
import { jwtDecode } from "jwt-decode";
import ServerResponseModel from "../interfaces/ServerResponseModel";
import config from "../application.json";

export interface IAuthProvider {
  signIn(credentialsModel: CredentialsModel): Promise<boolean>;
  refreshToken(): Promise<boolean>;
  signOut(): void;
  getAccount(): AccountModel | null;
}

const AuthContext = createContext<IAuthProvider | undefined>(undefined);

export const useAuth = () => {
  var authContext = useContext(AuthContext);

  if (!authContext) {
    throw new Error("useAuth must be used within a AuthProvider");
  }

  return authContext;
};

const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [accessToken, setAccessToken] = useState<string | null>(null);
  const [refreshToken, setRefreshToken] = useState<string | null>(null);

  const processTokenResponse = (tokenModel: TokenModel | null) => {
    if (tokenModel) {
      setAccessToken(tokenModel.accessToken);
      setRefreshToken(tokenModel.refreshToken);

      localStorage.setItem("access_token", tokenModel.accessToken);
      localStorage.setItem("refresh_token", tokenModel.refreshToken);
    } else {
      setAccessToken(null);
      setRefreshToken(null);

      localStorage.removeItem("access_token");
      localStorage.removeItem("refresh_token");
    }
  };

  const authProvider = {
    getAccount(): AccountModel | null {
      let token = accessToken ?? localStorage.getItem("access_token");
      if (!token) {
        return null;
      }

      let accountModel = jwtDecode<AccountModel>(token);
      if (new Date() >= new Date(accountModel.exp * 1000)) {
        return null;
      }

      return accountModel;
    },
    async signIn(credentialsModel: CredentialsModel): Promise<boolean> {
      try {
        const response = await axios.post<
          CredentialsModel,
          ServerResponseModel<TokenModel>
        >(config.apiUrl + "/api/tokens/create", credentialsModel);
        processTokenResponse(response.data);
        return true;
      } catch {
        processTokenResponse(null);
      }

      return false;
    },
    async refreshToken(): Promise<boolean> {
      if (!refreshToken) {
        return false;
      }

      try {
        const response = await axios.post<
          RefreshTokenModel,
          ServerResponseModel<TokenModel>
        >(config.apiUrl + "/api/tokens/refresh", {
          refreshToken: refreshToken
        });
        processTokenResponse(response.data);
        return true;
      } catch {
        processTokenResponse(null);
      }

      return false;
    },
    signOut(): void {
      processTokenResponse(null);
    }
  };

  return (
    <AuthContext.Provider value={authProvider}>{children}</AuthContext.Provider>
  );
};

export default AuthProvider;
