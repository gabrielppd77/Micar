import { useMutation } from "@tanstack/react-query";

import { loginUsuario } from "../api/services/loginUsuario";

export function useLogin() {
  return useMutation({
    mutationFn: loginUsuario,
  });
}
