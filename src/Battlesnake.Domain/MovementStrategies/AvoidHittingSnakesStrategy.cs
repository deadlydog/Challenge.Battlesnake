using Battlesnake.Domain.GameBoard;

namespace Battlesnake.Domain.MovementStrategies;

public class AvoidHittingSnakesStrategy : IMovementStrategy
{
	public static readonly int SnakeBodyPenaltyScore = -500;
	public static readonly int SnakeTailPenaltyScore = -100; // Lower penalty for tails since they will likely move, unless the snake eats food on next turn.

	public static DirectionScores CalculateDirectionScores(Board board)
	{
		var directionScores = new DirectionScores();
		var playerHead = board.OurSnakeHeadPosition;

		// Avoid running into any snake bodies.
		if (playerHead.Y < (board.Height - 2))
		{
			var cellAbove = board.GetCell(playerHead.X, playerHead.Y + 1);
			switch (cellAbove.Content)
			{
				case BoardCellContent.SnakeHead:
					// If the other snake is only 1 cell long, it's probably likely it will be gone next turn, unless it eats food.
					if (cellAbove.OccupyingSnake != null && cellAbove.OccupyingSnake.Length > 1)
					{
						directionScores.AddScore(MoveDirections.Up, SnakeBodyPenaltyScore);
					}
					else
					{
						directionScores.AddScore(MoveDirections.Up, SnakeTailPenaltyScore);
					}
					break;
				case BoardCellContent.SnakeBody: directionScores.AddScore(MoveDirections.Up, SnakeBodyPenaltyScore); break;
				case BoardCellContent.SnakeTail: directionScores.AddScore(MoveDirections.Up, SnakeTailPenaltyScore); break;
			}
		}
		if (playerHead.Y > 0)
		{
			var cellBelow = board.GetCell(playerHead.X, playerHead.Y - 1);
			switch (cellBelow.Content)
			{
				case BoardCellContent.SnakeHead:
					// If the other snake is only 1 cell long, it's probably likely it will be gone next turn, unless it eats food.
					if (cellBelow.OccupyingSnake != null && cellBelow.OccupyingSnake.Length > 1)
					{
						directionScores.AddScore(MoveDirections.Down, SnakeBodyPenaltyScore);
					}
					else
					{
						directionScores.AddScore(MoveDirections.Down, SnakeTailPenaltyScore);
					}
					break;
				case BoardCellContent.SnakeBody: directionScores.AddScore(MoveDirections.Down, SnakeBodyPenaltyScore); break;
				case BoardCellContent.SnakeTail: directionScores.AddScore(MoveDirections.Down, SnakeTailPenaltyScore); break;
			}
		}
		if (playerHead.X > 0)
		{
			var cellLeft = board.GetCell(playerHead.X - 1, playerHead.Y);
			switch (cellLeft.Content)
			{
				case BoardCellContent.SnakeHead:
					// If the other snake is only 1 cell long, it's probably likely it will be gone next turn, unless it eats food.
					if (cellLeft.OccupyingSnake != null && cellLeft.OccupyingSnake.Length > 1)
					{
						directionScores.AddScore(MoveDirections.Left, SnakeBodyPenaltyScore);
					}
					else
					{
						directionScores.AddScore(MoveDirections.Left, SnakeTailPenaltyScore);
					}
					break;
				case BoardCellContent.SnakeBody: directionScores.AddScore(MoveDirections.Left, SnakeBodyPenaltyScore); break;
				case BoardCellContent.SnakeTail: directionScores.AddScore(MoveDirections.Left, SnakeTailPenaltyScore); break;
			}
		}
		if (playerHead.X < (board.Width - 2))
		{
			var cellRight = board.GetCell(playerHead.X + 1, playerHead.Y);
			switch (cellRight.Content)
			{
				case BoardCellContent.SnakeHead:
					// If the other snake is only 1 cell long, it's probably likely it will be gone next turn, unless it eats food.
					if (cellRight.OccupyingSnake != null && cellRight.OccupyingSnake.Length > 1)
					{
						directionScores.AddScore(MoveDirections.Right, SnakeBodyPenaltyScore);
					}
					else
					{
						directionScores.AddScore(MoveDirections.Right, SnakeTailPenaltyScore);
					}
					break;
				case BoardCellContent.SnakeBody: directionScores.AddScore(MoveDirections.Right, SnakeBodyPenaltyScore); break;
				case BoardCellContent.SnakeTail: directionScores.AddScore(MoveDirections.Right, SnakeTailPenaltyScore); break;
			}
		}

		return directionScores;
	}
}
