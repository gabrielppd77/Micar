import { useMutation, useQueryClient } from "@tanstack/react-query";

import { createVeiculo } from "../api/services/createVeiculo";
import { veiculosKeys } from "../queries/keys";

export function useCreateVeiculo() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createVeiculo,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: veiculosKeys.lists() });
    },
  });
}
