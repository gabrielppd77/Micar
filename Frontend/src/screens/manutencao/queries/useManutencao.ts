import { useQuery } from "@tanstack/react-query";

import { getManutencaoById } from "../api/services/getManutencaoById";
import { manutencoesKeys } from "./keys";

export function useManutencao(id?: string) {
  return useQuery({
    queryKey: manutencoesKeys.detail(id ?? ""),
    queryFn: () => getManutencaoById(id as string),
    enabled: !!id,
  });
}
