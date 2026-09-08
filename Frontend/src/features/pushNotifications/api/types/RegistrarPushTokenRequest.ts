export type PlataformaPush = "Ios" | "Android";

export interface RegistrarPushTokenRequest {
  token: string;
  plataforma: PlataformaPush;
}
