using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Представляет объект - доска
    /// </summary>
    class Board
    {
        /// <summary>
        /// Позиция фигур на доске
        /// </summary>
        public string Fen { get; protected set; }
        /// <summary>
        /// Флаг хода фигур
        /// </summary>
        public Color moveColor { get; protected set; }

        /// <summary>
        /// Матрица фигур
        /// </summary>
        protected Figure[,] figures;

        /// <summary>
        /// Может ли ракироваться с ладьей на А1
        /// </summary>
        public bool CanCastleA1 { get; protected set; }
        /// <summary>
        /// Может ли ракироваться с ладьей на А8
        /// </summary>
        public bool CanCastleA8 { get; protected set; }
        /// <summary>
        /// Может ли ракироваться с ладьей на H1
        /// </summary>
        public bool CanCastleH1 { get; protected set; }
        /// <summary>
        /// Может ли ракироваться с ладьей на H8
        /// </summary>
        public bool CanCastleH8 { get; protected set; }
        /// <summary>
        /// Битое поле для пешки
        /// </summary>
        public Square enpSquare { get; protected set; }
        /// <summary>
        /// Номер хода для проверки "50 ходов" - 
        /// если в течении 50 ходов не происходит движение пешек и взятия фигур, то партия засчитывается ничьей.
        /// </summary>
        public int DrawNumber { get; protected set; }
        /// <summary>
        /// Номер хода
        /// </summary>
        public int moveNumber { get; protected set; }
        /// <summary>
        /// Создает экземпляр новой доски по новой махматной нотации FEN
        /// </summary>
        /// <param name="fen">Запись позиций с помощью нотации Форсайта—Эдвардса (FEN)</param>
        public Board(string fen)
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
        public Board Move(MotionFigure motionFigure)
        {
            return new BoardNextMove(Fen, motionFigure);
        }
        /// <summary>
        /// Формирует перечисление всех фигур одного цвета на клетках доски, в зависимости от того, чей ход.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FigureOnSquare> YieldFiguresOnSquare()
        {
            foreach (Square square in Square.YieldBoardMoves())
                if (GetFigureOnSquare(square).GetColor() == moveColor)
                    yield return new FigureOnSquare(GetFigureOnSquare(square), square);
        }
        /// <summary>
        /// Получаем фигуру на ячейке доски.
        /// </summary>
        /// <param name="square">Ячейка доски.</param>
        /// <returns></returns>
        public Figure GetFigureOnSquare(Square square)
        {
            if (square.OnBoard()) return figures[square.X, square.Y];
            return Figure.none;
        }
        /// <summary>
        /// Определяет, находится ли сейчас король под шахом.
        /// </summary>
        /// <returns></returns>
        internal bool IsCheckShah()
        {
            return IsCheckAfterMove(MotionFigure.none);
        }
        /// <summary>
        /// Определяет, окажется ли под шахом король, если сделает выбранный ход.
        /// </summary>
        /// <param name="motionFigure">Ячейка, куда ходит король</param>
        /// <returns></returns>
        internal bool IsCheckAfterMove(MotionFigure motionFigure)
        {
            Board after = Move(motionFigure);
            return after.CanEatKing();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CanEatKing()
        {
            Square squareToAlienKing = FindAlienKing();
            MovesFigures moves = new MovesFigures(this);
            foreach(FigureOnSquare figureOnSquare in YieldFiguresOnSquare())
            {
                if (moves.CanMove(new MotionFigure(figureOnSquare, squareToAlienKing)))
                    return true;
            }
            return false;
        }

        private Square FindAlienKing()
        {
            Figure figureKing = moveColor == Color.white
                ? Figure.blackKing
                : Figure.whiteKing;
            foreach (var itemSquare in Square.YieldBoardMoves())
            {
                if (GetFigureOnSquare(itemSquare) == figureKing)
                    return itemSquare;
            }
            return Square.none;
        }
        /// <summary>
        /// Обрабатывает строку FEN для инициализации шахматной партии.
        /// </summary>
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
        }
        /// <summary>
        /// Номер хода. Любой позиции может быть присвоено любое неотрицательное значение (по умолчанию 1), счётчик увеличивается на 1 после каждого хода чёрных.
        /// </summary>
        /// <param name="v"></param>
        private void InitMoveNumber(string v) => moveNumber = int.Parse(v);
        /// <summary>
        /// Определяет число полуходов, прошедших с последнего хода пешки или взятия фигуры. Используется для определения применения правила 50 ходов.
        /// </summary>
        /// <param name="v"></param>
        private void InitDrawNumber(string v) => DrawNumber = int.Parse(v);
        /// <summary>
        /// Возможность взятия пешки на проходе. Указывается проходимое поле, иначе «-».
        /// </summary>
        /// <param name="v"></param>
        private void InitEnpSquare(string v) => enpSquare = new Square(v);
        /// <summary>
        /// Возможность рокировки. k — в сторону королевского фланга (короткая), q — в сторону ферзевого фланга (длинная). Заглавными указываются белые. Невозможность рокировки обозначается «-».
        /// </summary>
        /// <param name="v"></param>
        private void InitCanCastle(string v)
        {
            CanCastleA1 = v.Contains("Q");
            CanCastleH1 = v.Contains("K");
            CanCastleA8 = v.Contains("q");
            CanCastleH8 = v.Contains("k");
        }
        /// <summary>
        /// Активная сторона: w — следующий ход принадлежит белым, b — следующий ход чёрных
        /// </summary>
        /// <param name="v"></param>
        private void InitMoveColor(string v) => moveColor = (v == "b") ? Color.black : Color.white;
        /// <summary>
        /// Инициализация массива фигур из переданной строки Fen.
        /// Положение фигур со стороны белых. Позиция описывается цифрами и буквами по горизонталям сверху вниз начиная с восьмой горизонтали и заканчивая первой. Расположение фигур на горизонтали записывается слева направо, данные каждой горизонтали разделяются косой чертой /. Белые фигуры обозначаются заглавными буквами. K, Q, R, B, N, P — соответственно белые король, ферзь, ладья, слон, конь, пешка. k, q, r, b, n, p — соответственно чёрные король, ферзь, ладья, слон, конь, пешка. Обозначения фигур взяты из англоязычного варианта алгебраической нотации. Цифра задаёт количество пустых полей на горизонтали, счёт ведётся либо от левого края доски, либо после фигуры (8 означает пустую горизонталь).
        /// </summary>
        /// <param name="v">Запись расположения фигур в строковом представлении (Fen)</param>
        private void InitFigures(string v)
        {
            for (int i = 8; i >= 2; i--)
                v = v.Replace(i.ToString(), (i - 1).ToString() + "1");

            v = v.Replace('1', (char)Figure.none);
            
            string[] vs = v.Split('/');

            for (int i = 7; i >= 0; i--)
                for (int j = 0; j < 8; j++)
                    figures[i, j] = (Figure)vs[7 - i][j];
        }
    }
}
