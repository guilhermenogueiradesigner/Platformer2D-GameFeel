# 0016. Ajustes das mecânicas, controles e câmera

**Data:** 2026-11-21 · **Status:** aceita; o uso de Cinemachine foi substituído pela decisão [0020](0020-camera-e-animacoes.md)

## Contexto

A decisão 0004 deixou alguns pontos das mecânicas em aberto, e o GDD propôs controles e câmera.

## Decisão

| Ponto | Escolha |
| --- | --- |
| Paredes que não seguram | As de metal, pela mesma regra do gancho (decisão 0012) |
| Dash e dano | O dash **não** deixa o Jack invulnerável |
| Gancho | Prende **só em tetos**, com alcance inicial de 6 tiles, ajustado nos playtests |
| Controles | Os propostos no GDD: Espaço ou Z pula, Shift ou X faz o dash, C usa o gancho, com equivalentes no controle |
| Câmera | Pixel Perfect Camera com Cinemachine, seguindo o Jack com um pequeno avanço na direção do movimento |

## Por quê

- **Dash sem invulnerabilidade:** as armadilhas continuam valendo durante o dash, e o jogador lê o perigo do mesmo jeito em qualquer estado. Também evita que o dash vire a solução para tudo.
- **Gancho só no teto:** separa as funções. Teto é do gancho, parede é das garras. O jogador sabe qual ferramenta usar olhando para a superfície.
- **Alcance de 6 tiles:** o suficiente para atravessar um abismo médio, mas curto o bastante para o level design controlar o caminho. É um ponto de partida para os playtests.

## Consequências

- Os valores ficam em `ScriptableObject` e entram na tabela de ajustes final do projeto.
