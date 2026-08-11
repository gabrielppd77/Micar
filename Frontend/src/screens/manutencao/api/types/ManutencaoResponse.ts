export interface ManutencaoResponse {
  id: string;
  data: string;
  nome: string;
  veiculoId: string;
  registroOdometroId: string | null;
  odometro: number | null;
  odometroVencimento: number | null;
  dataVencimento: string | null;
  valor: number | null;
}
