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
    icon: "speedometer-outline",
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
  diasSemAtualizacao: number | null,
) {
  if (diasSemAtualizacao === null) {
    return "Nenhum registro de odômetro ainda";
  }

  if (diasSemAtualizacao === 0) {
    return "Odômetro atualizado hoje";
  }

  const dias = `${diasSemAtualizacao} dia${diasSemAtualizacao === 1 ? "" : "s"}`;

  if (status === NivelAlertaEnum.Critico) {
    return `Odômetro sem atualizar há ${dias}`;
  }

  return `Odômetro atualizado há ${dias}`;
}

type OdometroStatusAlertProps = {
  status: NivelAlertaEnum;
  diasSemAtualizacao: number | null;
};

export function OdometroStatusAlert({
  status,
  diasSemAtualizacao,
}: OdometroStatusAlertProps) {
  const config = STATUS_CONFIG[status];

  return (
    <View className={`overflow-hidden rounded-xl ${config.bg}`}>
      <View className={`h-1.5 ${config.bar}`} />
      <View className="flex-row items-center gap-3 px-4 py-3">
        <Ionicons name={config.icon} size={22} color={config.iconColor} />
        <Text className={`flex-1 text-base font-semibold ${config.text}`}>
          {buildMessage(status, diasSemAtualizacao)}
        </Text>
      </View>
    </View>
  );
}
