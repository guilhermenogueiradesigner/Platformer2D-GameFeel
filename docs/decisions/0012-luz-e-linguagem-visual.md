# 0012. Cabeça que brilha e linguagem visual do esgoto

**Data:** 2026-11-21 · **Status:** aceita

## Contexto

A fase ensina sem texto, usando o espaço e a luz. O Jack tem cabeça de abóbora, como uma *jack-o'-lantern*, e o esgoto é um lugar escuro. Também faltava uma regra visual única para dizer ao jogador onde ele **não** consegue se segurar.

## Decisão

- **A cabeça do Jack brilha.** Uma luz 2D presa à cabeça ilumina um raio curto ao redor dele, e o esgoto fica em penumbra.
- **Luz guia, escuro esconde.** Pontos importantes têm luz própria: os altares das habilidades, os checkpoints e as saídas de cada área. O caminho principal é mais iluminado que os caminhos opcionais.
- **Checkpoints são lamparinas.** Ao passar por uma, o Jack a acende com a chama da cabeça. Lamparina acesa = checkpoint ativo.
- **Metal não segura.** Superfícies de metal (canos, chapas, grades) recusam tanto o gancho quanto o grudar na parede. Pedra e tijolo seguram. É uma regra só, para as duas mecânicas.

## Por quê

- A luz da cabeça transforma a identidade do personagem em ferramenta de level design, o que reforça o pilar "o espaço ensina".
- A lamparina mostra o checkpoint sem ícone nem texto, e acender algo é um feedback satisfatório.
- Uma única regra de material é mais fácil de aprender do que duas regras diferentes para gancho e parede.

## Consequências

- A cena usa luz global baixa do URP 2D e luzes pontuais nos lugares importantes.
- O Tilemap separa as superfícies em dois grupos: que seguram e que não seguram. O metal precisa ser reconhecível à primeira vista, inclusive na penumbra.
- A luz da cabeça precisa de um raio que permita ver o próximo salto. O valor é ajustado nos playtests.
