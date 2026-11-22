# JACK O'DRAIN

Jack, um ninja com cabeça de abóbora, caiu no esgoto e precisa fugir. Uma fase 2D curta que ensina um pacote completo de movimentação (pulo duplo, dash, gancho e grudar na parede) **sem texto de tutorial**, só com o espaço, a luz e a ordem dos desafios.

[English](README.md)

> Em desenvolvimento. Este README cresce a cada etapa.

**Engine:** Unity 6000.3 (URP 2D) · **Duração:** 5 a 10 minutos

## O que este projeto demonstra

- Level design que ensina pelo ambiente
- Game feel de movimentação de plataforma 2D
- Onboarding e fluxo

## Arquitetura

O código fica em `Assets/_Project/Scripts/`, com um assembly por módulo:

| Módulo | Conteúdo |
| --- | --- |
| `JackODrain.Core` | Peças pequenas e reutilizáveis (`Countdown`, `PixelGrid`) |
| `JackODrain.Player` | Movimento, habilidades, vida e respawn |
| `JackODrain.Cameras` | Câmera de plataforma: olha à frente, segura a altura no pulo, acomoda no pouso |
| `JackODrain.Level` | O que o jogador encontra: armadilhas, pergaminhos, lamparinas. Depende do Player, nunca o contrário |

O movimento usa um `Rigidbody2D` dinâmico com gravidade própria. O pulo é definido por altura em tiles e tempo até o ponto mais alto, então as métricas da fase e o código usam os mesmos números. Todos os valores de ajuste ficam num asset `MovementSettings`. A lógica pura tem testes EditMode em `Assets/_Project/Tests/`. Motivos: [docs/decisions/0018](docs/decisions/0018-arquitetura-do-movimento.md).

## Como rodar

1. Instale a Unity 6000.3.23f1 pelo Unity Hub.
2. Adquira o pacote [SHADOW Series](https://escape-pixel.itch.io/shadow-series-full-bundle), de Escape Pixell, e coloque os arquivos em `Assets/ThirdParty/EscapePixel/`. O pacote é pago e os arquivos não são redistribuídos aqui. Sem ele, o projeto abre, mas os sprites aparecem faltando.
3. Abra esta pasta como projeto.
4. Abra a cena principal em `Assets/_Project/Scenes/`.

## Créditos

Os assets de terceiros estão listados em [docs/creditos.md](docs/creditos.md).

## Documentação

Toda a documentação de design e desenvolvimento está em [docs/](docs/README.md).

## Uso de IA

A implementação foi acelerada com IA. Todas as decisões de design são minhas.
