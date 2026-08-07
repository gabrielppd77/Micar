import { api } from "@/libs/api";
import type { VeiculoResponse } from "../types/VeiculoResponse";

export async function getVeiculos() {
  const response = await api.get<VeiculoResponse[]>("/Veiculos");
  return response.data;
}
