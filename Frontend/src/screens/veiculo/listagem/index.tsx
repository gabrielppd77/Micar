import { StatusBar } from "expo-status-bar";
import {
  ActivityIndicator,
  Alert,
  FlatList,
  Pressable,
  Text,
  View,
} from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";

import { Button } from "@/components/Button";
import { ProfileButton } from "@/components/ProfileButton";
import { useAppGoTo } from "@/hooks/useAppGoTo";
import { useSelectedVeiculo } from "@/hooks/useSelectedVeiculo";
import type { VeiculoResponse } from "../common/api/types/VeiculoResponse";
import { useDeleteVeiculo } from "../common/mutations/useDeleteVeiculo";
import { useVeiculos } from "../common/queries/useVeiculos";

export function VeiculoListScreen() {
  const { goToVeiculoForm, goToHome } = useAppGoTo();
  const { data: veiculos, isLoading } = useVeiculos();
  const { setSelectedVeiculoId } = useSelectedVeiculo();
  const { mutate: deleteVeiculo } = useDeleteVeiculo();

  function handleSelect(veiculo: VeiculoResponse) {
    setSelectedVeiculoId(veiculo.id);
    goToHome();
  }

  function handleDelete(veiculo: VeiculoResponse) {
    Alert.alert(
      "Excluir veículo",
      `Tem certeza que deseja excluir "${veiculo.apelido}"?`,
      [
        { text: "Cancelar", style: "cancel" },
        {
          text: "Excluir",
          style: "destructive",
          onPress: () => deleteVeiculo(veiculo.id),
        },
      ],
    );
  }

  return (
    <SafeAreaView className="flex-1 bg-brand-50 px-6 pt-6">
      <StatusBar style="auto" />

      <View className="mb-6 flex-row items-center justify-between">
        <View>
          <Text className="text-3xl font-bold text-brand-900">MICAR</Text>
          <Text className="text-base text-brand-500">Seus veículos</Text>
        </View>
        <ProfileButton />
      </View>

      {isLoading ? (
        <ActivityIndicator className="mt-8" />
      ) : (
        <FlatList
          data={veiculos}
          keyExtractor={(veiculo) => veiculo.id}
          ItemSeparatorComponent={() => <View className="h-3" />}
          ListEmptyComponent={
            <Text className="text-center text-brand-500">
              Nenhum veículo cadastrado.
            </Text>
          }
          renderItem={({ item }) => (
            <Pressable
              onPress={() => handleSelect(item)}
              className="rounded-xl bg-white p-4 shadow-sm"
            >
              <Text className="text-lg font-semibold text-brand-900">
                {item.apelido}
              </Text>
              <Text className="text-sm text-brand-500">
                {item.placa} · {item.tipoVeiculo}
              </Text>

              <View className="mt-3 flex-row gap-4">
                <Pressable onPress={() => goToVeiculoForm(item.id)}>
                  <Text className="text-sm font-semibold text-brand-500">
                    Editar
                  </Text>
                </Pressable>
                <Pressable onPress={() => handleDelete(item)}>
                  <Text className="text-sm font-semibold text-red-600">
                    Excluir
                  </Text>
                </Pressable>
              </View>
            </Pressable>
          )}
        />
      )}

      <View className="w-full py-4">
        <Button label="Novo veículo" onPress={() => goToVeiculoForm()} />
      </View>
    </SafeAreaView>
  );
}
