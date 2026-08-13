import { useMutation, useQueryClient } from "@tanstack/react-query";

import { veiculosKeys } from "@/screens/veiculo/queries/keys";
import { createRegistroOdometro } from "../api/services/createRegistroOdometro";
import { registrosOdometroKeys } from "../queries/keys";

export function useCreateRegistroOdometro() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createRegistroOdometro,
    onSuccess: (_, { veiculoId }) => {
      queryClient.invalidateQueries({
        queryKey: veiculosKeys.detail(veiculoId),
      });
      queryClient.invalidateQueries({
        queryKey: registrosOdometroKeys.status(veiculoId),
      });
    },
  });
}
