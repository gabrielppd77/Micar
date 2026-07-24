---
name: iniciar
description: Registra uma nova história de trabalho em arquivo persistente (`1-historia.md`), para que as próximas etapas do fluxo (entendimento, implementação, análise de padrões, relato final, commit) consigam ler e retomar o contexto sem depender do histórico de chat. Use ao iniciar uma nova história, com ou sem o texto dela em mãos.
tipo: Skill (etapa de fluxo de trabalho)
---

# Skill: iniciar

## Objetivo
Registrar uma nova história de trabalho em um lugar persistente, para que as
próximas etapas do fluxo (entendimento, implementação, análise de padrões, relato final, commit)
consigam ler e retomar o contexto sem depender do histórico de chat.

## Quando usar
O usuário pede para iniciar uma nova história — com ou sem o texto da
história em mãos ainda.

## Entrada esperada
- Um nome curto para a história (opcional — se não vier, pergunte um nome
  curto antes de criar a pasta; não tente adivinhar um slug de um texto que
  ainda não existe).
- Texto da história (opcional — pode vir junto do comando, colado no chat
  depois, ou escrito direto no arquivo criado).

## Passos
1. Determine a data atual e um slug em kebab-case para a história
   (ex: `2026-07-11-cadastro-recorrencia-mensal`). Se não houver nome
   fornecido, pergunte um nome curto ao usuário antes de seguir.
2. Crie a pasta `ai-workflows/historias/<data>-<slug>/` caso não exista.
3. Duas situações:
   - **Usuário já forneceu o texto da história** (colado junto do comando ou
     na mensagem): salve em `1-historia.md` com um cabeçalho simples —
     título (`# <nome da história>`) e uma linha `> Criada em <data>` — mas
     o **texto do pedido em si vai verbatim**, sem resumir, corrigir ou
     reformatar, logo abaixo do cabeçalho. O cabeçalho é só moldura; a fonte
     de verdade é o texto original, intocado. Não encadeie automaticamente:
     pergunte explicitamente *"História registrada. Posso seguir para a
     etapa 2 (gerar o entendimento)?"* e só prossiga após confirmação.
   - **Usuário ainda não forneceu o texto**: crie `1-historia.md` só com o
     cabeçalho (`# <nome da história>` + `> Criada em <data>`) e informe:
     *"Arquivo pronto em `<caminho>` — pode escrever/colar a história ali,
     ou colar aqui no chat quando estiver pronta."* Não prossiga para o
     entendimento até o usuário sinalizar que o texto está pronto (editou o
     arquivo e avisou, ou colou no chat).

## Saída
- `ai-workflows/historias/<data>-<slug>/1-historia.md` (preenchido ou
  aguardando preenchimento pelo usuário).

## Notas
- Se já existir uma pasta com o mesmo slug no mesmo dia, pergunte ao usuário
  se é uma nova versão da mesma história (sobrescrever) ou uma história
  diferente (sufixo `-2`, `-3`, etc).
- Arquivos da pasta da história são prefixados com o número da etapa (ver
  `README.md` da pasta `ai-workflows/`) — `1-historia.md` é sempre o
  primeiro. Nunca avance de etapa sem perguntar primeiro (mesmo princípio
  geral do `README.md`).
