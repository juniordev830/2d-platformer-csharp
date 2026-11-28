# Engenharia reversa e estudo — 2D Platformer C#

## Objetivo e fluxo

Simulação 2D de movimento, gravidade e colisões AABB. Program chama GameSimulation, que usa Player e os tipos Vector2D e Rect. Observe onde velocidade muda e como o pouso é decidido.

**Tecnologias identificadas** · C#, .NET 8.

**Revisão de referência** · `0d5f1af4ba7b`.

## Mapa do código

| Caminho | Papel de leitura | Símbolos selecionados |
| --- | --- | --- |
| [GameSimulation.cs](../GameSimulation.cs) | Implementação | `PlatformerGame`, `GameSimulation`, `Run` |
| [Player.cs](../Player.cs) | Implementação | `PlatformerGame`, `Player`, `Jump`, `MoveHorizontal`, `ApplyGravity`, `TryLandOn` |
| [Program.cs](../Program.cs) | Implementação | `PlatformerGame`, `Program`, `Main` |
| [Rect.cs](../Rect.cs) | Implementação | `PlatformerGame`, `Rect`, `CreateUnchecked`, `Intersects` |
| [Vector2D.cs](../Vector2D.cs) | Implementação | `PlatformerGame`, `Vector2D`, `Normalize` |

## Configuração e referências

Ambiente e dependências: [PlatformerGame.csproj](../PlatformerGame.csproj), [tests/PlatformerGame.Tests/PlatformerGame.Tests.csproj](../tests/PlatformerGame.Tests/PlatformerGame.Tests.csproj).

Referências existentes: [docs/GUIA_DE_VALIDACAO.md](GUIA_DE_VALIDACAO.md).

## Roteiro de estudo e desenvolvimento

1. Comece pelo fluxo descrito acima e localize seus pontos de entrada no mapa. Acompanhe uma entrada válida até a transformação ou a resposta.
2. Abra um teste relacionado ao fluxo e descreva qual regra ele protege. Compare o tratamento esperado para entradas válidas, inválidas e nos limites.
3. Ao adicionar uma regra, determine qual módulo controla o dado e cubra o caso nesse nível. Em seguida, confira o efeito no comando, na API ou na interface de entrada.

**Pergunta de entrevista**

Como separaria um erro de integração de movimento de um erro no teste de colisão?

## Testes versionados

Arquivos para começar a leitura dos cenários de teste: [tests/PlatformerGame.Tests/JumpBufferTests.cs](../tests/PlatformerGame.Tests/JumpBufferTests.cs), [tests/PlatformerGame.Tests/PhysicsTests.cs](../tests/PlatformerGame.Tests/PhysicsTests.cs).

Os comandos e versões de runtime usados no CI ficam em [.github/workflows/ci.yml](../.github/workflows/ci.yml), [.github/workflows/seguranca-workflows.yml](../.github/workflows/seguranca-workflows.yml).

[Voltar ao README](../README.md).
