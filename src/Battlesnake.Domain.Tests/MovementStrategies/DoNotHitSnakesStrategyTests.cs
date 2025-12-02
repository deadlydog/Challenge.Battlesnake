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
	public void WhenSnakeIsInTopLeftCornerFacingRight_ThenScoresAreReducedForMovesTowardsBody()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(1, 10), // Head near top-left corner
			new Coordinate(0, 10), // Body to the left of head
			new Coordinate(0, 9),
			new Coordinate(0, 8),
			new Coordinate(0, 7)
		}, true);

		// Act
		var directionScores = DoNotHitSnakesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Left.ShouldBe(DoNotHitSnakesStrategy.AvoidHittingSnakeBodyScorePenalty);
		directionScores.Down.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenSnakeIsInTopLeftCornerFacingDown_ThenScoresAreReducedForMovesTowardsBody()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(0, 9), // Head near top-left corner
			new Coordinate(0, 10), // Body above head
			new Coordinate(1, 10),
			new Coordinate(2, 10),
			new Coordinate(3, 10)
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
	public void WhenSnakeIsInBottomLeftCornerFacingRight_ThenScoresAreReducedForMovesTowardsBody()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(1, 0), // Head near bottom-left corner
			new Coordinate(0, 0), // Body to the left of head
			new Coordinate(0, 1),
			new Coordinate(0, 2),
			new Coordinate(0, 3)
		}, true);

		// Act
		var directionScores = DoNotHitSnakesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Left.ShouldBe(DoNotHitSnakesStrategy.AvoidHittingSnakeBodyScorePenalty);
		directionScores.Down.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenSnakeIsInBottomLeftCornerFacingUp_ThenScoresAreReducedForMovesTowardsBody()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(0, 1), // Head near bottom-left corner
			new Coordinate(0, 0), // Body below head
			new Coordinate(1, 0),
			new Coordinate(2, 0),
			new Coordinate(3, 0)
		}, true);

		// Act
		var directionScores = DoNotHitSnakesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Down.ShouldBe(DoNotHitSnakesStrategy.AvoidHittingSnakeBodyScorePenalty);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenSnakeIsInBottomRightCornerFacingLeft_ThenScoresAreReducedForMovesTowardsBody()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(9, 0), // Head near bottom-right corner
			new Coordinate(10, 0), // Body to the right of head
			new Coordinate(10, 1),
			new Coordinate(10, 2),
			new Coordinate(10, 3)
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
	public void WhenSnakeIsInBottomRightCornerFacingUp_ThenScoresAreReducedForMovesTowardsBody()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(10, 1), // Head near bottom-right corner
			new Coordinate(10, 0), // Body below head
			new Coordinate(9, 0),
			new Coordinate(8, 0),
			new Coordinate(7, 0)
		}, true);
		// Act
		var directionScores = DoNotHitSnakesStrategy.CalculateDirectionScores(board);
		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Down.ShouldBe(DoNotHitSnakesStrategy.AvoidHittingSnakeBodyScorePenalty);
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
