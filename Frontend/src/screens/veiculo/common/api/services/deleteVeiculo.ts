import { api } from "@/libs/api";

export async function deleteVeiculo(id: string) {
  await api.delete(`/Veiculos/${id}`);
}
