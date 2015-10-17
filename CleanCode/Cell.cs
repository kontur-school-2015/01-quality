namespace CleanCode
{
    public enum PieceColor
    {
        Black,
        White
    }
	public class Cell
	{
		public static readonly Cell Empty = new Cell(null, PieceColor.White);
		public readonly PieceColor Color;
		public readonly Piece Piece;

		public Cell(Piece piece, PieceColor color)
		{
			Piece = piece;
			Color = color;
		}

		public bool IsWhiteKing
		{
			get { return Piece == Piece.King && Color == PieceColor.White; }
		}

		public override string ToString()
		{
			string c = Piece == null ? " ." : " " + Piece.Sign;
			return Color == PieceColor.Black ? c.ToLower() : c;
		}
	}
}