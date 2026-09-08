import { useMutation } from "@tanstack/react-query";

import { registrarPushToken } from "../api/services/registrarPushToken";

export function useRegistrarPushToken() {
  return useMutation({
    mutationFn: registrarPushToken,
  });
}
