import { useRoute, type RouteProp } from "@react-navigation/native";

import { Button } from "@/components/Button";
import { RecordListScreen } from "@/components/RecordListScreen";
import { useAppGoTo } from "@/hooks/useAppGoTo";
import { confirmDelete } from "@/libs/alert";
import { toDateInput } from "@/libs/date";
import type { AppStackParamList } from "@/navigation/types";
import type { ManutencaoResponse } from "../api/types/ManutencaoResponse";
import { useDeleteManutencao } from "../mutations/useDeleteManutencao";
import { useManutencoes } from "../queries/useManutencoes";

type ManutencaoListRouteProp = RouteProp<AppStackParamList, "ManutencaoList">;

export function ManutencaoListScreen() {
  const { goToManutencaoForm, goToHome } = useAppGoTo();
  const route = useRoute<ManutencaoListRouteProp>();
  const { veiculoId } = route.params;

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

    if (manutencao.valor != null) {
      subtitle += ` · R$ ${manutencao.valor.toFixed(2)}`;
    }

    return subtitle;
  }

  return (
    <RecordListScreen
      title="Manutenções"
      subtitle="Histórico do veículo"
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
