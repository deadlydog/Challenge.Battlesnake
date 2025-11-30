using Battlesnake.Domain.GameBoard;

namespace Battlesnake.Domain;

public class Game
{
	public string Id { get; init; }

	public GameSettings Settings { get; init; }

	public Game(string gameId, GameSettings settings)
	{
		if (string.IsNullOrWhiteSpace(gameId))
		{
			throw new ArgumentException("Game ID cannot be null or whitespace.", nameof(gameId));
		}

		if (settings == null)
		{
			throw new ArgumentNullException(nameof(settings), "Game settings cannot be null.");
		}

		Id = gameId;
		Settings = settings;
	}

	public TurnDecision MakeMove(Board board)
	{
		// Placeholder logic: always move up with no shout message.
		return new TurnDecision
		{
			MoveDirection = MoveDirections.Up,
			ShoutMessage = string.Empty
		};
	}
}
