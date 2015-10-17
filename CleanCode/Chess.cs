using System.IO;

namespace CleanCode
{
    public class Chess
    {
        private Board board;
        public string Result;

        public void Load(StreamReader reader)
        {
            board = new Board(reader);
        }

        // Определяет мат, шах или пат белым.
        public void Solve()
        {
            var isCheck = IsBad();
            var hasMoves = false;
            foreach (Location from in board.GetPieces(PieceColor.White))
            {
                foreach (Location to in board.Get(from).Piece.GetMoves(from, board))
                {
                    var old = board.Get(to);
                    board.Set(to, board.Get(from));
                    board.Set(from, Cell.Empty);
                    if (!IsBad())
                        hasMoves = true;
                    board.Set(from, board.Get(to));
                    board.Set(to, old);
                }
            }
            if (isCheck)
                if (hasMoves)
                    Result = "check";
                else Result = "mate";
            else 
                if (hasMoves) Result = "ok";
                else Result = "stalemate";
        }

        private bool IsBad()
        {
            bool isCheck = false;
            foreach (Location loc in board.GetPieces(PieceColor.Black))
            {
                var cell = board.Get(loc);
                var moves = cell.Piece.GetMoves(loc, board);
                foreach (Location to in moves)
                {
                    if (board.Get(to).IsWhiteKing)
                        isCheck = true;
                }
            }
            if (isCheck) return true;
            return false;
        }
    }
}