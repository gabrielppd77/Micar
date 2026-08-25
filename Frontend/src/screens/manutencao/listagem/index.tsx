import { useRoute, type RouteProp } from "@react-navigation/native";

import { Button } from "@/components/Button";
import { RecordListScreen } from "@/components/RecordListScreen";
import { VeiculoInfoLine } from "@/components/VeiculoInfoLine";
import { useAppGoTo } from "@/hooks/useAppGoTo";
import { confirmDelete } from "@/libs/alert";
import { toDateInput } from "@/libs/date";
import type { AppStackParamList } from "@/navigation/types";
import { useVeiculo } from "@/screens/veiculo/queries/useVeiculo";
import type { ManutencaoResponse } from "../api/types/ManutencaoResponse";
import { useDeleteManutencao } from "../mutations/useDeleteManutencao";
import { useManutencoes } from "../queries/useManutencoes";

type ManutencaoListRouteProp = RouteProp<AppStackParamList, "ManutencaoList">;

function formatKmRestante(
  manutencao: ManutencaoResponse,
  odometroAtual: number | null | undefined,
) {
  if (manutencao.odometroVencimento == null || odometroAtual == null) {
    return null;
  }

  const kmRestantes = manutencao.odometroVencimento - odometroAtual;

  if (kmRestantes <= 0) {
    return null;
  }

  return `Faltam ${kmRestantes} km`;
}

export function ManutencaoListScreen() {
  const { goToManutencaoForm, goToHome } = useAppGoTo();
  const route = useRoute<ManutencaoListRouteProp>();
  const { veiculoId } = route.params;

  const { data: veiculo, isLoading: isVeiculoLoading } = useVeiculo(veiculoId);
  const { data: manutencoes, isLoading } = useManutencoes(veiculoId);
  const { mutate: deleteManutencao } = useDeleteManutencao();

  function handleDelete(manutencao: ManutencaoResponse) {
    confirmDelete({
      title: "Excluir manutenção",
      message: `Tem certeza que deseja excluir "${manutencao.nome}"?`,
      onConfirm: () => deleteManutencao(manutencao.id),
    });
  }

  function renderSubtitle(manutencao: ManutencaoResponse) {
    let subtitle = toDateInput(manutencao.data);

    if (manutencao.odometro != null) {
      subtitle += ` · ${manutencao.odometro} km`;
    }

    const kmRestantes = formatKmRestante(manutencao, veiculo?.odometroAtual);
    if (kmRestantes != null) {
      subtitle += ` · ${kmRestantes}`;
    }

    if (manutencao.valor != null) {
      subtitle += ` · R$ ${manutencao.valor.toFixed(2)}`;
    }

    return subtitle;
  }

  return (
    <RecordListScreen
      title="Manutenções"
      subtitle={
        <VeiculoInfoLine veiculo={veiculo} isLoading={isVeiculoLoading} />
      }
      data={manutencoes}
      isLoading={isLoading}
      emptyMessage="Nenhuma manutenção registrada."
      keyExtractor={(manutencao) => manutencao.id}
      renderTitle={(manutencao) => manutencao.nome}
      renderSubtitle={renderSubtitle}
      onEdit={(manutencao) => goToManutencaoForm(veiculoId, manutencao.id)}
      onDelete={handleDelete}
      footer={
        <>
          <Button
            label="Nova manutenção"
            onPress={() => goToManutencaoForm(veiculoId)}
          />
          <Button label="Voltar" onPress={goToHome} variant="ghost" />
        </>
      }
    />
  );
}
