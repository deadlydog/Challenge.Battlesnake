using Battlesnake.Domain.GameBoard;
using Battlesnake.Domain.MovementStrategies;
using Shouldly;

namespace Battlesnake.Domain.Tests.MovementStrategies;

public class AvoidEnclosedSpacesStrategyTests
{
	[Fact]
	public void WhenMovingToCellWithNoEnclosureAndNoSnakes_ThenNoPenaltyIsApplied()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6),
			new Coordinate(5, 7),
		}, true);

		// Act
		var directionScores = AvoidEnclosedSpacesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenMovingToCellWithNoEnclosureAndWithSnakes_ThenNoPenaltyIsApplied()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6),
			new Coordinate(5, 7),
		}, true);
		board.AddSnake("opponent", 100, new List<Coordinate>
		{
			new Coordinate(4, 0), // Snake is far away from player
			new Coordinate(5, 0),
			new Coordinate(6, 0),
		}, false);

		// Act
		var directionScores = AvoidEnclosedSpacesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0); // No penalty as there is no enclosure
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenMovingToCellWithOneEnclosedSurroundingSnakeCell_ThenSlightlyEnclosedPenaltyIsApplied()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6),
			new Coordinate(5, 7),
		}, true);
		board.AddSnake("opponent", 100, new List<Coordinate>
		{
			new Coordinate(4, 3),
			new Coordinate(5, 3), // Body directly below player's Down movement
			new Coordinate(6, 3),
		}, false);

		// Act
		var directionScores = AvoidEnclosedSpacesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0); 
		directionScores.Down.ShouldBe(AvoidEnclosedSpacesStrategy.AvoidSlightlyEnclosedSpaceScorePenalty); // Small penalty for one enclosed surrounding cell
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenMovingToCellWithTwoEnclosedSurroundingSnakeCells_ThenMostlyEnclosedPenaltyIsApplied()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6),
			new Coordinate(5, 7),
		}, true);
		board.AddSnake("opponent", 100, new List<Coordinate>
		{
			new Coordinate(4, 3),
			new Coordinate(5, 3), // Body directly below player's Down movement
			new Coordinate(6, 3),
			new Coordinate(6, 4), // Body directly to the right of player's Down movement
			new Coordinate(7, 4),
		}, false);

		// Act
		var directionScores = AvoidEnclosedSpacesStrategy.CalculateDirectionScores(board);
		// Assert

		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(AvoidEnclosedSpacesStrategy.AvoidMostlyEnclosedSpaceScorePenalty); // Penalty for two enclosed surrounding cells (plus player's body)
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(AvoidEnclosedSpacesStrategy.AvoidSlightlyEnclosedSpaceScorePenalty);
	}

	[Fact]
	public void WhenMovingToCellWithThreeEnclosedSurroundingSnakeCells_ThenCompletelyEnclosedPenaltyIsApplied()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 5), // Head
			new Coordinate(5, 6),
			new Coordinate(5, 7),
		}, true);
		board.AddSnake("opponent", 100, new List<Coordinate>
		{
			new Coordinate(4, 4), // Head directly to the left of player's Down movement
			new Coordinate(4, 3),
			new Coordinate(5, 3), // Body directly below player's Down movement
			new Coordinate(6, 3),
			new Coordinate(6, 4), // Body directly to the right of player's Down movement
			new Coordinate(7, 4),
		}, false);

		// Act
		var directionScores = AvoidEnclosedSpacesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(AvoidEnclosedSpacesStrategy.AvoidCompletelyEnclosedSpaceScorePenalty); // Penalty for three enclosed surrounding cells
		directionScores.Left.ShouldBe(AvoidEnclosedSpacesStrategy.AvoidSlightlyEnclosedSpaceScorePenalty);
		directionScores.Right.ShouldBe(AvoidEnclosedSpacesStrategy.AvoidSlightlyEnclosedSpaceScorePenalty);
	}

	[Fact]
	public void WhenMovingToLeftWallCellWithTwoOpenSides_ThenSlightlyEnclosedPenaltyIsApplied()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(1, 5), // Head next to left edge
			new Coordinate(1, 6),
			new Coordinate(1, 7),
		}, true);

		// Act
		var directionScores = AvoidEnclosedSpacesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0); 
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(AvoidEnclosedSpacesStrategy.AvoidSlightlyEnclosedSpaceScorePenalty);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenMovingToTopWallCellWithTwoOpenSides_ThenNoPenaltyIsApplied()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 9), // Head next to top edge
			new Coordinate(5, 8),
			new Coordinate(5, 7),
		}, true);

		// Act
		var directionScores = AvoidEnclosedSpacesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(AvoidEnclosedSpacesStrategy.AvoidSlightlyEnclosedSpaceScorePenalty); // Small penalty for 2 enclosed surrounding cells
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenMovingToRightWallCellWithTwoOpenSides_ThenSlightlyEnclosedPenaltyIsApplied()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(9, 5), // Head next to right edge
			new Coordinate(9, 6),
			new Coordinate(9, 7),
		}, true);

		// Act
		var directionScores = AvoidEnclosedSpacesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0); 
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(AvoidEnclosedSpacesStrategy.AvoidSlightlyEnclosedSpaceScorePenalty);
	}

	[Fact]
	public void WhenMovingToBottomWallCellWithTwoOpenSides_ThenSlightlyEnclosedPenaltyIsApplied()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(5, 1), // Head next to bottom edge
			new Coordinate(5, 2),
			new Coordinate(5, 3),
		}, true);

		// Act
		var directionScores = AvoidEnclosedSpacesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0); 
		directionScores.Down.ShouldBe(AvoidEnclosedSpacesStrategy.AvoidSlightlyEnclosedSpaceScorePenalty); // Small penalty for 2 enclosed surrounding cells
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenMovingToTopLeftCornerCell_ThenMostlyEnclosedPenaltyIsApplied()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(0, 9), // Head near top-left corner
			new Coordinate(0, 8),
			new Coordinate(0, 7),
		}, true);
		// Act
		var directionScores = AvoidEnclosedSpacesStrategy.CalculateDirectionScores(board);
		// Assert
		directionScores.Up.ShouldBe(AvoidEnclosedSpacesStrategy.AvoidMostlyEnclosedSpaceScorePenalty); 
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0); 
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenMovingToTopRightCornerCell_ThenMostlyEnclosedPenaltyIsApplied()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(10, 9), // Head near top-right corner
			new Coordinate(10, 8),
			new Coordinate(10, 7),
		}, true);

		// Act
		var directionScores = AvoidEnclosedSpacesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(AvoidEnclosedSpacesStrategy.AvoidMostlyEnclosedSpaceScorePenalty);
		directionScores.Down.ShouldBe(0);
		directionScores.Left.ShouldBe(0); 
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenMovingToBottomLeftCornerCell_ThenMostlyEnclosedPenaltyIsApplied()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(0, 1), // Head near bottom-left corner
			new Coordinate(0, 2),
			new Coordinate(0, 3),
		}, true);

		// Act
		var directionScores = AvoidEnclosedSpacesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(AvoidEnclosedSpacesStrategy.AvoidMostlyEnclosedSpaceScorePenalty);
		directionScores.Left.ShouldBe(0); 
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenMovingToBottomRightCornerCell_ThenMostlyEnclosedPenaltyIsApplied()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(10, 1), // Head near bottom-right corner
			new Coordinate(10, 2),
			new Coordinate(10, 3),
		}, true);

		// Act
		var directionScores = AvoidEnclosedSpacesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(AvoidEnclosedSpacesStrategy.AvoidMostlyEnclosedSpaceScorePenalty);
		directionScores.Left.ShouldBe(0); 
		directionScores.Right.ShouldBe(0);
	}

	[Fact]
	public void WhenMovingToRightWallCellWithThreeEnclosedSides_ThenCompletelyEnclosedPenaltyIsApplied()
	{
		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(9, 5), // Head next to right edge
			new Coordinate(8, 5),
			new Coordinate(7, 5),
		}, true);
		board.AddSnake("opponent", 100, new List<Coordinate>
		{
			new Coordinate(10, 4), // Head directly below player's Right movement
			new Coordinate(10, 3),
			new Coordinate(10, 2),
		}, false);
		board.AddSnake("opponent2", 100, new List<Coordinate>
		{
			new Coordinate(10, 6), // Head directly above player's Right movement
			new Coordinate(10, 7),
			new Coordinate(10, 8),
		}, false);

		// Act
		var directionScores = AvoidEnclosedSpacesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(AvoidEnclosedSpacesStrategy.AvoidSlightlyEnclosedSpaceScorePenalty); 
		directionScores.Down.ShouldBe(AvoidEnclosedSpacesStrategy.AvoidSlightlyEnclosedSpaceScorePenalty);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(AvoidEnclosedSpacesStrategy.AvoidCompletelyEnclosedSpaceScorePenalty); // Penalty for three enclosed surrounding cells
	}

	[Fact]
	public void WhenMovingToRightWallWithTwoEnclosedSidesAndASnakeTail_ThenMostlyEnclosedPenaltyIsApplied()
	{

		// Arrange
		var board = new Board(11, 11);
		board.AddSnake("player", 100, new List<Coordinate>
		{
			new Coordinate(9, 5), // Head next to right edge
			new Coordinate(8, 5),
			new Coordinate(7, 5),
		}, true);
		board.AddSnake("opponent", 100, new List<Coordinate>
		{
			new Coordinate(10, 4), // Head directly below player's Right movement
			new Coordinate(10, 3),
			new Coordinate(10, 2),
		}, false);
		board.AddSnake("opponent2", 100, new List<Coordinate>
		{
			new Coordinate(10, 8),
			new Coordinate(10, 7),
			new Coordinate(10, 6), // Tail directly above player's Right movement. Tail will typically move before next two turns, so it is not considered enclosed.
		}, false);

		// Act
		var directionScores = AvoidEnclosedSpacesStrategy.CalculateDirectionScores(board);

		// Assert
		directionScores.Up.ShouldBe(0);
		directionScores.Down.ShouldBe(AvoidEnclosedSpacesStrategy.AvoidSlightlyEnclosedSpaceScorePenalty);
		directionScores.Left.ShouldBe(0);
		directionScores.Right.ShouldBe(AvoidEnclosedSpacesStrategy.AvoidMostlyEnclosedSpaceScorePenalty); // Penalty for two enclosed surrounding cells (plus player's body)
	}
}
