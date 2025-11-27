namespace Battlesnake.Domain.Board;

public class BoardCell
{
    public BoardCellContent Content { get; set; } = BoardCellContent.Empty;

    public Snake OccupyingSnake { get; set; } = Snake.Empty;
}
