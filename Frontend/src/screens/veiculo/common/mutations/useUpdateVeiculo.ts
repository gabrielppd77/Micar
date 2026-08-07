import { useMutation, useQueryClient } from "@tanstack/react-query";

import { updateVeiculo } from "../api/services/updateVeiculo";
import { veiculosKeys } from "../queries/keys";

export function useUpdateVeiculo() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: updateVeiculo,
    onSuccess: (_, { id }) => {
      queryClient.invalidateQueries({ queryKey: veiculosKeys.lists() });
      queryClient.invalidateQueries({ queryKey: veiculosKeys.detail(id) });
    },
  });
}
