using System;
namespace MineBoard.Core.Converters
{
	/// <summary>
	/// Used to interpret zero based index and provide
	/// position in alternative Chess like interpretation
	/// for UI experience
	/// </summary>
	public class ChessBoardPosition
	{
		private readonly Position _position;

		public ChessBoardPosition(Position position)
		{
			_position = position;
		}

		public string Column{
			get
			//TODO: handle many columns, e.g. A->Z->AA->AZ->BA->BZ->...N
			{ return ((char)(65 + _position.Column)).ToString(); }
		}

		public ushort Row{
			get { return (ushort)(_position.Row+1); }
		}
	}
}

