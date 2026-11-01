# 2D Platformer C#

## Visão geral

Este repositório contém um protótipo de engine 2D com física básica, gravidade, salto e detecção de colisão por bounding box (AABB). O projeto foi implementado em C# com .NET 8 e serve como estudo de mecânica de jogo, otimização de hot path e lógica de colisão.

## Tecnologias e arquitetura

- Linguagem: C#
- Runtime: .NET 8
- Aplicação: console app (`OutputType=Exe`)
- Estruturas principais:
  - `Vector2D` — vetor 2D imutável para física e movimentação
  - `Rect` — retângulo para colisão e limites
  - `Player` — posicionamento, velocidade, salto e estado do personagem
  - `GameSimulation` — fluxo principal de execução da simulação

## Estrutura do projeto

- `Program.cs` — tipos matemáticos, lógica do jogo e simulação do loop principal
- `PlatformerGame.csproj` — configuração do projeto .NET
- `.gitignore` — arquivos locais gerados pelo ambiente e IDE
- `README.md` — documentação do projeto
- `docs/AUDITORIA_TECNICA.md` — relatório técnico de auditoria
- `tests/PlatformerGame.Tests/` — testes automatizados da lógica principal

## Pré-requisitos

- .NET SDK 8.0 ou superior
- Ambiente compatível com .NET 8

## Como executar

```bash
dotnet build
dotnet run --project PlatformerGame.csproj
```

## Como executar os testes

```bash
dotnet test tests/PlatformerGame.Tests/PlatformerGame.Tests.csproj
```

## Observações importantes

- O projeto não expõe autenticação, APIs HTTP, banco de dados, credenciais ou serviços externos.
- Não há dados sensíveis ou segredos de produção no contexto atual.
- A lógica do jogo está concentrada em um único arquivo, o que facilita leitura e estudo, mas reduz modularização.

## Limitações conhecidas

- O código é voltado para protótipo e não para produto de alto porte.
- Não há pipeline de CI ou análise estática configurada no contexto atual.
- A cobertura automatizada era inexistente antes desta revisão.
- O uso de `float` é adequado ao protótipo, mas pode exigir validações adicionais em cenários mais exigentes.

## Próxima evolução recomendada

A próxima etapa mais útil é separar a lógica em componentes menores e manter a suíte de testes automatizados para regressões de comportamento e desempenho.
