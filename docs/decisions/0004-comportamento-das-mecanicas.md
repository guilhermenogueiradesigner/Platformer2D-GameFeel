# 0004. Comportamento de cada mecânica

**Data:** 2026-11-21 · **Status:** aceita

## Contexto

Cada mecânica tem variações comuns no gênero. A escolha define o que o jogador consegue fazer, o que a fase pode exigir e o custo de implementação.

## Decisão

| Mecânica | Comportamento |
| --- | --- |
| **Pulo duplo** | Um segundo pulo no ar |
| **Parede** | Gruda parado, sem escorregar, e pode dar pulo de parede |
| **Dash** | 8 direções, um por ida ao ar, e pode ser somado ao pulo duplo |
| **Gancho** | Prende em qualquer teto, puxa o jogador e depois balança como pêndulo |
| **Recarga** | Tocar o chão ou grudar na parede recarrega o pulo duplo e o dash |
| **Mira** | Dash e gancho usam a direção do controle (teclas ou analógico) |

### Detalhes do gancho

- **Mira visível:** uma linha pontilhada mostra a direção, e um marcador mostra o ponto de impacto antes do disparo.
- **Materiais que não prendem:** tetos de metal (e outros materiais a definir) recusam o gancho. São a ferramenta do level design para limitar o caminho.

## Opções consideradas e por quê

- **Parede que gruda parada** em vez de escorregar: deixa a parede como lugar de pausa e de leitura do espaço, no estilo metroidvania, em vez de pressão de tempo.
- **Dash em 8 direções** em vez de só horizontal: ajuda na exploração vertical e cria combinações com o pulo duplo.
- **Gancho em qualquer teto** em vez de pontos marcados: dá liberdade ao jogador. A previsibilidade vem do feedback da mira, e o controle do caminho vem dos materiais que não prendem.
- **Puxa e depois balança** em vez de só um dos dois: o puxão aproxima o jogador do teto e o balanço permite ganhar impulso e soltar no momento certo.
- **Recarga no chão e na parede:** permite encadear parede → pulo duplo → dash em poços longos, comum no gênero.
- **Mira pela direção do controle:** funciona igual no teclado e no controle, e o dash e o gancho usam a mesma lógica.

## Consequências

- O personagem precisa de uma máquina de estados com chão, ar, parede, dash, gancho puxando e gancho balançando.
- O Tilemap precisa distinguir tetos que prendem o gancho dos que não prendem.
- O gancho é a mecânica mais cara: física do pêndulo, transição entre puxar e balançar, câmera e feedback da mira. Por isso ele é implementado por último e com tempo reservado.

## Em aberto

- Alcance máximo do gancho e comprimento da corda no balanço.
- Se o gancho também prende em paredes ou só em tetos.
- Valores de altura, velocidade e duração: ajustados nos playtests.
