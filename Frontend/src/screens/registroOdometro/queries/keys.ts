export const registrosOdometroKeys = {
  all: ["registrosOdometro"] as const,
  status: (veiculoId: string) =>
    [...registrosOdometroKeys.all, "status", veiculoId] as const,
};
