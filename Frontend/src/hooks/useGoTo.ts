import { useNavigation } from "@react-navigation/native";
import type { NativeStackNavigationProp } from "@react-navigation/native-stack";

import type { AuthStackParamList } from "../navigation/types";

export function useGoTo() {
  const navigation =
    useNavigation<NativeStackNavigationProp<AuthStackParamList>>();

  function goToLogin() {
    navigation.navigate("Login");
  }

  function goToCadastro() {
    navigation.navigate("Cadastro");
  }

  return { goToLogin, goToCadastro };
}
