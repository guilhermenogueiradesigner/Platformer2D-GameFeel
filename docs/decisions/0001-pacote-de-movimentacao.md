# 0001. Pacote de movimentação e perspectiva 2D

**Data:** 2026-11-21 · **Status:** aceita

## Contexto

A fase precisa ensinar mecânicas sem texto de tutorial, só com o espaço. A escolha das mecânicas define o que o projeto consegue provar sobre level design e game feel.

## Opções consideradas

- **Pulo duplo + dash:** fácil de entender, boa para ritmo, mas comum e com pouco destaque.
- **Manipular gravidade ou objetos:** combina com ensinar pelo ambiente, mas a física traz casos difíceis e mais risco de escopo.
- **Gancho/corda:** forte no vídeo e com muitas variações, mas com implementação e câmera mais complexas.
- **Pacote completo (pulo duplo, dash, gancho e parede):** junta as forças das opções acima, com mais trabalho.

## Decisão

Pacote completo de movimentação de plataforma 2D: **pulo duplo, dash, gancho e grudar na parede**, em perspectiva 2D, no estilo de metroidvanias e plataformas de precisão.

## Por quê

Um pacote de movimentação mostra game feel de forma completa, e não só uma mecânica isolada. As quatro mecânicas interagem entre si, o que dá material para uma curva de aprendizado com variações e um desafio final que combina tudo. O 2D deixa a leitura do espaço mais clara e reduz o risco de câmera.

## Consequências

- A qualidade do controle pesa tanto quanto o desenho da fase.
- O personagem precisa de uma máquina de estados (chão, ar, dash, parede, gancho).
- A fase usa Tilemap em vez de ProBuilder.
- Quatro mecânicas em 5 a 10 minutos e 3 semanas é um escopo apertado. Se necessário, as mecânicas mais simples recebem menos tempo de fase.
