using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Представляет поверку на ход фигуры...
    /// </summary>
    class MovesFigures
    {
        private MotionFigure motionFigure;
        readonly BoardClass board;

        public MovesFigures(BoardClass board)
        {
            this.board = board;
        }
        /// <summary>
        /// Может ли сходить
        /// </summary>
        /// <param name="motionFigure"></param>
        /// <returns></returns>
        public bool CanMove(MotionFigure motionFigure)
        {
            this.motionFigure = motionFigure;
            return CanMoveFrom() && CanMoveTo() && CanFigureMove();
        }

        /// <summary>
        /// Может ди пойти туда...
        /// </summary>
        /// <returns></returns>
        private bool CanMoveTo()
        {
            return motionFigure.To.OnBoard() &&
                   board.GetFigureOnSquare(motionFigure.To).GetColor() != board.MoveColor;
        }
        /// <summary>
        /// Может ли пойти оттуда...
        /// </summary>
        /// <returns></returns>
        private bool CanMoveFrom()
        {
            return motionFigure.From.OnBoard() &&
                   motionFigure.Figure.GetColor() == board.MoveColor;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool CanFigureMove()
        {
            switch (motionFigure.Figure)
            {
                case Figure.whiteKing:
                case Figure.blackKing:
                    return CanMoveKing();

                case Figure.whiteQueen:
                case Figure.blackQueen:
                    return false;

                case Figure.whiteRook:
                case Figure.blackRook:
                    return CanMovrRook();

                case Figure.whiteBishop:
                case Figure.blackBishop:
                    return false;

                case Figure.whiteKnight:
                case Figure.blackKnight:
                    return CanMoveKnight();

                case Figure.whitePawn:
                case Figure.blackPawn:
                    return false;

                default:
                    return false;
            }
        }

        private bool CanMovrRook() =>
            motionFigure.AbsDeltaX == 0 || motionFigure.AbsDeltaY == 0;

        private bool CanMoveKnight() =>
            (motionFigure.AbsDeltaX == 2 && motionFigure.AbsDeltaY == 1) ||
            (motionFigure.AbsDeltaX == 1 && motionFigure.AbsDeltaY == 2);

        private bool CanMoveKing() =>
            motionFigure.AbsDeltaX <= 1 && motionFigure.AbsDeltaY <= 1;
    }
}