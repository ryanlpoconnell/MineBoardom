using System;
using MineBoard.Core.Converters;

namespace MineBoard.Core.Extensions
{
	public static class PositionExtensions
	{
		public static ChessBoardPosition ToChessBoardPosition(this Position position)
		{
			return new ChessBoardPosition(position);
		}
    }
}

