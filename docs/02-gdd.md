# GDD · JACK O'DRAIN

> Visão geral e pilares: [01-one-pager.md](01-one-pager.md). Motivos de cada decisão: [decisions/](decisions/).
> Os valores numéricos são pontos de partida e são ajustados nos playtests.

---

## 1. Resumo

Uma fase de plataforma 2D de 5 a 10 minutos. Jack, um ninja com cabeça de abóbora, caiu no esgoto e precisa fugir, ganhando quatro habilidades de movimento pelo caminho. Nada é explicado por texto: o espaço, a luz e a ordem dos desafios ensinam.

---

## 2. Personagem

| Item | Valor |
| --- | --- |
| Identidade | Jack, ninja com cabeça de abóbora, pequeno e quadradinho (decisão 0011) |
| Sprite | Base do pacote SHADOW Series, editada no Aseprite (decisão 0010) |
| Tamanho | Cerca de 13×23 px em um quadro de 32×32 (decisão 0006) |
| Luz | A cabeça brilha e ilumina um raio curto ao redor (decisão 0012) |

### Animações

| Animação | Estado |
| --- | --- |
| idle, run, jump, fall | No jogo, escolhidas pelo `PlayerAnimator` (decisão 0020) |
| hit | Existe no pacote |
| idle-up | Existe no pacote; serve de pose de mira para cima |
| double-jump, wall-cling, dash, grapple-pull, grapple-swing | A fazer, editando quadros do pacote |
| pose de pegar pergaminho | A fazer |

---

## 3. Controles

| Ação | Teclado | Controle |
| --- | --- | --- |
| Mover e mirar | Setas ou WASD | Analógico esquerdo ou direcional |
| Pular / pulo duplo / pulo de parede | Espaço ou Z | A (Xbox) / X (PlayStation) |
| Dash | Shift ou X | X (Xbox) / Quadrado (PlayStation) |
| Gancho (segurar) | C | RB / R1 |

O dash e o gancho usam a direção segurada no momento (decisão 0004). Sem direção, o dash vai para onde o Jack está virado e o gancho mira para cima.

---

## 4. Mecânicas

### 4.1. Movimento base

- Andar, pular, cair.
- Game feel: coyote time, jump buffer, altura variável do pulo, queda mais rápida e correção de canto (decisão 0005).

### 4.2. Pulo duplo (habilidade 1)

- Um segundo pulo no ar.
- Recarrega ao tocar o chão ou grudar na parede.

### 4.3. Parede (habilidade 2)

- Ao encostar numa parede no ar, o Jack gruda parado, sem escorregar.
- Pulo de parede para o lado oposto.
- Grudar recarrega o pulo duplo e o dash.
- Paredes de metal não seguram (decisão 0012).

### 4.4. Dash (habilidade 3)

- 8 direções, um por ida ao ar, somável ao pulo duplo.
- Recarrega ao tocar o chão ou grudar na parede.
- Não deixa o Jack invulnerável (decisão 0016).

### 4.5. Gancho (habilidade 4)

- Prende só em tetos, com alcance inicial de 6 tiles. Tetos de metal não prendem.
- Antes do disparo, uma linha pontilhada mostra a mira, e um marcador mostra o ponto de impacto. Sem teto válido, a mira mostra isso.
- Depois de prender, puxa o Jack até um comprimento de corda e então balança como pêndulo.
- Soltar o botão solta a corda, mantendo o impulso do balanço.

### 4.6. Máquina de estados

```
          ┌──────────── pular ────────────┐
          ▼                               │
     [No chão] ──sair da borda──► [No ar] ─┤
          ▲                         │  │   │
          └────── tocar o chão ─────┘  │   │
                                       │   │
     [Na parede] ◄── encostar ─────────┤   │
          │  └──── pulo de parede ─────┘   │
          ▼                                │
       [Dash] ◄──────── dash ──────────────┤
                                           │
  [Gancho puxando] ──► [Gancho balançando] ─┘ soltar
```

Cada estado define quais ações aceita. Isso evita combinações inválidas, como usar o dash duas vezes no mesmo ar.

