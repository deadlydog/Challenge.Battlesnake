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
	// Scale delta distance instead of multiplier.
	// Also, only consider 4 closest food to prevent score inflation, or maybe divide score by number of food considered....
	public static readonly int FoodProximityAttractionMultiplier = 50; // Used to make closer food more attractive.

	public static DirectionScores CalculateDirectionScores(Board board)
	{
		var directionScores = new DirectionScores();

		foreach (var food in board.FoodCells)
		{
			int xDelta = board.OurSnakeHeadPosition.X - food.X;
			int yDelta = board.OurSnakeHeadPosition.Y - food.Y;

			// The closer the player is to the cell, the more inticing it should be.
			int xScore = (board.Width - Math.Abs(xDelta)) * FoodProximityAttractionMultiplier;
			int yScore = (board.Height - Math.Abs(yDelta)) * FoodProximityAttractionMultiplier;

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
