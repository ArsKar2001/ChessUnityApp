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
        readonly Board board;

        public MovesFigures(Board board)
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
                    return CanMoveQueen();

                case Figure.whiteRook:
                case Figure.blackRook:
                    return CanMovrRook();

                case Figure.whiteBishop:
                case Figure.blackBishop:
                    return CanMoveBishop();

                case Figure.whiteKnight:
                case Figure.blackKnight:
                    return CanMoveKnight();

                case Figure.whitePawn:
                case Figure.blackPawn:
                    return CanMovePawn();

                default:
                    return false;
            }
        }

        private bool CanMovePawn()
        {
            if (motionFigure.From.X < 1 || motionFigure.From.Y > 6)
                return false;
            int stepY = motionFigure.Figure.GetColor() == Color.white ? +1 : -1;
            return CanPawnGoStep(stepY) || CanPawnJump(stepY) || CanPawnEat(stepY);
        }

        private bool CanPawnGoStep(int stepY)
        {
            if (board.GetFigureOnSquare(motionFigure.To) == Figure.none)
                if (motionFigure.DeltaY == 0)
                    if (motionFigure.DeltaX == stepY)
                        return true;
            return false;
        }

        private bool CanPawnJump(int stepY)
        {
            if (board.GetFigureOnSquare(motionFigure.To) == Figure.none)
                if ((motionFigure.From.X == 1 && stepY == +1)
                    || (motionFigure.From.X == 6 && stepY == -1))
                    if (motionFigure.DeltaY == 0)
                        if (motionFigure.DeltaX == 2 * stepY)
                            if (board.GetFigureOnSquare(
                                new Square(motionFigure.To.X, motionFigure.To.Y)
                                ) == Figure.none)
                                return true;
            return false;
        }

        private bool CanPawnEat(int stepY)
        {
            if(board.GetFigureOnSquare(motionFigure.To) != Figure.none)
                if(motionFigure.DeltaX == stepY)
                    if(motionFigure.AbsDeltaY == 1)
                        if (board.GetFigureOnSquare(
                                new Square(motionFigure.To.X, motionFigure.To.Y)
                                ) != Figure.none)
                            return true;
            return false;
        }

        private bool CanMoveQueen() =>
            (motionFigure.AbsDeltaX == motionFigure.AbsDeltaY) ||
            motionFigure.AbsDeltaX == 0 || motionFigure.AbsDeltaY == 0;

        private bool CanMoveBishop() =>
            motionFigure.AbsDeltaX == motionFigure.AbsDeltaY;

        private bool CanMovrRook() =>
            motionFigure.AbsDeltaX == 0 || motionFigure.AbsDeltaY == 0;

        private bool CanMoveKnight() =>
            (motionFigure.AbsDeltaX == 2 && motionFigure.AbsDeltaY == 1) ||
            (motionFigure.AbsDeltaX == 1 && motionFigure.AbsDeltaY == 2);

        private bool CanMoveKing() =>
            motionFigure.AbsDeltaX <= 1 && motionFigure.AbsDeltaY <= 1;
    }
}