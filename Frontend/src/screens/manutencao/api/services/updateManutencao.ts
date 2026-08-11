import { api } from "@/libs/api";
import type { UpdateManutencaoRequest } from "../types/UpdateManutencaoRequest";

interface UpdateManutencaoParams {
  id: string;
  data: UpdateManutencaoRequest;
}

export async function updateManutencao({ id, data }: UpdateManutencaoParams) {
  await api.put(`/Manutencoes/${id}`, data);
}
