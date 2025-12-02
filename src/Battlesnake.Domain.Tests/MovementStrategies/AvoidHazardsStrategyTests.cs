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
}
