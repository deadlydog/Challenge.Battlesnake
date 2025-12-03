using Battlesnake.Domain.GameBoard;
using Battlesnake.Domain.MovementStrategies;
using Shouldly;

namespace Battlesnake.Domain.Tests.MovementStrategies;

public class MoveTowardsFoodStrategyTests
{
	[Fact]
	public void WhenFoodIsDirectlyAbove_ThenScoresAreIncreasedForMoveUp()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 4),
			new Coordinate(5, 3)
		}, true);
		board.AddSnake("opponent", 100, new List<Coordinate>
		{
			new Coordinate(0, 0), // Opponent head
			new Coordinate(0, 1),
			new Coordinate(0, 2)
		}, false);
		board.AddFood(5, 7); // Food directly above head

		// Act
		var directionScores = MoveTowardsFoodStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBeGreaterThan(0); // Increased score towards food
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenFoodIsBothDirectlyAboveAndDirectlyBelow_ThenScoresAreIncreasedForBothDirections()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 4),
			new Coordinate(5, 3)
		}, true);
		board.AddSnake("opponent", 100, new List<Coordinate>
		{
			new Coordinate(0, 0), // Opponent head
			new Coordinate(0, 1),
			new Coordinate(0, 2)
		}, false);
		board.AddFood(5, 7); // Food directly above head
		board.AddFood(5, 1); // Food directly below head

		// Act
		var directionScores = MoveTowardsFoodStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBeGreaterThan(0); // Increased score towards food above
		directionScores.Down.ShouldBeGreaterThan(0); // Increased score towards food below
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
		directionScores.Up.ShouldBeGreaterThan(directionScores.Down); // Score towards closer food should be higher
	}

	[Fact]
	public void WhenFoodIsDiagonallyPositioned_ThenScoresAreIncreasedForBothRelevantDirections()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 4),
			new Coordinate(5, 3)
		}, true);
		board.AddSnake("opponent", 100, new List<Coordinate>
		{
			new Coordinate(0, 0), // Opponent head
			new Coordinate(0, 1),
			new Coordinate(0, 2)
		}, false);
		board.AddFood(7, 7); // Food diagonally positioned (up-right) from head

		// Act
		var directionScores = MoveTowardsFoodStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBeGreaterThan(0); // Increased score towards food
		directionScores.Right.ShouldBeGreaterThan(0); // Increased score towards food
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Up.ShouldBe(directionScores.Right); // Scores should be equal due to equal distance from head
	}

	[Fact]
	public void WhenFoodIsSlightlyDiagonallyPositioned_ThenScoresAreCalculatedBasedOnDistance()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 4),
			new Coordinate(5, 3)
		}, true);
		board.AddSnake("opponent", 100, new List<Coordinate>
		{
			new Coordinate(0, 0), // Opponent head
			new Coordinate(0, 1),
			new Coordinate(0, 2)
		}, false);
		board.AddFood(6, 8); // Food slightly diagonally positioned (up-right) from head

		// Act
		var directionScores = MoveTowardsFoodStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBeGreaterThan(0); // Increased score towards food
		directionScores.Right.ShouldBeGreaterThan(0); // Increased score towards food
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBeGreaterThan(directionScores.Up); // Right score should be higher due to closer distance
	}

	[Fact]
	public void WhenFoodIsFarAway_ThenScoresAreStillCalculatedCorrectly()
	{
		// Arrange
		var board = new Board(20, 20);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(10, 10), // Head
			new Coordinate(10, 9),
			new Coordinate(10, 8)
		}, true);
		board.AddSnake("opponent", 100, new List<Coordinate>
		{
			new Coordinate(1, 0), // Opponent head
			new Coordinate(1, 1),
			new Coordinate(1, 2)
		}, false);
		board.AddFood(0, 0); // Food far from head (bottom-left)

		// Act
		var directionScores = MoveTowardsFoodStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Left.ShouldBeGreaterThan(0); // Increased score towards food
		directionScores.Down.ShouldBeGreaterThan(0); // Increased score towards food
		directionScores.Up.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
		directionScores.Left.ShouldBe(directionScores.Down); // Scores should be equal due to equal distance from head
	}

	[Fact]
	public void WhenNoFoodIsPresent_ThenAllScoresRemainZero()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 4),
			new Coordinate(5, 3)
		}, true);
		board.AddSnake("opponent", 100, new List<Coordinate>
		{
			new Coordinate(0, 0), // Opponent head
			new Coordinate(0, 1),
			new Coordinate(0, 2)
		}, false);

		// Act
		var directionScores = MoveTowardsFoodStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenMultipleFoodItemsArePresentAndEquallyDistantInOppositeDirections_ThenScoresShouldAllBeEqual()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 4),
			new Coordinate(5, 3)
		}, true);
		board.AddSnake("opponent", 100, new List<Coordinate>
		{
			new Coordinate(1, 0), // Opponent head
			new Coordinate(1, 1),
			new Coordinate(1, 2)
		}, false);
		board.AddFood(10, 10); // Food above and to the right
		board.AddFood(0, 0); // Food below and to the left

		// Act
		var directionScores = MoveTowardsFoodStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBeGreaterThan(0);
		directionScores.Right.ShouldBeGreaterThan(0);
		directionScores.Down.ShouldBeGreaterThan(0);
		directionScores.Left.ShouldBeGreaterThan(0);
		directionScores.Up.ShouldBe(directionScores.Down); // Scores should be equal due to symmetry
		directionScores.Left.ShouldBe(directionScores.Right); // Scores should be equal due to symmetry
	}

	[Fact]
	public void WhenOurSnakeIsSignificantlyLargerThanOpponentsAndHasHighHealth_ThenNoScoresAreAdded()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 4),
			new Coordinate(5, 3),
			new Coordinate(5, 2),
			new Coordinate(5, 1),
			new Coordinate(5, 0),
			new Coordinate(6, 0),
			new Coordinate(7, 0)
		}, true); // Our snake of length 8
		board.AddSnake("opponent", 100, new List<Coordinate>
		{
			new Coordinate(0, 0), // Opponent head
			new Coordinate(0, 1),
			new Coordinate(0, 2)
		}, false); // Opponent snake of length 3
		board.AddFood(5, 7); // Food above head

		// Act
		var directionScores = MoveTowardsFoodStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0); // No score added since our snake is significantly larger and healthy
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenOurSnakeIsSignificantlyLargerThanOpponentsButHasLowHealth_ThenScoresAreAdded()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", MoveTowardsFoodStrategy.ForceFoodAttractionWhenPlayerHealthIsBelow - 5, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 4),
			new Coordinate(5, 3),
			new Coordinate(5, 2),
			new Coordinate(5, 1),
			new Coordinate(5, 0),
			new Coordinate(6, 0),
			new Coordinate(7, 0)
		}, true); // Our snake of length 8
		board.AddSnake("opponent", 100, new List<Coordinate>
		{
			new Coordinate(0, 0), // Opponent head
			new Coordinate(0, 1),
			new Coordinate(0, 2)
		}, false); // Opponent snake of length 3
		board.AddFood(5, 7); // Food above head

		// Act
		var directionScores = MoveTowardsFoodStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBeGreaterThan(0); // Score added since our health is low
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenOurSnakeHasLowHealthAndIsNotSignificantlyLargerThanOpponents_ThenScoresAreAddedRegardlessOfSizeAdvantage()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", MoveTowardsFoodStrategy.ForceFoodAttractionWhenPlayerHealthIsBelow - 5, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 4),
			new Coordinate(5, 3)
		}, true); // Our snake of length 3
		board.AddSnake("opponent", 100, new List<Coordinate>
		{
			new Coordinate(0, 0), // Opponent head
			new Coordinate(0, 1),
			new Coordinate(0, 2)
		}, false); // Opponent snake of length 3
		board.AddFood(5, 7); // Food above head

		// Act
		var directionScores = MoveTowardsFoodStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBeGreaterThan(0); // Score added since our health is low
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenOurSnakeIsAloneAndHasHighHealth_ThenNoScoresAreAdded()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 4),
			new Coordinate(5, 3)
		}, true); // Our snake of length 3
		board.AddFood(5, 7); // Food above head

		// Act
		var directionScores = MoveTowardsFoodStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0); // No score added since our snake is alone and healthy
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenOurSnakeIsAloneButHasLowHealth_ThenScoresAreAdded()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", MoveTowardsFoodStrategy.ForceFoodAttractionWhenPlayerHealthIsBelow - 5, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 4),
			new Coordinate(5, 3)
		}, true); // Our snake of length 3
		board.AddFood(5, 7); // Food above head

		// Act
		var directionScores = MoveTowardsFoodStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBeGreaterThan(0); // Score added since our health is low
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}
}
