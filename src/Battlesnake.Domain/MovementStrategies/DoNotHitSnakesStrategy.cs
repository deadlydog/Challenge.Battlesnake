using Battlesnake.Domain.GameBoard;

namespace Battlesnake.Domain.MovementStrategies;

/// <summary>
/// This strategy penalizes moves that would result in hitting another snake's body or tail.
/// This does not take into account the possibility of head-to-head collisions; that is handled in a separate strategy.
/// </summary>
public class DoNotHitSnakesStrategy : IMovementStrategy
{
	public static readonly int AvoidHittingSnakeBodyScorePenalty = -5000; // Guaranteed death, so a high penalty.
	public static readonly int AvoidHittingSnakeTailScorePenalty = -1000; // Lower penalty for tails since they will likely move, unless the snake eats food on next turn.

	public static DirectionScores CalculateDirectionScores(Board board)
	{
		var directionScores = new DirectionScores();
		var playerHead = board.OurSnakeHeadPosition;

		// If the Player's snake is able to move up.
		if (playerHead.Y < (board.Height - 2))
		{
			var cellAbove = board.GetCell(playerHead.X, playerHead.Y + 1);
			switch (cellAbove.Content)
			{
				case BoardCellContent.SnakeHead:
					// If the other snake is only 1 cell long, it's probably likely it will be gone next turn, unless it eats food.
					if (cellAbove.OccupyingSnake != null && cellAbove.OccupyingSnake.Length > 1)
					{
						directionScores.AddScore(MoveDirections.Up, AvoidHittingSnakeBodyScorePenalty);
					}
					else
					{
						directionScores.AddScore(MoveDirections.Up, AvoidHittingSnakeTailScorePenalty);
					}
					break;
				case BoardCellContent.SnakeBody: directionScores.AddScore(MoveDirections.Up, AvoidHittingSnakeBodyScorePenalty); break;
				case BoardCellContent.SnakeTail: directionScores.AddScore(MoveDirections.Up, AvoidHittingSnakeTailScorePenalty); break;
			}
		}

		// If the Player's snake is able to move down.
		if (playerHead.Y > 0)
		{
			var cellBelow = board.GetCell(playerHead.X, playerHead.Y - 1);
			switch (cellBelow.Content)
			{
				case BoardCellContent.SnakeHead:
					// If the other snake is only 1 cell long, it's probably likely it will be gone next turn, unless it eats food.
					if (cellBelow.OccupyingSnake != null && cellBelow.OccupyingSnake.Length > 1)
					{
						directionScores.AddScore(MoveDirections.Down, AvoidHittingSnakeBodyScorePenalty);
					}
					else
					{
						directionScores.AddScore(MoveDirections.Down, AvoidHittingSnakeTailScorePenalty);
					}
					break;
				case BoardCellContent.SnakeBody: directionScores.AddScore(MoveDirections.Down, AvoidHittingSnakeBodyScorePenalty); break;
				case BoardCellContent.SnakeTail: directionScores.AddScore(MoveDirections.Down, AvoidHittingSnakeTailScorePenalty); break;
			}
		}

		// If the Player's snake is able to move left.
		if (playerHead.X > 0)
		{
			var cellLeft = board.GetCell(playerHead.X - 1, playerHead.Y);
			switch (cellLeft.Content)
			{
				case BoardCellContent.SnakeHead:
					// If the other snake is only 1 cell long, it's probably likely it will be gone next turn, unless it eats food.
					if (cellLeft.OccupyingSnake != null && cellLeft.OccupyingSnake.Length > 1)
					{
						directionScores.AddScore(MoveDirections.Left, AvoidHittingSnakeBodyScorePenalty);
					}
					else
					{
						directionScores.AddScore(MoveDirections.Left, AvoidHittingSnakeTailScorePenalty);
					}
					break;
				case BoardCellContent.SnakeBody: directionScores.AddScore(MoveDirections.Left, AvoidHittingSnakeBodyScorePenalty); break;
				case BoardCellContent.SnakeTail: directionScores.AddScore(MoveDirections.Left, AvoidHittingSnakeTailScorePenalty); break;
			}
		}

		// If the Player's snake is able to move right.
		if (playerHead.X < (board.Width - 2))
		{
			var cellRight = board.GetCell(playerHead.X + 1, playerHead.Y);
			switch (cellRight.Content)
			{
				case BoardCellContent.SnakeHead:
					// If the other snake is only 1 cell long, it's probably likely it will be gone next turn, unless it eats food.
					if (cellRight.OccupyingSnake != null && cellRight.OccupyingSnake.Length > 1)
					{
						directionScores.AddScore(MoveDirections.Right, AvoidHittingSnakeBodyScorePenalty);
					}
					else
					{
						directionScores.AddScore(MoveDirections.Right, AvoidHittingSnakeTailScorePenalty);
					}
					break;
				case BoardCellContent.SnakeBody: directionScores.AddScore(MoveDirections.Right, AvoidHittingSnakeBodyScorePenalty); break;
				case BoardCellContent.SnakeTail: directionScores.AddScore(MoveDirections.Right, AvoidHittingSnakeTailScorePenalty); break;
			}
		}

		return directionScores;
	}
}
