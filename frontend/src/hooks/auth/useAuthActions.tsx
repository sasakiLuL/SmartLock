import { useContext } from "react";
import { KeycloakContext } from "./KeycloakProvider";
import { KeycloakLoginOptions, KeycloakLogoutOptions, KeycloakRegisterOptions } from "keycloak-js";

export interface AuthActions {
  login: (options?: KeycloakLoginOptions) => Promise<void>;
  register: (options?: KeycloakRegisterOptions) => Promise<void>;
  logout: (options?: KeycloakLogoutOptions) => Promise<void>;
}

export default function useAuthActions() {
  const keycloakService = useContext(KeycloakContext);

  if (keycloakService === undefined) {
    throw new Error("useAuthActions must be used with a KeycloakProvider");
  }

  var authActions: AuthActions = {
    login: keycloakService.login,
    register: keycloakService.register,
    logout: keycloakService.logout,
  };

  return authActions;
}
