using System;
using System.Collections.Generic;
using System.Linq;

namespace Battlesnake.Domain.Board;

public class Board
{
	private readonly BoardCell[,] _cells;
	private readonly List<Snake> _snakes = new List<Snake>();

	public Snake OurSnake { get; private set; }
	public Coordinate OurSnakeHeadPosition { get; private set; }

	public Board(int width, int height)
	{
		if (width < 2 || height < 2)
		{
			throw new ArgumentException($"Board dimensions must be at least 2x2. Provided dimensions were {width} x {height}.");
		}

		_cells = new BoardCell[width, height];
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				_cells[x, y] = new BoardCell();
			}
		}
	}

	public void AddFood(int x, int y)
	{
		_cells[x, y].Content = BoardCellContent.Food;
	}

	public void AddHazard(int x, int y)
	{
		_cells[x, y].Content = BoardCellContent.Hazard;
	}

	/// <summary>
	/// Adds a snake to the board at the specified body coordinates.
	/// </summary>
	/// <param name="id">The unique ID of the snake.</param>
	/// <param name="health">The health of the snake.</param>
	/// <param name="body">The body coordinates of the snake. The first coordinate represents the head.</param>
	/// <param name="isOurSnake">Indicates whether the snake belongs to the player or not.</param>
	public void AddSnake(string id, int health, IEnumerable<Coordinate> body, bool isOurSnake)
	{
		var snakeBody = body.ToList();

		var snake = new Snake(id, snakeBody.Count, health);
		_snakes.Add(snake);

		foreach (var segment in snakeBody)
		{
			_cells[segment.X, segment.Y].Content = BoardCellContent.SnakeBody;
			_cells[segment.X, segment.Y].OccupyingSnake = snake;
		}
		_cells[snakeBody.First().X, snakeBody.First().Y].Content = BoardCellContent.SnakeHead;

		if (isOurSnake)
		{
			OurSnake = snake;
		}
	}
}
