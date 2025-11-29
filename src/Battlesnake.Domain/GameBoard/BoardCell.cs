namespace Battlesnake.Domain.GameBoard;

public class BoardCell
{
    public BoardCellContent Content { get; set; } = BoardCellContent.Empty;

	public Snake OccupyingSnake { get; set; }

	public bool IsOccupiedBySnake => OccupyingSnake != null;
}
