import { StatusBar } from "expo-status-bar";

import { Button } from "@/components/Button";
import { RecordListScreen } from "@/components/RecordListScreen";
import { useAppGoTo } from "@/hooks/useAppGoTo";
import { useSelectedVeiculo } from "@/hooks/useSelectedVeiculo";
import { confirmDelete } from "@/libs/alert";
import type { VeiculoResponse } from "../api/types/VeiculoResponse";
import { useDeleteVeiculo } from "../mutations/useDeleteVeiculo";
import { useVeiculos } from "../queries/useVeiculos";

export function VeiculoListScreen() {
  const { goToVeiculoForm, goToHome } = useAppGoTo();
  const { data: veiculos, isLoading } = useVeiculos();
  const { setSelectedVeiculoId } = useSelectedVeiculo();
  const { mutate: deleteVeiculo } = useDeleteVeiculo();

  function handleSelect(veiculo: VeiculoResponse) {
    setSelectedVeiculoId(veiculo.id);
    goToHome();
  }

  function handleDelete(veiculo: VeiculoResponse) {
    confirmDelete({
      title: "Excluir veículo",
      message: `Tem certeza que deseja excluir "${veiculo.apelido}"?`,
      onConfirm: () => deleteVeiculo(veiculo.id),
    });
  }

  return (
    <>
      <StatusBar style="auto" />
      <RecordListScreen
        title="MICAR"
        subtitle="Seus veículos"
        data={veiculos}
        isLoading={isLoading}
        emptyMessage="Nenhum veículo cadastrado."
        keyExtractor={(veiculo) => veiculo.id}
        renderTitle={(veiculo) => veiculo.apelido}
        renderSubtitle={(veiculo) => `${veiculo.placa} · ${veiculo.tipoVeiculo}`}
        onSelect={handleSelect}
        onEdit={(veiculo) => goToVeiculoForm(veiculo.id)}
        onDelete={handleDelete}
        footer={
          <Button label="Novo veículo" onPress={() => goToVeiculoForm()} />
        }
      />
    </>
  );
}
