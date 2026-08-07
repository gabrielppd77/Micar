import { useMutation } from "@tanstack/react-query";

import { loginUsuario } from "../api/services/loginUsuario";

export function useLoginUsuario() {
  return useMutation({
    mutationFn: loginUsuario,
  });
}
