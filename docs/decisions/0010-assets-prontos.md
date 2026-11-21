# 0010. Arte com assets prontos, editados no Aseprite

**Data:** 2026-11-21 · **Status:** aceita · **Substitui:** a parte de arte da decisão [0005](0005-game-feel-e-arte-propria.md)

## Contexto

A decisão 0005 previa sprites desenhados do zero. O objetivo do projeto é provar level design e game feel, e desenhar toda a arte disputava tempo com esse objetivo dentro das 3 semanas.

## Opções consideradas

- **Arte própria do zero:** projeto mais autoral, mas o maior risco de escopo.
- **Assets prontos, sem edição:** mais rápido, mas o visual fica genérico e pode não casar com o Max.
- **Assets prontos, editados no Aseprite:** o tempo vai para o que o projeto precisa provar, e a edição adapta o visual ao Max e ao esgoto.

## Decisão

Usar **assets prontos** e editá-los no **Aseprite** quando precisar. A escala da decisão 0006 continua valendo: tile de 16 px, quadro de 32×32, Pixels Per Unit 16.

## Por quê

O foco do portfólio é o design da fase e a sensação do controle. Assets prontos liberam tempo para iterar nos playtests, que é onde está o valor do projeto. A edição no Aseprite mantém a identidade do Max.

## Consequências

- Os assets escolhidos precisam seguir a escala de 16 px, ou ser adaptados a ela.
- O fluxo de importação continua o mesmo: arquivos `.aseprite` importados direto pelo pacote `com.unity.2d.aseprite`.
- **Licenças:** cada asset precisa de uma licença que permita uso em projeto público. Muitos pacotes permitem usar no jogo, mas **proíbem redistribuir os arquivos-fonte**, e um repositório público é redistribuição. Para cada pacote:
  - se a licença permite redistribuir, os arquivos vão para o repositório;
  - se não permite, a pasta do pacote fica no `.gitignore`, e o README explica onde baixá-lo.
- Os créditos de cada asset (autor, link e licença) ficam em `docs/creditos.md` e no README.

## Aplicação (21/11/2026)

O primeiro pacote, **SHADOW Series** (Escape Pixell), permite uso comercial e modificação, mas a página não fala de redistribuição, e o pacote é pago. Por isso:

- Os arquivos do pacote ficam em `Assets/ThirdParty/EscapePixel/`, mesmo depois de editados, porque continuam contendo a arte do autor.
- `Assets/ThirdParty/` está no `.gitignore`. Essa pasta é reservada para assets que **não podem** ir para o repositório público.
- Quem clonar o repositório precisa comprar o pacote e colocá-lo nessa pasta, como explica o README. Sem ele, o projeto abre, mas os sprites aparecem faltando.
- A build publicada no itch.io inclui a arte normalmente, porque usar o asset dentro do jogo é permitido.
