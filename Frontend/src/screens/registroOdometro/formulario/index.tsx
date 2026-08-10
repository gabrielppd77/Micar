import { zodResolver } from "@hookform/resolvers/zod";
import { useRoute, type RouteProp } from "@react-navigation/native";
import { useMemo } from "react";
import { Controller, useForm } from "react-hook-form";
import {
  KeyboardAvoidingView,
  Platform,
  ScrollView,
  Text,
  View,
} from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { z } from "zod";

import { Button } from "@/components/Button";
import { TextInput } from "@/components/TextInput";
import { useAppGoTo } from "@/hooks/useAppGoTo";
import type { AppStackParamList } from "@/navigation/types";
import { useVeiculo } from "@/screens/veiculo/queries/useVeiculo";
import { useCreateRegistroOdometro } from "../mutations/useCreateRegistroOdometro";

function createSchema(odometroAnterior: number | null | undefined) {
  return z.object({
    odometro: z
      .string()
      .trim()
      .min(1, "Informe o odômetro.")
      .refine((value) => !Number.isNaN(Number(value)), {
        message: "Informe um número válido.",
      })
      .refine((value) => Number(value) > (odometroAnterior ?? -1), {
        message:
          odometroAnterior != null
            ? `Odômetro deve ser maior que ${odometroAnterior} km.`
            : "Odômetro deve ser maior que zero.",
      }),
  });
}

type RegistroOdometroFormValues = z.infer<ReturnType<typeof createSchema>>;

type RegistroOdometroFormRouteProp = RouteProp<
  AppStackParamList,
  "RegistroOdometroForm"
>;

export function RegistroOdometroFormScreen() {
  const { goToHome } = useAppGoTo();
  const route = useRoute<RegistroOdometroFormRouteProp>();
  const { veiculoId } = route.params;

  const { data: veiculo } = useVeiculo(veiculoId);
  const { mutateAsync: createRegistroOdometro, isPending } =
    useCreateRegistroOdometro();

  const schema = useMemo(
    () => createSchema(veiculo?.odometroAtual),
    [veiculo?.odometroAtual],
  );

  const { control, handleSubmit } = useForm<RegistroOdometroFormValues>({
    resolver: zodResolver(schema),
    defaultValues: { odometro: "" },
  });

  async function onSubmit(values: RegistroOdometroFormValues) {
    await createRegistroOdometro({
      veiculoId,
      odometro: Number(values.odometro),
    });

    goToHome();
  }

  return (
    <SafeAreaView className="flex-1 bg-brand-50">
      <KeyboardAvoidingView
        behavior={Platform.OS === "ios" ? "padding" : undefined}
        className="flex-1"
      >
        <ScrollView
          contentContainerClassName="flex-1 justify-center px-6"
          keyboardShouldPersistTaps="handled"
        >
          <View className="mb-6 items-center">
            <Text className="text-3xl font-bold text-brand-900">
              Registrar odômetro
            </Text>
            {veiculo && (
              <Text className="mt-1 text-base text-brand-500">
                {veiculo.apelido} · {veiculo.placa}
              </Text>
            )}
          </View>

          {veiculo?.odometroAtual != null && (
            <View className="mb-4 rounded-2xl bg-white p-4 shadow-sm">
              <Text className="text-sm text-brand-500">
                Último odômetro registrado
              </Text>
              <Text className="text-lg font-semibold text-brand-900">
                {veiculo.odometroAtual} km
              </Text>
            </View>
          )}

          <View className="rounded-2xl bg-white p-6 shadow-sm">
            <Controller
              control={control}
              name="odometro"
              render={({ field, fieldState }) => (
                <TextInput
                  label="Odômetro (km)"
                  value={field.value}
                  onChangeText={field.onChange}
                  onBlur={field.onBlur}
                  error={fieldState.error?.message}
                  keyboardType="numeric"
                  placeholder="Ex: 12345"
                  autoFocus
                  returnKeyType="done"
                  onSubmitEditing={handleSubmit(onSubmit)}
                />
              )}
            />

            <Button
              label="Salvar registro"
              onPress={handleSubmit(onSubmit)}
              loading={isPending}
            />
          </View>

          <Button label="Cancelar" onPress={goToHome} variant="ghost" />
        </ScrollView>
      </KeyboardAvoidingView>
    </SafeAreaView>
  );
}
