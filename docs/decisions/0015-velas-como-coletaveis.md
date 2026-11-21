# 0015. Velas como coletáveis

**Data:** 2026-11-21 · **Status:** aceita

## Contexto

Os coletáveis recompensam a exploração e o retorno a áreas (decisão 0009). Faltava decidir o que eles são, quantos existem e como aparecem.

## Decisão

- O coletável é uma **vela**. Combina com a *jack-o'-lantern*, que tem uma vela dentro.
- **5 velas**, uma por área, cada uma escondida num lugar que só se alcança com a habilidade **seguinte** à daquela área.
- Cada vela coletada deixa a luz da cabeça do Jack **um pouco mais forte**. É só visual: não muda nenhuma regra.
- A tela de fim mostra quantas velas foram coletadas (por exemplo, 3/5).

## Por quê

A vela tem luz própria, então é visível de longe no escuro e funciona como isca: o jogador vê uma vela fora de alcance e entende que vai voltar ali. O brilho mais forte é uma recompensa visível sem criar um sistema novo.

## Consequências

- A tabela de áreas do GDD define onde fica cada vela.
- A UI mostra a contagem de velas ao lado dos corações.
- O aumento do brilho tem um limite, para a luz não tirar a penumbra da qual o level design depende.
