using Battlesnake.Domain.GameBoard;

namespace Battlesnake.Domain.MovementStrategies;

public class DoNotHitWallsStrategy : IMovementStrategy
{
	public static readonly int HitWallScorePenalty = -1000;

	public static DirectionScores CalculateDirectionScores(Board board)
	{
		var directionScores = new DirectionScores();

		var head = board.OurSnakeHeadPosition;

		// Avoid walls by penalizing moves that would hit the wall
		if (head.Y == 0)
			directionScores.AddScore(MoveDirections.Down, HitWallScorePenalty); // Wall at bottom
		if (head.Y == board.Height - 1)
			directionScores.AddScore(MoveDirections.Up, HitWallScorePenalty); // Wall at top
		if (head.X == 0)
			directionScores.AddScore(MoveDirections.Left, HitWallScorePenalty); // Wall at left
		if (head.X == board.Width - 1)
			directionScores.AddScore(MoveDirections.Right, HitWallScorePenalty); // Wall at right
		return directionScores;
	}
}
