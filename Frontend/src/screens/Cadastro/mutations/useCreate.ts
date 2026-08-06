import { useMutation } from "@tanstack/react-query";

import { createUsuario } from "../api/services/createUsuario";

export function useCreate() {
  return useMutation({
    mutationFn: createUsuario,
  });
}
