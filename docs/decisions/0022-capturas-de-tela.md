# 0022. Capturas de tela durante o desenvolvimento

**Data:** 2026-11-22 · **Status:** aceita

## Contexto

O mapa da fase precisa mostrar o blockout cinza ao lado da versão final, e o devlog fica mais claro com imagens. As capturas precisam ser feitas durante o jogo, no momento exato de um teste, e ir direto para a documentação.

## Decisão

Um componente `ScreenshotCapture` (módulo DevTools) captura a Game view ao apertar uma tecla no Play Mode. O padrão é **F12**, configurável no Inspector; na cena de teste a tecla usada é o **0**.

- No editor, salva em `docs/media/capturas/` com data e hora no nome (`AAAA-MM-DD_HH-mm-ss.png`).
- Numa build, salva na pasta de dados do jogador, para não depender da estrutura do repositório.
- O tamanho pode ser multiplicado (1 a 4) no Inspector.

## Por quê

A captura sai do próprio jogo, sem barras do editor e na resolução da Game view, e cai direto na pasta da documentação. O nome com data deixa a ordem cronológica automática, como no devlog.

## Consequências

- Cada cena de teste e de blockout tem um objeto com `ScreenshotCapture`.
- As capturas usadas nos documentos são referenciadas a partir de `docs/media/capturas/`. Capturas que não forem usadas podem ser apagadas antes do commit.
