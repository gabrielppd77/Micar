import { api } from "@/libs/api";
import type { CreateVeiculoRequest } from "../types/CreateVeiculoRequest";

export async function createVeiculo(data: CreateVeiculoRequest) {
  await api.post("/Veiculos", data);
}
