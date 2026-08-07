import { api } from "@/libs/api";
import type { UpdateVeiculoRequest } from "../types/UpdateVeiculoRequest";

interface UpdateVeiculoParams {
  id: string;
  data: UpdateVeiculoRequest;
}

export async function updateVeiculo({ id, data }: UpdateVeiculoParams) {
  await api.put(`/Veiculos/${id}`, data);
}
