using Battlesnake.Domain;
using Battlesnake.WebApi.Adapters;
using Battlesnake.WebApi.BattlesnakeApi.Requests;
using Battlesnake.WebApi.BattlesnakeApi.Responses;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseHttpsRedirection();

/// <summary>
/// This request will be made periodically to retrieve information about your Battlesnake,
/// including its display options, author, etc.
/// </summary>
app.MapGet("/", () =>
{
    return new InitResponse
    {
        ApiVersion = "1",
        Author = "deadlydog-DanSchroeder",
        Color = "#0b7d1cff",
        Head = "do-sammy",
        Tail = "nr-booster"
	};
});

/// <summary>
/// Your Battlesnake will receive this request when it has been entered into a new game.
/// Every game has a unique ID that can be used to allocate resources or data you may need.
/// Your response to this request will be ignored.
/// </summary>
app.MapPost("/start", (GameStatusRequest gameStatusRequest) =>
{
	app.Logger.LogInformation($"Game {gameStatusRequest.Game.Id} started.");
	Results.Ok();
});

/// <summary>
/// This request will be sent for every turn of the game.
/// Use the information provided to determine how your
/// Battlesnake will move on that turn, either up, down, left, or right.
/// </summary>
app.MapPost("/move", (GameStatusRequest gameStatusRequest) =>
{
	var board = ApiBoardToDomainBoardAdapter.Convert(gameStatusRequest.Board, gameStatusRequest.You.Id);
	var gameSettings = ApiGameToDomainGameSettingsAdapter.Convert(gameStatusRequest.Game);
	var decision = GameEngine.MakeMove(app.Logger, gameSettings, board, gameStatusRequest.Turn);

	app.Logger.LogInformation($"Turn {gameStatusRequest.Turn}. Chosen Direction: {decision.MoveDirection}");

	return new MoveResponse
    {
        Move = MoveDirectionsToResponseStringAdapter.Convert(decision.MoveDirection),
        Shout = decision.ShoutMessage
    };
});

/// <summary>
/// Your Battlesnake will receive this request whenever a game it was playing has ended.
/// Use it to learn how your Battlesnake won or lost and deallocated any server-side resources.
/// Your response to this request will be ignored.
/// </summary>
app.MapPost("/end", (GameStatusRequest gameStatusRequest) =>
{
	app.Logger.LogInformation($"Game {gameStatusRequest.Game.Id} ended.");
	Results.Ok();
});

app.Run();
