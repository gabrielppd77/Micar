import { Pressable, Text, View } from "react-native";

import { StatusManutencaoBadge } from "@/components/StatusManutencaoBadge";
import { toDateInput } from "@/libs/date";
import type { ManutencaoPendenciaResponse } from "@/screens/manutencao/api/types/ManutencaoPendenciaResponse";

function formatDataParte(pendencia: ManutencaoPendenciaResponse) {
  if (!pendencia.dataVencimento || pendencia.diasRestantes === null)
    return null;

  const dataFormatada = toDateInput(pendencia.dataVencimento);

  if (pendencia.diasRestantes < 0) {
    return `Venceu há ${Math.abs(pendencia.diasRestantes)} dia(s) (${dataFormatada})`;
  }

  if (pendencia.diasRestantes === 0) {
    return `Vence hoje (${dataFormatada})`;
  }

  return `Vence em ${pendencia.diasRestantes} dia(s) (${dataFormatada})`;
}

function formatOdometroParte(pendencia: ManutencaoPendenciaResponse) {
  if (pendencia.odometroVencimento === null || pendencia.kmRestantes === null)
    return null;

  if (pendencia.kmRestantes <= 0) {
    return `${Math.abs(pendencia.kmRestantes)} km além do limite (${pendencia.odometroVencimento} km)`;
  }

  return `Faltam ${pendencia.kmRestantes} km (limite: ${pendencia.odometroVencimento} km)`;
}

function buildDetalhe(pendencia: ManutencaoPendenciaResponse) {
  return [formatDataParte(pendencia), formatOdometroParte(pendencia)]
    .filter((parte): parte is string => parte !== null)
    .join(" · ");
}

type ManutencaoPendenciaCardProps = {
  pendencia: ManutencaoPendenciaResponse;
  onPress: () => void;
};

export function ManutencaoPendenciaCard({
  pendencia,
  onPress,
}: ManutencaoPendenciaCardProps) {
  return (
    <Pressable
      onPress={onPress}
      className="rounded-xl bg-white p-4 shadow-sm"
    >
      <View className="flex-row items-start justify-between gap-3">
        <View className="flex-1">
          <Text className="text-base font-semibold text-brand-900">
            {pendencia.nome}
          </Text>
          <Text className="mt-1 text-sm text-brand-500">
            {buildDetalhe(pendencia)}
          </Text>
        </View>
        <StatusManutencaoBadge status={pendencia.status} />
      </View>
    </Pressable>
  );
}
