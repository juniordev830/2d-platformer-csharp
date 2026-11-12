# Guia de manutenção — 2D Platformer C#

## Escopo confirmado

Protótipo de física de plataforma 2D em console e .NET 8, não um jogo gráfico completo.

## Arquivos principais

`GameSimulation.cs`, `Player.cs`, `Vector2D.cs`, `Rect.cs`, `Program.cs` e `tests/PlatformerGame.Tests/PhysicsTests.cs`.

## Validar uma alteração

No diretório raiz, usar os comandos adequados ao ambiente: `dotnet build PlatformerGame.csproj` e `dotnet test tests/PlatformerGame.Tests/PlatformerGame.Tests.csproj`.

Registrar o resultado dos testes no PR e conferir se a execução do GitHub Actions corresponde ao commit revisado. Não presumir que a interface ou o serviço é de produção apenas porque o build passou.

## Revisão de casos críticos

Cobrir gravidade e salto com passos de tempo variáveis, colisão por bordas/cantos, velocidades extremas e penetração em blocos.

## Limites e próxima entrega

Prioridade: definir tolerâncias numéricas e testes determinísticos de colisões antes de integrar motor de renderização.

### Checklist para PR

- [ ] A mudança está associada a um comportamento identificável.
- [ ] Foram documentados os casos novos e os limites encontrados.
- [ ] Há teste automatizado quando o módulo comporta execução reproduzível.
- [ ] Exemplos não contêm credenciais ou dados reais.
- [ ] A branch pode ser revisada antes de qualquer merge na principal.
