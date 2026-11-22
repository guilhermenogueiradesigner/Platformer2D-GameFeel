# 0021. Peças do blockout: velas, luzes, HUD e gerador de prefabs

**Data:** 2026-11-22 · **Status:** aceita

## Contexto

Para montar o blockout das áreas 0 e 1 faltavam as velas (decisão 0015), a luz da cabeça e das lamparinas (decisão 0012) e uma interface mínima de corações e velas (decisão 0017). Também era preciso deixar o level design rápido: arrastar peças prontas em vez de montar cada objeto à mão.

## Decisão

| Peça | Módulo | Responsabilidade |
| --- | --- | --- |
| `PlayerCandles` | Player | Conta as velas coletadas e avisa por evento |
| `Candle` | Level | Vela no mundo: ao ser tocada, soma uma vela e some |
| `HeadLight` | Player | Luz 2D da cabeça. Cada vela aumenta intensidade e raio, até 5 velas |
| `CheckpointLamp` | Level | Acende a chama da lamparina quando o checkpoint é ativado |
| `IconMeter` | UI | Fileira de ícones cheios e vazios, sem texto |
| `HealthMeter` e `CandleMeter` | UI | Ligam o `IconMeter` aos eventos de vida e de velas |
| `BlockoutPrefabBuilder` | Editor | Menu *Tools > Jack O'Drain > Create Blockout Prefabs*, que gera os prefabs cinza |

Prefabs gerados em `Assets/_Project/Prefabs/Blockout/`: `Lamp`, `DoubleJumpScroll`, `Spikes`, `ToxicWater`, `Candle` e `Hud`. Cada um tem a base no chão (pivô embaixo), para encaixar direto no Tilemap.

## Por quê

- **Gerador de prefabs em código:** os prefabs ficam iguais toda vez, e o que cada um contém está escrito e revisável no repositório. O gerador não sobrescreve um prefab que já existe, para não apagar ajustes feitos à mão.
- **Hitbox dos espinhos menor que o desenho:** o gatilho dos espinhos tem 0,8 × 0,35 tile, menor que o visual de 1 × 0,5. Raspar na ponta não machuca. Isso sustenta o pilar "controle justo".
- **HUD com ícones, sem texto:** combina com o pilar "o espaço ensina" e não depende de fonte.
- **UI separada do Player:** a vida e as velas não sabem que existe uma tela. A UI escuta os eventos, como o respawn já fazia.

## Consequências

- A luz da cabeça é um objeto filho do Jack com `Light2D` e `HeadLight`.
- A cena precisa de luz global baixa para a penumbra funcionar. O valor é ajustado no blockout.
- O HUD precisa receber o Jack nos campos `Health` e `Candles` depois de colocado na cena.
- A arte final substitui os quadrados cinza na Etapa 6, mantendo os componentes.
- No URP 17, a `Light2D` fica no assembly `Unity.RenderPipelines.Universal.2D.Runtime`. Os módulos que usam luz referenciam esse assembly.
