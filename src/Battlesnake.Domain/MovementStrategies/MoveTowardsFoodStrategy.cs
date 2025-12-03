using Battlesnake.Domain.GameBoard;
using System.Numerics;

namespace Battlesnake.Domain.MovementStrategies;

/// <summary>
/// This strategy awards higher scores to moves that bring the snake closer to food items on the board.
/// The closer the food is to the head, the higher the score awarded to moves in that direction.
/// To prevent overwhelming the overall scoring, only the closest few food items are considered.
/// </summary>
public class MoveTowardsFoodStrategy : IMovementStrategy
{
	public static readonly int FoodProximityAttractionMaxMultiplier = 50; // Used to make closer food more attractive. Max attraction per food is 10 * multiplier.
	public static readonly int MaxCloseFoodToBeAttractedTo = 5; // Only the closest food will affect attraction. Closer food is given more weight.

	public static DirectionScores CalculateDirectionScores(Board board)
	{
		var directionScores = new DirectionScores();

		// The FoodProximityAttractionMultiplier assumes the default board size of 11x11.
		// The xDelta and yDelta calculations below can be much higher or lower depending on board size, which affects the scores assigned.
		// To compensate, we scale the delta values based on board size.
		float boardSizeScaler = 11f / Math.Max(board.Width, board.Height);

		// To prevent the food attraction from overloading the overall scores, only consider the closest food items.
		var playerHead = board.OurSnakeHeadPosition;
		var closestFood = board.FoodCells
			.OrderBy(food => playerHead.GetDistanceTo(food))
			.Take(MaxCloseFoodToBeAttractedTo);

		int foodOrder = 0;
		foreach (var food in closestFood)
		{
			float foodOrderPriorityScaler = Math.Max(1f - (foodOrder * 0.2f), 0.1f); // Closer food has more influence.
			foodOrder++;

			int xDelta = board.OurSnakeHeadPosition.X - food.X;
			int yDelta = board.OurSnakeHeadPosition.Y - food.Y;

			// The closer the player is to the food, the more inticing it should be, so invert the deltas so that short distances result in larger values.
			int xDeltaInverted = board.Width - Math.Abs(xDelta);
			int yDeltaInverted = board.Height - Math.Abs(yDelta);

			// Scale the inverted deltas based on board size. This prevents large boards from generating very high scores, and small boards from generating very low scores.
			int xDeltaScaled = (int)(xDeltaInverted * boardSizeScaler);
			int yDeltaScaled = (int)(yDeltaInverted * boardSizeScaler);

			// Finally, apply the attraction multiplier, scaled according to how close this food is relative to other food.
			int xScore = xDeltaScaled * (int)(FoodProximityAttractionMaxMultiplier * foodOrderPriorityScaler);
			int yScore = yDeltaScaled * (int)(FoodProximityAttractionMaxMultiplier * foodOrderPriorityScaler);

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
