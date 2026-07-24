---
name: relato-funcionalidade
description: Gera, ao final da implementação e da análise de padrões, um relato curto da funcionalidade entregue (`5-relato-funcionalidade.md`). Use apenas depois que o usuário confirmar explicitamente que a história pode ser finalizada, e depois da etapa de análise de padrões.
tipo: Skill (etapa de fluxo de trabalho)
---

# Skill: relato-funcionalidade

## Objetivo
Gerar, ao final do processo de uma história, um registro curto da
funcionalidade entregue — um "mapa" de contexto para uma IA (ou pessoa)
futura que precisar entender rapidamente o que foi feito e onde procurar.
Não substitui teste: comportamento detalhado é garantido por teste, este
relato é só o atalho de contexto (o quê, por quê, onde).

## Quando usar
Depois da etapa de análise de padrões (etapa 4), quando a IA perguntar
explicitamente "As alterações foram feitas, podemos finalizar a história?"
e o usuário confirmar. Nunca gerar antes dessa confirmação.

## Entrada esperada
- `2-entendimento.md` confirmado (o que foi pedido).
- As alterações efetivamente aplicadas na etapa de implementação (etapa 3),
  já revisadas pela etapa de análise de padrões (etapa 4) — não o que foi
  originalmente planejado, e sim o que de fato foi feito (pode ter mudado no
  meio do caminho, depois de ajustes), incluindo os achados relevantes da
  análise de padrões, se houver.

## Passos
1. Confirme com o usuário que pode finalizar antes de gerar qualquer coisa.
2. Gere `5-relato-funcionalidade.md` seguindo o padrão abaixo — curto,
   objetivo, sem repetir o que já está no código ou nos testes, incorporando
   os achados relevantes da análise de padrões (se houver).
3. Apresente um resumo ao usuário e pergunte se pode considerar a história
   encerrada.
4. Se o usuário confirmar o encerramento, siga para a etapa de commit
   (etapa 6). Não crie o commit aqui — essa confirmação autoriza o relato e
   o encerramento, mas o commit é uma etapa própria.

## Padrão de `5-relato-funcionalidade.md` (proposta inicial — ajustável)

```markdown
# <nome da história>

> Concluída em <data>

## O que foi feito
<1-3 frases, direto ao ponto>

## Por que
<motivação/contexto que justificou a mudança, se relevante para quem for
ler isso no futuro>

## Principais arquivos/classes
- `caminho/Arquivo.cs` — <papel no todo, 1 linha>

## Testes relacionados
- `caminho/ArquivoTeste.cs` — <o que cobre>
```

## Notas
- Trate este arquivo como índice, não como documentação de comportamento —
  comportamento detalhado é papel do teste, não deste relato. Isso mantém o
  arquivo barato de escrever e com baixo risco de ficar desatualizado.
- Onde guardar definitivamente esse relato (dentro da própria pasta da
  história em `historias/<data>-<slug>/`, ou centralizado em outro lugar
  para facilitar busca por assunto) ainda não está decidido — por ora,
  salvar dentro da pasta da história.
