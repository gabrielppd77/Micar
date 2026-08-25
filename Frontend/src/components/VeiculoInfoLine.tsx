import { ActivityIndicator, Text } from "react-native";

import type { VeiculoResponse } from "@/screens/veiculo/api/types/VeiculoResponse";

type VeiculoInfoLineProps = {
  veiculo: VeiculoResponse | undefined;
  isLoading: boolean;
};

export function VeiculoInfoLine({ veiculo, isLoading }: VeiculoInfoLineProps) {
  if (isLoading) {
    return <ActivityIndicator className="mt-1 self-start" />;
  }

  return (
    <Text className="text-base text-brand-500">
      {veiculo?.apelido} · {veiculo?.placa} · {veiculo?.odometroAtual} km
    </Text>
  );
}
