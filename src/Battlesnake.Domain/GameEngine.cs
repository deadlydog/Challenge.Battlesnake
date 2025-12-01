using Battlesnake.Domain.GameBoard;
using Battlesnake.Domain.MovementStrategies;

namespace Battlesnake.Domain;

public static class GameEngine
{
	public static TurnDecision MakeMove(GameSettings gameSettings, Board board)
	{
		var directionScores = new DirectionScores();

		directionScores += DoNotHitWallsStrategy.CalculateDirectionScores(board);
		directionScores += DoNotHitSnakesStrategy.CalculateDirectionScores(board);
		directionScores += EatCloseFoodStrategy.CalculateDirectionScores(board);
		directionScores += MoveTowardsFoodStrategy.CalculateDirectionScores(board);

		var bestDirectionsToMove = directionScores.GetHighestScoreDirection();
		var directionToMove = bestDirectionsToMove.First();

		// If multiple directions have the same score, pick one at random.
		if (bestDirectionsToMove.Count() > 1)
		{
			directionToMove = bestDirectionsToMove.ElementAt(new Random().Next(0, bestDirectionsToMove.Count()));
		}

		return new TurnDecision
		{
			MoveDirection = directionToMove,
			ShoutMessage = string.Empty
		};
	}
}
