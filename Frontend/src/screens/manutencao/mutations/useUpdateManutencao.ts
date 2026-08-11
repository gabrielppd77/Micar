import { useMutation, useQueryClient } from "@tanstack/react-query";

import { updateManutencao } from "../api/services/updateManutencao";
import { manutencoesKeys } from "../queries/keys";

export function useUpdateManutencao() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: updateManutencao,
    onSuccess: (_, { id }) => {
      queryClient.invalidateQueries({ queryKey: manutencoesKeys.lists() });
      queryClient.invalidateQueries({ queryKey: manutencoesKeys.detail(id) });
    },
  });
}
