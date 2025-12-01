using Battlesnake.Domain.GameBoard;

namespace Battlesnake.Domain.MovementStrategies;

public class AvoidWallsStrategy : IMovementStrategy
{
	public static DirectionScores CalculateDirectionScores(Board board)
	{
		var scores = new DirectionScores();

		var head = board.OurSnakeHeadPosition;

		// Avoid walls by penalizing moves that would hit the wall
		if (head.Y == 0)
			scores.AddScore(MoveDirections.Down, -1000); // Wall at bottom
		if (head.Y == board.Height - 1)
			scores.AddScore(MoveDirections.Up, -1000); // Wall at top
		if (head.X == 0)
			scores.AddScore(MoveDirections.Left, -1000); // Wall at left
		if (head.X == board.Width - 1)
			scores.AddScore(MoveDirections.Right, -1000); // Wall at right
		return scores;
	}
}
