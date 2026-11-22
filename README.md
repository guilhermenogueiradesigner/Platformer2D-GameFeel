# JACK O'DRAIN

Jack, a pumpkin-headed ninja, fell into the sewer and needs to escape. A short 2D level that teaches a full movement kit (double jump, dash, grappling hook and wall cling) **without tutorial text**, using only space, light and the order of challenges.

[Português](README.pt-BR.md)

> Work in progress. This README grows with each development stage.

**Engine:** Unity 6000.3 (URP 2D) · **Duration:** 5 to 10 minutes

## What this project demonstrates

- Level design that teaches through the environment
- Game feel for 2D platformer movement
- Onboarding and flow

## Architecture

Code lives in `Assets/_Project/Scripts/`, one assembly per module:

| Module | Content |
| --- | --- |
| `JackODrain.Core` | Small reusable pieces (`Countdown`, `PixelGrid`) |
| `JackODrain.Player` | Movement, abilities, health and respawn |
| `JackODrain.Cameras` | Platformer camera: look-ahead, holds height during jumps, settles on landing |
| `JackODrain.Level` | Things the player meets: hazards, ability scrolls, checkpoints. Depends on Player, never the other way around |

Movement uses a dynamic `Rigidbody2D` with custom gravity. Jumps are defined by height in tiles and time to apex, so level metrics and code use the same numbers. All tuning values live in a `MovementSettings` asset. Pure logic is covered by EditMode tests in `Assets/_Project/Tests/`. Design reasoning: [docs/decisions/0018](docs/decisions/0018-arquitetura-do-movimento.md).

## How to run

1. Install Unity 6000.3.23f1 through Unity Hub.
2. Get the [SHADOW Series](https://escape-pixel.itch.io/shadow-series-full-bundle) pack by Escape Pixell and place the files in `Assets/ThirdParty/EscapePixel/`. The pack is paid and its files are not redistributed here. Without it, the project opens but the sprites are missing.
3. Open this folder as a project.
4. Open the main scene in `Assets/_Project/Scenes/`.

## Credits

Third-party assets are listed in [docs/creditos.md](docs/creditos.md).

## Documentation

All design and development documentation lives in [docs/](docs/README.md).

## Use of AI

Implementation was accelerated with AI. All design decisions are mine.
