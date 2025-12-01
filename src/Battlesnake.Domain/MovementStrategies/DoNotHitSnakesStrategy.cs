using Battlesnake.Domain.GameBoard;

namespace Battlesnake.Domain.MovementStrategies;

public class DoNotHitSnakesStrategy : IMovementStrategy
{
	public static readonly int HitSnakeBodyScorePenalty = -5000;
	public static readonly int HitSnakeTailScorePenalty = -1000; // Lower penalty for tails since they will likely move, unless the snake eats food on next turn.

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
						directionScores.AddScore(MoveDirections.Up, HitSnakeBodyScorePenalty);
					}
					else
					{
						directionScores.AddScore(MoveDirections.Up, HitSnakeTailScorePenalty);
					}
					break;
				case BoardCellContent.SnakeBody: directionScores.AddScore(MoveDirections.Up, HitSnakeBodyScorePenalty); break;
				case BoardCellContent.SnakeTail: directionScores.AddScore(MoveDirections.Up, HitSnakeTailScorePenalty); break;
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
						directionScores.AddScore(MoveDirections.Down, HitSnakeBodyScorePenalty);
					}
					else
					{
						directionScores.AddScore(MoveDirections.Down, HitSnakeTailScorePenalty);
					}
					break;
				case BoardCellContent.SnakeBody: directionScores.AddScore(MoveDirections.Down, HitSnakeBodyScorePenalty); break;
				case BoardCellContent.SnakeTail: directionScores.AddScore(MoveDirections.Down, HitSnakeTailScorePenalty); break;
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
						directionScores.AddScore(MoveDirections.Left, HitSnakeBodyScorePenalty);
					}
					else
					{
						directionScores.AddScore(MoveDirections.Left, HitSnakeTailScorePenalty);
					}
					break;
				case BoardCellContent.SnakeBody: directionScores.AddScore(MoveDirections.Left, HitSnakeBodyScorePenalty); break;
				case BoardCellContent.SnakeTail: directionScores.AddScore(MoveDirections.Left, HitSnakeTailScorePenalty); break;
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
						directionScores.AddScore(MoveDirections.Right, HitSnakeBodyScorePenalty);
					}
					else
					{
						directionScores.AddScore(MoveDirections.Right, HitSnakeTailScorePenalty);
					}
					break;
				case BoardCellContent.SnakeBody: directionScores.AddScore(MoveDirections.Right, HitSnakeBodyScorePenalty); break;
				case BoardCellContent.SnakeTail: directionScores.AddScore(MoveDirections.Right, HitSnakeTailScorePenalty); break;
			}
		}

		return directionScores;
	}
}
