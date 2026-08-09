import { Alert } from "react-native";

import { getApiErrorMessage, isSessionExpiredError } from "./api";

export function fireError(error: unknown) {
  if (isSessionExpiredError(error)) {
    return;
  }

  Alert.alert("Houston, temos um problema", getApiErrorMessage(error));
}
