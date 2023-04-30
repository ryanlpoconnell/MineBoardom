using System;
namespace MineBoard.Core.Converters
{
	public class TextToMoveConverter
	{		
        /// <summary>
        /// Receives text and returns Move
        /// </summary>
        /// <param name="text"></param>
        /// <returns>interpreted move from text</returns>
        /// <exception cref="InvalidOperationException">if text cannot be interpreted exception thrown</exception>
		public MoveEnum Convert(string text)
		{
            switch (text.ToLower())
            {
                case "up":
                    return MoveEnum.Up;
                case "down":
                    return MoveEnum.Down;
                case "left":
                    return MoveEnum.Left;
                case "right":
                    return MoveEnum.Right;
                default:
                    throw new InvalidOperationException($"The following move instruction cannot be intepreted: \"{text}\"");
            }
        }
	}
}

