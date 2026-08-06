import {
  MutationCache,
  QueryCache,
  QueryClient,
  QueryClientProvider as QueryClientProviderLib,
} from "@tanstack/react-query";

import { fireError } from "../libs/alert";

const queryClient = new QueryClient({
  defaultOptions: { queries: { retry: 1 } },
  queryCache: new QueryCache({ onError: fireError }),
  mutationCache: new MutationCache({ onError: fireError }),
});

interface QueryClientProviderProps {
  children: React.ReactNode;
}

export function QueryClientProvider({
  children,
}: QueryClientProviderProps) {
  return (
    <QueryClientProviderLib client={queryClient}>
      {children}
    </QueryClientProviderLib>
  );
}
