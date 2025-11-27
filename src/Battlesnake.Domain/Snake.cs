namespace Battlesnake.Domain;

public class Snake
{
	public string Id { get; set; }

	public int Length { get; set; }

	public static Snake Empty => new Snake
	{
		Id = string.Empty,
		Length = 0
	};
}
