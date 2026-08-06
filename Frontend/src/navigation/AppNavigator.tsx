import { createNativeStackNavigator } from "@react-navigation/native-stack";

import { VeiculoListScreen } from "@/screens/veiculo/listagem";
import type { AppStackParamList } from "./types";

const Stack = createNativeStackNavigator<AppStackParamList>();

export function AppNavigator() {
  return (
    <Stack.Navigator screenOptions={{ headerShown: false }}>
      <Stack.Screen name="Veiculo" component={VeiculoListScreen} />
    </Stack.Navigator>
  );
}
