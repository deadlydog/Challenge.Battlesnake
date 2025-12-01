using Battlesnake.Domain.GameBoard;

namespace Battlesnake.Domain.MovementStrategies;

public interface IMovementStrategy
{
	static abstract DirectionScores CalculateDirectionScores(Board board);
}
