# MAX

**Em uma frase:** Max, um gato preto ninja, caiu no esgoto e precisa fugir, ganhando novos movimentos pelo caminho.
**O que este projeto prova:** level design que ensina sem texto, game feel e onboarding.
**Público:** fãs de plataforma de precisão e de metroidvania.
**Plataforma e duração:** PC (WebGL no itch.io) · 5 a 10 minutos.

## Pilares de design

1. **O espaço ensina:** nada é explicado por texto. A geometria, a luz e a ordem dos desafios mostram o que fazer.
2. **Controle justo:** quando o jogador erra, ele sabe que a culpa foi dele, nunca do controle.
3. **Movimento expressivo:** há mais de um jeito de atravessar cada trecho.
4. **Fluxo contínuo:** encadear mecânicas sem parar é recompensado.

## Personagem

Max é um gato preto ninja que anda em pé. O estilo é pequeno e quadradinho, mais que humanoide. Ser ninja explica as habilidades, inclusive o gancho.

## Experiência do jogador

O jogador começa no fundo do esgoto e ganha as habilidades aos poucos: pulo duplo, grudar na parede, dash em 8 direções e gancho. Cada habilidade nova abre caminho adiante e também permite voltar a lugares que antes estavam fora de alcance. No fim, a saída só é alcançada combinando tudo. A sensação final é de domínio: o jogador pode se mover como quiser, com controle total do Max.

## Regras principais

- **Progressão:** cada habilidade é ganha em um ponto da fase, na ordem abaixo.
- **Coletáveis:** itens simples escondidos nas áreas de retorno, como recompensa pela exploração.
- **Vida:** o Max tem 5 corações. Cada armadilha tira um coração e o leva de volta ao último chão seguro. Sem corações, ele volta ao checkpoint da área, onde os corações voltam ao máximo.

## Mecânicas

| Ordem | Mecânica | O que permite |
| --- | --- | --- |
| 1 | Pulo duplo | Alcançar plataformas mais altas |
| 2 | Parede | Grudar parado e pular de parede para subir poços |
| 3 | Dash | Atravessar vãos em 8 direções, somando com o pulo duplo |
| 4 | Gancho | Prender em tetos, ser puxado e balançar sobre abismos |

Detalhes e motivos em [decisions/](decisions/).

## Fora do escopo

- Inimigos e sistema de combate. Só armadilhas causam dano ao Max.
- Texto de tutorial, diálogos e narrativa além da premissa.
- Lojas, upgrades e qualquer sistema que use os coletáveis. Eles existem só como recompensa.
