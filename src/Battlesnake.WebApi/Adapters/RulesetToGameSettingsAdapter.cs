namespace Battlesnake.WebApi.Adapters;

public static class RulesetToGameSettingsAdapter
{
	public static Domain.GameSettings Convert(Battlesnake.WebApi.BattlesnakeApi.Model.Ruleset ruleset)
	{
		return new Domain.GameSettings
		{
			GameMode = Enum.Parse<Domain.GameModes>(ruleset.Name, ignoreCase: true),
			FoodSpawnChancePercentage = ruleset.Settings.FoodSpawnChance,
			MinimumFoodOnBoard = ruleset.Settings.MinimumFood,
			HazardDamagePerTurn = ruleset.Settings.HazardDamagePerTurn,
			RoyaleSettings = new Domain.GameSettings.RoyaleModeSettings
			{
				ShrinkEveryNTurns = ruleset.Settings.Royale?.ShrinkEveryNTurns ?? 0
			},
			SquadSettings = new Domain.GameSettings.SquadModeSettings
			{
				AllowBodyCollisions = ruleset.Settings.Squad?.AllowBodyCollisions ?? false,
				SharedElimination = ruleset.Settings.Squad?.SharedElimination ?? false,
				SharedHealth = ruleset.Settings.Squad?.SharedHealth ?? false,
				SharedLength = ruleset.Settings.Squad?.SharedLength ?? false
			}
		};
	}
}
