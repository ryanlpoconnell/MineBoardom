
using MineBoard.Core;
using MineBoard.Core.Exceptions;

namespace MineBoard.Test;

public class MineBoardGameStateTest
{
    #region Move Tests

    [Fact]
    public void CanMoveUp()
    {
        //Arrange
        var move = MoveEnum.Up;
        ushort columns= 4, rows = 4, lives = 1;
        var minePositions = new Position[]{ };

        Position playerStartPosition = new Position(1, 1);
        Position expectedPlayerPosition = new Position(1,0);
        GameStateSettings gameOptions = new GameStateSettings(columns:columns,rows:rows, lives:lives, startPosition: playerStartPosition, minePositions: minePositions);
        MineboardGameState state = new MineboardGameState(gameOptions);

        //Act
        state.Move(move);
        var result = state.PlayerPosition;

        //Assert
        Assert.Equal(expectedPlayerPosition, result);
    }

    [Fact]
    public void CanMoveDown()
    {
        //Arrange
        var move = MoveEnum.Down;
        ushort columns = 4, rows = 4, lives = 1;
        var minePositions = new Position[] { };

        Position playerStartPosition = new Position(1, 1);
        Position expectedPlayerPosition = new Position(1, 2);
        GameStateSettings gameOptions = new GameStateSettings(columns: columns, rows: rows, lives: lives, startPosition: playerStartPosition, minePositions: minePositions);
        MineboardGameState state = new MineboardGameState(gameOptions);

        //Act
        state.Move(move);
        var result = state.PlayerPosition;

        //Assert
        Assert.Equal(expectedPlayerPosition, result);
    }

    [Fact]
    public void CanMoveLeft()
    {
        //Arrange
        var move = MoveEnum.Left;
        ushort columns = 4, rows = 4, lives = 1;
        var minePositions = new Position[] { };

        Position playerStartPosition = new Position(1, 1);
        Position expectedPlayerPosition = new Position(0, 1);
        GameStateSettings gameOptions = new GameStateSettings(columns: columns, rows: rows, lives: lives, startPosition: playerStartPosition, minePositions: minePositions);
        MineboardGameState state = new MineboardGameState(gameOptions);

        //Act
        state.Move(move);
        var result = state.PlayerPosition;

        //Assert
        Assert.Equal(expectedPlayerPosition, result);
    }

    [Fact]
    public void CanMoveRight()
    {
        //Arrange
        var move = MoveEnum.Right;
        ushort columns = 4, rows = 4, lives = 1;
        var minePositions = new Position[] { };

        Position playerStartPosition = new Position(1, 1);
        Position expectedPlayerPosition = new Position(2, 1);
        GameStateSettings gameOptions = new GameStateSettings(columns: columns, rows: rows, lives: lives, startPosition: playerStartPosition, minePositions: minePositions);
        MineboardGameState state = new MineboardGameState(gameOptions);

        //Act
        state.Move(move);
        var result = state.PlayerPosition;

        //Assert
        Assert.Equal(expectedPlayerPosition, result);
    }

    [Fact]
    public void CannotMoveIfDied()
    {
        //Arrange
        var move = MoveEnum.Right;
        ushort columns = 4, rows = 4, lives = 0;
        var minePositions = new Position[] { };

        Position playerStartPosition = new Position(1, 1);
        GameStateSettings gameOptions = new GameStateSettings(columns: columns, rows: rows, lives: lives, startPosition: playerStartPosition, minePositions: minePositions);
        MineboardGameState state = new MineboardGameState(gameOptions);

        //Act
        //Assert
        Assert.Equal(GameResultEnum.Died, state.GameResult);
        var ex = Assert.Throws<InvalidGameMoveException>(()=> state.Move(move));
        Assert.Equal("The game has ended - result Died",ex.Message);
    }

    [Fact]
    public void CannotMoveIfWon()
    {
        //Arrange
        var move = MoveEnum.Right;
        ushort columns = 4, rows = 4, lives = 1;
        var minePositions = new Position[] { };

        Position playerStartPosition = new Position((ushort)(columns - 1), 1);
        GameStateSettings gameOptions = new GameStateSettings(columns: columns, rows: rows, lives: lives, startPosition: playerStartPosition, minePositions: minePositions);
        MineboardGameState state = new MineboardGameState(gameOptions);

        //Act
        //Assert
        Assert.Equal(GameResultEnum.Won, state.GameResult);
        var ex = Assert.Throws<InvalidGameMoveException>(() => state.Move(move));
        Assert.Equal("The game has ended - result Won", ex.Message);
    }

    [Fact]
    public void MoveIncrements()
    {
        //Arrange
        var expectedMoves = 1;
        var move = MoveEnum.Down;
        ushort columns = 4, rows = 4, lives = 1;
        var minePositions = new Position[] { };

        Position playerStartPosition = new Position(1, 1);
        Position expectedPlayerPosition = new Position(1, 2);
        GameStateSettings gameOptions = new GameStateSettings(columns: columns, rows: rows, lives: lives, startPosition: playerStartPosition, minePositions: minePositions);
        MineboardGameState state = new MineboardGameState(gameOptions);

        //Act
        Assert.Equal(0, state.Moves);
        state.Move(move);
        var result = state.Moves;
        
        //Assert
        Assert.Equal(expectedMoves, result);
    }

    [Fact]
    public void ExceptionWhenMovingUpOutsideBoard()
    {
        //Arrange
        var move = MoveEnum.Up;
        ushort columns = 4, rows = 4, lives = 1;
        var minePositions = new Position[] { };

        Position playerStartPosition = new Position(0, 0);
        GameStateSettings gameOptions = new GameStateSettings(columns: columns, rows: rows, lives: lives, startPosition: playerStartPosition, minePositions: minePositions);
        MineboardGameState state = new MineboardGameState(gameOptions);

        //Act
        //Assert
        var ex  = Assert.Throws<InvalidGameMoveException>(() => state.Move(move));
        Assert.Equal("Cannot move outside of game board", ex.Message);
    }

