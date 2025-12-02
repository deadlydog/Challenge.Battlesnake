using Battlesnake.Domain.GameBoard;
using Battlesnake.Domain.MovementStrategies;
using Shouldly;

namespace Battlesnake.Domain.Tests.MovementStrategies;

public class DoNotHitWallsStrategyTests
{
	[Fact]
	public void WallPenaltyScore_IsNegative()
	{
		DoNotHitWallsStrategy.AvoidHittingWallScorePenalty.ShouldBeLessThan(0);
	}

	[Fact]
	public void WhenSnakeIsNextToLeftWall_ThenScoresAreReducedForMovesTowardsWall()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(0, 5), // Head is next to the left wall
			new Coordinate(1, 5),
			new Coordinate(2, 5)
		}, true);

		// Act
		var directionScores = DoNotHitWallsStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(DoNotHitWallsStrategy.AvoidHittingWallScorePenalty); // Reduced score towards wall
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenSnakeIsNextToRightWall_ThenScoresAreReducedForMovesTowardsWall()
	{
		// Arrange
		var board = new Board(9, 9);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(8, 5), // Head is next to the right wall
			new Coordinate(7, 5),
			new Coordinate(6, 5)
		}, true);
		// Act
		var directionScores = DoNotHitWallsStrategy.CalculateDirectionScores(board);
		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(DoNotHitWallsStrategy.AvoidHittingWallScorePenalty); // Reduced score towards wall
	}

	[Fact]
	public void WhenSnakeIsNextToTopWall_ThenScoresAreReducedForMovesTowardsWall()
	{
		// Arrange
		var board = new Board(10, 10);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 9), // Head is next to the top wall
			new Coordinate(5, 8),
			new Coordinate(5, 7)
		}, true);
		// Act
		var directionScores = DoNotHitWallsStrategy.CalculateDirectionScores(board);
		// Assert
		directionScores.Up.ShouldBe(DoNotHitWallsStrategy.AvoidHittingWallScorePenalty); // Reduced score towards wall
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenSnakeIsNextToBottomWall_ThenScoresAreReducedForMovesTowardsWall()
	{
		// Arrange
		var board = new Board(7, 7);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 0), // Head is next to the bottom wall
			new Coordinate(5, 1),
			new Coordinate(5, 2)
		}, true);
		// Act
		var directionScores = DoNotHitWallsStrategy.CalculateDirectionScores(board);
		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(DoNotHitWallsStrategy.AvoidHittingWallScorePenalty); // Reduced score towards wall
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenSnakeIsInCenter_ThenNoScoresAreReduced()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head is in the center
			new Coordinate(5, 4),
			new Coordinate(5, 3)
		}, true);
		// Act
		var directionScores = DoNotHitWallsStrategy.CalculateDirectionScores(board);
		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenSnakeIsInBottomLeftCorner_ThenScoresAreReducedForBothWalls()
	{
		// Arrange
		var board = new Board(10, 10);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(0, 0), // Head is in the bottom-left corner
			new Coordinate(0, 1),
			new Coordinate(0, 2)
		}, true);
		// Act
		var directionScores = DoNotHitWallsStrategy.CalculateDirectionScores(board);
		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(DoNotHitWallsStrategy.AvoidHittingWallScorePenalty); // Reduced score towards bottom wall
		directionScores.Left.ShouldBe(DoNotHitWallsStrategy.AvoidHittingWallScorePenalty); // Reduced score towards left wall
		directionScores.Right.ShouldBe(0);
	}
}
