# 0014. Armadilhas

**Data:** 2026-11-21 · **Status:** aceita

## Contexto

As armadilhas são a única fonte de dano (decisão 0008). Cada tipo novo precisa ser ensinado, então poucos tipos bem explorados valem mais que muitos tipos rasos.

## Opções consideradas

Água tóxica, espinhos ou grades, jatos de vapor, pás de ventilação.

## Decisão

Três tipos:

| Armadilha | Comportamento | Para que serve no design |
| --- | --- | --- |
| **Espinhos** | Parados, em chão, parede ou teto | Definir o espaço seguro com precisão |
| **Água tóxica** | Parada, no fundo de poços e canais | Punir quedas e motivar pulo duplo e gancho |
| **Jatos de vapor** | Ligam e desligam em ritmo, com aviso antes de ligar | Ensinar tempo, combinando com o dash |

As pás de ventilação ficam fora.

## Por quê

Os espinhos e a água são estáticos: o jogador lê o perigo de longe e o erro é sempre dele (pilar "controle justo"). O vapor é o único elemento com tempo e entra quando o jogador já domina o movimento. As pás exigiriam física e colisão próprias, com custo alto para pouca variação nova.

## Consequências

- O vapor precisa de um aviso visual e sonoro antes de ligar (chiado e fumaça fraca).
- Os espinhos de parede e de teto conversam com a parede e o gancho: limitam onde o Jack pode se segurar.
