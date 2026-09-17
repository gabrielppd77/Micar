import { useQuery } from "@tanstack/react-query";

import { getManutencoes } from "../api/services/getManutencoes";
import { manutencoesKeys } from "./keys";

export function useManutencoes(veiculoId: string, termo?: string) {
  return useQuery({
    queryKey: manutencoesKeys.list(veiculoId, termo),
    queryFn: () => getManutencoes(veiculoId, termo),
    placeholderData: (previousData) => previousData,
  });
}
