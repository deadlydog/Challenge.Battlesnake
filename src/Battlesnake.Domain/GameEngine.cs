using Battlesnake.Domain.GameBoard;
using Battlesnake.Domain.MovementStrategies;
using Microsoft.Extensions.Logging;

namespace Battlesnake.Domain;

public static class GameEngine
{
	public static TurnDecision MakeMove(ILogger logger, GameSettings gameSettings, Board board, int round)
	{
		var directionScores = new DirectionScores();

		var wallScores = DoNotHitWallsStrategy.CalculateDirectionScores(board);
		var snakeScores = DoNotHitSnakesStrategy.CalculateDirectionScores(board);
		var eatFoodScores = EatCloseFoodStrategy.CalculateDirectionScores(board);
		var findFoodScores = MoveTowardsFoodStrategy.CalculateDirectionScores(board);
		var avoidAndEatSnakeHeadsScores = AvoidLargerSnakeHeadsAndEatSmallerSnakeHeadsStrategy.CalculateDirectionScores(board);
		var avoidHazardsScores = AvoidHazardsStrategy.CalculateDirectionScores(board);
		directionScores = wallScores + snakeScores + eatFoodScores + findFoodScores + avoidAndEatSnakeHeadsScores + avoidHazardsScores;

		var bestDirectionsToMove = directionScores.GetHighestScoreDirection();
		var directionToMove = bestDirectionsToMove.First();

		// If multiple directions have the same score, pick one at random.
		if (bestDirectionsToMove.Count() > 1)
		{
			directionToMove = bestDirectionsToMove.ElementAt(new Random().Next(0, bestDirectionsToMove.Count()));
		}

		logger.LogDebug(
			"Turn {Round}. Chosen Direction: {Direction}{NewLine}" +
			"Final Scores: {DirectionScores}{NewLine}" +
			"Wall scores: {WallScores}{NewLine}" +
			"Snake scores: {SnakeScores}{NewLine}" +
			"Eat Food scores: {EatFoodScores}{NewLine}" +
			"Find Food scores: {FindFoodScores}{NewLine}" +
			"Avoid and Eat Snake Heads scores: {AvoidAndEatSnakeHeadsScores}{NewLine}" +
			"Avoid Hazards scores: {AvoidHazardsScores}",
			round, directionToMove, Environment.NewLine,
			directionScores, Environment.NewLine,
			wallScores, Environment.NewLine,
			snakeScores, Environment.NewLine,
			eatFoodScores, Environment.NewLine,
			findFoodScores, Environment.NewLine,
			avoidAndEatSnakeHeadsScores, Environment.NewLine,
			avoidHazardsScores
		);

		return new TurnDecision
		{
			MoveDirection = directionToMove,
			ShoutMessage = string.Empty
		};
	}
}
