import { useQuery } from "@tanstack/react-query";

import { getManutencaoStatus } from "../api/services/getManutencaoStatus";
import { manutencoesKeys } from "./keys";

export function useManutencaoStatus(veiculoId?: string) {
  return useQuery({
    queryKey: manutencoesKeys.status(veiculoId ?? ""),
    queryFn: () => getManutencaoStatus(veiculoId as string),
    enabled: !!veiculoId,
  });
}
