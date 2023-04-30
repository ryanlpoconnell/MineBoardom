using System;
namespace MineBoard.Core
{
	public class GameStateSettings
	{
		public GameStateSettings(ushort columns, ushort rows, ushort lives, Position startPosition, Position[] minePositions)
		{
			Columns = columns;
			Rows = rows;
			Lives = lives;
			PlayerStartPosition = startPosition;
			MinePositions = minePositions;
		}

		public ushort Columns { get; set; }
		public ushort Rows { get; set; }

		public ushort Lives { get; set; }
		public Position PlayerStartPosition { get; set; }
		public Position[] MinePositions { get; set; }

    }
}

