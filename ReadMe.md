# Dan's Battlesnake

This is my crack at creating a [Battlesnake](https://play.battlesnake.com) in C#.
Check out [the docs](https://docs.battlesnake.com/) describing the Battlesnake API that this project implements.

## Testing locally

You can use the [Battlesnake CLI](https://github.com/BattlesnakeOfficial/rules/tree/main/cli) to run and test your Battlesnake locally.

Install the Battlesnake CLI using Go:

```cmd
go install github.com/BattlesnakeOfficial/rules/cli/battlesnake@latest
```

And then run the app in Visual Studio (or using `dotnet run`), and run the CLI against it using:

```cmd
battlesnake play -W 11 -H 11 --name snake_main --url https://localhost:5001/ -g standard -v --delay 100 --browser
```

If you have 2 instances of your repo running (e.g. one of the snakes is in anther git branch with different config), you can have them play against each other using:

```cmd
battlesnake play -W 11 -H 11 --name snake_main --url https://localhost:5001/ --name snake_branch --url https://localhost:5003/ -g standard -v --delay 100 --browser
```

The Battlesnake website currently supports board sizes of 7x7, 11x11, and 19x19, so those are good options to test with by adjusting the `-W` and `-H` parameters.

## Deployment

Once you are happy with your Battlesnake, you'll need to deploy it somewhere for it to compete against other Battlesnakes, such as an Azure App Service.
[Checkout the docs](https://docs.battlesnake.com/guides/hosting-suggestions) for more hosting suggestions.
[Checkout the starter project](https://github.com/neistow/battlesnake-starter-csharp) this was forked from for instructions on manually deploying to Azure.
This repo uses GitHub Actions to automatically deploy to Azure on each push to the `main` branch.

You can then register the Battlesnake with your [Battlesnake account](https://play.battlesnake.com), and provide the URL of your deployed service.
From there, let your Battlesnake compete in games and climb the leaderboard!

## Ideas for improvement

- Merge in [this PR](https://github.com/deadlydog/Challenge.Battlesnake/pull/1/files) to not have the snake so aggressively chase food.
- Improve pathfinding to avoid getting trapped or cornered.
- Implement more aggressive strategies to block and trap opponent snakes, when not in need of food.
  - e.g. Chase your own tail to kill time when when the board is crowded, allowing other snakes to battle, or to wait for them to get close before cutting them off.
  - e.g. Circle around smaller snakes to trap them against walls or your own body.
- Implement different strategies based on the game mode (solo, 1v1, 1v7, royale, constrictor, etc).
  - For example, in constrictor mode, focus on circling and trapping opponent snakes, or always staying near your body to maximize your length by not blocking paths.
