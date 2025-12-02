using Battlesnake.Domain.GameBoard;
using Battlesnake.Domain.MovementStrategies;
using Shouldly;

namespace Battlesnake.Domain.Tests.MovementStrategies;

public class DoNotHitSnakesStrategyTests
{
	[Fact]
	public void PenaltyScores_AreNegative()
	{
		DoNotHitSnakesStrategy.AvoidHittingSnakeBodyScorePenalty.ShouldBeLessThan(0);
		DoNotHitSnakesStrategy.AvoidHittingSnakeTailScorePenalty.ShouldBeLessThan(0);

		// Score for hitting body should be more severe than hitting tail.
		DoNotHitSnakesStrategy.AvoidHittingSnakeBodyScorePenalty.ShouldBeLessThan(DoNotHitSnakesStrategy.AvoidHittingSnakeTailScorePenalty);
	}

	[Fact]
	public void WhenSnakeIsNextToItsOwnBody_ThenScoresAreReducedForMovesTowardsBody()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6), // Body directly above head
			new Coordinate(5, 7)
		}, true);

		// Act
		var directionScores = DoNotHitSnakesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(DoNotHitSnakesStrategy.AvoidHittingSnakeBodyScorePenalty); // Reduced score towards own body
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenSnakeIsInTopRightCornerFacingLeft_ThenScoresAreReducedForMovesTowardsBody()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(9, 10), // Head near top-right corner
			new Coordinate(10, 10), // Body to the right of head
			new Coordinate(10, 9),
			new Coordinate(10, 8),
			new Coordinate(10, 7)
		}, true);

		// Act
		var directionScores = DoNotHitSnakesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Down.ShouldBe(0);
		directionScores.Right.ShouldBe(DoNotHitSnakesStrategy.AvoidHittingSnakeBodyScorePenalty);
	}

	[Fact]
	public void WhenSnakeIsInTopRightCornerFacingDown_ThenScoresAreReducedForMovesTowardsBody()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(10, 9), // Head near top-right corner
			new Coordinate(10, 10), // Body above head
			new Coordinate(9, 10),
			new Coordinate(8, 10),
			new Coordinate(7, 10)
		}, true);

		// Act
		var directionScores = DoNotHitSnakesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(DoNotHitSnakesStrategy.AvoidHittingSnakeBodyScorePenalty);
		directionScores.Left.ShouldBe(0);
		directionScores.Down.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenHeadIsNextToAnotherSnakeBody_ThenScoresAreReducedForMovesTowardsSnakeBody()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6), // Player's own body is directly above head
			new Coordinate(5, 7)
		}, true);
		board.AddSnake("opponent", 100, new List<Coordinate>
		{
			new Coordinate(4, 4),
			new Coordinate(5, 4), // Opponent's body is directly below player's head
			new Coordinate(6, 4)
		}, false);

		// Act
		var directionScores = DoNotHitSnakesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(DoNotHitSnakesStrategy.AvoidHittingSnakeBodyScorePenalty); // Reduced score towards own body
		directionScores.Down.ShouldBe(DoNotHitSnakesStrategy.AvoidHittingSnakeBodyScorePenalty); // Reduced score towards opponent's body
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenHeadIsSurroundedByOtherSnakeExceptToTheRight_ThenScoresForAllDirectionsExceptRightAreReduced()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6), // Own body is directly above head
			new Coordinate(5, 7)
		}, true);
		board.AddSnake("opponent", 100, new List<Coordinate>
		{
			new Coordinate(4, 4), // Opponent's body is directly below head
			new Coordinate(5, 4),
			new Coordinate(6, 4)
		}, false);
		board.AddSnake("opponent2", 100, new List<Coordinate>
		{
			new Coordinate(4, 5), // Opponent's head is directly to the left of head
			new Coordinate(4, 6),
			new Coordinate(4, 7)
		}, false);

		// Act
		var directionScores = DoNotHitSnakesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(DoNotHitSnakesStrategy.AvoidHittingSnakeBodyScorePenalty);
		directionScores.Down.ShouldBe(DoNotHitSnakesStrategy.AvoidHittingSnakeBodyScorePenalty);
		directionScores.Left.ShouldBe(DoNotHitSnakesStrategy.AvoidHittingSnakeBodyScorePenalty);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenHeadIsSurroundedBySnakesAndOneIsATail_ItShouldGiveTheTailDirectionTheHighestRanking()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6), // Own body is directly above head
			new Coordinate(5, 7)
		}, true);
		board.AddSnake("opponent", 100, new List<Coordinate>
		{
			new Coordinate(4, 4), // Opponent's body is directly below head
			new Coordinate(5, 4),
			new Coordinate(6, 4)
		}, false);
		board.AddSnake("opponent2", 100, new List<Coordinate>
		{
			new Coordinate(4, 5), // Opponent's head is directly to the left of head
			new Coordinate(4, 6),
			new Coordinate(4, 7)
		}, false);
		board.AddSnake("opponent3", 100, new List<Coordinate>
		{
			new Coordinate(8, 5),
			new Coordinate(7, 5),
			new Coordinate(6, 5) // Opponent's tail is directly to the right of head
		}, false);

		// Act
		var directionScores = DoNotHitSnakesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(DoNotHitSnakesStrategy.AvoidHittingSnakeBodyScorePenalty);
		directionScores.Down.ShouldBe(DoNotHitSnakesStrategy.AvoidHittingSnakeBodyScorePenalty);
		directionScores.Left.ShouldBe(DoNotHitSnakesStrategy.AvoidHittingSnakeBodyScorePenalty);
		directionScores.Right.ShouldBe(DoNotHitSnakesStrategy.AvoidHittingSnakeTailScorePenalty); // Less severe penalty for tail
	}
}
