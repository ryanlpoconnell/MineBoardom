using MineBoard.Core.Exceptions;

namespace MineBoard.Core;
public class MineboardGameState
{
    const string INVALIDMOVEMESSAGE = "Cannot move outside of game board";

    #region Private Fields

    //Board size meta
    private readonly ushort _columns;
    private readonly ushort _rows;

    //Where are the mines
    private readonly Position[] _minePositions;

    //The target side of the board to win
    private readonly ushort _winColumn;

    #endregion

    public MineboardGameState(GameStateSettings gameOptions)
    {
        //Configurable..
        _columns = gameOptions.Columns;
        _rows = gameOptions.Rows;
        _minePositions = gameOptions.MinePositions;
        PlayerPosition = gameOptions.PlayerStartPosition;
        Lives = gameOptions.Lives;

        //Initialize
        _winColumn = (ushort)(_columns - 1);
        Moves = 0;

        //Make sure the configured options affect final state from the begining
        //  e.g. if you're out of lives
        CheckForFinalStateCondition();
    }

    #region Properties

    /// <summary>
    /// The final result of the game, set once the game
    /// has reached a resolution and no more moves can be played
    /// </summary>
    public GameResultEnum? GameResult { get; private set; }

    /// <summary>
    /// Number of moves so far
    /// </summary>
    public int Moves{
        get;
        private set;
    }

    /// <summary>
    /// Current number of Lives left
    /// </summary>
    public ushort Lives{
        get;
        private set;
    }

    /// <summary>
    /// Current player position
    /// </summary>
    public Position PlayerPosition{
        get;
        private set;
    }

    /// <summary>
    /// Game score
    /// </summary>
    public int Score => Moves;

    #endregion

    #region Public Methods

    /// <summary>
    /// Update the game state by making a move
    /// </summary>
    /// <param name="move"></param>
    /// <exception cref="InvalidGameMoveException">Invalid moves will result in this exception type</exception>
    public void Move(MoveEnum move)
    {
        //Validate game can be still be played.
        if (GameResult != null)
            throw new InvalidGameMoveException($"The game has ended - result {GameResult}");

        //Predetermine new position
        Position newPosition = new Position(PlayerPosition.Column, PlayerPosition.Row);
        switch (move)
        {
            case MoveEnum.Up:
                if (newPosition.Row == 0)
                    throw new InvalidGameMoveException(INVALIDMOVEMESSAGE);
                newPosition.Row--;
                break;
            case MoveEnum.Down:
                if (newPosition.Row == _rows-1)
                    throw new InvalidGameMoveException(INVALIDMOVEMESSAGE);
                newPosition.Row++;
                break;
            case MoveEnum.Left:
                if (newPosition.Column == 0)
                    throw new InvalidGameMoveException(INVALIDMOVEMESSAGE);
                newPosition.Column--;
                break;
            case MoveEnum.Right:
                if (newPosition.Column == _columns-1)
                    throw new InvalidGameMoveException(INVALIDMOVEMESSAGE);
                newPosition.Column++;
                break;
        }

        //now we've pre-validated the move, update the new position of the player
        PlayerPosition = newPosition;

        //increment number of moves
        Moves++;

        //update the game state for post move updates
        UpdateForPostMoveConditions();
                
    }

    #endregion

    #region Private Helpers

    /// <summary>
    /// Having moved player position, what are the consequences?
    /// </summary>
    private void UpdateForPostMoveConditions()
    {
        if (_minePositions.Contains(PlayerPosition))
        {
            //Uh oh, you hit a mine!
            Lives--;
        }

        //update the game state
        CheckForFinalStateCondition();
    }

    /// <summary>
    /// Check if current state conditions meet requirements for Game result
    /// </summary>
    private void CheckForFinalStateCondition()
    {
        if (Lives <= 0)
        {
            //Out of lives, so you died
            GameResult = GameResultEnum.Died;
        }
        else if (PlayerPosition.Column == _winColumn)
        {
            //Reached target column position, so, you won!
            GameResult = GameResultEnum.Won;
        }
        else
        {
            //No resolution reached, the game goes on!
        }

    }

    #endregion
}
