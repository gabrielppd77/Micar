import axios from "axios";

import { isTokenExpired } from "./jwt";

export const api = axios.create({
  baseURL: process.env.EXPO_PUBLIC_API_URL,
});

let onSessionExpired: (() => void) | null = null;

export function setSessionExpiredHandler(handler: (() => void) | null) {
  onSessionExpired = handler;
}

export class SessionExpiredError extends Error {
  constructor() {
    super("Sessão expirada.");
    this.name = "SessionExpiredError";
  }
}

export function isSessionExpiredError(
  error: unknown,
): error is SessionExpiredError {
  return error instanceof SessionExpiredError;
}

api.interceptors.request.use((config) => {
  const authorization = config.headers?.Authorization;
  const token =
    typeof authorization === "string"
      ? authorization.replace("Bearer ", "")
      : null;

  if (token && isTokenExpired(token)) {
    onSessionExpired?.();
    return Promise.reject(new SessionExpiredError());
  }

  return config;
});

export function getApiErrorMessage(error: unknown) {
  const fallBack = "Não foi possível processar a sua requisição.";

  if (axios.isAxiosError(error)) {
    return (
      error.response?.data?.detail ?? error.response?.data?.title ?? fallBack
    );
  }

  return fallBack;
}
