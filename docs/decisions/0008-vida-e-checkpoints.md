# 0008. Vida em corações e checkpoints por área

**Data:** 2026-11-21 · **Status:** aceita

## Contexto

Não há inimigos, só armadilhas. Era preciso definir o que acontece quando o Jack toma dano, porque isso define o custo do erro e o quanto o jogador arrisca.

## Opções consideradas

- **Morte instantânea com retorno imediato** (padrão de plataforma de precisão): erro barato, muitas tentativas rápidas.
- **Vida em corações com checkpoint:** cada erro custa um coração, e o jogador só volta ao checkpoint quando perde todos.

## Decisão

| Regra | Valor |
| --- | --- |
| Vida | **5 corações** |
| Dano | Cada armadilha tira um coração, e o Jack volta ao **último chão seguro** |
| Sem corações | O Jack volta ao **checkpoint da área** |
| No checkpoint | Os corações voltam ao máximo |

## Por quê

Os corações dão uma margem de erro enquanto o jogador ainda aprende cada mecânica, e o checkpoint por área mantém um custo real para o erro sem obrigar a refazer a fase inteira. Combina com o público de metroidvania, acostumado a vida em corações.

Voltar ao último chão seguro funciona com qualquer armadilha, inclusive poços e água tóxica, onde ficar no lugar seria impossível. O erro custa um coração e alguns segundos, não o trecho inteiro, o que sustenta o pilar "controle justo": o jogador tenta de novo logo em seguida, no mesmo desafio.

Resetar os corações no checkpoint faz cada área começar do mesmo jeito, e a dificuldade de uma área não depende de como o jogador se saiu na anterior.

## Consequências

- A UI mínima mostra os 5 corações.
- Cada área tem um checkpoint claro, sinalizado sem texto.
- **Chão seguro:** a posição do Jack é salva quando ele está em cima de uma plataforma, com chão firme meio tile além de cada pé, a pelo menos 1 tile de qualquer armadilha e fora da invulnerabilidade. Ao tomar dano, ele volta à última posição salva.
- **Invulnerabilidade ao reaparecer:** o Jack fica um instante sem receber dano, para o jogador se situar.
- **Revisto no [playtest 01](../playtests/2026-11-22-sessao-01.md):** a regra original salvava na quina das plataformas e, durante a invulnerabilidade, em cima dos espinhos. As condições de borda, distância de armadilhas e invulnerabilidade foram acrescentadas por isso.
- Plataformas muito estreitas ou cercadas de armadilhas não salvam posição. Nelas, o Jack volta ao último ponto seguro anterior.
