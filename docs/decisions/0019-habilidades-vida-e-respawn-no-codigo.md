# 0019. Habilidades, vida e respawn no código

**Data:** 2026-11-21 · **Status:** aceita

## Contexto

O blockout das áreas 0 e 1 precisa de pulo duplo desbloqueável, armadilhas, corações, retorno ao chão seguro e checkpoints (decisões 0007, 0008, 0013 e 0014).

## Decisão

| Peça | Módulo | Responsabilidade |
| --- | --- | --- |
| `Ability` e `PlayerAbilities` | Player | Quais habilidades o Jack tem. Cenas de teste podem começar com habilidades liberadas |
| Pulo duplo em `PlayerMovement` | Player | Um pulo extra no ar, recarregado ao tocar o chão |
| `HealthPool` | Player | Regras puras de vida: corações, invulnerabilidade, resultado de cada golpe |
| `PlayerHealth` | Player | Liga o `HealthPool` à cena e avisa por eventos: perdeu coração, acabou a vida |
| `PlayerRespawn` | Player | Guarda a última posição em cima de plataforma e o checkpoint; reage aos eventos de vida |
| `Hazard` | Level | Qualquer colisor de gatilho que tira um coração (espinhos, água, vapor) |
| `AbilityScroll` | Level | Pergaminho que libera uma habilidade ao ser tocado |
| `Checkpoint` | Level | Lamparina: guarda o ponto de retorno e enche os corações ao ser acesa |

O módulo **Level** depende do **Player**, e nunca o contrário. O jogador não sabe que armadilhas ou pergaminhos existem; ele só oferece `TakeHit`, `Unlock` e `SetCheckpoint`.

O segundo pulo é definido pela **altura que ele acrescenta** (2 tiles), e sua velocidade é calculada com a mesma gravidade do pulo normal. Pulo de 3 tiles + segundo pulo de 2 = 5 tiles, a métrica do mapa.

## Por quê

- **Eventos em vez de referências cruzadas:** `PlayerHealth` não sabe o que acontece quando o Jack se machuca. `PlayerRespawn` escuta e decide. Uma futura tela de corações vai escutar o mesmo evento, sem mudar nada no código da vida.
- **Regras puras testáveis:** `HealthPool` não depende da cena e tem testes EditMode.
- **Armadilha genérica:** um único componente serve para os três tipos de armadilha. O que muda entre elas é a forma, a arte e, no vapor, o liga e desliga.

## Consequências

- Os corações também enchem ao acender uma lamparina nova, para cada área começar igual (decisão 0008).
- A interface de corações entra depois, escutando `HeartsChanged`.
- A pose e o pictograma do pergaminho (decisão 0013) entram no polimento, escutando `PlayerAbilities.Unlocked`.
