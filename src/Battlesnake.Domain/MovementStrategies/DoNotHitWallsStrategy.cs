using Battlesnake.Domain.GameBoard;

namespace Battlesnake.Domain.MovementStrategies;

/// <summary>
/// This strategy penalizes moves that would result in hitting the walls of the board and dying.
/// </summary>
public class DoNotHitWallsStrategy : IMovementStrategy
{
	public static readonly int AvoidHittingWallScorePenalty = -10000; // Guaranteed death, so a high penalty.

	public static DirectionScores CalculateDirectionScores(Board board)
	{
		var directionScores = new DirectionScores();
		var head = board.OurSnakeHeadPosition;

		// Avoid walls by penalizing moves that would hit the wall
		if (head.Y == 0)
			directionScores.AddScore(MoveDirections.Down, AvoidHittingWallScorePenalty); // Wall at bottom
		if (head.Y == board.Height - 1)
			directionScores.AddScore(MoveDirections.Up, AvoidHittingWallScorePenalty); // Wall at top
		if (head.X == 0)
			directionScores.AddScore(MoveDirections.Left, AvoidHittingWallScorePenalty); // Wall at left
		if (head.X == board.Width - 1)
			directionScores.AddScore(MoveDirections.Right, AvoidHittingWallScorePenalty); // Wall at right
		return directionScores;
	}
}
