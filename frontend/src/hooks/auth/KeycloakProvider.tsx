import Keycloak, { KeycloakLoginOptions, KeycloakLogoutOptions, KeycloakRegisterOptions, KeycloakTokenParsed } from "keycloak-js";
import { createContext, ReactNode, useEffect, useState } from "react";
import { KeycloakSettings } from "../../utils/KeycloakSettings";

export interface KeycloakProviderProps {
  children: ReactNode;
}

export interface KeycloakService {
  isAuthenticated: boolean;
  token: string | null;
  tokenParsed: KeycloakTokenParsed | null;
  login: (options?: KeycloakLoginOptions) => Promise<void>;
  register: (options?: KeycloakRegisterOptions) => Promise<void>;
  logout: (options?: KeycloakLogoutOptions) => Promise<void>;
}

const keycloak = new Keycloak(KeycloakSettings);

keycloak
  .init({ onLoad: "check-sso" })
  .catch((err) => console.error("Keycloak init failed:", err));

export const KeycloakContext = createContext<KeycloakService | undefined>(
  undefined
);

export function KeycloakProvider({ children }: KeycloakProviderProps) {
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
  const [token, setToken] = useState<string | null>(null);
  const [tokenParsed, setTokenParsed] = useState<KeycloakTokenParsed | null>(
    null
  );

  useEffect(() => {
    keycloak.onAuthSuccess = () => {
      setIsAuthenticated(true);
      setToken(keycloak.token!);
      setTokenParsed(keycloak.tokenParsed!);
    };
    keycloak.onAuthLogout = () => {
      setIsAuthenticated(false);
      setToken(null);
      setTokenParsed(null);
    };
    keycloak.onTokenExpired = () => {
      keycloak.updateToken(30).catch(() => {
        setIsAuthenticated(false);
        setToken(null);
        setTokenParsed(null);
      });
    };
  }, []);

  return (
    <KeycloakContext.Provider
      value={{
        isAuthenticated: isAuthenticated,
        token: token,
        tokenParsed: tokenParsed,
        login: keycloak.login,
        register: keycloak.register,
        logout: keycloak.logout,
      }}
    >
      {children}
    </KeycloakContext.Provider>
  );
}
