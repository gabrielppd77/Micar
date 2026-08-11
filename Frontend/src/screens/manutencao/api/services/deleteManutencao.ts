import { api } from "@/libs/api";

export async function deleteManutencao(id: string) {
  await api.delete(`/Manutencoes/${id}`);
}
