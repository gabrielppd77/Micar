import { api } from "@/libs/api";
import type { AuthenticationResponse } from "../../../common/api/types/AuthenticationResponse";
import type { CreateUsuarioRequest } from "../types/CreateUsuarioRequest";

export async function createUsuario(data: CreateUsuarioRequest) {
  const response = await api.post<AuthenticationResponse>(
    "/Usuarios",
    data,
  );
  return response.data;
}
