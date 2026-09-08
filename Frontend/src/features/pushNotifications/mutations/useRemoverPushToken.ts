import { useMutation } from "@tanstack/react-query";

import { removerPushToken } from "../api/services/removerPushToken";

export function useRemoverPushToken() {
  return useMutation({
    mutationFn: removerPushToken,
  });
}
