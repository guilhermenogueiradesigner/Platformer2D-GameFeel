# 0006. Escala da pixel art

**Data:** 2026-11-21 · **Status:** aceita

## Contexto

Os sprites são desenhados à mão no Aseprite. Antes do primeiro desenho é preciso fixar a escala: o tamanho do tile, do personagem e a resolução em que o jogo é renderizado. Mudar isso depois obriga a redesenhar tudo.

## Opções consideradas

- **Tile de 8 px:** rápido de desenhar, mas o personagem fica sem espaço para expressão.
- **Tile de 16 px:** padrão do gênero, equilíbrio entre detalhe e tempo de produção.
- **Tile de 32 px:** mais detalhe, mas multiplica o tempo de cada tile e de cada animação.

## Decisão

| Item | Valor |
| --- | --- |
| Tile | 16×16 px |
| Canvas de cada quadro do Jack | 32×32 px |
| Altura do Jack no quadro | cerca de 24 px (1,5 tile) |
| Pixels Per Unit | 16 (1 tile = 1 unidade da Unity) |
| Resolução de referência | 480×270 |

## Por quê

- O tile de 16 px é legível e cabe no prazo.
- O Jack com 1,5 tile de altura deixa claro para o jogador o tamanho dos vãos: uma passagem de 2 tiles é "apertada, mas cabe".
- O canvas de 32 px dá espaço para as poses que se esticam (gancho, dash, parede) sem mudar o pivô entre quadros.
- Com Pixels Per Unit 16, os valores de ajuste ficam em tiles: "pulo de 3 unidades" é "pulo de 3 tiles".
- 480×270 multiplica exato para 1080p (×4) e 4K (×8), sem pixels borrados.

## Consequências

- A câmera usa o Pixel Perfect Camera do URP com a resolução de referência acima.
- Os sprites são importados com filtro `Point`, sem compressão, e pivô em "Bottom Center".
- Como o Jack tem estilo pequeno e quadradinho, ele pode ficar abaixo de 24 px de altura. O limite do canvas continua 32×32.