---

## 5. Linguagem visual

Decisão 0012.

| Elemento | Significado |
| --- | --- |
| Luz forte | Caminho principal, altar de habilidade, checkpoint, saída |
| Penumbra | Caminhos opcionais e segredos |
| Lamparina acesa | Checkpoint ativo |
| Pedra e tijolo | Seguram (parede e gancho) |
| Metal | Não segura |
| Vela distante e fora de alcance | Lugar para voltar depois |

---

## 6. Estrutura da fase

Ordem decidida na decisão 0003. Cada área ensina uma habilidade com a sequência **apresentação → prática segura → variação**.

| Área | Habilidade ganha | O que a área ensina | Vela escondida, alcançável com |
| --- | --- | --- | --- |
| 0. Queda | nenhuma | Movimento base, espinhos | Pulo duplo |
| 1. | Pulo duplo | Altura, água tóxica | Parede |
| 2. | Parede | Subir poços, metal não segura | Dash |
| 3. | Dash | Atravessar vãos, vapor em ritmo | Gancho |
| 4. | Gancho | Abismos e balanço | Combinação de tudo, perto da saída |
| 5. Saída | — | Desafio final combinando tudo | — |

- Cada habilidade vem de um pergaminho num altar iluminado, seguido de um trecho de prática segura (decisão 0013).
- Cada área começa numa lamparina (checkpoint).
- Os nomes das áreas e o mapa detalhado vão para `03-mapa-da-fase.md`.

---

## 7. Armadilhas

Decisão 0014. Cada uma tira um coração e leva o Jack ao último chão seguro.

| Armadilha | Comportamento | Aparece a partir da área |
| --- | --- | --- |
| Espinhos | Parados, em chão, parede ou teto | 0 |
| Água tóxica | Parada, no fundo de poços e canais | 1 |
| Jatos de vapor | Ligam e desligam em ritmo, com chiado e fumaça antes de ligar | 3 |

---

## 8. Vida e respawn

Decisão 0008.

| Regra | Valor |
| --- | --- |
| Vida | 5 corações |
| Dano | Perde um coração e volta à última posição salva em cima de plataforma |
| Ao reaparecer | Invulnerável por um instante |
| Sem corações | Volta à lamparina da área, com os corações cheios |

---

## 9. Coletáveis

Decisão 0015.

- **5 velas**, uma por área, cada uma alcançável só com a habilidade seguinte.
- Cada vela deixa a luz da cabeça um pouco mais forte, com limite. É só visual.

---

## 10. Interface

Decisão 0017.

| Tela | Conteúdo |
| --- | --- |
| Durante o jogo | 5 corações e contagem de velas, no canto superior |
| Menu inicial | Título, jogar, controles, sair |
| Tela de fim | Tempo total, velas coletadas, corações perdidos |

---

## 11. Câmera

Decisões 0006 e 0020.

- Pixel Perfect Camera com resolução de referência 480×270.
- Câmera própria: olha 2 tiles à frente da direção do Jack e segura a altura do último chão durante o pulo, acomodando com suavidade no pouso.
- Subidas grandes e quedas são acompanhadas no ar.
- Cada área tem limites próprios de câmera.

---

## 12. Áudio e feedback

Decisões 0005 e 0017.

- Feedback: poeira, deformação do sprite, rastro no dash, tremor de câmera e sons de impacto.
- Uma faixa de música para a fase toda, mais som ambiente do esgoto.
- Pacotes prontos, com a regra de licença da decisão 0010.

---

## 13. Escopo e riscos

| Risco | Mitigação |
| --- | --- |
| Gancho que puxa e balança é a mecânica mais cara | Implementado por último, com tempo reservado |
| Retorno a áreas aumenta o mapa | Retornos curtos, perto do caminho principal |
| Animações novas para quatro habilidades | Editar quadros do pacote; placeholders até o polimento |
| Iluminação pode deixar a fase escura demais | Raio da luz e luz global ajustados nos playtests |
| Fase de 5 a 10 minutos com 6 áreas | Áreas curtas; a duração é medida nos playtests |
