import type { NivelAlertaEnum } from "@/screens/common/api/types/NivelAlertaEnum";

export interface ManutencaoPendenciaResponse {
  id: string;
  nome: string;
  status: NivelAlertaEnum;
  dataVencimento: string | null;
  diasRestantes: number | null;
  odometroVencimento: number | null;
  kmRestantes: number | null;
}
