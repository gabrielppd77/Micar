import { useQuery } from "@tanstack/react-query";

import { getVeiculos } from "../api/services/getVeiculos";
import { veiculosKeys } from "./keys";

export function useVeiculos() {
  return useQuery({
    queryKey: veiculosKeys.lists(),
    queryFn: getVeiculos,
  });
}
