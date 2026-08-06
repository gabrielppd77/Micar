import axios from "axios";

export const api = axios.create({
  baseURL: process.env.EXPO_PUBLIC_API_URL,
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
