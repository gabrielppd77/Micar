---
name: apresentar-entendimento
description: Lê a história registrada, produz um entendimento estruturado (`2-entendimento.md`) e obtém confirmação explícita do usuário antes de qualquer análise de código ou implementação. Use logo após `iniciar`, ou para atualizar o entendimento depois que dúvidas da etapa de implementação forem respondidas.
tipo: Skill (etapa de fluxo de trabalho)
---

# Skill: apresentar-entendimento

## Objetivo
Ler a história registrada, produzir um entendimento estruturado dela, e obter
confirmação explícita do usuário antes de qualquer análise de código ou
implementação.

## Quando usar
Logo após `iniciar`. Também é usada para **atualizar** o
entendimento depois que a etapa de implementação (etapa 3) levantar dúvidas e
elas forem respondidas — mas isso não é um "reinício do zero": se a conversa
já tem o contexto da história e das dúvidas respondidas, apenas atualize
`2-entendimento.md` incorporando as respostas, sem reler tudo como se fosse a
primeira vez. Reler os arquivos do zero só é necessário se a sessão de fato
não tiver mais esse contexto (ex: nova sessão, ou o usuário editou
`1-historia.md`/`3-implementacao.md` diretamente fora da conversa).

## Entrada esperada
- `ai-workflows/historias/<data>-<slug>/1-historia.md`
- Se existir, `3-implementacao.md` com dúvidas já respondidas (ver skill de
  implementação) — nesse caso, o entendimento deve incorporar as respostas,
  não só o texto original.

## Passos
1. Leia `1-historia.md` (e `3-implementacao.md` respondido, se existir).
2. Gere o conteúdo de `2-entendimento.md` seguindo o padrão abaixo.
3. Escreva o arquivo em `ai-workflows/historias/<data>-<slug>/2-entendimento.md`.
4. Apresente ao usuário, no chat, um resumo curto (não o arquivo inteiro) e
   pergunte objetivamente: **"Esse entendimento está correto? Posso seguir
   para a etapa 3 (implementação), ou algo precisa ser ajustado?"**
5. Se o usuário pedir ajustes: atualize `2-entendimento.md` e pergunte de
   novo. Não avance de etapa sem confirmação.
6. Quando o usuário confirmar, adicione ao topo do arquivo uma linha:
   `> Confirmado pelo usuário em <data>` e informe que a próxima etapa
   (implementação) pode começar — mas só inicie de fato depois que o usuário
   responder que sim (ver princípio geral no `README.md`).

## Padrão de `entendimento.md` (proposta inicial — ajustável)

```markdown
# Entendimento: <nome da história>

> Status: aguardando confirmação do usuário

## Objetivo
<1-3 frases: o que a história pede, no seu entendimento>

## Contexto / motivação
<por que isso é necessário, se estiver explícito ou dedutível na história>

## Escopo
- <o que está incluído>

## Fora do escopo
- <o que explicitamente não está incluído, se mencionado ou inferido>

## Critérios de aceite
- <lista extraída ou inferida da história>

## Suposições e perguntas
Responda diretamente abaixo de cada pergunta, na linha que começa com
`> Resposta:` (pode apagar o placeholder e escrever ali mesmo).

1. <suposição/dúvida formulada como pergunta objetiva>
   > Resposta:

2. <próxima pergunta>
   > Resposta:

## Observações
<espaço livre — se durante o processo você lembrar de algo relevante que não
se encaixa em nenhuma pergunta acima, registre aqui.>
```

## Notas
- "Suposições e perguntas" é a seção mais importante para o usuário revisar —
  é onde erros de entendimento normalmente aparecem. Cada item deve ser
  formulado como pergunta fechada e objetiva (idealmente sim/não ou escolha
  entre opções), não como uma afirmação genérica — isso é o que torna a
  resposta rápida de dar diretamente no arquivo.
- O marcador `> Resposta:` é fixo e proposital: ao reler o arquivo depois, a
  IA identifica perguntas ainda pendentes por terem essa linha vazia (sem
  texto além do placeholder), sem precisar de um `duvidas.md` separado só
  para esta etapa. Se o usuário responder no chat em vez de no arquivo,
  transcreva a resposta para a linha `> Resposta:` correspondente ao
  atualizar o arquivo, para manter o arquivo como fonte de verdade única.
- A seção "Observações" fica sempre por último e sempre vazia na criação —
  é espaço livre do usuário, não gerado por suposição da IA. Ao reprocessar
  o entendimento, leia essa seção: qualquer coisa escrita ali deve ser
  incorporada (pode virar nova pergunta, ajustar escopo, etc.) e depois a
  seção deve ser esvaziada de novo para a próxima rodada.
- Não pule para sugerir solução técnica aqui — esta etapa é só sobre
  entender o pedido, a análise de código/impedimentos vem na etapa seguinte.
