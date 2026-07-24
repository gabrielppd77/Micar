---
name: analise-padrao
description: Revisa o diff das alterações efetivamente aplicadas contra os padrões definidos nas skills de front-end e back-end do projeto, corrigindo o que for mecânico e relatando o que exigir decisão do usuário. Use depois que a implementação (etapa 3) for confirmada como concluída, antes de gerar o relato final.
tipo: Skill (etapa de fluxo de trabalho)
---

# Skill: analise-padrao

## Objetivo
Garantir que o código efetivamente alterado segue os padrões do projeto antes de fechar a
história — revisando o diff contra as skills de front-end/back-end, corrigindo o que for
mecânico e relatando o que exigir decisão do usuário.

## Quando usar
Depois que o usuário confirmar, na etapa de implementação, que "as alterações foram feitas" e a
história pode seguir adiante — mas antes de gerar `5-relato-funcionalidade.md`.

## Entrada esperada
- Alterações efetivamente aplicadas na etapa de implementação (diff/arquivos tocados).
- `3-implementacao.md`, para saber quais desvios já foram decididos e registrados durante a
  história (não são "achados" novos).

## Passos
1. Levante o diff das alterações efetivamente aplicadas na etapa de implementação.
2. Revise esse diff contra os padrões do repositório: arquivos da pasta `frontend/` contra as
   skills em `/skills/frontend`; arquivos da pasta `backend/` contra as skills em
   `/skills/backend`. Se a história não tocou um dos dois lados, pule a análise correspondente.
3. Foque no que essas skills descrevem como regra (nomenclatura, separação de responsabilidades,
   tratamento de erro, testes, convenções de contrato, etc.), não em preferência de estilo
   pessoal.
4. Desvios já **decididos e registrados** durante a história (ex.: um mock temporário aprovado em
   `3-implementacao.md`) não são "achados" — não os reabra nem sugira revertê-los; apenas cite-os
   como exceção conhecida se ajudar o contexto do relato.
5. Corrija na hora o que for mecânico e de baixo risco (import não utilizado, nome fora da
   convenção, `any` evitável). Para o que for ambíguo ou mudar comportamento/decisão de design,
   relate ao usuário em vez de aplicar sozinho.
6. Apresente um resumo dos achados relevantes (se houver) ao usuário antes de seguir para o
   relato.

## Saída
- Código ajustado com as correções mecânicas aplicadas.
- Lista de achados relevantes (se houver), para incluir no relato da etapa seguinte.

## Notas
- Esta etapa é sobre aderência a padrão, não sobre revisão de comportamento/lógica de negócio —
  isso já foi validado nas etapas anteriores (entendimento/implementação).
