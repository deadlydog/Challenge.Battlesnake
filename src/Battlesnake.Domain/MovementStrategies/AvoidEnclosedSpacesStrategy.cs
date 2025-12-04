using Battlesnake.Domain.GameBoard;
using System.Runtime.CompilerServices;

namespace Battlesnake.Domain.MovementStrategies;

public class AvoidEnclosedSpacesStrategy : IMovementStrategy
{
	public const int AvoidCompletelyEnclosedSpaceScorePenalty = -5000; // Almost guaranteed death on next turn, so high penalty.
	public const int AvoidMostlyEnclosedSpaceScorePenalty = -1200; // Penalty to avoid potentially getting trapped, but less than hitting a wall or snake body as you could live longer.
	public const int AvoidSlightlyEnclosedSpaceScorePenalty = -200; // Small penalty to avoid wide open spaces.

	public static DirectionScores CalculateDirectionScores(Board board)
	{
		var directionScores = new DirectionScores();
		var playerHead = board.OurSnakeHeadPosition;

		var cellsAroundPlayer = new[]
		{
			new { Direction = MoveDirections.Up, Coordinate = new Coordinate(playerHead.X, playerHead.Y + 1) },
			new { Direction = MoveDirections.Down, Coordinate = new Coordinate(playerHead.X, playerHead.Y - 1) },
			new { Direction = MoveDirections.Left, Coordinate = new Coordinate(playerHead.X - 1, playerHead.Y) },
			new { Direction = MoveDirections.Right, Coordinate = new Coordinate(playerHead.X + 1, playerHead.Y) }
		};

		// Only consider cells that are actually on the board.
		var cellsPlayerCanMoveTo = cellsAroundPlayer.Where(entry =>
			entry.Coordinate.X >= 0 && entry.Coordinate.X < board.Width &&
			entry.Coordinate.Y >= 0 && entry.Coordinate.Y < board.Height);

		// Do not bother checking moving the player backward, as it would run into its own body.
		// The 2nd segment of the snake's body (after the head) is always at index 1.
		cellsPlayerCanMoveTo = cellsPlayerCanMoveTo.Where(entry =>
			entry.Coordinate != board.OurSnake.Body.Skip(1).First());

		// See if any of the cells surrounding the move cell are enclosed, and adjust scores accordingly.
		foreach (var potentialMoveCell in cellsPlayerCanMoveTo)
		{
			var cellsAroundPotentialMoveCell = new[]
			{
				new Coordinate(potentialMoveCell.Coordinate.X, potentialMoveCell.Coordinate.Y + 1),
				new Coordinate(potentialMoveCell.Coordinate.X, potentialMoveCell.Coordinate.Y - 1),
				new Coordinate(potentialMoveCell.Coordinate.X - 1, potentialMoveCell.Coordinate.Y),
				new Coordinate(potentialMoveCell.Coordinate.X + 1, potentialMoveCell.Coordinate.Y)
			};

			int numberOfEnclosedSurroundingCells = 0;
			foreach (var surroundingCell in cellsAroundPotentialMoveCell)
			{
				bool surroundingCellIsOffTheBoard =
					surroundingCell.X < 0 || surroundingCell.X >= board.Width ||
					surroundingCell.Y < 0 || surroundingCell.Y >= board.Height;
				if (surroundingCellIsOffTheBoard)
				{
					numberOfEnclosedSurroundingCells++;
					continue;
				}

				var cellContent = board.GetCell(surroundingCell.X, surroundingCell.Y).Content;
				if (cellContent == BoardCellContent.SnakeHead ||
					cellContent == BoardCellContent.SnakeBody)
				{
					numberOfEnclosedSurroundingCells++;
				}
			}

			// We always expect at least one cell to be enclosed (the cell we would be moving from).
			if (numberOfEnclosedSurroundingCells == 4)
			{
				// This move would result in being completely enclosed and death, so apply penalty.
				directionScores.AddScore(potentialMoveCell.Direction, AvoidCompletelyEnclosedSpaceScorePenalty);
			}
			else if (numberOfEnclosedSurroundingCells == 3)
			{
				// This move would result in being mostly enclosed, which could lead to getting trapped, so apply a lesser penalty.
				directionScores.AddScore(potentialMoveCell.Direction, AvoidMostlyEnclosedSpaceScorePenalty);
			}
			else if (numberOfEnclosedSurroundingCells == 2)
			{
				// This move would result in being slightly enclosed, which may lead to getting trapped, so apply a small penalty.
				directionScores.AddScore(potentialMoveCell.Direction, AvoidSlightlyEnclosedSpaceScorePenalty);
			}
		}

		return directionScores;
	}
}
