using Shouldly;

namespace Battlesnake.Domain.Tests;

public static class DirectionScoresTests
{
	public class WhenAddingTwoDirectionScoresTogether
	{
		[Fact]
		public void ThenTheResultingScoresAreTheSumOfTheTwo()
		{
			// Arrange
			var scores1 = new DirectionScores();
			scores1.AddScore(MoveDirections.Up, 10);
			scores1.AddScore(MoveDirections.Down, 20);
			scores1.AddScore(MoveDirections.Left, 30);
			scores1.AddScore(MoveDirections.Right, 40);

			var scores2 = new DirectionScores();
			scores2.AddScore(MoveDirections.Up, 1);
			scores2.AddScore(MoveDirections.Down, 2);
			scores2.AddScore(MoveDirections.Left, 3);
			scores2.AddScore(MoveDirections.Right, 4);

			// Act
			var result = scores1 + scores2;

			// Assert
			result.Up.ShouldBe(11);
			result.Down.ShouldBe(22);
			result.Left.ShouldBe(33);
			result.Right.ShouldBe(44);
		}

		[Fact]
		public void NegativeScoresAreHandledCorrectly()
		{
			// Arrange
			var scores1 = new DirectionScores();
			scores1.AddScore(MoveDirections.Up, -10);
			scores1.AddScore(MoveDirections.Down, -20);
			scores1.AddScore(MoveDirections.Left, -30);
			scores1.AddScore(MoveDirections.Right, -40);

			var scores2 = new DirectionScores();
			scores2.AddScore(MoveDirections.Up, 5);
			scores2.AddScore(MoveDirections.Down, 15);
			scores2.AddScore(MoveDirections.Left, 25);
			scores2.AddScore(MoveDirections.Right, 35);

			// Act
			var result = scores1 + scores2;

			// Assert
			result.Up.ShouldBe(-5);
			result.Down.ShouldBe(-5);
			result.Left.ShouldBe(-5);
			result.Right.ShouldBe(-5);
		}
	}
}
