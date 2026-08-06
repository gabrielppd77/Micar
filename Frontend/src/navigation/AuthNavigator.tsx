import { createNativeStackNavigator } from "@react-navigation/native-stack";

import { CadastroScreen } from "../screens/usuario/cadastro";
import { LoginScreen } from "../screens/usuario/login";
import type { AuthStackParamList } from "./types";

const Stack = createNativeStackNavigator<AuthStackParamList>();

export function AuthNavigator() {
  return (
    <Stack.Navigator screenOptions={{ headerShown: false }}>
      <Stack.Screen name="Login" component={LoginScreen} />
      <Stack.Screen name="Cadastro" component={CadastroScreen} />
    </Stack.Navigator>
  );
}
