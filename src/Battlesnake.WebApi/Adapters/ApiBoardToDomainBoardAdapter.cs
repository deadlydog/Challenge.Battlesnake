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
			domainBoard.Cells[food.X, food.Y].Content = Domain.Board.BoardCellContent.Food;
		}

		foreach (var hazard in apiBoard.Hazards)
		{
			domainBoard.Cells[hazard.X, hazard.Y].Content = Domain.Board.BoardCellContent.Hazard;
		}

		foreach (var snake in apiBoard.Snakes)
		{
			var domainSnake = new Domain.Snake(snake.Id, snake.Length, snake.Health);

			foreach (var segment in snake.Body)
			{
				domainBoard.Cells[segment.X, segment.Y].Content = Domain.Board.BoardCellContent.SnakeBody;
				domainBoard.Cells[segment.X, segment.Y].OccupyingSnake = domainSnake;
			}
			domainBoard.Cells[snake.Head.X, snake.Head.Y].Content = Domain.Board.BoardCellContent.SnakeHead;
		}

		return domainBoard;
	}
}
