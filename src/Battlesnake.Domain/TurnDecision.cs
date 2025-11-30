namespace Battlesnake.Domain;

public class TurnDecision
{
	public MoveDirections MoveDirection { get; init; }
	public string ShoutMessage { get; init; } = string.Empty;
}
