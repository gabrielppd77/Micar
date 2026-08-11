import { api } from "@/libs/api";
import type { ManutencaoResponse } from "../types/ManutencaoResponse";

export async function getManutencaoById(id: string) {
  const response = await api.get<ManutencaoResponse>(`/Manutencoes/${id}`);
  return response.data;
}
