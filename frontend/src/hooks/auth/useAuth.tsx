import { useContext } from "react";
import { KeycloakContext } from "./KeycloakProvider";
import { KeycloakTokenParsed } from "keycloak-js";

export interface Auth {
  isAuthenticated: boolean;
  token: string | null;
  tokenParsed: KeycloakTokenParsed | null;
}

export function useAuth() {
  const keycloakService = useContext(KeycloakContext);

  if (keycloakService === undefined) {
    throw new Error("useAuth must be used with a KeycloakProvider");
  }

  var auth: Auth = {
    isAuthenticated: keycloakService.isAuthenticated,
    token: keycloakService.token,
    tokenParsed: keycloakService.tokenParsed,
  };

  return auth;
}
