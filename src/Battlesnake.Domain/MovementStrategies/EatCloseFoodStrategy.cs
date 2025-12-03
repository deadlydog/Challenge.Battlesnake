using Battlesnake.Domain.GameBoard;

namespace Battlesnake.Domain.MovementStrategies;

/// <summary>
/// This strategy awards a high score to moves that result in eating food on the next turn (i.e. food that is directly adjacent to the snake's head).
/// </summary>
public class EatCloseFoodStrategy : IMovementStrategy
{
	public static readonly int EatFoodScoreBoost = 1000;

	public static DirectionScores CalculateDirectionScores(Board board)
	{
		var directionScores = new DirectionScores();
		var playerHead = board.OurSnakeHeadPosition;

		// If the Player's snake is able to move up.
		if (playerHead.Y < (board.Height - 1))
		{
			var cellAbove = board.GetCell(playerHead.X, playerHead.Y + 1);
			switch (cellAbove.Content)
			{
				case BoardCellContent.Food: directionScores.AddScore(MoveDirections.Up, EatFoodScoreBoost); break;
			}
		}

		// If the Player's snake is able to move down.
		if (playerHead.Y > 0)
		{
			var cellBelow = board.GetCell(playerHead.X, playerHead.Y - 1);
			switch (cellBelow.Content)
			{
				case BoardCellContent.Food: directionScores.AddScore(MoveDirections.Down, EatFoodScoreBoost); break;
			}
		}

		// If the Player's snake is able to move left.
		if (playerHead.X > 0)
		{
			var cellLeft = board.GetCell(playerHead.X - 1, playerHead.Y);
			switch (cellLeft.Content)
			{
				case BoardCellContent.Food: directionScores.AddScore(MoveDirections.Left, EatFoodScoreBoost); break;
			}
		}

		// If the Player's snake is able to move right.
		if (playerHead.X < (board.Width - 1))
		{
			var cellRight = board.GetCell(playerHead.X + 1, playerHead.Y);
			switch (cellRight.Content)
			{
				case BoardCellContent.Food: directionScores.AddScore(MoveDirections.Right, EatFoodScoreBoost); break;
			}
		}

		return directionScores;
	}
}
