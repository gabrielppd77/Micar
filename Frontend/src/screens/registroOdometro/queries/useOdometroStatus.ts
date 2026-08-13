import { useQuery } from "@tanstack/react-query";

import { getOdometroStatus } from "../api/services/getOdometroStatus";
import { registrosOdometroKeys } from "./keys";

export function useOdometroStatus(veiculoId?: string) {
  return useQuery({
    queryKey: registrosOdometroKeys.status(veiculoId ?? ""),
    queryFn: () => getOdometroStatus(veiculoId as string),
    enabled: !!veiculoId,
  });
}
