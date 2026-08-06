import { RootNavigator } from "./src/navigation/RootNavigator";
import { AuthProvider } from "./src/providers/AuthProvider";
import { QueryClientProvider } from "./src/providers/QueryClientProvider";

export function App() {
  return (
    <QueryClientProvider>
      <AuthProvider>
        <RootNavigator />
      </AuthProvider>
    </QueryClientProvider>
  );
}
