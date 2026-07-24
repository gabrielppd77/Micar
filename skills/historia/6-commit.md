---
name: commit
description: Cria o commit de encerramento da história, com o código efetivamente alterado (os arquivos da própria história ficam de fora, via gitignore). Use como último passo, só depois que o usuário confirmar que o relato final está correto e a história pode ser encerrada.
tipo: Skill (etapa de fluxo de trabalho)
---

# Skill: commit

## Objetivo
Fechar a história com o commit do código efetivamente alterado, separado dos arquivos de
acompanhamento da própria história (que não entram no versionamento).

## Quando usar
Só depois que o usuário confirmar o relato gerado em `5-relato-funcionalidade.md` e que a
história pode ser encerrada. Não crie o commit antes dessa confirmação.

## Passos
1. Faça `git status`/`git diff` para revisar exatamente o que será incluído antes de dar
   `git add` — nunca use `-A`/`.` sem olhar a lista de arquivos primeiro.
2. Os arquivos da própria história ficam em `ai-workflows/historias/<data>-<slug>/` (história,
   entendimento, implementação, relato) — essa pasta deve estar no `.gitignore` do projeto, então
   esses arquivos são só locais e **não entram no commit**. O commit leva apenas o código
   efetivamente alterado.
3. Escreva a mensagem no mesmo estilo do restante do repositório: uma linha, em português,
   descrevendo o que foi feito (ver `git log` para o tom — sem prefixos tipo `feat:`/`fix:`,
   direto ao ponto).
4. Regras normais de commit do projeto se aplicam (não usar `--no-verify`, não fazer `push` sem
   pedido explícito, etc.).
5. Depois do commit, informe o hash/resumo ao usuário — não é preciso pedir confirmação de novo
   para este passo específico, já que ele decorre direto da confirmação de encerramento dada na
   etapa de relato.

## Saída
- Commit criado no repositório com o código alterado.
- Hash/resumo do commit informado ao usuário.
