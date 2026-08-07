import { createContext } from "react";

export interface SelectedVeiculoContextState {
  selectedVeiculoId: string | null;
  isLoading: boolean;
  setSelectedVeiculoId: (id: string | null) => void;
}

export const SelectedVeiculoContext =
  createContext<SelectedVeiculoContextState>({
    selectedVeiculoId: null,
    isLoading: true,
    setSelectedVeiculoId: () => undefined,
  });
