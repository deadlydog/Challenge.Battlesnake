namespace Battlesnake.Domain;

public record Snake
{
	public string Id { get; init; }

	public int Length { get; init; }

	public int Health { get; init; }

	public Snake(string id, int length, int health)
	{
		if (string.IsNullOrWhiteSpace(id))
		{
			throw new ArgumentException("Snake ID cannot be null or whitespace.", nameof(id));
		}

		if (length < 1)
		{
			throw new ArgumentException("Snake length must be at least 1.", nameof(length));
		}

		if (health < 0 || health > 100)
		{
			throw new ArgumentException("Snake health must be between 0 and 100.", nameof(health));
		}

		Id = id;
		Length = length;
		Health = health;
	}
}
