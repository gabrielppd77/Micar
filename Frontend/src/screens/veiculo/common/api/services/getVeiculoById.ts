import { api } from "@/libs/api";
import type { VeiculoResponse } from "../types/VeiculoResponse";

export async function getVeiculoById(id: string) {
  const response = await api.get<VeiculoResponse>(`/Veiculos/${id}`);
  return response.data;
}
