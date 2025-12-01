using Battlesnake.Domain.GameBoard;

namespace Battlesnake.Domain.MovementStrategies;

public class AvoidWallsStrategy : IMovementStrategy
{
	private static readonly int PenaltyScore = -1000;

	public static DirectionScores CalculateDirectionScores(Board board)
	{
		var scores = new DirectionScores();

		var head = board.OurSnakeHeadPosition;

		// Avoid walls by penalizing moves that would hit the wall
		if (head.Y == 0)
			scores.AddScore(MoveDirections.Down, PenaltyScore); // Wall at bottom
		if (head.Y == board.Height - 1)
			scores.AddScore(MoveDirections.Up, PenaltyScore); // Wall at top
		if (head.X == 0)
			scores.AddScore(MoveDirections.Left, PenaltyScore); // Wall at left
		if (head.X == board.Width - 1)
			scores.AddScore(MoveDirections.Right, PenaltyScore); // Wall at right
		return scores;
	}
}
