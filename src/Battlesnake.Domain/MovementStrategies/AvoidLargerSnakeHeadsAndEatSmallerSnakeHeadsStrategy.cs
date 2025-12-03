using Battlesnake.Domain.GameBoard;

namespace Battlesnake.Domain.MovementStrategies;

/// <summary>
/// This strategy penalizes moves that would result in the player's snake head moving into a cell adjacent to the head of a larger or equal-sized snake, as they might collide.
/// There's a chance the larger snake could move into that same cell on the next turn, resulting in a head-to-head collision and the player's snake dying.
/// Conversely, if the other snake is smaller, the player's snake can safely headbutt it, so we actually boost the score in that direction to encourage a collision.
/// </summary>
public class AvoidLargerSnakeHeadsAndEatSmallerSnakeHeadsStrategy : IMovementStrategy
{
	public static readonly int AvoidAdjacentLargerSnakeHeadScorePenalty = -3000; // High penalty to avoid death, but smaller penaly than guaranteed death of hitting a wall or snake body.
	public static readonly int AttackAdjacentSmallerSnakeHeadScoreBonus = 2000; // Bonus to encourage headbutting smaller snakes.

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
			entry.Coordinate.X >= 0 && entry.Coordinate.X < (board.Width - 1) &&
			entry.Coordinate.Y >= 0 && entry.Coordinate.Y < (board.Height - 1));

		// See if any other snakes can move into the same cells that the player can, and adjust scores accordingly.
		foreach (var opponentSnake in board.OpponentSnakes)
		{
			var cellsAroundOtherSnakeHead = new[]
			{
				new Coordinate(opponentSnake.Head.X, opponentSnake.Head.Y + 1),
				new Coordinate(opponentSnake.Head.X, opponentSnake.Head.Y - 1),
				new Coordinate(opponentSnake.Head.X - 1, opponentSnake.Head.Y),
				new Coordinate(opponentSnake.Head.X + 1, opponentSnake.Head.Y)
			};

			// Only consider cells that are actually on the board.
			var cellsOtherSnakeCanMoveTo = cellsAroundOtherSnakeHead.Where(coord =>
				coord.X >= 0 && coord.X < (board.Width - 1) &&
				coord.Y >= 0 && coord.Y < (board.Height - 1));

			foreach (var playerPotentialCell in cellsPlayerCanMoveTo)
			{
				if (cellsOtherSnakeCanMoveTo.Any(otherSnakePotenialCell => otherSnakePotenialCell == playerPotentialCell.Coordinate))
				{
					if (opponentSnake.Length >= board.OurSnake.Length)
					{
						// Larger or equal-sized snake head nearby - penalize this move.
						directionScores.AddScore(playerPotentialCell.Direction, AvoidAdjacentLargerSnakeHeadScorePenalty);
					}
					else
					{
						// Smaller snake head nearby - encourage headbutting it.
						directionScores.AddScore(playerPotentialCell.Direction, AttackAdjacentSmallerSnakeHeadScoreBonus);
					}
				}
			}
		}

		return directionScores;
	}
}
