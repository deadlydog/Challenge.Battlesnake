namespace Battlesnake.Domain;

public record GameSettings
{
	/// <summary>
	/// The percentage chance (0-100) that food will spawn on an empty cell at the end of each turn.
	/// </summary>
	public int FoodSpawnChancePercentage { get; init; }

	/// <summary>
	/// Gets the minimum amount of food that must be present on the board every turn.
	/// </summary>
	public int MinimumFoodOnBoard { get; init; }

	/// <summary>
	/// Health damage a snake takes when it ends its turn on a hazard cell.
	/// This stacks on top of the normal 1 health damage per turn.
	/// </summary>
	public int HazardDamagePerTurn { get; init; }
}
