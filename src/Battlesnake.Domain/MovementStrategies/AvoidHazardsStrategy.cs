using Battlesnake.Domain.GameBoard;

namespace Battlesnake.Domain.MovementStrategies;

public class AvoidHazardsStrategy : IMovementStrategy
{
	public static readonly int AvoidHazardScorePenalty = -1000; // We want to avoid hazards, but not as much as hitting a wall or snake body.

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
				case BoardCellContent.Hazard: directionScores.AddScore(MoveDirections.Up, AvoidHazardScorePenalty); break;
			}
		}

		// If the Player's snake is able to move down.
		if (playerHead.Y > 0)
		{
			var cellBelow = board.GetCell(playerHead.X, playerHead.Y - 1);
			switch (cellBelow.Content)
			{
				case BoardCellContent.Hazard: directionScores.AddScore(MoveDirections.Down, AvoidHazardScorePenalty); break;
			}
		}

		// If the Player's snake is able to move left.
		if (playerHead.X > 0)
		{
			var cellLeft = board.GetCell(playerHead.X - 1, playerHead.Y);
			switch (cellLeft.Content)
			{
				case BoardCellContent.Hazard: directionScores.AddScore(MoveDirections.Left, AvoidHazardScorePenalty); break;
			}
		}

		// If the Player's snake is able to move right.
		if (playerHead.X < (board.Width - 2))
		{
			var cellRight = board.GetCell(playerHead.X + 1, playerHead.Y);
			switch (cellRight.Content)
			{
				case BoardCellContent.Hazard: directionScores.AddScore(MoveDirections.Right, AvoidHazardScorePenalty); break;
			}
		}

		return directionScores;
	}
}
