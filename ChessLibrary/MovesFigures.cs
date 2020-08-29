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
                   board.GetFigureOnSquare(motionFigure.To).GetColor() != board.moveColor;
        }
        /// <summary>
        /// Может ли пойти оттуда...
        /// </summary>
        /// <returns></returns>
        private bool CanMoveFrom()
        {
            return motionFigure.From.OnBoard() &&
                   motionFigure.Figure.GetColor() == board.moveColor;
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
                    return CanMoveKing() || CanKingCastle();

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
                    return CanPawnMove();

                default:
                    return false;
            }
        }

        private bool CanKingCastle()
        {
            return CanBlackKingCastle() || CanWhiteKingCastle();
        }

        private bool CanBlackKingCastle()
        {
            if (motionFigure.Figure == Figure.blackKing)
            {
                if (motionFigure.From == new Square("e8"))
                    if (motionFigure.To == new Square("g8"))
                        if (board.CanCastleH8)
                            if (board.GetFigureOnSquare(new Square("h8")) == Figure.blackRook)
                                if (board.GetFigureOnSquare(new Square("f8")) == Figure.none)
                                    if (board.GetFigureOnSquare(new Square("g8")) == Figure.none)
                                        if(board.IsCheckShah())
                                        if(board.IsCheckAfterMove(new MotionFigure("Ke8f8")))
                                        return true;
                if (motionFigure.From == new Square("e8"))
                    if (motionFigure.To == new Square("c8"))
                        if (board.CanCastleA8)
                            if (board.GetFigureOnSquare(new Square("a8")) == Figure.blackRook)
                                if (board.GetFigureOnSquare(new Square("d8")) == Figure.none)
                                    if (board.GetFigureOnSquare(new Square("c8")) == Figure.none)
                                        if (board.GetFigureOnSquare(new Square("b8")) == Figure.none)
                                            if(board.IsCheckShah())
                                            if(board.IsCheckAfterMove(new MotionFigure("Ke8d8")))
                                            return true;
            }
            return false;
        }

        private bool CanWhiteKingCastle()
        {
            if (motionFigure.Figure == Figure.whiteKing)
            {
                if (motionFigure.From == new Square("e1"))
                    if (motionFigure.To == new Square("g1"))
                        if (board.CanCastleH1)
                            if (board.GetFigureOnSquare(new Square("h1")) == Figure.whiteRook)
                                if (board.GetFigureOnSquare(new Square("f1")) == Figure.none)
                                    if (board.GetFigureOnSquare(new Square("g1")) == Figure.none)
                                        if(board.IsCheckShah())
                                        if(board.IsCheckAfterMove(new MotionFigure("Ke1f1")))
                                        return true;
                if (motionFigure.From == new Square("e1"))
                    if (motionFigure.To == new Square("c1"))
                        if (board.CanCastleA1)
                            if (board.GetFigureOnSquare(new Square("a1")) == Figure.whiteRook)
                                if (board.GetFigureOnSquare(new Square("d1")) == Figure.none)
                                    if (board.GetFigureOnSquare(new Square("c1")) == Figure.none)
                                        if (board.GetFigureOnSquare(new Square("b1")) == Figure.none)
                                            if(board.IsCheckShah())
                                            if(board.IsCheckAfterMove(new MotionFigure("Ke1d1")))
                                            return true;
            }
            return false;
        }

        private bool CanPawnMove()
        {
            if (motionFigure.From.X < 1 || motionFigure.From.Y > 6)
                return false;
            int stepX = motionFigure.Figure.GetColor() == Color.white ? +1 : -1;
            return CanPawnGoStep(stepX) || 
                CanPawnJump(stepX) || 
                CanPawnEat(stepX) ||
                CanPavwEnp(stepX);
        }

        private bool CanPavwEnp(int stepX)
        {
            if (motionFigure.To == board.enpSquare)
                if (board.GetFigureOnSquare(motionFigure.To) == Figure.none)
                    if (motionFigure.DeltaX == stepX)
                        if (motionFigure.AbsDeltaY == 1)
                            if (stepX == +1 && motionFigure.From.X == 4 ||
                                stepX == -1 && motionFigure.From.X == 3)
                                return true;
            return false;
        }

        private bool CanPawnGoStep(int stepX)
        {
            if (board.GetFigureOnSquare(motionFigure.To) == Figure.none)
                if (motionFigure.DeltaY == 0)
                    if (motionFigure.DeltaX == stepX)
                        return true;
            return false;
        }

        private bool CanPawnJump(int stepX)
        {
            if (board.GetFigureOnSquare(motionFigure.To) == Figure.none)
                if ((motionFigure.From.X == 1 && stepX == +1)
                    || (motionFigure.From.X == 6 && stepX == -1))
                    if (motionFigure.DeltaY == 0)
                        if (motionFigure.DeltaX == 2 * stepX)
                            if (board.GetFigureOnSquare(
                                new Square(motionFigure.To.X, motionFigure.To.Y)
                                ) == Figure.none)
                                return true;
            return false;
        }

        private bool CanPawnEat(int stepX)
        {
            if (board.GetFigureOnSquare(motionFigure.To) != Figure.none)
                if (motionFigure.DeltaX == stepX)
                    if (motionFigure.AbsDeltaY == 1)
                        if (board.GetFigureOnSquare(
                                new Square(motionFigure.To.X, motionFigure.To.Y)
                                ) != Figure.none)
                            return true;
            return false;
        }

        private bool CanMoveQueen()
        {
            Square squareAt = motionFigure.From;
            do
            {
                squareAt = new Square(squareAt.X + motionFigure.SignX, squareAt.Y + motionFigure.SignY);
                if (squareAt == motionFigure.To)
                    return true;

            } while (squareAt.OnBoard() && board.GetFigureOnSquare(squareAt) == Figure.none);
            return false;
        }

        private bool CanMoveBishop()
        {
            Square squareAt = motionFigure.From;
            do
            {
                squareAt = new Square(squareAt.X + motionFigure.SignX, squareAt.Y + motionFigure.SignY);
                if (squareAt == motionFigure.To)
                    if (motionFigure.AbsDeltaX == motionFigure.AbsDeltaY)
                        return true;

            } while (squareAt.OnBoard() && board.GetFigureOnSquare(squareAt) == Figure.none);
            return false;
        }

        private bool CanMovrRook()
        {
            Square squareAt = motionFigure.From;
            do
            {
                squareAt = new Square(squareAt.X + motionFigure.SignX, squareAt.Y + motionFigure.SignY);
                if (squareAt == motionFigure.To)
                    if (motionFigure.AbsDeltaX == 0 || motionFigure.AbsDeltaY == 0)
                        return true;

            } while (squareAt.OnBoard() && board.GetFigureOnSquare(squareAt) == Figure.none);
            return false;
        }

        private bool CanMoveKnight() =>
            (motionFigure.AbsDeltaX == 2 && motionFigure.AbsDeltaY == 1) ||
            (motionFigure.AbsDeltaX == 1 && motionFigure.AbsDeltaY == 2);

        private bool CanMoveKing() =>
                    motionFigure.AbsDeltaX <= 1 && motionFigure.AbsDeltaY <= 1;
    }
}