
using MineBoard.Core.Converters;

namespace MineBoard.Test;

public class TextToMoveConverterTest
{
    [Fact]
    public void CanConvertUp()
    {
        //Arrange
        var text = "up";
        var converter = new TextToMoveConverter();

        //Act
        var result = converter.Convert(text);

        //Assert
        Assert.Equal(Core.MoveEnum.Up, result);
    }

    [Fact]
    public void CanConvertDown()
    {
        //Arrange
        var text = "down";
        var converter = new TextToMoveConverter();

        //Act
        var result = converter.Convert(text);

        //Assert
        Assert.Equal(Core.MoveEnum.Down, result);
    }

    [Fact]
    public void CanConvertLeft()
    {
        //Arrange
        var text = "left";
        var converter = new TextToMoveConverter();

        //Act
        var result = converter.Convert(text);

        //Assert
        Assert.Equal(Core.MoveEnum.Left, result);
    }

    [Fact]
    public void CanConvertRight()
    {
        //Arrange
        var text = "right";
        var converter = new TextToMoveConverter();

        //Act
        var result = converter.Convert(text);

        //Assert
        Assert.Equal(Core.MoveEnum.Right, result);
    }

    [Fact]
    public void CanConvertCaseInsensitive()
    {
        //Arrange
        var text = "RiGhT";
        var converter = new TextToMoveConverter();

        //Act
        var result = converter.Convert(text);

        //Assert
        Assert.Equal(Core.MoveEnum.Right, result);
    }

    [Fact]
    public void CanConvertInvalidInstructionThrowsException()
    {
        //Arrange
        var text = "invalidinstruction";
        var converter = new TextToMoveConverter();

        //Act
        //Assert
        Assert.Throws<InvalidOperationException>(()=> converter.Convert(text));
    }

}
