import { Ionicons } from "@expo/vector-icons";
import { StatusBar } from "expo-status-bar";
import { useEffect } from "react";
import { ActivityIndicator, Pressable, Text, View } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";

import { Fab } from "@/components/Fab";
import { ProfileButton } from "@/components/ProfileButton";
import { useAppGoTo } from "@/hooks/useAppGoTo";
import { useSelectedVeiculo } from "@/hooks/useSelectedVeiculo";
import { useVeiculo } from "@/screens/veiculo/queries/useVeiculo";

export function HomeScreen() {
  const {
    goToRegistroOdometroForm,
    goToManutencaoForm,
    goToManutencaoList,
    goToVeiculoList,
  } = useAppGoTo();
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
      <SafeAreaView
        edges={["top", "left", "right"]}
        className="flex-1 items-center justify-center bg-brand-50"
      >
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

      <View className="mb-6 flex-row items-center justify-between">
        <View>
          <Text className="text-3xl font-bold text-brand-900">MICAR</Text>
          {isLoading ? (
            <ActivityIndicator className="mt-1 self-start" />
          ) : (
            <Text className="text-base text-brand-500">
              {veiculo?.apelido} · {veiculo?.placa}
            </Text>
          )}
        </View>
        <View className="flex-row items-center gap-3">
          <Pressable
            onPress={() => goToManutencaoList(selectedVeiculoId)}
            className="h-10 w-10 items-center justify-center rounded-full bg-brand-100"
          >
            <Ionicons name="construct-outline" size={20} color="#235777" />
          </Pressable>
          <ProfileButton />
        </View>
      </View>

      <View className="flex-1 items-center justify-center">
        <Text className="text-center text-brand-500">
          Gráficos e manutenções futuras aparecerão aqui em breve.
        </Text>
      </View>

      <Fab
        actions={[
          {
            key: "manutencao",
            label: "Registrar manutenção",
            icon: "construct-outline",
            onPress: () => goToManutencaoForm(selectedVeiculoId),
          },
          {
            key: "odometro",
            label: "Registrar odômetro",
            icon: "speedometer-outline",
            onPress: () => goToRegistroOdometroForm(selectedVeiculoId),
          },
        ]}
      />
    </SafeAreaView>
  );
}
