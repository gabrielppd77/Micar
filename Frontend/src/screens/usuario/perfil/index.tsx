import { useMemo } from "react";
import { Text, View } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";

import { Button } from "@/components/Button";
import { useAppGoTo } from "@/hooks/useAppGoTo";
import { useAuth } from "@/hooks/useAuth";
import { useSelectedVeiculo } from "@/hooks/useSelectedVeiculo";
import { decodeJwt } from "@/libs/jwt";

export function PerfilScreen() {
  const { goToVeiculoList } = useAppGoTo();
  const { token, setToken } = useAuth();
  const { setSelectedVeiculoId } = useSelectedVeiculo();

  const usuario = useMemo(() => (token ? decodeJwt(token) : null), [token]);

  function handleSair() {
    setToken(null);
    setSelectedVeiculoId(null);
  }

  return (
    <SafeAreaView className="flex-1 bg-brand-50 px-6 pt-6">
      <Text className="mb-6 text-3xl font-bold text-brand-900">Perfil</Text>

      <View className="mb-6 rounded-2xl bg-white p-6 shadow-sm">
        <Text className="text-sm font-medium text-brand-500">Nome</Text>
        <Text className="mb-4 text-base text-brand-900">
          {usuario?.name ?? "-"}
        </Text>

        <Text className="text-sm font-medium text-brand-500">Email</Text>
        <Text className="text-base text-brand-900">
          {usuario?.email ?? "-"}
        </Text>
      </View>

      <Button label="Ver veículos" onPress={goToVeiculoList} />
      <View className="h-3" />
      <Button label="Sair" onPress={handleSair} variant="ghost" />
    </SafeAreaView>
  );
}
