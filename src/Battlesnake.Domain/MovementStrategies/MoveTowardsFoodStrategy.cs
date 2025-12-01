using Battlesnake.Domain.GameBoard;
using System.Numerics;

namespace Battlesnake.Domain.MovementStrategies;

public class MoveTowardsFoodStrategy : IMovementStrategy
{
	public static readonly int FoodProximityMultiplier = 50;

	public static DirectionScores CalculateDirectionScores(Board board)
	{
		var directionScores = new DirectionScores();

		foreach (var food in board.FoodCells)
		{
			int xDelta = board.OurSnakeHeadPosition.X - food.X;
			int yDelta = board.OurSnakeHeadPosition.Y - food.Y;

			// The closer the player is to the cell, the more inticing it should be.
			int xScore = (board.Width - Math.Abs(xDelta)) * FoodProximityMultiplier;
			int yScore = (board.Height - Math.Abs(yDelta)) * FoodProximityMultiplier;
			
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
