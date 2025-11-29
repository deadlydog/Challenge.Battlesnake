namespace Battlesnake.Domain;

public class Game
{
	public string Id { get; init; }

	public GameSettings Settings { get; init; }

	public Game(string id, GameSettings settings)
	{
		Id = id;
		Settings = settings;
	}

	public TurnDecision MakeMove(Board.Board board)
	{
		// Placeholder logic: always move up with no shout message.
		return new TurnDecision
		{
			Move = MoveDirection.Up,
			ShoutMessage = string.Empty
		};
	}
}
