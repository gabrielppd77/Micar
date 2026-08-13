import { Ionicons } from "@expo/vector-icons";
import { Text, View } from "react-native";

import { NivelAlertaEnum } from "@/screens/common/api/types/NivelAlertaEnum";

const STATUS_CONFIG: Record<
  NivelAlertaEnum,
  {
    bar: string;
    bg: string;
    text: string;
    icon: keyof typeof Ionicons.glyphMap;
    iconColor: string;
  }
> = {
  [NivelAlertaEnum.Normal]: {
    bar: "bg-success-500",
    bg: "bg-success-50",
    text: "text-success-600",
    icon: "checkmark-circle",
    iconColor: "#158049",
  },
  [NivelAlertaEnum.Atencao]: {
    bar: "bg-warning-500",
    bg: "bg-warning-50",
    text: "text-warning-600",
    icon: "alert-circle",
    iconColor: "#D6870F",
  },
  [NivelAlertaEnum.Critico]: {
    bar: "bg-danger-500",
    bg: "bg-danger-50",
    text: "text-danger-600",
    icon: "warning",
    iconColor: "#B91C1C",
  },
};

function buildMessage(
  status: NivelAlertaEnum,
  quantidadeVencidas: number,
  quantidadeProximas: number,
) {
  if (status === NivelAlertaEnum.Critico) {
    return quantidadeVencidas === 1
      ? "1 manutenção vencida"
      : `${quantidadeVencidas} manutenções vencidas`;
  }

  if (status === NivelAlertaEnum.Atencao) {
    return quantidadeProximas === 1
      ? "1 manutenção próxima do vencimento"
      : `${quantidadeProximas} manutenções próximas do vencimento`;
  }

  return "Tudo em dia";
}

type VeiculoStatusBarProps = {
  status: NivelAlertaEnum;
  quantidadeTotal: number;
  quantidadeEmDia: number;
  quantidadeProximas: number;
  quantidadeVencidas: number;
};

export function VeiculoStatusBar({
  status,
  quantidadeTotal,
  quantidadeEmDia,
  quantidadeProximas,
  quantidadeVencidas,
}: VeiculoStatusBarProps) {
  const config = STATUS_CONFIG[status];

  const emDiaPercent =
    quantidadeTotal > 0 ? (quantidadeEmDia / quantidadeTotal) * 100 : 100;
  const proximasPercent =
    quantidadeTotal > 0 ? (quantidadeProximas / quantidadeTotal) * 100 : 0;
  const vencidasPercent =
    quantidadeTotal > 0 ? (quantidadeVencidas / quantidadeTotal) * 100 : 0;

  return (
    <View className={`gap-3 overflow-hidden rounded-xl ${config.bg} p-4`}>
      <View className="flex-row items-center gap-3">
        <Ionicons name={config.icon} size={22} color={config.iconColor} />
        <Text className={`flex-1 text-base font-semibold ${config.text}`}>
          {buildMessage(status, quantidadeVencidas, quantidadeProximas)}
        </Text>
      </View>

      <View className="h-3 w-full flex-row overflow-hidden rounded-full bg-success-100">
        {emDiaPercent > 0 && (
          <View
            style={{ width: `${emDiaPercent}%` }}
            className="h-full bg-success-500"
          />
        )}
        {proximasPercent > 0 && (
          <View
            style={{ width: `${proximasPercent}%` }}
            className="h-full bg-warning-500"
          />
        )}
        {vencidasPercent > 0 && (
          <View
            style={{ width: `${vencidasPercent}%` }}
            className="h-full bg-danger-500"
          />
        )}
      </View>
    </View>
  );
}
