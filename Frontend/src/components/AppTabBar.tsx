import clsx from "clsx";
import { Pressable, Text, View } from "react-native";
import { useSafeAreaInsets } from "react-native-safe-area-context";

import { useAppGoTo } from "@/hooks/useAppGoTo";
import { useSelectedVeiculo } from "@/hooks/useSelectedVeiculo";

export function AppTabBar() {
  const insets = useSafeAreaInsets();
  const { goToVeiculoList, goToManutencaoList, goToPerfil } = useAppGoTo();
  const { selectedVeiculoId } = useSelectedVeiculo();

  return (
    <View
      className="flex-row items-center justify-center gap-8 border-t border-brand-200 bg-white pt-3"
      style={{ paddingBottom: insets.bottom + 12 }}
    >
      <Pressable
        onPress={goToVeiculoList}
        className="h-10 w-10 items-center justify-center rounded-full bg-brand-100"
      >
        <Text className="text-lg">🚗</Text>
      </Pressable>

      <Pressable
        onPress={() =>
          selectedVeiculoId && goToManutencaoList(selectedVeiculoId)
        }
        disabled={!selectedVeiculoId}
        className={clsx(
          "h-10 w-10 items-center justify-center rounded-full bg-brand-100",
          { "opacity-40": !selectedVeiculoId },
        )}
      >
        <Text className="text-lg">🔧</Text>
      </Pressable>

      <Pressable
        onPress={goToPerfil}
        className="h-10 w-10 items-center justify-center rounded-full bg-brand-100"
      >
        <Text className="text-lg">👤</Text>
      </Pressable>
    </View>
  );
}
