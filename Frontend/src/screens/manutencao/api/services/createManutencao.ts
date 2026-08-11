import { api } from "@/libs/api";
import type { CreateManutencaoRequest } from "../types/CreateManutencaoRequest";

export async function createManutencao(data: CreateManutencaoRequest) {
  await api.post("/Manutencoes", data);
}
