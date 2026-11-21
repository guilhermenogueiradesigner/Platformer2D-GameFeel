# 0017. Interface, menus e áudio

**Data:** 2026-11-21 · **Status:** aceita

## Contexto

A fase não usa texto de tutorial, mas ainda precisa de um início, de um fim e de som. O público de plataforma de precisão gosta de medir o próprio desempenho.

## Decisão

- **Durante o jogo:** só os 5 corações e a contagem de velas, no canto superior.
- **Menu inicial:** título, jogar, controles e sair. O menu pode ter texto, porque não é tutorial.
- **Tela de fim:** tempo total, velas coletadas e corações perdidos.
- **Música:** uma faixa para a fase toda, mais som ambiente do esgoto (gotas, água correndo).
- **Origem do áudio:** pacotes prontos, com a mesma regra de licença dos assets visuais (decisão 0010).

## Por quê

A UI mínima deixa a atenção no espaço. A tela de fim com tempo e velas convida a jogar de novo, que é como o público do gênero joga. Uma faixa só cabe no escopo, e o som ambiente dá identidade ao esgoto com pouco custo.

## Consequências

- Os créditos de áudio entram em `docs/creditos.md`.
- O tempo é medido do início da fase até a saída.
