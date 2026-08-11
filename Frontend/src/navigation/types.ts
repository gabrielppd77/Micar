export type AuthStackParamList = {
  Login: undefined;
  Cadastro: undefined;
};

export type AppStackParamList = {
  VeiculoList: undefined;
  VeiculoForm: { id?: string } | undefined;
  Home: undefined;
  Perfil: undefined;
  RegistroOdometroForm: { veiculoId: string };
  ManutencaoList: { veiculoId: string };
  ManutencaoForm: { veiculoId: string; id?: string };
};
