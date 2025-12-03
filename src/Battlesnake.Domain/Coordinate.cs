namespace Battlesnake.Domain;

public record Coordinate(int X, int Y)
{
	public float GetDistanceTo(Coordinate other)
	{
		int deltaX = X - other.X;
		int deltaY = Y - other.Y;
		return MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);
	}
}
