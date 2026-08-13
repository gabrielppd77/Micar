import { NivelAlertaEnum } from "@/screens/common/api/types/NivelAlertaEnum";

export interface VeiculoStatusOdometroResponse {
  status: NivelAlertaEnum;
  ultimaAtualizacao: string | null;
  odometro: number | null;
  diasSemAtualizacao: number | null;
}
