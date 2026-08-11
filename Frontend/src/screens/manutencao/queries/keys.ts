export const manutencoesKeys = {
  all: ["manutencoes"] as const,
  lists: () => [...manutencoesKeys.all, "list"] as const,
  list: (veiculoId: string) => [...manutencoesKeys.lists(), veiculoId] as const,
  details: () => [...manutencoesKeys.all, "detail"] as const,
  detail: (id: string) => [...manutencoesKeys.details(), id] as const,
};
