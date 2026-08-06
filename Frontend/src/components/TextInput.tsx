import { forwardRef, useState } from "react";
import {
  Pressable,
  Text,
  TextInput as TextInputNative,
  View,
  type TextInputProps as TextInputPropsNative,
} from "react-native";

type TextInputProps = TextInputPropsNative & {
  label: string;
  error?: string;
  isPassword?: boolean;
};

export const TextInput = forwardRef<TextInputNative, TextInputProps>(
  function TextInput(
    { label, error, isPassword = false, ...inputProps },
    ref,
  ) {
    const [hidden, setHidden] = useState(isPassword);

    return (
      <View className="mb-4">
        <Text className="mb-1 text-sm font-medium text-brand-900">
          {label}
        </Text>
        <View className="flex-row items-center rounded-xl border border-brand-200 bg-white px-4">
          <TextInputNative
            ref={ref}
            {...inputProps}
            secureTextEntry={isPassword && hidden}
            placeholderTextColor="#9CA3AF"
            className="flex-1 py-3 text-base text-brand-900"
          />
          {isPassword && (
            <Pressable onPress={() => setHidden((prev) => !prev)}>
              <Text className="text-sm font-medium text-brand-500">
                {hidden ? "Mostrar" : "Ocultar"}
              </Text>
            </Pressable>
          )}
        </View>
        {error && <Text className="mt-1 text-sm text-red-600">{error}</Text>}
      </View>
    );
  },
);
