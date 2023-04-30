using System;
namespace MineBoard.Core
{
	public class Position
	{
		public Position(ushort column, ushort row)
		{
			Column = column;
			Row = row;
		}

		public ushort Column { get; set; }
		public ushort Row { get; set; }

        public override bool Equals(object? obj)
        {
			return Equals(obj as Position);
        }

		public bool Equals(Position position)
		{
			if (position == null)
				return false;

			return position.Column == Column && position.Row == Row;
		}
    }
}

