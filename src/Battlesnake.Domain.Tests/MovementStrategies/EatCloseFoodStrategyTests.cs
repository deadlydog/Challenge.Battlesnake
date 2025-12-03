using Shouldly;
using Battlesnake.Domain.GameBoard;
using Battlesnake.Domain.MovementStrategies;

namespace Battlesnake.Domain.Tests.MovementStrategies;

public class EatCloseFoodStrategyTests
{
	[Fact]
	public void WhenFoodIsDirectlyAdjacent_ThenScoresAreIncreasedForMovesTowardsFood()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6),
			new Coordinate(5, 7)
		}, true);
		board.AddFood(5, 4); // Food directly below head

		// Act
		var directionScores = EatCloseFoodStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(EatCloseFoodStrategy.EatFoodScoreBoost); // Increased score towards food
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenFoodIsNotAdjacent_ThenNoScoresAreIncreased()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6),
			new Coordinate(5, 7)
		}, true);
		board.AddFood(0, 0); // Food far from head
		// Act
		var directionScores = EatCloseFoodStrategy.CalculateDirectionScores(board);
		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenMultipleFoodItemsAreAdjacent_ThenScoresAreIncreasedForAllDirectionsTowardsFood()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6),
			new Coordinate(5, 7)
		}, true);
		board.AddFood(5, 4); // Food directly below head
		board.AddFood(4, 5); // Food directly to the left of head
		// Act
		var directionScores = EatCloseFoodStrategy.CalculateDirectionScores(board);
		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(EatCloseFoodStrategy.EatFoodScoreBoost); // Increased score towards food below
		directionScores.Left.ShouldBe(EatCloseFoodStrategy.EatFoodScoreBoost); // Increased score towards food left
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenSnakeIsByFoodAtTopLeftCorner_ThenScoreIsIncreasedForMoveTowardsIt()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(0, 9), // Head
			new Coordinate(0, 8),
			new Coordinate(0, 7)
		}, true);
		board.AddFood(0, 10); // Food directly aboe head

		// Act
		var directionScores = EatCloseFoodStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(EatCloseFoodStrategy.EatFoodScoreBoost); // Increased score towards food above
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenSnakeIsByFoodAtTopRightCorner_ThenScoreIsIncreasedForMoveTowardsIt()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(10, 9), // Head
			new Coordinate(10, 8),
			new Coordinate(10, 7)
		}, true);
		board.AddFood(10, 10); // Food directly above head

		// Act
		var directionScores = EatCloseFoodStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(EatCloseFoodStrategy.EatFoodScoreBoost); // Increased score towards food above
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenSnakeIsByFoodAtBottomLeftCorner_ThenScoreIsIncreasedForMoveTowardsIt()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(0, 1), // Head
			new Coordinate(0, 2),
			new Coordinate(0, 3)
		}, true);
		board.AddFood(0, 0); // Food directly below head

		// Act
		var directionScores = EatCloseFoodStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Down.ShouldBe(EatCloseFoodStrategy.EatFoodScoreBoost); // Increased score towards food below
		directionScores.Up.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenSnakeIsByFoodAtBottomRightCorner_ThenScoreIsIncreasedForMoveTowardsIt()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(10, 1), // Head
			new Coordinate(10, 2),
			new Coordinate(10, 3)
		}, true);
		board.AddFood(10, 0); // Food directly below head

		// Act
		var directionScores = EatCloseFoodStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Down.ShouldBe(EatCloseFoodStrategy.EatFoodScoreBoost); // Increased score towards food below
		directionScores.Up.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenSnakeIsByFoodAtTopAndRightEdges_ThenScoreIsIncreasedForMoveTowardsIt()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(9, 9), // Head
			new Coordinate(9, 8),
			new Coordinate(9, 7)
		}, true);
		board.AddFood(10, 9); // Food directly to the right of head
		board.AddFood(9, 10); // Food directly above head

		// Act
		var directionScores = EatCloseFoodStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(EatCloseFoodStrategy.EatFoodScoreBoost); // Increased score towards food above
		directionScores.Right.ShouldBe(EatCloseFoodStrategy.EatFoodScoreBoost); // Increased score towards food right
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
	}

	[Fact]
	public void WhenSnakeIsByFoodAtLeftAndBottomEdges_ThenScoreIsIncreasedForMoveTowardsIt()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(1, 1), // Head
			new Coordinate(1, 2),
			new Coordinate(1, 3)
		}, true);
		board.AddFood(0, 1); // Food directly to the left of head
		board.AddFood(1, 0); // Food directly below head

		// Act
		var directionScores = EatCloseFoodStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Down.ShouldBe(EatCloseFoodStrategy.EatFoodScoreBoost); // Increased score towards food below
		directionScores.Left.ShouldBe(EatCloseFoodStrategy.EatFoodScoreBoost); // Increased score towards food left
		directionScores.Up.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}
}
