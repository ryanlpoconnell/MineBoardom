using MineBoard.Core;
using MineBoard.Core.Converters;
using MineBoard.Core.Exceptions;
using MineBoard.Core.Extensions;

Console.WriteLine("======================");
Console.WriteLine("Welcome to MineBoardom");
Console.WriteLine("======================");
await Task.Delay(750);
Console.WriteLine("Move from Left side of board to the Right to win.");
await Task.Delay(750);
Console.WriteLine("Avoid mines along the way to stay alive.");
await Task.Delay(750);
Console.WriteLine("Good luck!");
await Task.Delay(750);
Console.WriteLine("");

//Configure game options
//  TODO: employ difficulty level generator for different size boards, lives and mine distribution.
var options = new GameStateSettings(columns: 8,
                               rows: 8,
                               lives: 2,
                               startPosition: new Position(column: 0, row: 0),
                               minePositions:
                                    new Position[]{
                                         new Position(1, 0),
                                         new Position(1, 1),
                                         new Position(1, 2),
                                         new Position(1, 3),
                                         new Position(1, 4),
                                         new Position(1, 5),
                                         new Position(1, 6),
                                         new Position(1, 7),
                                         new Position(3, 3),
                                         new Position(5, 5),
                                         new Position(7, 7) }
                                    );
//Configure helpers
var moveInterpreter = new TextToMoveConverter();

var defaultColor = Console.BackgroundColor;

//Run game
RunGame(options);

//Let user take it in for a momment, it's been wild!
await Task.Delay(3000);
Console.WriteLine("");
Console.WriteLine("Enter any key to exit");
Console.ReadKey();

void RunGame(GameStateSettings gameOptions)
{
    var game = new MineBoard.Core.MineboardGameState(gameOptions);

    while(game.GameResult == null)
    {
        var chessPosition = game.PlayerPosition.ToChessBoardPosition();

        Console.WriteLine($"Position: {chessPosition.Column}{chessPosition.Row}");
        if (game.Lives <= 1)
            Console.BackgroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Lives remaining: {game.Lives}");
        Console.BackgroundColor = defaultColor;
        Console.WriteLine($"Moves: {game.Moves}");
        Console.WriteLine($"Your move (\"up\", \"down\", \"left\", \"right\")");

        MoveEnum? move = null;

        //convert from text to move understood by game
        try
        {
            move = moveInterpreter.Convert(Console.ReadLine()!);
        }
        catch (InvalidOperationException invEx)
        {
            move = null;
            Console.WriteLine($"Invalid instruction. Try again! - {invEx.Message}");
        }

        //if we have a move to make, make it
        if (move.HasValue)
        {
            try
            {
                game.Move(move.Value);

            }
            catch(InvalidGameMoveException moveEx)
            {
                Console.WriteLine($"Yikes, you can't make that move - {moveEx.Message}");
            }
        }

        Console.WriteLine("");
    }

    //couldn't resist a tiny amount of pizazz
    switch (game.GameResult)
    {
        case GameResultEnum.Died:
            Console.BackgroundColor = ConsoleColor.Red;
            break;
        case GameResultEnum.Won:
            Console.BackgroundColor = ConsoleColor.Green;
            break;           
    }
    Console.WriteLine($"You {game.GameResult}, Final Score: {game.Score}");
    
}
