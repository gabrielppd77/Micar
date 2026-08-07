import { useQuery } from "@tanstack/react-query";

import { getVeiculoById } from "../api/services/getVeiculoById";
import { veiculosKeys } from "./keys";

export function useVeiculo(id?: string) {
  return useQuery({
    queryKey: veiculosKeys.detail(id ?? ""),
    queryFn: () => getVeiculoById(id as string),
    enabled: !!id,
  });
}
