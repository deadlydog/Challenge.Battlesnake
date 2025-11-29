namespace Battlesnake.Domain;

public class TurnDecision
{
	public MoveDirection Move { get; init; }
	public string ShoutMessage { get; init; } = string.Empty;
}

public enum MoveDirection
{
	Up,
	Down,
	Left,
	Right
}
