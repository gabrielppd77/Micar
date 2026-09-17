export const manutencoesKeys = {
  all: ["manutencoes"] as const,
  lists: () => [...manutencoesKeys.all, "list"] as const,
  list: (veiculoId: string, termo?: string) =>
    [...manutencoesKeys.lists(), veiculoId, termo ?? ""] as const,
  details: () => [...manutencoesKeys.all, "detail"] as const,
  detail: (id: string) => [...manutencoesKeys.details(), id] as const,
  status: (veiculoId: string) =>
    [...manutencoesKeys.all, "status", veiculoId] as const,
};
