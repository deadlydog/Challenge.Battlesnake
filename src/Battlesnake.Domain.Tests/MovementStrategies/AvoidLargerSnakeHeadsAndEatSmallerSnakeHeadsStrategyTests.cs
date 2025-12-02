using Battlesnake.Domain.GameBoard;
using Battlesnake.Domain.MovementStrategies;
using Shouldly;

namespace Battlesnake.Domain.Tests.MovementStrategies;

public class AvoidLargerSnakeHeadsAndEatSmallerSnakeHeadsStrategyTests
{
	[Fact]
	public void WhenALargerSnakeIsClose_ThenPenaltyIsAppliedToAvoidOccupyingTheSameCell()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6),
			new Coordinate(5, 7)
		}, true);
		board.AddSnake("large_opponent", 100, new List<Coordinate>
		{
			new Coordinate(5, 3), // Head two spaces below player's head
			new Coordinate(5, 2),
			new Coordinate(5, 1),
			new Coordinate(5, 0)
		}, false);

		// Act
		var directionScores = AvoidLargerSnakeHeadsAndEatSmallerSnakeHeadsStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Down.ShouldBe(AvoidLargerSnakeHeadsAndEatSmallerSnakeHeadsStrategy.AvoidAdjacentLargerSnakeHeadScorePenalty);
		directionScores.Up.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenASmallerSnakeIsClose_ThenBonusIsAppliedToEncourageHeadbutting()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6),
			new Coordinate(5, 7),
			new Coordinate(5, 8)
		}, true);
		board.AddSnake("small_opponent", 100, new List<Coordinate>
		{
			new Coordinate(7, 5), // Head two spaces to the right of player's head
			new Coordinate(7, 4),
			new Coordinate(7, 3)
		}, false);

		// Act
		var directionScores = AvoidLargerSnakeHeadsAndEatSmallerSnakeHeadsStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Right.ShouldBe(AvoidLargerSnakeHeadsAndEatSmallerSnakeHeadsStrategy.AttackAdjacentSmallerSnakeHeadScoreBonus);
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
	}

	[Fact]
	public void WhenSameSizeSnakeIsClose_ThenPenaltyIsAppliedToAvoidOccupyingTheSameCell()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6),
			new Coordinate(5, 7)
		}, true);
		board.AddSnake("equal_opponent", 100, new List<Coordinate>
		{
			new Coordinate(3, 5), // Head two spaces to the left of player's head
			new Coordinate(3, 4),
			new Coordinate(3, 3)
		}, false);

		// Act
		var directionScores = AvoidLargerSnakeHeadsAndEatSmallerSnakeHeadsStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Left.ShouldBe(AvoidLargerSnakeHeadsAndEatSmallerSnakeHeadsStrategy.AvoidAdjacentLargerSnakeHeadScorePenalty);
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenMultipleSnakesAreClose_ThenAllRelevantScoreAdjustmentsAreApplied()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6),
			new Coordinate(5, 7),
			new Coordinate(5, 8)
		}, true);
		board.AddSnake("large_opponent", 100, new List<Coordinate>
		{
			new Coordinate(5, 3), // Larger head two spaces below player's head
			new Coordinate(5, 2),
			new Coordinate(5, 1),
			new Coordinate(5, 0),
			new Coordinate(6, 0)
		}, false);
		board.AddSnake("small_opponent", 100, new List<Coordinate>
		{
			new Coordinate(7, 5), // Smaller two spaces to the right of player's head
			new Coordinate(7, 4),
			new Coordinate(7, 3)
		}, false);
		board.AddSnake("same_size_opponent", 100, new List<Coordinate>
		{
			new Coordinate(3, 5), // Same size two spaces to the left of player's head
			new Coordinate(3, 4),
			new Coordinate(3, 3),
			new Coordinate(3, 2)
		}, false);

		// Act
		var directionScores = AvoidLargerSnakeHeadsAndEatSmallerSnakeHeadsStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Down.ShouldBe(AvoidLargerSnakeHeadsAndEatSmallerSnakeHeadsStrategy.AvoidAdjacentLargerSnakeHeadScorePenalty);
		directionScores.Right.ShouldBe(AvoidLargerSnakeHeadsAndEatSmallerSnakeHeadsStrategy.AttackAdjacentSmallerSnakeHeadScoreBonus);
		directionScores.Left.ShouldBe(AvoidLargerSnakeHeadsAndEatSmallerSnakeHeadsStrategy.AvoidAdjacentLargerSnakeHeadScorePenalty);
		directionScores.Up.ShouldBe(0);
	}

	[Fact]
	public void WhenNoSnakesAreClose_ThenNoScoreAdjustmentsAreMade()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6),
			new Coordinate(5, 7)
		}, true);
		board.AddSnake("distant_opponent", 100, new List<Coordinate>
		{
			new Coordinate(0, 0), // Head far from player's head
			new Coordinate(0, 1),
			new Coordinate(0, 2)
		}, false);

		// Act
		var directionScores = AvoidLargerSnakeHeadsAndEatSmallerSnakeHeadsStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenNoOtherSnakesAreOnTheBoard_ThenNoScoreAdjustmentsAreMade()
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
		var directionScores = AvoidLargerSnakeHeadsAndEatSmallerSnakeHeadsStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}
}
