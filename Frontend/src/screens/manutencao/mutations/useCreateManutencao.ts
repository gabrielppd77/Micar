import { useMutation, useQueryClient } from "@tanstack/react-query";

import { createManutencao } from "../api/services/createManutencao";
import { manutencoesKeys } from "../queries/keys";

export function useCreateManutencao() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createManutencao,
    onSuccess: (_, { veiculoId }) => {
      queryClient.invalidateQueries({
        queryKey: manutencoesKeys.list(veiculoId),
      });
    },
  });
}
