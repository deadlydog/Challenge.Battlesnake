using Battlesnake.Domain.GameBoard;
using Battlesnake.Domain.MovementStrategies;

namespace Battlesnake.Domain;

public static class GameEngine
{
	public static TurnDecision MakeMove(GameSettings gameSettings, Board board, int round)
	{
		var directionScores = new DirectionScores();

		var wallScores = DoNotHitWallsStrategy.CalculateDirectionScores(board);
		var snakeScores = DoNotHitSnakesStrategy.CalculateDirectionScores(board);
		var eatFoodScores = EatCloseFoodStrategy.CalculateDirectionScores(board);
		var findFoodScores = MoveTowardsFoodStrategy.CalculateDirectionScores(board);
		var avoidAndEatSnakeHeadsScores = AvoidLargerSnakeHeadsAndEatSmallerSnakeHeadsStrategy.CalculateDirectionScores(board);
		directionScores = wallScores + snakeScores + eatFoodScores + findFoodScores + avoidAndEatSnakeHeadsScores;

		var bestDirectionsToMove = directionScores.GetHighestScoreDirection();
		var directionToMove = bestDirectionsToMove.First();

		// If multiple directions have the same score, pick one at random.
		if (bestDirectionsToMove.Count() > 1)
		{
			directionToMove = bestDirectionsToMove.ElementAt(new Random().Next(0, bestDirectionsToMove.Count()));
		}

		Console.WriteLine($"Turn {round}. Chosen Direction: {directionToMove}{Environment.NewLine}" +
			$"Final Scores: {directionScores}{Environment.NewLine}" +
			$"Wall scores: {wallScores}{Environment.NewLine}" +
			$"Snake scores: {snakeScores}{Environment.NewLine}" +
			$"Eat Food scores: {eatFoodScores}{Environment.NewLine}" +
			$"Find Food scores: {findFoodScores}{Environment.NewLine}");

		return new TurnDecision
		{
			MoveDirection = directionToMove,
			ShoutMessage = string.Empty
		};
	}
}
