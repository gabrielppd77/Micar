import { StatusBar } from "expo-status-bar";
import { Text } from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";

export default function HomeScreen() {
  return (
    <SafeAreaView className="flex-1 items-center justify-center bg-white px-6">
      <StatusBar style="auto" />
      <Text className="mb-2 text-3xl font-bold">MICAR</Text>
      <Text className="text-center text-base text-gray-600">
        Manutenções do seu veículo, sem complicação
      </Text>
    </SafeAreaView>
  );
}
