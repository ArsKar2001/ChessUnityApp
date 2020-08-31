using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessLibrary
{
    /// <summary>
    /// Представляет класс шахмат
    /// </summary>
    public class Chess
    {
        /// <summary>
        /// Запись позиций с помощью нотации Форсайта—Эдвардса (FEN).
        /// Начальная позиция шахматной партии:
        ///    rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
        /// </summary>
        public string Fen => board.Fen;

        public bool IsCheckShahk { private set; get; }
        public bool IsCheckMat { private set; get; }
        public bool IsCheckPat { private set; get; }

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
            SetCheckFlags();
        }
        /// <summary>
        /// Совершается ход/Создается новая доска с измененным расположением фигур
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public Chess Move(string move)
        {
            if (!IsValidMove(move))
                return this;

            MotionFigure motionFigure = new MotionFigure(move);
            Board nextBoard = board.Move(motionFigure);
            Chess nexChess = new Chess(nextBoard);
            return nexChess;
        }
        public bool IsValidMove(string move)
        {
            MotionFigure motionFigure = new MotionFigure(move);

            if (!moves.CanMove(motionFigure)) return false;
            if (board.IsCheckAfterMove(motionFigure)) return false;

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        void SetCheckFlags()
        {
            IsCheckShahk = board.IsCheckShah();
            IsCheckMat = false;
            IsCheckPat = false;

            foreach (var item in YieldValidMoves())
                return;

            if (IsCheckShahk)
                IsCheckMat = true;
            else
                IsCheckPat = true;
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
        /// Возвращает код фигуры на определенной ячейке доски.
        /// </summary>
        /// <param name="nameXY"></param>
        /// <returns></returns>
        public char GetFigure(string nameXY)
        {
            Square square = new Square(nameXY);
            Figure figure = board.GetFigureOnSquare(square);
            return figure == Figure.none ? '.' : (char)figure;
        }
        /// <summary>
        /// Генерирует перечисление всех возможных ходов на доске для фигур. 
        /// А также варинты превращения пешки.
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
                            if(!board.IsCheckAfterMove(motionFigure))
                                yield return motionFigure.ToString();
                    }
        }
    }
}
