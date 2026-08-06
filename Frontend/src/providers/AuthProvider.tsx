import * as SecureStore from "expo-secure-store";
import { useEffect, useMemo, useState, type ReactNode } from "react";

import { AuthContext } from "@/contexts/AuthContext";
import { api } from "@/libs/api";

const TOKEN_KEY = "token";

type AuthProviderProps = {
  children: ReactNode;
};

export function AuthProvider({ children }: AuthProviderProps) {
  const [token, setToken_] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    SecureStore.getItemAsync(TOKEN_KEY).then((storedToken) => {
      setToken_(storedToken);
      setIsLoading(false);
    });
  }, []);

  useEffect(() => {
    if (token) {
      api.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    } else {
      delete api.defaults.headers.common["Authorization"];
    }
  }, [token]);

  function setToken(newToken: string | null) {
    setToken_(newToken);

    if (newToken) {
      SecureStore.setItemAsync(TOKEN_KEY, newToken);
    } else {
      SecureStore.deleteItemAsync(TOKEN_KEY);
    }
  }

  const contextValue = useMemo(
    () => ({ token, isLoading, setToken }),
    [token, isLoading],
  );

  return (
    <AuthContext.Provider value={contextValue}>
      {children}
    </AuthContext.Provider>
  );
}
