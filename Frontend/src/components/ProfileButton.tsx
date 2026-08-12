import { Ionicons } from "@expo/vector-icons";
import { Pressable } from "react-native";

import { useAppGoTo } from "@/hooks/useAppGoTo";

export function ProfileButton() {
  const { goToPerfil } = useAppGoTo();

  return (
    <Pressable
      onPress={goToPerfil}
      className="h-10 w-10 items-center justify-center rounded-full bg-brand-100"
    >
      <Ionicons name="person-outline" size={20} color="#235777" />
    </Pressable>
  );
}
