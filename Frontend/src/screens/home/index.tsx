import { Ionicons } from "@expo/vector-icons";
import { StatusBar } from "expo-status-bar";
import { useEffect } from "react";
import {
  ActivityIndicator,
  Pressable,
  ScrollView,
  Text,
  View,
} from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";

import { Fab } from "@/components/Fab";
import { ManutencaoPendenciaCard } from "@/components/ManutencaoPendenciaCard";
import { OdometroStatusAlert } from "@/components/OdometroStatusAlert";
import { ProfileButton } from "@/components/ProfileButton";
import { VeiculoStatusBar } from "@/components/VeiculoStatusBar";
import { useAppGoTo } from "@/hooks/useAppGoTo";
import { useSelectedVeiculo } from "@/hooks/useSelectedVeiculo";
import { useManutencaoStatus } from "@/screens/manutencao/queries/useManutencaoStatus";
import { useOdometroStatus } from "@/screens/registroOdometro/queries/useOdometroStatus";
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
  const { data: status, isLoading: isStatusLoading } = useManutencaoStatus(
    selectedVeiculoId ?? undefined,
  );
  const { data: odometroStatus } = useOdometroStatus(
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
              {veiculo?.apelido} · {veiculo?.placa} · {veiculo?.odometroAtual}{" "}
              km
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

      {isStatusLoading ? (
        <View className="flex-1 items-center justify-center">
          <ActivityIndicator color="#2E6E8E" />
        </View>
      ) : (
        <ScrollView
          className="flex-1"
          contentContainerClassName="gap-3 pb-24"
          showsVerticalScrollIndicator={false}
        >
          {odometroStatus && (
            <OdometroStatusAlert
              status={odometroStatus.status}
              diasSemAtualizacao={odometroStatus.diasSemAtualizacao}
            />
          )}

          {status && (
            <VeiculoStatusBar
              status={status.statusGeral}
              quantidadeTotal={status.quantidadeTotal}
              quantidadeEmDia={status.quantidadeEmDia}
              quantidadeProximas={status.quantidadeProximas}
              quantidadeVencidas={status.quantidadeVencidas}
            />
          )}

          {status && status.pendencias.length > 0 ? (
            status.pendencias.map((pendencia) => (
              <ManutencaoPendenciaCard
                key={pendencia.id}
                pendencia={pendencia}
                onPress={() =>
                  goToManutencaoForm(selectedVeiculoId, pendencia.id)
                }
              />
            ))
          ) : (
            <Text className="mt-4 text-center text-brand-500">
              Nenhuma manutenção pendente. Tudo certo por aqui!
            </Text>
          )}
        </ScrollView>
      )}

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
