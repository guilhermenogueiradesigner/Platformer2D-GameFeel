# Projeto 2: MAX (fase de plataforma 2D)

Uma fase de 5 a 10 minutos que ensina, sem texto de tutorial, um pacote completo de movimentação de plataforma 2D: **pulo duplo, dash, gancho e grudar na parede**. O foco é game feel e level design que ensina pelo espaço.

## Stack

- Unity 6000.3.23f1, template Universal 2D (URP com 2D Renderer)
- Input System
- Tilemap para a fase

## Regras deste repositório

- Este repositório é público. Nada de informação pessoal neste arquivo nem em `docs/`.
- Código sem nenhum comentário. O porquê de cada escolha vai para `docs/decisions/`.
- Tudo que é nosso fica em `Assets/_Project/`.
- Valores de ajuste do controle ficam em `ScriptableObject`, nunca no código.
- Cada módulo em `Assets/_Project/Scripts/` tem seu próprio `.asmdef`.
- Nunca fazer commit, push ou qualquer operação de git. Avisar quais arquivos mudaram e sugerir a mensagem de commit.
- Documentação escrita junto com cada etapa. O índice fica em [docs/README.md](docs/README.md).

## Estado atual

- Etapa 0 (preparação) concluída.
- Ordem, comportamento das mecânicas e game feel decididos: ver `docs/decisions/0003` a `0005`.
- Arte com assets prontos, editados no Aseprite e importados com `com.unity.2d.aseprite` (`docs/decisions/0010`). Conferir a licença de cada pacote antes de versionar e registrar créditos em `docs/creditos.md`. Assets que não podem ser redistribuídos ficam em `Assets/ThirdParty/<Autor>/`, que está no `.gitignore`.
- Etapa 1 (one-pager) escrita: `docs/01-one-pager.md`. Max é um gato preto ninja, em pé e quadradinho, fugindo do esgoto. Mecânicas desbloqueadas aos poucos com retorno a áreas (metroidvania), recompensado por coletáveis simples. Sem inimigos nem combate; 5 corações; cada armadilha tira um e leva à última posição salva em cima de plataforma, com invulnerabilidade curta; sem corações, volta ao checkpoint da área com corações cheios. Escala da arte: `docs/decisions/0006`.
- Próximo: Etapa 2, GDD.
