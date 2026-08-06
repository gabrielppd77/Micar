import { createContext } from "react";

export interface AuthContextState {
  token: string | null;
  isLoading: boolean;
  setToken: (newToken: string | null) => void;
}

export const AuthContext = createContext<AuthContextState>({
  token: null,
  isLoading: true,
  setToken: () => undefined,
});
