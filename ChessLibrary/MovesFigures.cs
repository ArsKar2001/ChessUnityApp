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
        MotionFigure motionFigure;
        BoardClass board;

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
            return motionFigure.to.OnBoard() &&
                   board.GetFigureOnSquare(motionFigure.to).GetColor() != board.moveColor;
        }
        /// <summary>
        /// Может ли пойти оттуда...
        /// </summary>
        /// <returns></returns>
        private bool CanMoveFrom()
        {
            return motionFigure.from.OnBoard() &&
                   motionFigure.figure.GetColor() == board.moveColor;
        }
    }
}
