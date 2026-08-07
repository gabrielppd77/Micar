export const veiculosKeys = {
  all: ["veiculos"] as const,
  lists: () => [...veiculosKeys.all, "list"] as const,
  details: () => [...veiculosKeys.all, "detail"] as const,
  detail: (id: string) => [...veiculosKeys.details(), id] as const,
};
