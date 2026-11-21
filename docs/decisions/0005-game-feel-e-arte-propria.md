# 0005. Recursos de game feel e arte própria

**Data:** 2026-11-21 · **Status:** aceita; a parte de arte foi substituída pela decisão [0010](0010-assets-prontos.md)

## Contexto

Em um jogo de movimentação, a sensação do controle é o produto. Pequenos recursos invisíveis fazem o controle parecer justo, e o feedback visual faz cada ação parecer forte. A arte também define quanto do projeto é contribuição autoral.

## Decisão

Todos os recursos abaixo entram no controle:

| Recurso | O que faz |
| --- | --- |
| Coyote time | Permite pular alguns instantes depois de sair da borda |
| Jump buffer | Guarda o botão de pulo apertado um pouco antes de tocar o chão |
| Altura variável do pulo | Soltar o botão cedo encurta o pulo |
| Queda mais rápida | A gravidade aumenta na descida |
| Correção de canto | Empurra o jogador alguns pixels quando ele bate a cabeça na quina |
| Feedback visual e sonoro | Poeira, deformação do sprite, rastro no dash, tremor de câmera e sons de impacto |

Os **sprites são desenhados por mim no Aseprite**.

## Por quê

Os quatro primeiros recursos corrigem a diferença entre o que o jogador quis fazer e o que o jogo entendeu, e sem eles o controle parece injusto mesmo quando está "correto". O feedback dá peso a cada ação. Desenhar os sprites usa a minha formação em design gráfico e deixa o projeto inteiro autoral, não só o código e a fase.

## Consequências

- Cada recurso tem seus valores em `ScriptableObject`, para ajustar durante os playtests.
- O pacote `com.unity.2d.aseprite` (já no projeto) importa os arquivos `.aseprite` direto, com camadas e animações, sem exportar PNG.
- Os arquivos `.aseprite` vão para o Git LFS (já configurado no `.gitattributes`).
- **Risco de escopo:** a arte própria soma tempo às 3 semanas. O blockout cinza permite desenhar a fase e testar o controle antes da arte, e a arte entra na Etapa 6 (polimento).
