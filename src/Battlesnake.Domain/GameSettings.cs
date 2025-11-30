namespace Battlesnake.Domain;

public record GameSettings
{
	/// <summary>
	/// The percentage chance (0-100) that food will spawn on an empty cell each round.
	/// </summary>
	public int FoodSpawnChancePercentage { get; init; }

	/// <summary>
	/// Gets the minimum amount of food that must be present on the board each round.
	/// </summary>
	public int MinimumFoodOnBoard { get; init; }

	/// <summary>
	/// Health damage a snake takes when it ends its turn on a hazard cell.
	/// This stacks on top of the normal 1 health damage per turn.
	/// </summary>
	public int HazardDamagePerTurn { get; init; }

	public RoyaleModeSettings RoyaleSettings { get; init; } = new RoyaleModeSettings();

	public SquadModeSettings SquadSettings { get; init; } = new SquadModeSettings();

	public record RoyaleModeSettings
	{
		/// <summary>
		/// In Royale mode, the number of turns between generating new hazards (shrinking the safe board space).
		/// </summary>
		public int ShrinkEveryNTurns { get; init; }
	}

	public record SquadModeSettings
	{
		/// <summary>
		/// In Squad mode, allow members of the same squad to move over each other without dying.
		/// </summary>
		public bool AllowBodyCollisions { get; init; }

		/// <summary>
		/// In Squad mode, all squad members are eliminated when one is eliminated.
		/// </summary>
		public bool SharedElimination { get; init; }

		/// <summary>
		/// In Squad mode, all squad members share health.
		/// </summary>
		public bool SharedHealth { get; init; }

		/// <summary>
		/// In Squad mode, all squad members share length.
		/// </summary>
		public bool SharedLength { get; init; }
	}
}
