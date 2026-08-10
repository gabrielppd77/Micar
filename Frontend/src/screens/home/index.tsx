import { StatusBar } from "expo-status-bar";
import { ActivityIndicator, Text, View } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";

import { Button } from "@/components/Button";
import { ProfileButton } from "@/components/ProfileButton";
import { useAppGoTo } from "@/hooks/useAppGoTo";
import { useSelectedVeiculo } from "@/hooks/useSelectedVeiculo";
import { useVeiculo } from "@/screens/veiculo/queries/useVeiculo";

export function HomeScreen() {
  const { goToRegistroOdometroForm } = useAppGoTo();
  const { selectedVeiculoId } = useSelectedVeiculo();
  const { data: veiculo, isLoading } = useVeiculo(
    selectedVeiculoId ?? undefined,
  );

  return (
    <SafeAreaView className="flex-1 bg-brand-50 px-6 pt-6">
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
        <ProfileButton />
      </View>

      <View className="flex-1 items-center justify-center">
        <Text className="text-center text-brand-500">
          Gráficos e manutenções futuras aparecerão aqui em breve.
        </Text>
      </View>

      {selectedVeiculoId && (
        <View className="w-full py-4">
          <Button
            label="Registrar odômetro"
            onPress={() => goToRegistroOdometroForm(selectedVeiculoId)}
          />
        </View>
      )}
    </SafeAreaView>
  );
}
