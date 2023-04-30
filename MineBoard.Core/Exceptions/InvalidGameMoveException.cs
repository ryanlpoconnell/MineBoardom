using System;
namespace MineBoard.Core.Exceptions
{
	public class InvalidGameMoveException : Exception
	{
		public InvalidGameMoveException(string message) : base(message)
		{
		}
	}
}

