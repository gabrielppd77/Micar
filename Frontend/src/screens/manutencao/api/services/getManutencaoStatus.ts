import { api } from "@/libs/api";
import type { VeiculoStatusManutencaoResponse } from "../types/VeiculoStatusManutencaoResponse";

export async function getManutencaoStatus(veiculoId: string) {
  const response = await api.get<VeiculoStatusManutencaoResponse>(
    `/Manutencoes/veiculo/${veiculoId}/status`,
  );
  return response.data;
}
