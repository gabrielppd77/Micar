import { useContext } from "react";

import {
  SelectedVeiculoContext,
  type SelectedVeiculoContextState,
} from "@/contexts/SelectedVeiculoContext";

export function useSelectedVeiculo() {
  return useContext<SelectedVeiculoContextState>(SelectedVeiculoContext);
}
