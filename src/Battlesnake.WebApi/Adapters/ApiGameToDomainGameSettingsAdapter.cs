namespace Battlesnake.WebApi.Adapters;

public static class ApiGameToDomainGameSettingsAdapter
{
	public static Domain.GameSettings Convert(Battlesnake.WebApi.BattlesnakeApi.Model.Game game)
	{
		return new Domain.GameSettings
		{
			GameId = game.Id,
			GameMode = Enum.Parse<Domain.GameModes>(game.Ruleset.Name, ignoreCase: true),
			FoodSpawnChancePercentage = game.Ruleset.Settings.FoodSpawnChance,
			MinimumFoodOnBoard = game.Ruleset.Settings.MinimumFood,
			HazardDamagePerTurn = game.Ruleset.Settings.HazardDamagePerTurn,
			RoyaleSettings = new Domain.GameSettings.RoyaleModeSettings
			{
				ShrinkEveryNTurns = game.Ruleset.Settings.Royale?.ShrinkEveryNTurns ?? 0
			},
			SquadSettings = new Domain.GameSettings.SquadModeSettings
			{
				AllowBodyCollisions = game.Ruleset.Settings.Squad?.AllowBodyCollisions ?? false,
				SharedElimination = game.Ruleset.Settings.Squad?.SharedElimination ?? false,
				SharedHealth = game.Ruleset.Settings.Squad?.SharedHealth ?? false,
				SharedLength = game.Ruleset.Settings.Squad?.SharedLength ?? false
			}
		};
	}
}