    [Fact]
    public void ExceptionWhenMovingDownOutsideBoard()
    {
        //Arrange
        var move = MoveEnum.Down;
        ushort columns = 4, rows = 4, lives = 1;
        var minePositions = new Position[] { };

        Position playerStartPosition = new Position(0, 3);
        GameStateSettings gameOptions = new GameStateSettings(columns: columns, rows: rows, lives: lives, startPosition: playerStartPosition, minePositions: minePositions);
        MineboardGameState state = new MineboardGameState(gameOptions);

        //Act
        //Assert
        var ex = Assert.Throws<InvalidGameMoveException>(() => state.Move(move));
        Assert.Equal("Cannot move outside of game board", ex.Message);
    }

    [Fact]
    public void ExceptionWhenMovingLeftOutsideBoard()
    {
        //Arrange
        var move = MoveEnum.Left;
        ushort columns = 4, rows = 4, lives = 1;
        var minePositions = new Position[] { };

        Position playerStartPosition = new Position(0, 0);
        GameStateSettings gameOptions = new GameStateSettings(columns: columns, rows: rows, lives: lives, startPosition: playerStartPosition, minePositions: minePositions);
        MineboardGameState state = new MineboardGameState(gameOptions);

        //Act
        //Assert
        var ex = Assert.Throws<InvalidGameMoveException>(() => state.Move(move));
        Assert.Equal("Cannot move outside of game board", ex.Message);
    }
        
    #endregion

    #region Mine Tests

    [Fact]
    public void LivesDecrementsWhenHittingMineOnMove()
    {
        //Arrange
        var expectedLives = 9;
        var move = MoveEnum.Right;
        ushort columns = 4, rows = 4, lives = 10;
        //  Configure mine in final position so player dies on final column
        var minePositions = new Position[] { new Position(2, 1) };

        Position playerStartPosition = new Position(1, 1);
        GameStateSettings gameOptions = new GameStateSettings(columns: columns, rows: rows, lives: lives, startPosition: playerStartPosition, minePositions: minePositions);
        MineboardGameState state = new MineboardGameState(gameOptions);

        //Act
        state.Move(move);

        //Assert
        Assert.Equal(expectedLives,state.Lives);
    }

    #endregion

    #region Win / Die Tests

    [Fact]
    public void WinWhenReachingFinalColumnWithAtLeastOneLife()
    {
        //Arrange
        var move = MoveEnum.Right;
        ushort columns = 4, rows = 4, lives = 1;
        var minePositions = new Position[] { };

        Position playerStartPosition = new Position(1, 1);
        GameStateSettings gameOptions = new GameStateSettings(columns: columns, rows: rows, lives: lives, startPosition: playerStartPosition, minePositions: minePositions);
        MineboardGameState state = new MineboardGameState(gameOptions);

        //Act
        state.Move(move);
        state.Move(move);

        //Assert
        Assert.True(state.Lives > 0);
        Assert.Equal(GameResultEnum.Won, state.GameResult);
    }

    [Fact]
    public void DieWhenReachingFinalColumnWithAtZeroLives()
    {
        //Arrange
        var move = MoveEnum.Right;
        ushort columns = 4, rows = 4, lives = 1;
        //  Configure mine in final position so player dies on final column
        var minePositions = new Position[] { new Position((ushort)(columns - 1), 1)};

        Position playerStartPosition = new Position(1, 1);
        GameStateSettings gameOptions = new GameStateSettings(columns: columns, rows: rows, lives: lives, startPosition: playerStartPosition, minePositions: minePositions);
        MineboardGameState state = new MineboardGameState(gameOptions);

        //Act
        state.Move(move);
        state.Move(move);

        //Assert
        Assert.True(state.Lives == 0);
        Assert.Equal(GameResultEnum.Died, state.GameResult);
    }

    [Fact]
    public void DieWhenReachingZeroLives()
    {
        //Arrange
        var move = MoveEnum.Right;
        ushort columns = 4, rows = 4, lives = 1;
        //  Configure mine in final position so player dies on final column
        var minePositions = new Position[] { new Position(2, 1) };

        Position playerStartPosition = new Position(1, 1);
        GameStateSettings gameOptions = new GameStateSettings(columns: columns, rows: rows, lives: lives, startPosition: playerStartPosition, minePositions: minePositions);
        MineboardGameState state = new MineboardGameState(gameOptions);

        //Act
        state.Move(move);
        
        //Assert
        Assert.True(state.Lives == 0);
        Assert.Equal(GameResultEnum.Died, state.GameResult);
    }

    #endregion

    [Fact]
    public void FinalScoreEqualsMoves()
    {
        //Arrange
        var expectedMoves = 2;
        var move = MoveEnum.Right;
        ushort columns = 4, rows = 4, lives = 1;
        var minePositions = new Position[] { };

        Position playerStartPosition = new Position(1, 1);
        GameStateSettings gameOptions = new GameStateSettings(columns: columns, rows: rows, lives: lives, startPosition: playerStartPosition, minePositions: minePositions);
        MineboardGameState state = new MineboardGameState(gameOptions);

        //Act
        Assert.Equal(0, state.Moves);
        state.Move(move);
        state.Move(move);
        

        //Assert
        Assert.Equal(expectedMoves, state.Moves);
        Assert.Equal(state.Moves, state.Score);
        Assert.Equal(GameResultEnum.Won, state.GameResult);
    }


}
