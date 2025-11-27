using System;

namespace Battlesnake.Domain.Board;

public class Board
{
	public BoardCell[,] Cells { get; set; }

	public Board(int width, int height)
	{
		if (width < 2 || height < 2)
		{
			throw new ArgumentException($"Board dimensions must be at least 2x2. Provided dimensions were {width} x {height}.");
		}

		Cells = new BoardCell[width, height];
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				Cells[x, y] = new BoardCell();
			}
		}
	}
}
