import { useContext } from "react";
import { KeycloakContext } from "./KeycloakProvider";

export interface AuthActions {
  login: () => void;
  register: () => void;
  logout: () => void;
}

export function useAuthActions() {
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
