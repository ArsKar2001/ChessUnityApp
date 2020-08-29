using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// 
    /// </summary>
    class BoardNextMove : Board
    {
        MotionFigure motionFigure;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fen"></param>
        /// <param name="motionFigure"></param>
        public BoardNextMove(string fen, MotionFigure motionFigure) : base(fen)
        {
            this.motionFigure = motionFigure;
            MoveFigure();

            DropFigureEnpassantSquare();
            SetEnpassantSquare();

            RookMovementToCastle();
            UpdateCastleFlags();

            MoveNumber();
            UpdateColorMove();

            GenerateFEN();
        }

        private void RookMovementToCastle()
        {
            if (motionFigure.Figure == Figure.whiteKing)
                if (motionFigure.From == new Square("e1"))
                {
                    if (motionFigure.To == new Square("g1"))
                    {
                        SetFigureOnSquare(new Square("h1"), Figure.none);
                        SetFigureOnSquare(new Square("f1"), Figure.whiteRook);
                        return;
                    }
                    if (motionFigure.To == new Square("c1"))
                    {
                        SetFigureOnSquare(new Square("a1"), Figure.none);
                        SetFigureOnSquare(new Square("d1"), Figure.whiteRook);
                        return;
                    }
                }
            if (motionFigure.Figure == Figure.blackKing)
                if (motionFigure.From == new Square("e8"))
                {
                    if (motionFigure.To == new Square("g8"))
                    {
                        SetFigureOnSquare(new Square("h8"), Figure.none);
                        SetFigureOnSquare(new Square("f8"), Figure.blackRook);
                        return;
                    }
                    if (motionFigure.To == new Square("c8"))
                    {
                        SetFigureOnSquare(new Square("a8"), Figure.none);
                        SetFigureOnSquare(new Square("d8"), Figure.blackRook);
                        return;
                    }
                }
        }

        private void UpdateCastleFlags()
        {
            switch (motionFigure.Figure)
            {
                case Figure.whiteKing:
                    CanCastleA1 = false;
                    CanCastleH1 = false;
                    return;
                case Figure.whiteRook:
                    if (motionFigure.From == new Square("a1"))
                        CanCastleA1 = false;
                    if (motionFigure.From == new Square("h1"))
                        CanCastleH1 = false;
                    return;
                case Figure.blackKing:
                    CanCastleA8 = false;
                    CanCastleH8 = false;
                    return;
                case Figure.blackRook:
                    if (motionFigure.From == new Square("a8"))
                        CanCastleA8 = false;
                    if (motionFigure.From == new Square("h8"))
                        CanCastleH8 = false;
                    return;
                default: return;
            }
        }

        private void DropFigureEnpassantSquare()
        {
            if (motionFigure.To == enpSquare)
                if (motionFigure.Figure == Figure.blackPawn ||
                   motionFigure.Figure == Figure.whitePawn)
                    SetFigureOnSquare(new Square(motionFigure.From.X, motionFigure.To.Y), Figure.none);
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetEnpassantSquare()
        {
            enpSquare = Square.none;
            if (motionFigure.Figure == Figure.whitePawn)
                if (motionFigure.From.X == 1 && motionFigure.To.X == 3)
                    enpSquare = new Square(2, motionFigure.From.Y);
            if (motionFigure.Figure == Figure.blackPawn)
                if (motionFigure.From.X == 6 && motionFigure.To.X == 4)
                    enpSquare = new Square(5, motionFigure.From.Y);
        }
        /// <summary>
        /// 
        /// </summary>
        private void UpdateColorMove()
        {
            moveColor = moveColor.FlipColor();
        }
        /// <summary>
        /// 
        /// </summary>
        private void MoveNumber()
        {
            if (moveColor == Color.black)
                moveNumber++;
        }
        /// <summary>
        /// 
        /// 
        /// </summary>
        private void MoveFigure()
        {
            SetFigureOnSquare(motionFigure.From, Figure.none);
            SetFigureOnSquare(motionFigure.To, motionFigure.PlacedFigure);
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
        /// <summary>
        /// 
        /// </summary>
        private void GenerateFEN() => Fen = String.Format("{0} {1} {2} {3} {4} {5}",
                FenGetFigures(), FenGetMoveColor(),
                FenGetCanCastle(), FenGetEnpSquaret(),
                FenGetDrawNumber(), FenGetMoveNumber());
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string FenGetFigures()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 7; i >= 0; i--)
            {
                for (int j = 0; j < 8; j++)
                    sb.Append(figures[i, j] == Figure.none ? '1' : (char)figures[i, j]);
                if (i > 0)
                    sb.Append("/");
            }
            string temp = "11111111";
            for (int i = 8; i >= 2; i--)
            {
                sb = sb.Replace(temp.Substring(0, i), i.ToString());
            }
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string FenGetMoveColor() => 
            this.moveColor == Color.black ? "b" : "w";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string FenGetCanCastle()
        {
            string var = (CanCastleA1 ? "Q" : "") +
            (CanCastleA8 ? "q" : "") +
            (CanCastleH1 ? "K" : "") +
            (CanCastleH8 ? "k" : "");
            return var == "" ? "-" : var;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string FenGetEnpSquaret() => this.enpSquare.Name;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string FenGetDrawNumber() => this.DrawNumber.ToString();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string FenGetMoveNumber() =>
            (moveColor == Color.white) ?
            (moveNumber + 1).ToString() :
            moveNumber.ToString();
        
    }
}
