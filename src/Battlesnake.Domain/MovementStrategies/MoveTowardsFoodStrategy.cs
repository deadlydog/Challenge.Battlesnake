using Battlesnake.Domain.GameBoard;
using System.Numerics;

namespace Battlesnake.Domain.MovementStrategies;

/// <summary>
/// This strategy awards higher scores to moves that bring the snake closer to food items on the board.
/// The closer the food is to the head, the higher the score awarded to moves in that direction.
/// </summary>
public class MoveTowardsFoodStrategy : IMovementStrategy
{
	// TODO: Should be dynamic based on board size so it doesn't give crazy high scores on larger boards.
	// Also, only consider 4 closest food to prevent score inflation, or maybe divide score by number of food considered....
	public static readonly int FoodProximityAttractionMultiplier = 50; // Used to make closer food more attractive.

	public static DirectionScores CalculateDirectionScores(Board board)
	{
		var directionScores = new DirectionScores();

		// The FoodProximityAttractionMultiplier assumes the default board size of 11x11.
		// The xDelta and yDelta calculations below can be much higher or lower depending on board size, which affects the scores assigned.
		// To compensate, we scale the delta values based on board size.
		float boardSizeScaler = 11f / Math.Max(board.Width, board.Height);

		foreach (var food in board.FoodCells)
		{
			int xDelta = board.OurSnakeHeadPosition.X - food.X;
			int yDelta = board.OurSnakeHeadPosition.Y - food.Y;

			// The closer the player is to the food, the more inticing it should be, so invert the deltas so that short distances result in larger values.
			int xDeltaInverted = board.Width - Math.Abs(xDelta);
			int yDeltaInverted = board.Height - Math.Abs(yDelta);

			// Scale the inverted deltas based on board size. This prevents large boards from generating very high scores, and small boards from generating very low scores.
			int xDeltaScaled = (int)(xDeltaInverted * boardSizeScaler);
			int yDeltaScaled = (int)(yDeltaInverted * boardSizeScaler);

			// Finally, apply the attraction multiplier.
			int xScore = xDeltaScaled * FoodProximityAttractionMultiplier;
			int yScore = yDeltaScaled * FoodProximityAttractionMultiplier;

			if (xDelta > 0)
			{
				directionScores.AddScore(MoveDirections.Left, xScore);
			}
			else if (xDelta < 0)
			{
				directionScores.AddScore(MoveDirections.Right, xScore);
			}

			if (yDelta > 0)
			{
				directionScores.AddScore(MoveDirections.Down, yScore);
			}
			else if (yDelta < 0)
			{
				directionScores.AddScore(MoveDirections.Up, yScore);
			}
		}

		return directionScores;
	}
}
