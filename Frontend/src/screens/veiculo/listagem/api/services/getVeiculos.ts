import { api } from "@/libs/api";
import type { Veiculo } from "../types/Veiculo";

export async function getVeiculos() {
  const response = await api.get<Veiculo[]>("/Veiculos");
  return response.data;
}
