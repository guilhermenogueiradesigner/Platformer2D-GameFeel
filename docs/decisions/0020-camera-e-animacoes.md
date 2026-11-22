# 0020. Câmera de plataforma e animações do Jack

**Data:** 2026-11-21 · **Status:** aceita · **Substitui:** o uso de Cinemachine da decisão [0016](0016-ajustes-das-mecanicas.md)

## Contexto

A câmera precisa ajudar o jogador a agir: mostrar o que vem pela frente e não balançar a cada pulo. O Jack também precisa das primeiras animações (idle, run, jump e fall), que já existem no arquivo do pacote.

## Opções consideradas

- **Cinemachine:** pacote pronto e flexível, mas o comportamento de "segurar a altura até pousar" exige combinar vários componentes, e a lógica fica escondida em configuração.
- **Câmera própria:** poucas linhas, com o comportamento exato que a fase precisa, legível no repositório e sem pacote novo.

## Decisão

Uma **câmera própria** (`PlatformerCamera`), com os valores em `CameraSettings`:

| Comportamento | Como funciona |
| --- | --- |
| **Segurar a altura no pulo** | No ar, a câmera mantém a altura do último chão. Um pulo comum não mexe a câmera |
| **Acomodar no pouso** | Ao pousar numa altura nova, a câmera vai até ela com suavidade (0,3 s) |
| **Seguir subidas grandes** | Se o Jack sobe mais de 3,5 tiles no ar (pulo duplo, parede), a câmera passa a acompanhar |
| **Seguir quedas** | Se o Jack cai mais de 2 tiles abaixo do último chão, a câmera acompanha rápido (0,1 s) para mostrar onde ele vai cair |
| **Olhar à frente** | A câmera fica 2 tiles adiante da direção em que o Jack está virado e troca de lado com suavidade |

As animações usam o **Animator gerado pelo importador do Aseprite**. O `PlayerAnimator` escolhe o estado pelo movimento e chama `Animator.Play` só quando o estado muda:

| Situação | Estado |
| --- | --- |
| No chão, parado | `idle` |
| No chão, andando | `run` |
| No ar, subindo | `jump` |
| No ar, caindo | `fall` |

O sprite é espelhado conforme a direção do Jack.

## Por quê

- **Câmera que segura a altura:** se a câmera sobe e desce a cada pulo, a tela balança e o jogador perde a referência do chão. Segurando a altura até o pouso, o chão fica parado na tela e o jogador mede melhor os saltos. É o padrão de jogos de plataforma de precisão.
- **Olhar à frente:** quem corre precisa ver o próximo obstáculo, não o que ficou para trás.
- **Estados escolhidos pelo código, sem transições no Animator:** em pixel art não há mistura entre animações, e a regra "qual animação tocar" fica num lugar só, legível e fácil de estender com as próximas habilidades.

## Consequências

- O módulo `JackODrain.Cameras` depende do Player.
- O `PlayerMovement` passa a expor `FacingDirection`.
- Os nomes das tags no Aseprite precisam ser `idle`, `run`, `jump` e `fall`. Se faltar algum estado, o `PlayerAnimator` avisa no Console.
- Os limites de câmera por área entram com o blockout.
- O Pixel Perfect Camera continua valendo (decisão 0006).
