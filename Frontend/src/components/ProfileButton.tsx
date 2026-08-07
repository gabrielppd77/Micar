import { Pressable, Text } from "react-native";

import { useAppGoTo } from "@/hooks/useAppGoTo";

export function ProfileButton() {
  const { goToPerfil } = useAppGoTo();

  return (
    <Pressable
      onPress={goToPerfil}
      className="h-10 w-10 items-center justify-center rounded-full bg-brand-100"
    >
      <Text className="text-lg">👤</Text>
    </Pressable>
  );
}
