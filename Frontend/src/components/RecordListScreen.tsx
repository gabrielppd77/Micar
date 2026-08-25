import { Ionicons } from "@expo/vector-icons";
import type { ReactNode } from "react";
import {
  ActivityIndicator,
  FlatList,
  Pressable,
  Text,
  View,
} from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";

import { ProfileButton } from "@/components/ProfileButton";

type RecordListScreenProps<T> = {
  title: string;
  subtitle: ReactNode;
  data: T[] | undefined;
  isLoading: boolean;
  emptyMessage: string;
  keyExtractor: (item: T) => string;
  renderTitle: (item: T) => string;
  renderSubtitle: (item: T) => string;
  onEdit: (item: T) => void;
  onDelete: (item: T) => void;
  onSelect?: (item: T) => void;
  footer: ReactNode;
};

export function RecordListScreen<T>({
  title,
  subtitle,
  data,
  isLoading,
  emptyMessage,
  keyExtractor,
  renderTitle,
  renderSubtitle,
  onEdit,
  onDelete,
  onSelect,
  footer,
}: RecordListScreenProps<T>) {
  return (
    <SafeAreaView
      edges={["top", "left", "right"]}
      className="flex-1 bg-brand-50 px-6 pt-6"
    >
      <View className="mb-6 flex-row items-center justify-between">
        <View>
          <Text className="text-3xl font-bold text-brand-900">{title}</Text>
          <Text className="text-base text-brand-500">{subtitle}</Text>
        </View>
        <ProfileButton />
      </View>

      {isLoading ? (
        <ActivityIndicator className="mt-8" />
      ) : (
        <FlatList
          data={data}
          keyExtractor={keyExtractor}
          ItemSeparatorComponent={() => <View className="h-3" />}
          ListEmptyComponent={
            <Text className="text-center text-brand-500">{emptyMessage}</Text>
          }
          renderItem={({ item }) => (
            <RecordListItem
              title={renderTitle(item)}
              subtitle={renderSubtitle(item)}
              onSelect={onSelect ? () => onSelect(item) : undefined}
              onEdit={() => onEdit(item)}
              onDelete={() => onDelete(item)}
            />
          )}
        />
      )}

      <View className="gap-3 py-4">{footer}</View>
    </SafeAreaView>
  );
}

type RecordListItemProps = {
  title: string;
  subtitle: string;
  onSelect?: () => void;
  onEdit: () => void;
  onDelete: () => void;
};

function RecordListItem({
  title,
  subtitle,
  onSelect,
  onEdit,
  onDelete,
}: RecordListItemProps) {
  const content = (
    <View className="flex flex-row w-full justify-between items-center">
      <View className="flex-1">
        <View className="flex-row items-center justify-between w-full">
          <View className="flex-1">
            <Text className="text-lg font-semibold text-brand-900">
              {title}
            </Text>
            <Text className="text-sm text-brand-500">{subtitle}</Text>
          </View>
        </View>

        <View className="mt-3 flex-row gap-4">
          <Pressable onPress={onEdit}>
            <Text className="text-sm font-semibold text-brand-500">Editar</Text>
          </Pressable>
          <Pressable onPress={onDelete}>
            <Text className="text-sm font-semibold text-red-600">Excluir</Text>
          </Pressable>
        </View>
      </View>

      {onSelect && (
        <View className="ml-3 h-9 w-9 items-center justify-center rounded-full bg-accent-500">
          <Ionicons name="chevron-forward" size={18} color="white" />
        </View>
      )}
    </View>
  );

  if (onSelect) {
    return (
      <Pressable
        onPress={onSelect}
        className="rounded-xl bg-white p-4 shadow-sm "
      >
        {content}
      </Pressable>
    );
  }

  return <View className="rounded-xl bg-white p-4 shadow-sm">{content}</View>;
}
