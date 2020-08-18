using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Представляет объект - доска
    /// </summary>
    class BoardClass
    {
        /// <summary>
        /// Позиция фигур на доске
        /// </summary>
        public string Fen { get; private set; }
        /// <summary>
        /// Флаг хода фигур
        /// </summary>
        public Color MoveColor { get; private set; }

        /// <summary>
        /// Матрица фигур
        /// </summary>
        readonly Figure[,] figures;

        /// <summary>
        /// Может ли ракироваться с ладьей на А1
        /// </summary>
        public bool CanCastleA1 { get; private set; }
        /// <summary>
        /// Может ли ракироваться с ладьей на А8
        /// </summary>
        public bool CanCastleA8 { get; private set; }
        /// <summary>
        /// Может ли ракироваться с ладьей на H1
        /// </summary>
        public bool CanCastleH1 { get; private set; }
        /// <summary>
        /// Может ли ракироваться с ладьей на H8
        /// </summary>
        public bool CanCastleH8 { get; private set; }
        /// <summary>
        /// Битое поле для пешки
        /// </summary>
        public Square EnpSquare { get; private set; }
        /// <summary>
        /// Номер хода для проверки "50 ходов" - 
        /// если в течении 50 ходов не происходит движение пешек и взятия фигур, то партия засчитывается ничьей.
        /// </summary>
        public int DrawNumber { get; private set; }
        /// <summary>
        /// Номер хода
        /// </summary>
        public int MoveNumber { get; private set; }

        public BoardClass(string fen)
        {
            this.Fen = fen;
            figures = new Figure[8, 8];
            Init();
        }
        /// <summary>
        /// Движение фигур на достке
        /// </summary>
        /// <param name="motionFigure"></param>
        /// <returns></returns>
        public BoardClass Move(MotionFigure motionFigure)
        {
            BoardClass nextBoardClass = new BoardClass(Fen);
            nextBoardClass.SetFigureOnSquare(motionFigure.From, Figure.none);
            nextBoardClass.SetFigureOnSquare(motionFigure.To, motionFigure.Figure);
            nextBoardClass.MoveColor = MoveColor.FlipColor();
            nextBoardClass.GenerateFEN();
            return nextBoardClass;
        }

        public IEnumerable<FigureOnSquare> YieldFiguresOnSquare()
        {
            foreach (Square square in Square.YieldBoardMoves())
                if (GetFigureOnSquare(square).GetColor() == MoveColor)
                    yield return new FigureOnSquare(GetFigureOnSquare(square), square);
        }

        private void GenerateFEN()
        {
            Fen = String.Format("{0} {1} {2} {3} {4} {5}",
                FenGetFigures(), FenGetMoveColor(), 
                FenGetCanCastle(), FenGetEnpSquaret(), 
                FenGetDrawNumber(), FenGetMoveNumber());
        }

        private object FenGetFigures()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 7; i >= 0; i--)
            {
                for (int j = 0; j < 8; j++)
                    sb.Append(figures[i,j] == Figure.none ? '1' : (char)figures[i, j]);
                if (i > 0)
                    sb.Append("/");
            }
            string temp = "11111111";
            for (int i = 8; i >=2 ; i--)
            {
                sb = sb.Replace(temp.Substring(0, i), i.ToString());
            }
            return sb.ToString();
        }

        private object FenGetMoveColor() => this.MoveColor == Color.black ? "b" : "w";

        private object FenGetCanCastle()
        {
            string var = (CanCastleA1 ? "Q" : "") +
            (CanCastleA8 ? "q" : "") +
            (CanCastleH1 ? "K" : "") +
            (CanCastleH8 ? "k" : "");
            return var == "" ? "-" : var;
        }

        private object FenGetEnpSquaret() => this.EnpSquare.Name;

        private object FenGetDrawNumber() => this.DrawNumber.ToString();

        private object FenGetMoveNumber() =>
            (MoveColor == Color.white) ?
            (MoveNumber + 1).ToString() :
            MoveNumber.ToString();


        /// <summary>
        /// Получаем выбранную фигуру на доске
        /// </summary>
        /// <param name="square"></param>
        /// <returns></returns>
        public Figure GetFigureOnSquare(Square square)
        {
            if (square.OnBoard()) return figures[square.X, square.Y];
            return Figure.none;
        }
        /// <summary>
        /// Устанавливаем выбранную фигуру на доску
        /// </summary>
        /// <param name="square"></param>
        /// <param name="figure"></param>
        private void SetFigureOnSquare(Square square, Figure figure)
        {
            if (square.OnBoard())
                figures[square.X, square.Y] = figure;
        }
        private void Init()
        {
            // rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
            string[] fenParts = this.Fen.Split();
            InitFigures(fenParts[0]);
            InitMoveColor(fenParts[1]);
            InitCanCastle(fenParts[2]);
            InitEnpSquare(fenParts[3]);
            InitDrawNumber(fenParts[4]);
            InitMoveNumber(fenParts[5]);
            MoveColor = Color.white;
        }

        private void InitMoveNumber(string v) => MoveNumber = int.Parse(v);

        private void InitDrawNumber(string v) => DrawNumber = int.Parse(v);

        private void InitEnpSquare(string v) => EnpSquare = new Square(v);

        private void InitCanCastle(string v)
        {
            CanCastleA1 = v.Contains("Q");
            CanCastleH1 = v.Contains("K");
            CanCastleA8 = v.Contains("q");
            CanCastleH8 = v.Contains("k");
        }

        private void InitMoveColor(string v) => MoveColor = (v == "b") ? Color.black : Color.white;

        private void InitFigures(string v)
        {
            for (int i = 8; i >= 2; i--)
                v = v.Replace(i.ToString(), (i - 1).ToString() + "1");

            v = v.Replace('1', (char)Figure.none);
            string[] vs = v.Split('/');

            for (int i = 7; i >= 0; i--)
                for (int j = 0; j < 8; j++)
                    SetFigureOnSquare(new Square(i, j), (Figure)vs[7 - i][j]);
        }
    }
}
