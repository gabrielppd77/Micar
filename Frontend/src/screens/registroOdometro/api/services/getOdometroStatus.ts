import { api } from "@/libs/api";
import type { VeiculoStatusOdometroResponse } from "../types/VeiculoStatusOdometroResponse";

export async function getOdometroStatus(veiculoId: string) {
  const response = await api.get<VeiculoStatusOdometroResponse>(
    `/RegistrosOdometro/veiculo/${veiculoId}/status`,
  );
  return response.data;
}
