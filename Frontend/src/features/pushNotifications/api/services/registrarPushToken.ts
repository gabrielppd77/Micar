import { api } from "@/libs/api";
import type { RegistrarPushTokenRequest } from "../types/RegistrarPushTokenRequest";

export async function registrarPushToken(data: RegistrarPushTokenRequest) {
  await api.post("/PushTokens", data);
}
