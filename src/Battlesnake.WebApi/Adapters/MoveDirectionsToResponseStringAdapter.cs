namespace Battlesnake.WebApi.Adapters;

public static class MoveDirectionsToResponseStringAdapter
{
	public static string Convert(Domain.MoveDirections direction)
	{
		return direction switch
		{
			Domain.MoveDirections.Up => "up",
			Domain.MoveDirections.Down => "down",
			Domain.MoveDirections.Left => "left",
			Domain.MoveDirections.Right => "right",
			_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
		};
	}
}
