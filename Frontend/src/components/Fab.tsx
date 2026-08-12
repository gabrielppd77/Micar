import { Ionicons } from "@expo/vector-icons";
import { useRef, useState } from "react";
import { Animated, Pressable, Text, View } from "react-native";
import { useSafeAreaInsets } from "react-native-safe-area-context";

type FabAction = {
  key: string;
  label: string;
  icon: keyof typeof Ionicons.glyphMap;
  onPress: () => void;
};

type FabProps = {
  actions: FabAction[];
};

export function Fab({ actions }: FabProps) {
  const insets = useSafeAreaInsets();
  const [open, setOpen] = useState(false);
  const animation = useRef(new Animated.Value(0)).current;

  function toggle() {
    const next = !open;
    setOpen(next);
    Animated.timing(animation, {
      toValue: next ? 1 : 0,
      duration: 180,
      useNativeDriver: true,
    }).start();
  }

  function handleActionPress(action: FabAction) {
    setOpen(false);
    animation.setValue(0);
    action.onPress();
  }

  return (
    <>
      {open && (
        <Pressable
          onPress={toggle}
          className="absolute bottom-0 left-0 right-0 top-0 bg-brand-900/10"
        />
      )}

      <View
        className="absolute right-6 items-end gap-3"
        style={{ bottom: insets.bottom + 24 }}
      >
        {open &&
          actions.map((action) => (
            <Animated.View
              key={action.key}
              className="flex-row items-center gap-3"
              style={{
                opacity: animation,
                transform: [
                  {
                    translateY: animation.interpolate({
                      inputRange: [0, 1],
                      outputRange: [12, 0],
                    }),
                  },
                ],
              }}
            >
              <View className="rounded-lg bg-brand-900 px-3 py-2 shadow-sm">
                <Text className="text-sm font-medium text-white">
                  {action.label}
                </Text>
              </View>
              <Pressable
                onPress={() => handleActionPress(action)}
                className="h-12 w-12 items-center justify-center rounded-full bg-white shadow-sm"
              >
                <Ionicons name={action.icon} size={22} color="#2E6E8E" />
              </Pressable>
            </Animated.View>
          ))}

        <Pressable
          onPress={toggle}
          className="h-14 w-14 items-center justify-center rounded-full bg-accent-500 shadow-md"
        >
          <Animated.View
            style={{
              transform: [
                {
                  rotate: animation.interpolate({
                    inputRange: [0, 1],
                    outputRange: ["0deg", "45deg"],
                  }),
                },
              ],
            }}
          >
            <Ionicons name="add" size={28} color="white" />
          </Animated.View>
        </Pressable>
      </View>
    </>
  );
}
