import { Alert } from "react-native";

import { getApiErrorMessage } from "./api";

export function fireError(error: unknown) {
  Alert.alert("Houston, temos um problema", getApiErrorMessage(error));
}
