# 0011. Nome do jogo e personagem

**Data:** 2026-11-21 · **Status:** aceita · **Substitui:** a identidade "gato ninja" da decisão 0007

## Contexto

O personagem foi pensado como um gato ninja chamado Max. Com o uso de assets prontos (decisão 0010), o sprite escolhido passou a ser um ninja com cabeça de abóbora, e o nome precisava acompanhar a nova identidade.

## Opções consideradas

- **SQUASH:** abóbora em inglês e também *squash and stretch*, princípio de animação ligado ao game feel.
- **KABOCHA:** a abóbora japonesa, ligando a cabeça ao ninja.
- **JACK O'DRAIN:** trocadilho com *jack-o'-lantern* e *drain* (dreno, esgoto).
- **GOURD** e **PUMPKIN RUN:** diretos, mas menos expressivos.

## Decisão

- Jogo: **JACK O'DRAIN**.
- Personagem: **Jack**, um ninja com cabeça de abóbora, de estilo pequeno e quadradinho.
- Namespace raiz do código: `JackODrain`, porque identificadores de C# não aceitam apóstrofo.

## Por quê

O nome junta o personagem (a abóbora de *jack-o'-lantern*) e o cenário (o dreno do esgoto) num trocadilho bem-humorado e fácil de lembrar, e dá ao personagem um nome próprio. Uma busca na web não encontrou outro jogo com esse nome.

## Consequências

- Os documentos, os READMEs e o `CLAUDE.md` usam o nome novo.
- O repositório no GitHub pode continuar como `Platformer2D-GameFeel` ou ser renomeado; o nome descritivo ajuda quem busca por competência.
