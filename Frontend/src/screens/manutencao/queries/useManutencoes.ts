import { useQuery } from "@tanstack/react-query";

import { getManutencoes } from "../api/services/getManutencoes";
import { manutencoesKeys } from "./keys";

export function useManutencoes(veiculoId: string) {
  return useQuery({
    queryKey: manutencoesKeys.list(veiculoId),
    queryFn: () => getManutencoes(veiculoId),
  });
}
