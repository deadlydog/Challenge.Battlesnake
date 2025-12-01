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
}
