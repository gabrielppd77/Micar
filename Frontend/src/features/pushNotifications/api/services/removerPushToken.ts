import { api } from "@/libs/api";

export async function removerPushToken(token: string) {
  await api.delete("/PushTokens", { params: { token } });
}
