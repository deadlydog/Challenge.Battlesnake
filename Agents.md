# Agents.md

## Project Overview

This is a C# implementation of a [Battlesnake](https://play.battlesnake.com) AI that competes in snake game battles. The snake makes movement decisions every turn by evaluating direction scores from multiple strategies.

## Architecture

### Three-Layer Design

- **`Battlesnake.WebApi`**: ASP.NET Core minimal API implementing Battlesnake HTTP endpoints (`/`, `/start`, `/move`, `/end`)
- **`Battlesnake.Domain`**: Core game logic with no external dependencies - contains the decision-making engine
- **`Battlesnake.Domain.Tests`**: xUnit tests using Shouldly for assertions

### Decision-Making Engine (`GameEngine.cs`)

The core pattern is **score-based strategy composition**:

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

- **Coordinate system**: `(0,0)` is bottom-left, Y increases upward
- **Snake tracking**: First coordinate in body list is the head, last is the tail
- **Cell contents**: Enum `BoardCellContent` (Empty, Food, Hazard, SnakeBody, SnakeHead, SnakeTail)
- **Our snake**: Board tracks `OurSnake`, `OurSnakeHeadPosition`, `OurSnakeTailPosition` for quick access

### Testing Standards

- **Test framework**: xUnit v3 with Shouldly assertions (NOT FluentAssertions)
- **Naming**: `WhenSituationX_ThenOutcomeY` pattern
- **Coverage**: Movement strategies have comprehensive edge case tests (corners, center, all walls)

### Project Configuration

- **Target framework**: .NET 10.0 (`net10.0`)
- **Solution file**: Uses new `.slnx` format (not `.sln`)
- **API ports**: HTTPS on 5001, HTTP on 5000

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

- **Battlesnake API**: v1 spec from https://docs.battlesnake.com/api
