import clsx from "clsx";
import { ActivityIndicator, Pressable, Text } from "react-native";

type ButtonProps = {
  label: string;
  onPress: () => void;
  variant?: "primary" | "ghost";
  loading?: boolean;
  disabled?: boolean;
};

export function Button({
  label,
  onPress,
  variant = "primary",
  loading = false,
  disabled = false,
}: ButtonProps) {
  const isDisabled = disabled || loading;
  const isPrimary = variant === "primary";

  return (
    <Pressable
      onPress={onPress}
      disabled={isDisabled}
      className={clsx("items-center", {
        "rounded-xl py-4": isPrimary,
        "bg-accent-500": isPrimary && !isDisabled,
        "bg-accent-300": isPrimary && isDisabled,
        "py-3": !isPrimary,
      })}
    >
      {loading && isPrimary ? (
        <ActivityIndicator color="white" />
      ) : (
        <Text
          className={clsx("text-base", {
            "font-bold text-white": isPrimary,
            "font-semibold text-brand-600": !isPrimary,
          })}
        >
          {label}
        </Text>
      )}
    </Pressable>
  );
}
