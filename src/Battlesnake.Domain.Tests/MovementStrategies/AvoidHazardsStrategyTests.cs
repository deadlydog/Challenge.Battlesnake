using Battlesnake.Domain.GameBoard;
using Battlesnake.Domain.MovementStrategies;
using Shouldly;

namespace Battlesnake.Domain.Tests.MovementStrategies;

public class AvoidHazardsStrategyTests
{
	[Fact]
	public void WhenOneHazardIsAdjacent_ThenPenaltyIsAppliedToAvoidIt()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6),
			new Coordinate(5, 7)
		}, true);
		board.AddHazard(5, 4); // Down

		// Act
		var directionScores = AvoidHazardsStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Down.ShouldBe(AvoidHazardsStrategy.AvoidHazardScorePenalty);
		directionScores.Up.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenHazardsAreAdjacent_ThenPenaltiesAreAppliedToAvoidThem()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6),
			new Coordinate(5, 7)
		}, true);
		board.AddHazard(5, 4); // Down
		board.AddHazard(6, 5); // Right

		// Act
		var directionScores = AvoidHazardsStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Down.ShouldBe(AvoidHazardsStrategy.AvoidHazardScorePenalty);
		directionScores.Right.ShouldBe(AvoidHazardsStrategy.AvoidHazardScorePenalty);
		directionScores.Up.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
	}

	[Fact]
	public void WhenNoHazardsAreAdjacent_ThenNoPenaltiesAreApplied()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6),
			new Coordinate(5, 7)
		}, true);
		board.AddHazard(0,0); // Far from head

		// Act
		var directionScores = AvoidHazardsStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenNoHazardsExist_ThenNoPenaltiesAreApplied()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6),
			new Coordinate(5, 7)
		}, true);

		// Act
		var directionScores = AvoidHazardsStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenHazardIsInTopLeftCorner_ThenPenaltyIsAppliedToAvoidIt()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(0, 9), // Head
			new Coordinate(0, 8),
			new Coordinate(0, 7)
		}, true);
		board.AddHazard(0, 10); // Hazard directly above head

		// Act
		var directionScores = AvoidHazardsStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(AvoidHazardsStrategy.AvoidHazardScorePenalty);
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenHazardIsInTopRightCorner_ThenPenaltyIsAppliedToAvoidIt()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(10, 9), // Head
			new Coordinate(10, 8),
			new Coordinate(10, 7)
		}, true);
		board.AddHazard(10, 10); // Hazard directly above head

		// Act
		var directionScores = AvoidHazardsStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(AvoidHazardsStrategy.AvoidHazardScorePenalty);
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenHazardIsInBottomLeftCorner_ThenPenaltyIsAppliedToAvoidIt()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(0, 1), // Head
			new Coordinate(0, 2),
			new Coordinate(0, 3)
		}, true);
		board.AddHazard(0, 0); // Hazard directly below head

		// Act
		var directionScores = AvoidHazardsStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Down.ShouldBe(AvoidHazardsStrategy.AvoidHazardScorePenalty);
		directionScores.Up.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenHazardIsInBottomRightCorner_ThenPenaltyIsAppliedToAvoidIt()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(10, 1), // Head
			new Coordinate(10, 2),
			new Coordinate(10, 3)
		}, true);
		board.AddHazard(10, 0); // Hazard directly below head

		// Act
		var directionScores = AvoidHazardsStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Down.ShouldBe(AvoidHazardsStrategy.AvoidHazardScorePenalty);
		directionScores.Up.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenSnakeIsByHazardsAtTopAndRightBoardEdges_ThenPenaltyIsAppliedToAvoidIt()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(9, 9), // Head
			new Coordinate(8, 9),
			new Coordinate(7, 9)
		}, true);
		board.AddHazard(9, 10); // Hazard directly above head
		board.AddHazard(10, 9); // Hazard directly right of head

		// Act
		var directionScores = AvoidHazardsStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(AvoidHazardsStrategy.AvoidHazardScorePenalty);
		directionScores.Right.ShouldBe(AvoidHazardsStrategy.AvoidHazardScorePenalty);
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
	}

	[Fact]
	public void WhenSnakeIsByHazardsAtBottomAndRightEdges_ThenPenaltyIsAppliedToAvoidIt()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(1, 1), // Head
			new Coordinate(2, 1),
			new Coordinate(3, 1)
		}, true);
		board.AddHazard(0, 1); // Hazard directly left of head
		board.AddHazard(1, 0); // Hazard directly below head

		// Act
		var directionScores = AvoidHazardsStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Left.ShouldBe(AvoidHazardsStrategy.AvoidHazardScorePenalty);
		directionScores.Down.ShouldBe(AvoidHazardsStrategy.AvoidHazardScorePenalty);
		directionScores.Up.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}
}
