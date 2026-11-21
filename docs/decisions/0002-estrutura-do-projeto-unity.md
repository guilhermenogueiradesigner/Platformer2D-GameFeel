# 0002. Template, pipeline de renderização e organização do projeto

**Data:** 2026-11-21 · **Status:** aceita

## Contexto

O projeto precisa de uma base técnica que funcione para um jogo 2D com luz (a luz é uma das ferramentas para ensinar pelo espaço) e que seja fácil de ler para quem abrir o repositório.

## Opções consideradas

- **Template 2D com Built-in Render Pipeline:** mais simples, mas sem as luzes 2D e descontinuado nas versões novas da Unity.
- **Template Universal 2D (URP com 2D Renderer):** traz luzes 2D, sombras 2D e pós-processamento, e é o padrão atual da Unity para 2D.

## Decisão

- Template **Universal 2D** na Unity 6000.3.23f1.
- **Input System** para os controles, que já vem no template e suporta teclado e controle com o mesmo mapa de ações.
- Todo o conteúdo do projeto em `Assets/_Project/`, separado do que vem de pacotes e do template.
- Um `.asmdef` por módulo de código.

- Remover os pacotes que o template trouxe e que um jogo de plataforma 2D não usa: compras in-app (`purchasing`), `analytics`, Microsoft GDK, `multiplayer.center`, `ai.navigation`, `xr.legacyinputhelpers`, Visual Scripting e Unity Version Control (`collab-proxy`, já que o projeto usa git).

## Por quê

As luzes 2D do URP permitem usar a luz para guiar o jogador, que é um dos recursos da fase. A pasta `_Project` aparece no topo do painel do projeto e deixa claro o que foi feito por mim. Os `.asmdef` deixam as dependências entre módulos explícitas e reduzem o tempo de compilação. Com o `manifest.json` só com o que o jogo usa, quem abrir o projeto entende a stack de imediato, e a importação e o build ficam mais leves.

## Consequências

- Os arquivos de configuração do template (`Assets/Settings/`) ficam onde a Unity os criou, porque o projeto os referencia.
- A cena de exemplo do template foi movida para `Assets/_Project/Scenes/` e será substituída pela cena da fase na Etapa 3.
- As pastas de `Assets/_Project/` são criadas só quando recebem conteúdo. Uma pasta vazia não entra no git, mas o `.meta` dela entra, e a Unity reclama do `.meta` sem pasta ao clonar o repositório.
