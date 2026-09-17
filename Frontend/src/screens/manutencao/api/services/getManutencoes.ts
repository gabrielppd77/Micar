import { api } from "@/libs/api";
import type { ManutencaoResponse } from "../types/ManutencaoResponse";

export async function getManutencoes(veiculoId: string, termo?: string) {
  const response = await api.get<ManutencaoResponse[]>(
    `/Manutencoes/veiculo/${veiculoId}`,
    { params: termo ? { termo } : undefined },
  );
  return response.data;
}
