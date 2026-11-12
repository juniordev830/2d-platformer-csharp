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

- `Program.cs` — entrada da aplicação
- `GameSimulation.cs` — loop principal da simulação
- `Player.cs` — lógica do player
- `Vector2D.cs` — tipo matemático de vetor
- `Rect.cs` — tipo de colisão
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
- A lógica do jogo está dividida em componentes menores, melhorando leitura e manutenção.

## Limitações conhecidas

- O código é voltado para protótipo e não para produto de alto porte.
- O workflow de CI executa build e testes em pushes e pull requests.
- A suíte de testes cobre vetores, colisões AABB, validação de dimensões, gravidade, aterrissagem e salto.
- O uso de `float` é adequado ao protótipo, mas pode exigir validações adicionais em cenários mais exigentes.

## Próxima evolução recomendada

A próxima etapa mais útil é manter a suíte de testes automatizados para regressões de comportamento e desempenho.

## Validação e manutenção

Consulte o [guia técnico de validação](docs/GUIA_DE_VALIDACAO.md) antes de alterar o código: ele reúne os comandos, casos de regressão e limites atuais do projeto.
