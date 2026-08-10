import { api } from "@/libs/api";
import type { CreateRegistroOdometroRequest } from "../types/CreateRegistroOdometroRequest";

export async function createRegistroOdometro(
  data: CreateRegistroOdometroRequest,
) {
  await api.post("/RegistrosOdometro", data);
}
