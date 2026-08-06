import { useContext } from "react";

import { AuthContext, type AuthContextState } from "../contexts/AuthContext";

export function useAuth() {
  return useContext<AuthContextState>(AuthContext);
}
