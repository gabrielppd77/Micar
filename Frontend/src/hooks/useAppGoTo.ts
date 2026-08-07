import { useNavigation } from "@react-navigation/native";
import type { NativeStackNavigationProp } from "@react-navigation/native-stack";

import type { AppStackParamList } from "@/navigation/types";

export function useAppGoTo() {
  const navigation =
    useNavigation<NativeStackNavigationProp<AppStackParamList>>();

  function goToVeiculoList() {
    navigation.navigate("VeiculoList");
  }

  function goToVeiculoForm(id?: string) {
    navigation.navigate("VeiculoForm", { id });
  }

  function goToHome() {
    navigation.navigate("Home");
  }

  function goToPerfil() {
    navigation.navigate("Perfil");
  }

  return { goToVeiculoList, goToVeiculoForm, goToHome, goToPerfil };
}
