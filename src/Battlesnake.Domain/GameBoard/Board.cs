namespace Battlesnake.Domain.GameBoard;

public class Board
{
	private readonly BoardCell[,] _cells;
	private readonly List<Snake> _snakes = new List<Snake>();

	public int Width { get; private set; }
	public int Height { get; private set; }

	public Snake OurSnake { get; private set; }
	public Coordinate OurSnakeHeadPosition { get; private set; }
	public Coordinate OurSnakeTailPosition { get; private set; }

	public Board(int width, int height)
	{
		if (width < 2 || height < 2)
		{
			throw new ArgumentException($"Board dimensions must be at least 2x2. Provided dimensions were {width} x {height}.");
		}

		Width = width;
		Height = height;

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

		int headX = snakeBody.First().X;
		int headY = snakeBody.First().Y;
		int tailX = snakeBody.Last().X;
		int tailY = snakeBody.Last().Y;

		foreach (var segment in snakeBody)
		{
			_cells[segment.X, segment.Y].Content = BoardCellContent.SnakeBody;
			_cells[segment.X, segment.Y].OccupyingSnake = snake;
		}
		_cells[headX, headY].Content = BoardCellContent.SnakeHead;
		_cells[tailX, tailY].Content = BoardCellContent.SnakeTail;

		if (isOurSnake)
		{
			OurSnake = snake;
			OurSnakeHeadPosition = new Coordinate(headX, headY);
			OurSnakeTailPosition = new Coordinate(tailX, tailY);
		}
	}

	public BoardCell GetCell(int x, int y)
	{
		return _cells[x, y];
	}
}
