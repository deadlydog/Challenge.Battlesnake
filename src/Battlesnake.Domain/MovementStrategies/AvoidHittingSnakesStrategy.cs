using Battlesnake.Domain.GameBoard;

namespace Battlesnake.Domain.MovementStrategies;

public class AvoidHittingSnakesStrategy : IMovementStrategy
{
	public static readonly int SnakeBodyPenaltyScore = -500;
	public static readonly int SnakeTailPenaltyScore = -100;

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
				case BoardCellContent.SnakeBody: directionScores.AddScore(MoveDirections.Up, SnakeBodyPenaltyScore); break;
				case BoardCellContent.SnakeTail: directionScores.AddScore(MoveDirections.Up, SnakeTailPenaltyScore); break;
			}
		}
		if (playerHead.Y > 0)
		{
			var cellBelow = board.GetCell(playerHead.X, playerHead.Y - 1);
			switch (cellBelow.Content)
			{
				case BoardCellContent.SnakeBody: directionScores.AddScore(MoveDirections.Down, SnakeBodyPenaltyScore); break;
				case BoardCellContent.SnakeTail: directionScores.AddScore(MoveDirections.Down, SnakeTailPenaltyScore); break;
			}
		}
		if (playerHead.X > 0)
		{
			var cellLeft = board.GetCell(playerHead.X - 1, playerHead.Y);
			switch (cellLeft.Content)
			{
				case BoardCellContent.SnakeBody: directionScores.AddScore(MoveDirections.Left, SnakeBodyPenaltyScore); break;
				case BoardCellContent.SnakeTail: directionScores.AddScore(MoveDirections.Left, SnakeTailPenaltyScore); break;
			}
		}
		if (playerHead.X < (board.Width - 2))
		{
			var cellRight = board.GetCell(playerHead.X + 1, playerHead.Y);
			switch (cellRight.Content)
			{
				case BoardCellContent.SnakeBody: directionScores.AddScore(MoveDirections.Right, SnakeBodyPenaltyScore); break;
				case BoardCellContent.SnakeTail: directionScores.AddScore(MoveDirections.Right, SnakeTailPenaltyScore); break;
			}
		}

		return directionScores;
	}
}
