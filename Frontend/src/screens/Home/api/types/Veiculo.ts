export type TipoVeiculo = "Carro" | "Moto";

export interface Veiculo {
  id: string;
  placa: string;
  apelido: string;
  tipoVeiculo: TipoVeiculo;
}
