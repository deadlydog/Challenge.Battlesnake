namespace Battlesnake.Domain;

public class Snake
{
	public string Id { get; }

	public int Length { get; }

	public int Health { get; }

	public Snake(string id, int length, int health)
	{
		Id = id;
		Length = length;
		Health = health;
	}

	public static Snake Empty => new Snake(string.Empty, 0, 0);
}
