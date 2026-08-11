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

  function goToRegistroOdometroForm(veiculoId: string) {
    navigation.navigate("RegistroOdometroForm", { veiculoId });
  }

  function goToManutencaoList(veiculoId: string) {
    navigation.navigate("ManutencaoList", { veiculoId });
  }

  function goToManutencaoForm(veiculoId: string, id?: string) {
    navigation.navigate("ManutencaoForm", { veiculoId, id });
  }

  return {
    goToVeiculoList,
    goToVeiculoForm,
    goToHome,
    goToPerfil,
    goToRegistroOdometroForm,
    goToManutencaoList,
    goToManutencaoForm,
  };
}
