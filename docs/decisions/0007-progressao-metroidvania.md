# 0007. Progressão: mecânicas desbloqueadas e retorno a áreas

**Data:** 2026-11-21 · **Status:** aceita

## Contexto

A fase apresenta quatro mecânicas em ordem. Era preciso decidir se o Max já tem todas desde o início ou se as ganha aos poucos, e se o mapa é um caminho linear ou permite voltar.

## Opções consideradas

- **Todas desde o início:** mais liberdade, mas o jogador pode pular etapas do ensino.
- **Desbloqueio progressivo, caminho linear:** garante a ordem de ensino, mas cada área é visitada uma vez só.
- **Desbloqueio progressivo com retorno (estilo metroidvania):** garante a ordem de ensino, e áreas antes inalcançáveis passam a ser acessíveis com uma habilidade nova.

## Decisão

O Max **ganha as mecânicas aos poucos**, na ordem da decisão 0003. O mapa **permite voltar** a áreas que antes não podiam ser alcançadas sem a habilidade certa.

O Max é um **gato ninja**: anda em pé e tem estilo pequeno e quadradinho, mais que humanoide. Ser ninja justifica o gancho dentro do mundo.

## Por quê

O desbloqueio garante que cada mecânica seja ensinada antes de ser exigida, o que sustenta o pilar "o espaço ensina". O retorno a áreas mostra um raciocínio de level design típico de metroidvania: o jogador vê um lugar inalcançável, guarda na memória e volta quando pode. Isso prova leitura de espaço e planejamento de mapa.

## Consequências

- Cada habilidade é ganha em um ponto da fase, e esse ponto precisa ser marcante sem usar texto.
- O mapa precisa de "portas" de habilidade: lugares visíveis cedo que só se abrem com uma mecânica posterior.
- **Escopo:** retorno a áreas aumenta o tamanho do mapa e o trabalho de level design. Para caber em 5 a 10 minutos, os retornos devem ser curtos (atalhos e trechos opcionais perto do caminho principal).
- **Recompensa do retorno:** coletáveis simples, escondidos nas áreas que só se alcançam com uma habilidade posterior. Ver a decisão [0009](0009-coletaveis-como-recompensa.md).
