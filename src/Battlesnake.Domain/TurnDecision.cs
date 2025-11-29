namespace Battlesnake.Domain;

public class TurnDecision
{
	public MoveDirections Move { get; init; }
	public string ShoutMessage { get; init; } = string.Empty;
}

public enum MoveDirections
{
	Up,
	Down,
	Left,
	Right
}
