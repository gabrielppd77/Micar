import { useMutation, useQueryClient } from "@tanstack/react-query";

import { deleteVeiculo } from "../api/services/deleteVeiculo";
import { veiculosKeys } from "../queries/keys";

export function useDeleteVeiculo() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteVeiculo,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: veiculosKeys.lists() });
    },
  });
}
