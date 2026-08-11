import { forwardRef } from "react";
import type { TextInput as TextInputNative } from "react-native";
import { Masks, useMaskedInputProps } from "react-native-mask-input";

import { TextInput, type TextInputProps } from "@/components/TextInput";

type DateFieldProps = Omit<TextInputProps, "onChangeText" | "value"> & {
  value?: string;
  onChangeText: (value: string) => void;
};

export const DateField = forwardRef<TextInputNative, DateFieldProps>(
  function DateField({ value, onChangeText, ...rest }, ref) {
    const maskedInputProps = useMaskedInputProps({
      value,
      onChangeText: (masked) => onChangeText(masked),
      mask: Masks.DATE_DDMMYYYY,
    });

    return (
      <TextInput
        ref={ref}
        keyboardType="numeric"
        {...maskedInputProps}
        {...rest}
      />
    );
  },
);
