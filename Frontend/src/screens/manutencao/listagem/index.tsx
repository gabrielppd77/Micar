import { useRoute, type RouteProp } from "@react-navigation/native";
import { useEffect, useState } from "react";

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

function formatKmRestante(manutencao: ManutencaoResponse) {
  if (manutencao.kmRestantes == null || manutencao.kmRestantes <= 0) {
    return null;
  }

  return `Faltam ${manutencao.kmRestantes} km`;
}

function formatDiasRestante(manutencao: ManutencaoResponse) {
  if (manutencao.diasRestantes == null || manutencao.diasRestantes <= 0) {
    return null;
  }

  return `Faltam ${manutencao.diasRestantes} dia(s)`;
}

export function ManutencaoListScreen() {
  const { goToManutencaoForm, goToHome } = useAppGoTo();
  const route = useRoute<ManutencaoListRouteProp>();
  const { veiculoId } = route.params;

  const { data: veiculo, isLoading: isVeiculoLoading } = useVeiculo(veiculoId);
  const { mutate: deleteManutencao } = useDeleteManutencao();

  const [termo, setTermo] = useState("");
  const [termoBusca, setTermoBusca] = useState("");

  useEffect(() => {
    const timeout = setTimeout(() => setTermoBusca(termo), 300);
    return () => clearTimeout(timeout);
  }, [termo]);

  const { data: manutencoes, isLoading } = useManutencoes(
    veiculoId,
    termoBusca,
  );

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

    const kmRestantes = formatKmRestante(manutencao);
    if (kmRestantes != null) {
      subtitle += ` · ${kmRestantes}`;
    }

    const diasRestantes = formatDiasRestante(manutencao);
    if (diasRestantes != null) {
      subtitle += ` · ${diasRestantes}`;
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
      search={{
        value: termo,
        onChangeText: setTermo,
        placeholder: "Buscar manutenção",
      }}
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
