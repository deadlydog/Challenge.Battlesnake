# Agents.md

## Project Overview

This is a C# implementation of a [Battlesnake](https://play.battlesnake.com) AI that competes in snake game battles. The snake makes movement decisions every turn by evaluating direction scores from multiple strategies.

## Architecture

### Three-Layer Design

- __`Battlesnake.WebApi`__: ASP.NET Core minimal API implementing Battlesnake HTTP endpoints (`/`, `/start`, `/move`, `/end`)
- __`Battlesnake.Domain`__: Core game logic with no external dependencies - contains the decision-making engine
- __`Battlesnake.Domain.Tests`__: xUnit tests using Shouldly for assertions

### Decision-Making Engine (`GameEngine.cs`)

The core pattern is __score-based strategy composition__:

1. Each movement strategy returns a `DirectionScores` object with scores for moving the player's snake Up/Down/Left/Right, where the highest score is the best move

### Movement Strategy Pattern

All strategies implement `IMovementStrategy` with a static abstract method:

```csharp
public interface IMovementStrategy
{
    static abstract DirectionScores CalculateDirectionScores(Board board);
}
```

### Board Representation

`Board` uses a 2D `BoardCell[,]` array where:

- __Coordinate system__: `(0,0)` is bottom-left, Y increases upward
- __Snake tracking__: First coordinate in body list is the head, last is the tail
- __Cell contents__: Enum `BoardCellContent` (Empty, Food, Hazard, SnakeBody, SnakeHead, SnakeTail)
- __Our snake__: Board tracks `OurSnake`, `OurSnakeHeadPosition`, `OurSnakeTailPosition` for quick access

### Testing Standards

- __Test framework__: xUnit v3 with Shouldly assertions (NOT FluentAssertions)
- __Naming__: `WhenSituationX_ThenOutcomeY` pattern
- __Structure__: Arrange-Act-Assert within each test, with an empty line between sections
- __Coverage__: Movement strategies have comprehensive edge case tests (corners, center, all walls)

### Project Configuration

- __Target framework__: .NET 10.0 (`net10.0`)
- __Solution file__: Uses new `.slnx` format (not `.sln`)
- __API ports__: HTTPS on 5001, HTTP on 5000

## Development Workflows

### Local Testing

Use Battlesnake CLI against the running API:

```powershell
dotnet run --project src/Battlesnake.WebApi
battlesnake play -W 11 -H 11 --name test_snake --url https://localhost:5001/ -g solo -v --delay 500 --browser
```

## Common Gotchas

- Boundary checks in strategies: Board array is 0-indexed, so right wall is at `Width - 1`, top wall at `Height - 1`
- Snake body coordinates are ordered: index 0 is head, last index is tail

## External Integration

- __Battlesnake API__: v1 spec from https://docs.battlesnake.com/api
