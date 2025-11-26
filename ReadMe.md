# Dan's Battlesnake

This is my crack at creating a [Battlesnake](https://play.battlesnake.com) in C#.
Check out [the docs](https://docs.battlesnake.com/) describing the Battlesnake API that this project implements.

## Testing locally

You can use the [Battlesnake CLI](https://github.com/BattlesnakeOfficial/rules/tree/main/cli) to run and test your Battlesnake locally.

Install using Go:

```cmd
go install github.com/BattlesnakeOfficial/rules/cli/battlesnake@latest
```

And then run the app in Visual Studio, and run the CLI against it using:

```cmd
battlesnake play -W 11 -H 11 --name test_snake --url https://localhost:5001/ -g solo -v --delay 1000
```

## Deployment

Once you are happy with your Battlesnake, you'll need to deploy it somewhere for it to compete against other Battlesnakes, such as an Azure App Service.
[Checkout the docs](https://docs.battlesnake.com/guides/hosting-suggestions) for more hosting suggestions.
[Checkout the starter project](https://github.com/neistow/battlesnake-starter-csharp) this was forked from for instructions on deploying to Azure.

You can then register the Battlesnake with your [Battlesnake account](https://play.battlesnake.com), and provide the URL of your deployed service.
From there, let your Battlesnake compete in games and climb the leaderboard!
