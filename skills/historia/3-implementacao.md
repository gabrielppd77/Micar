---
name: implementacao
description: Analisa o código do projeto à luz do entendimento confirmado, levanta dúvidas técnicas objetivas quando necessário, define um plano curto e aplica as alterações. Use depois que o entendimento (etapa 2) for confirmado pelo usuário; repita quantas vezes forem necessárias até a implementação estar pronta para a etapa de análise de padrões.
tipo: Skill (etapa de fluxo de trabalho)
---

# Skill: implementacao

## Objetivo
Passar do entendimento confirmado para a implementação de fato: analisar o código
existente, levantar dúvidas técnicas objetivas (quando a análise revelar algo que o
entendimento não cobre), definir um plano curto de alteração, e só então aplicar as
mudanças no código.

## Quando usar
Logo após `apresentar-entendimento` (etapa 2) ser confirmado pelo usuário. Esta etapa
pode se repetir várias vezes (nova rodada de dúvidas, ajuste de plano, nova aplicação)
antes de a história estar pronta para a etapa de análise de padrões (`analise-padrao`,
etapa 4) — nunca avance sem o usuário confirmar explicitamente que as alterações estão
concluídas.

## Entrada esperada
- `2-entendimento.md` confirmado.
- Acesso ao código do repositório para análise.

## Passos
1. Leia `2-entendimento.md` confirmado.
2. Analise o código relevante do repositório à luz do entendimento — identifique arquivos,
   padrões já existentes e possíveis impedimentos técnicos.
3. Se a análise revelar dúvidas técnicas objetivas (algo que o entendimento não decide),
   registre-as em `3-implementacao.md`, no mesmo formato de pergunta fechada + linha
   `> Resposta:` usado em `2-entendimento.md`, e pergunte ao usuário antes de prosseguir.
4. Com as dúvidas respondidas (ou se não houver nenhuma), apresente um plano curto das
   alterações a fazer e pergunte objetivamente: **"Esse plano está correto? Posso aplicar
   as alterações?"**
5. Só após confirmação, aplique as alterações no código, seguindo os padrões definidos nas
   skills de front-end e back-end do projeto.
6. Ao concluir uma rodada de alterações, pergunte objetivamente: **"As alterações foram
   feitas, podemos finalizar a história?"** Se o usuário pedir ajustes, volte ao passo 2 ou
   3 conforme necessário — não é preciso reiniciar a etapa do zero. Só ao confirmar, siga
   para a etapa de análise de padrões (`analise-padrao`).

## Saída
- `ai-workflows/historias/<data>-<slug>/3-implementacao.md` — dúvidas técnicas levantadas
  durante a análise (se houver) e o plano de alterações, ambos já respondidos/confirmados
  pelo usuário.
- As alterações de código efetivamente aplicadas no repositório.

## Notas
- Nem toda história vai gerar dúvidas técnicas nesta etapa — se a análise não revelar
  nada em aberto, pule direto para o plano.
- Esta etapa pode se repetir várias vezes; cada rodada de ajuste continua sendo a etapa 3,
  não uma etapa nova.
