namespace Battlesnake.WebApi.Adapters;
using DomainBoard = Battlesnake.Domain.Board.Board;
using WebApiBoard = BattlesnakeApi.Model.Board;

public static class ApiBoardToDomainBoardAdapter
{
	public static DomainBoard Convert(WebApiBoard apiBoard, string playerSnakeId)
	{
		var domainBoard = new DomainBoard(apiBoard.Width, apiBoard.Height);

		foreach (var food in apiBoard.Food)
		{
			domainBoard.AddFood(food.X, food.Y);
		}

		foreach (var hazard in apiBoard.Hazards)
		{
			domainBoard.AddHazard(hazard.X, hazard.Y);
		}

		foreach (var snake in apiBoard.Snakes)
		{
			domainBoard.AddSnake(snake.Id, snake.Health,
				snake.Body.Select(coord => new Domain.Coordinate(coord.X, coord.Y)),
				string.Equals(snake.Id, playerSnakeId));
		}

		return domainBoard;
	}
}
