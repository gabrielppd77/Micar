import { useMutation } from "@tanstack/react-query";

import { createUsuario } from "../api/services/createUsuario";

export function useCreateUsuario() {
  return useMutation({
    mutationFn: createUsuario,
  });
}
