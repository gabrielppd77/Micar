import { Text, View } from "react-native";

import { NivelAlertaEnum } from "@/screens/common/api/types/NivelAlertaEnum";

const STATUS_STYLES: Record<
  NivelAlertaEnum,
  { bg: string; text: string; label: string }
> = {
  [NivelAlertaEnum.Normal]: {
    bg: "bg-success-100",
    text: "text-success-600",
    label: "Em dia",
  },
  [NivelAlertaEnum.Atencao]: {
    bg: "bg-warning-100",
    text: "text-warning-600",
    label: "Próxima",
  },
  [NivelAlertaEnum.Critico]: {
    bg: "bg-danger-100",
    text: "text-danger-600",
    label: "Vencida",
  },
};

type StatusManutencaoBadgeProps = {
  status: NivelAlertaEnum;
};

export function StatusManutencaoBadge({ status }: StatusManutencaoBadgeProps) {
  const { bg, text, label } = STATUS_STYLES[status];

  return (
    <View className={`rounded-full px-3 py-1 ${bg}`}>
      <Text className={`text-xs font-semibold ${text}`}>{label}</Text>
    </View>
  );
}
