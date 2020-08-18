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
            return CanMoveFrom() && CanMoveTo();
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
    }
}
