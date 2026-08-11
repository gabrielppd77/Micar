export interface CreateManutencaoRequest {
  nome: string;
  data: string;
  veiculoId: string;
  odometro: number;
  odometroVencimento?: number;
  dataVencimento?: string;
  valor?: number;
}
