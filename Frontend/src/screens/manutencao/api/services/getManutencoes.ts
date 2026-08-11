import { api } from "@/libs/api";
import type { ManutencaoResponse } from "../types/ManutencaoResponse";

export async function getManutencoes(veiculoId: string) {
  const response = await api.get<ManutencaoResponse[]>(
    `/Manutencoes/veiculo/${veiculoId}`,
  );
  return response.data;
}
