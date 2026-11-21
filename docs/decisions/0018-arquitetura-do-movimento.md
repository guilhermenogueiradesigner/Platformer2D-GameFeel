# 0018. Arquitetura do movimento

**Data:** 2026-11-21 · **Status:** aceita

## Contexto

O movimento é o centro do projeto. Ele precisa ser preciso, fácil de ajustar nos playtests, e o código precisa ser lido por quem avaliar o repositório. As quatro habilidades vão ser somadas ao movimento base, uma de cada vez.

## Opções consideradas

- **Física padrão da Unity (gravidade e atrito do Rigidbody2D):** pouco código, mas difícil de controlar com precisão. O pulo varia com a massa e o atrito, e não dá para pensar em tiles.
- **Character controller cinemático, com colisão própria:** controle total, mas exige escrever a resolução de colisão inteira.
- **Rigidbody2D dinâmico com gravidade própria:** a Unity resolve as colisões, e o código controla a velocidade a cada passo de física.

## Decisão

**Rigidbody2D dinâmico com gravidade própria**, dividido em responsabilidades pequenas:

| Peça | Responsabilidade |
| --- | --- |
| `MovementSettings` (ScriptableObject) | Todos os valores de ajuste, em tiles e segundos |
| `JumpPhysics` | Converte altura e tempo do pulo em gravidade e velocidade de lançamento |
| `PlayerInputReader` | Lê o Input System e expõe intenção: direção e pulo |
| `CollisionSensor` | Pergunta ao mundo: está no chão? há teto? dá para desviar da quina? |
| `PlayerMovement` | Junta tudo: corrida, pulo, gravidade e os recursos de game feel |
| `Countdown` (módulo Core) | Temporizador simples, usado pelo coyote time e pelo jump buffer |

O pulo é definido por **altura em tiles** e **tempo até o ponto mais alto**, e não por força:

- gravidade = 2 × altura ÷ tempo²
- velocidade de lançamento = 2 × altura ÷ tempo

## Por quê

- **Pensar em tiles:** com a altura do pulo em tiles, a métrica do mapa ("pulo de 3 tiles") e o valor no código são o mesmo número. O level design e o código falam a mesma língua.
- **Separar leitura de entrada, sensores e regras:** cada peça é pequena e tem um motivo só para mudar. Trocar o controle não mexe na física, e mudar o pulo não mexe nos sensores.
- **Partes puras testáveis:** `JumpPhysics` e `Countdown` não dependem da cena e têm testes automáticos (EditMode).
- **Atrito zero no corpo:** sem atrito, o Jack não gruda nas paredes ao empurrar contra elas no ar. A parede de verdade é uma habilidade com regra própria.

## Consequências

- A máquina de estados do GDD entra quando chegar a parede (área 2). Até lá, chão e ar cabem em `PlayerMovement` sem estados explícitos, e criar estados antes seria complexidade sem uso.
- Novas habilidades entram como peças próprias, sem inchar `PlayerMovement`.
- A camada das superfícies sólidas é escolhida no Inspector (`Solid Layers`).
