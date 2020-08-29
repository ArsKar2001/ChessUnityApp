using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Представляет класс шахмат
    /// </summary>
    public class Chess
    {

        public string Fen => board.Fen;

        readonly Board board;
        readonly MovesFigures moves;

        /// <summary>
        /// Параметрический конструктор.
        /// </summary>
        /// <param name="fen">
        /// Запись позиций с помощью нотации Форсайта—Эдвардса (FEN).
        /// Начальная позиция шахматной партии:
        ///    rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
        /// </param>
        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            board = new Board(fen);
            moves = new MovesFigures(board);
        }
        /// <summary>
        /// Дополнительный конструктор для создания шахмат на новой доске.
        /// </summary>
        /// <param name="board"></param>
        private Chess(Board board)
        {
            this.board = board;
            moves = new MovesFigures(board);
        }
        /// <summary>
        /// Совершается ход/Создается новая доска с измененным расположением фигур
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public Chess Move(string move)
        {
            MotionFigure motionFigure = new MotionFigure(move);
            if (!moves.CanMove(motionFigure)) return this;

            Board nextBoard = board.Move(motionFigure);
            Chess nexChess = new Chess(nextBoard);

            return nexChess;
        }
        /// <summary>
        /// Возвращает код фигуры на определенной ячейке доски.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public char GetFigure(int x, int y)
        {
            Square square = new Square(x, y);
            Figure figure = board.GetFigureOnSquare(square);
            return figure == Figure.none ? '.' : (char)figure;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> YieldValidMoves()
        {
            foreach (FigureOnSquare figureOnSquare in board.YieldFiguresOnSquare())
                foreach (Square toItem in Square.YieldBoardMoves())
                    foreach (Figure promotion in figureOnSquare.Figure.Promotions(toItem))
                    {
                        MotionFigure motionFigure = new MotionFigure(figureOnSquare, toItem, promotion);
                        if (moves.CanMove(motionFigure))
                            if(!board.IsCheckShahAfter(motionFigure))
                                yield return motionFigure.ToString();
                    }
        }
    }
}
