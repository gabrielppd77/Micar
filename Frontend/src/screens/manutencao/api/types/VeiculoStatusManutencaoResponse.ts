import type { NivelAlertaEnum } from "@/screens/common/api/types/NivelAlertaEnum";

import type { ManutencaoPendenciaResponse } from "./ManutencaoPendenciaResponse";

export interface VeiculoStatusManutencaoResponse {
  statusGeral: NivelAlertaEnum;
  quantidadeTotal: number;
  quantidadeEmDia: number;
  quantidadeProximas: number;
  quantidadeVencidas: number;
  pendencias: ManutencaoPendenciaResponse[];
}
