# Mapa da fase · JACK O'DRAIN

> Rascunho para revisão. As medidas estão em tiles (1 tile = 16 px = 1 unidade da Unity).
> Este documento cresce durante a Etapa 3: capturas do blockout cinza entram em `media/` e a versão final é colocada ao lado.

---

## 1. Métricas de movimento

O level design de plataforma começa pelas métricas: quanto o personagem pula, quanto avança no dash, até onde o gancho alcança. A fase é construída com essas medidas, e cada desafio fica **um pouco acima** ou **um pouco abaixo** do que o jogador consegue fazer sem a habilidade nova. É isso que deixa claro, sem texto, quando a habilidade é necessária.

Valores-alvo iniciais, validados no blockout:

| Movimento | Altura máxima | Distância máxima | Regra para o level design |
| --- | --- | --- | --- |
| Pulo ✅ | 3 tiles | 4 tiles | Obstáculo de 4 tiles de altura = "não dá só com o pulo" |
| Pulo + pulo duplo | 5 tiles | 7 tiles | Obstáculo de 5 tiles = "precisa do pulo duplo" |
| Dash | — | 5 tiles | Vão de 6 a 8 tiles = "precisa do dash" |
| Pulo duplo + dash | 5 tiles | 11 tiles | Vão de 9 a 11 tiles = combinação |
| Gancho | — | 6 tiles de alcance | Teto entre 4 e 6 tiles acima do Jack |
| Poço de parede | — | 3 a 4 tiles de largura | Estreito o bastante para alternar paredes |

A tela mostra cerca de **30×17 tiles** (480×270 px).

✅ = confirmado jogando na cena `MetricsTest` (21/11/2026: a parede de 3 tiles passa só com o pulo, a de 4 não).

---

## 2. Visão geral

O Jack cai pelo bueiro até o fundo do esgoto e sobe de volta à superfície. A progressão é **de baixo para cima e do escuro para a luz**: cada área fica mais alta que a anterior, e a saída é a luz do dia entrando pelo bueiro.

```
                                   ☀ SAÍDA (bueiro)
                                  ┌──────────────┐
                                  │ 5. Saída     │ desafio final
                     ┌────────────┴──────────────┤
                     │ 4. Galeria do gancho      │ tetos de pedra e metal
         ┌───────────┴───────────┐               │
         │ 3. Canal do vapor     │ dash          │
         └───────────┬───────────┘               │
                ┌────┴─────┐                     │
                │ 2. Poço  │ parede (vertical)   │
                └────┬─────┘                     │
       ┌─────────────┴───────┐                   │
       │ 1. Cisterna         │ pulo duplo        │
       └─────────────┬───────┘                   │
     ┌───────────────┴───┐                       │
     │ 0. Fundo          │ queda e movimento base│
     └───────────────────┘
```

Duração-alvo: cerca de 1 minuto por área, de 6 a 8 minutos no total, mais o tempo de quem busca as velas.

**Retornos curtos:** cada vela fica perto do caminho da área seguinte, separada por uma descida curta. O jogador volta alguns passos, não a área inteira.

---

## 3. Áreas

Cada área segue **apresentação → prática segura → variação** (decisão 0003) e começa numa lamparina (checkpoint).

### Área 0 · Fundo

- **Entrada:** o Jack cai do bueiro e aterrissa no fundo. A luz do bueiro lá em cima mostra o objetivo da fase inteira.
- **Apresentação:** chão plano e um degrau de 1 tile. O jogador descobre andar e pular.
- **Prática segura:** vãos de 2 e 3 tiles sobre chão raso, sem perigo.
- **Variação:** primeiros espinhos no chão, em trechos curtos e bem iluminados.
- **Vela 0:** numa saliência a 5 tiles de altura, visível e fora de alcance (pede o pulo duplo).

### Área 1 · Cisterna (pulo duplo)

- **Altar:** logo na entrada, iluminado.
- **Prática segura:** uma parede de 4 tiles. Com o pulo simples não dá; com o pulo duplo dá.
- **Variação:** plataformas em escada sobre **água tóxica**, com vãos que pedem o pulo duplo na hora certa.
- **Vela 1:** no alto de um poço estreito de tijolo (pede a parede).

### Área 2 · Poço (parede)

- **Altar:** na base do poço.
- **Prática segura:** poço de 3 tiles de largura, todo de tijolo, sem armadilhas. O jogador sobe alternando paredes.
- **Variação:** trechos de **metal** no poço (não seguram), forçando o jogador a escolher em qual parede se apoiar; depois, espinhos nas paredes.
- **Vela 2:** do outro lado de um vão de 7 tiles na saída do poço (pede o dash).

### Área 3 · Canal do vapor (dash)

- **Altar:** numa plataforma antes do primeiro vão.
- **Prática segura:** vão de 6 tiles sobre um piso seco logo abaixo. Cair não machuca.
- **Variação:** dash na diagonal para cima; **jatos de vapor** em ritmo; um vão de 10 tiles que pede pulo duplo + dash.
- **Vela 3:** sobre um abismo sob um teto de pedra (pede o gancho).

### Área 4 · Galeria do gancho (gancho)

- **Altar:** na borda do primeiro abismo.
- **Prática segura:** teto de pedra baixo sobre um abismo raso e seco. O jogador aprende a mira e o puxão.
- **Variação:** tetos alternando **pedra e metal**, obrigando a escolher onde prender; balanço em sequência sobre água tóxica.
- **Vela 4:** num nicho no alto, perto da saída, alcançável combinando gancho e parede.

### Área 5 · Saída (desafio final)

- **Subida final até o bueiro**, combinando as quatro habilidades sem pausa, com as três armadilhas.
- Desenhada para ser feita em fluxo contínuo: quem encadeia bem não precisa parar (pilar 4).
- **Fim:** o Jack sai pelo bueiro para a luz do dia. Tela de fim com tempo, velas e corações perdidos.

---

## 4. Ordem de construção (blockout)

1. Movimento base e métricas no código, numa cena de teste com obstáculos medidos (3, 4 e 5 tiles de altura; vãos de 4, 6 e 8).
2. Blockout cinza das áreas 0 e 1, com pulo duplo.
3. Área 2 com a parede.
4. Área 3 com o dash e o vapor.
5. Área 4 com o gancho.
6. Área 5, desafio final.
7. Primeiro playtest completo do blockout.

Cada passo gera captura do blockout em `media/` e uma entrada no devlog.

---

## 5. A revisar

- Nomes das áreas (Fundo, Cisterna, Poço, Canal do vapor, Galeria do gancho, Saída).
- As métricas da seção 1, que só se confirmam jogando.
- A sequência de desafios de cada área.
