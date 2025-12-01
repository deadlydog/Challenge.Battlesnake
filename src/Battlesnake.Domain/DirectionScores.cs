using System.Diagnostics;

namespace Battlesnake.Domain;

public class DirectionScores
{
	public int Up { get; private set; }
	public int Down { get; private set; }
	public int Left { get; private set; }
	public int Right { get; private set; }

	public void AddScore(MoveDirections direction, int score)
	{
		switch (direction)
		{
			case MoveDirections.Up: Up += score; break;
			case MoveDirections.Down: Down += score; break;
			case MoveDirections.Left: Left += score; break;
			case MoveDirections.Right: Right += score; break;
		}
	}

	public IEnumerable<MoveDirections> GetHighestScoreDirection()
	{
		var sortedScores = new int[] { Up, Down, Left, Right };
		sortedScores.Sort();
		var highestScore = sortedScores[^1];

		var highestScoreDirections = new List<MoveDirections>();
		if (Up == highestScore)
			highestScoreDirections.Add(MoveDirections.Up);
		if (Down == highestScore)
			highestScoreDirections.Add(MoveDirections.Down);
		if (Left == highestScore)
			highestScoreDirections.Add(MoveDirections.Left);
		if (Right == highestScore)
			highestScoreDirections.Add(MoveDirections.Right);

		return highestScoreDirections;
	}

	public static DirectionScores operator +(DirectionScores a, DirectionScores b)
	{
		var result = new DirectionScores();
		result.Up = a.Up + b.Up;
		result.Down = a.Down + b.Down;
		result.Left = a.Left + b.Left;
		result.Right = a.Right + b.Right;
		return result;
	}
}
