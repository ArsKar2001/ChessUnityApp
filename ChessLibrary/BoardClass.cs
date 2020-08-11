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
        public string fen { get; private set; }
        /// <summary>
        /// Флаг хода фигур
        /// </summary>
        public Color moveColor { get; private set; }
        /// <summary>
        /// Матрица фигур
        /// </summary>
        Figure[,] figures;

        public BoardClass(string fen)
        {
            this.fen = fen;
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
            BoardClass nextBoardClass = new BoardClass(fen);
            nextBoardClass.SetFigureOnSquare(motionFigure.from, Figure.none);
            nextBoardClass.SetFigureOnSquare(motionFigure.to, motionFigure.figure);
            nextBoardClass.moveColor = moveColor.FlipColor();
            return nextBoardClass;
        }
        /// <summary>
        /// Получаем выбранную фигуру на доске
        /// </summary>
        /// <param name="square"></param>
        /// <returns></returns>
        public Figure GetFigureOnSquare(Square square)
        {
            if (square.OnBoard()) return figures[square.x, square.y];
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
                figures[square.x, square.y] = figure;
        }
        private void Init()
        {
            SetFigureOnSquare(new Square("a3"), Figure.blackKing);
            SetFigureOnSquare(new Square("a8"), Figure.whiteKing);
            moveColor = Color.white;
        }
    }
}
