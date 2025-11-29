namespace Battlesnake.WebApi.Adapters;
using DomainBoard = Battlesnake.Domain.Board.Board;
using WebApiBoard = BattlesnakeApi.Model.Board;

public static class ApiBoardToDomainBoardAdapter
{
	public static DomainBoard Convert(WebApiBoard apiBoard)
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
			domainBoard.AddSnake(snake.Id, snake.Health, snake.Body.Select(coord => (coord.X, coord.Y)));
		}

		return domainBoard;
	}
}
