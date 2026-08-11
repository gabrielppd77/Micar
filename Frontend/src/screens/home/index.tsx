import { StatusBar } from "expo-status-bar";
import { useEffect } from "react";
import { ActivityIndicator, Text, View } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";

import { Button } from "@/components/Button";
import { useAppGoTo } from "@/hooks/useAppGoTo";
import { useSelectedVeiculo } from "@/hooks/useSelectedVeiculo";
import { useVeiculo } from "@/screens/veiculo/queries/useVeiculo";

export function HomeScreen() {
  const { goToRegistroOdometroForm, goToManutencaoForm, goToVeiculoList } =
    useAppGoTo();
  const { selectedVeiculoId } = useSelectedVeiculo();

  useEffect(() => {
    if (!selectedVeiculoId) {
      goToVeiculoList();
    }
  }, [selectedVeiculoId]);

  const { data: veiculo, isLoading } = useVeiculo(
    selectedVeiculoId ?? undefined,
  );

  if (!selectedVeiculoId) {
    return (
      <SafeAreaView className="flex-1 items-center justify-center bg-brand-50">
        <ActivityIndicator color="#2E6E8E" />
      </SafeAreaView>
    );
  }

  return (
    <SafeAreaView
      edges={["top", "left", "right"]}
      className="flex-1 bg-brand-50 px-6 pt-6"
    >
      <StatusBar style="auto" />

      <View className="mb-6">
        <Text className="text-3xl font-bold text-brand-900">MICAR</Text>
        {isLoading ? (
          <ActivityIndicator className="mt-1 self-start" />
        ) : (
          <Text className="text-base text-brand-500">
            {veiculo?.apelido} · {veiculo?.placa}
          </Text>
        )}
      </View>

      <View className="flex-1 items-center justify-center">
        <Text className="text-center text-brand-500">
          Gráficos e manutenções futuras aparecerão aqui em breve.
        </Text>
      </View>

      <View className="w-full gap-3 py-4">
        <Button
          label="Registrar manutenção"
          onPress={() => goToManutencaoForm(selectedVeiculoId)}
        />
        <Button
          label="Registrar odômetro"
          onPress={() => goToRegistroOdometroForm(selectedVeiculoId)}
        />
      </View>
    </SafeAreaView>
  );
}
