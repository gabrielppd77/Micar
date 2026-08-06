export const veiculosKeys = {
  all: ["veiculos"] as const,
  lists: () => [...veiculosKeys.all, "list"] as const,
};
