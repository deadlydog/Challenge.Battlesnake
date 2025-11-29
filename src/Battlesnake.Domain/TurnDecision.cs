namespace Battlesnake.Domain;

public class TurnDecision
{
	public MoveDirections Move { get; init; }
	public string ShoutMessage { get; init; } = string.Empty;
}
