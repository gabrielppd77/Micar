import { api } from "@/libs/api";
import type { AuthenticationResponse } from "../../../common/api/types/AuthenticationResponse";
import type { LoginUsuarioRequest } from "../types/LoginUsuarioRequest";

export async function loginUsuario(data: LoginUsuarioRequest) {
  const response = await api.post<AuthenticationResponse>(
    "/Usuarios/login",
    data,
  );
  return response.data;
}
