# Projeto 2: Fase de plataforma 2D

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
- Próximo: Etapa 1, one-pager. Antes dele, é preciso decidir a ordem de introdução das mecânicas, o comportamento de cada uma e os recursos de game feel.
- O namespace do código será definido junto com o nome do jogo.
