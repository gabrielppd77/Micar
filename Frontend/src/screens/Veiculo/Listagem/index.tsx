import { StatusBar } from "expo-status-bar";
import { ActivityIndicator, FlatList, Text, View } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";

import { Button } from "../../../components/Button";
import { useAuth } from "../../../hooks/useAuth";
import { useVeiculos } from "./queries/useVeiculos";

export function VeiculoListScreen() {
  const { setToken } = useAuth();
  const { data: veiculos, isLoading } = useVeiculos();

  return (
    <SafeAreaView className="flex-1 bg-brand-50 px-6 pt-6">
      <StatusBar style="auto" />
      <Text className="mb-2 text-3xl font-bold text-brand-900">MICAR</Text>
      <Text className="mb-6 text-base text-brand-500">Seus veículos</Text>

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
            <View className="rounded-xl bg-white p-4 shadow-sm">
              <Text className="text-lg font-semibold text-brand-900">
                {item.apelido}
              </Text>
              <Text className="text-sm text-brand-500">
                {item.placa} · {item.tipoVeiculo}
              </Text>
            </View>
          )}
        />
      )}

      <View className="w-full py-4">
        <Button
          label="Sair"
          onPress={() => setToken(null)}
          variant="ghost"
        />
      </View>
    </SafeAreaView>
  );
}
