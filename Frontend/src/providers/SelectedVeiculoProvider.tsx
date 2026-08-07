import * as SecureStore from "expo-secure-store";
import { useEffect, useMemo, useState, type ReactNode } from "react";

import { SelectedVeiculoContext } from "@/contexts/SelectedVeiculoContext";

const SELECTED_VEICULO_KEY = "selectedVeiculoId";

type SelectedVeiculoProviderProps = {
  children: ReactNode;
};

export function SelectedVeiculoProvider({
  children,
}: SelectedVeiculoProviderProps) {
  const [selectedVeiculoId, setSelectedVeiculoId_] = useState<string | null>(
    null,
  );
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    SecureStore.getItemAsync(SELECTED_VEICULO_KEY).then((storedId) => {
      setSelectedVeiculoId_(storedId);
      setIsLoading(false);
    });
  }, []);

  function setSelectedVeiculoId(id: string | null) {
    setSelectedVeiculoId_(id);

    if (id) {
      SecureStore.setItemAsync(SELECTED_VEICULO_KEY, id);
    } else {
      SecureStore.deleteItemAsync(SELECTED_VEICULO_KEY);
    }
  }

  const contextValue = useMemo(
    () => ({ selectedVeiculoId, isLoading, setSelectedVeiculoId }),
    [selectedVeiculoId, isLoading],
  );

  return (
    <SelectedVeiculoContext.Provider value={contextValue}>
      {children}
    </SelectedVeiculoContext.Provider>
  );
}
