import { useMutation, useQueryClient } from "@tanstack/react-query";

import { deleteManutencao } from "../api/services/deleteManutencao";
import { manutencoesKeys } from "../queries/keys";

export function useDeleteManutencao() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteManutencao,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: manutencoesKeys.all });
    },
  });
}
