using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    /// <summary>
    /// Представляет перемещение фигуры на доске
    /// </summary>
    class MotionFigure
    {
        /// <summary>
        /// Выбранная фигура:
        /// </summary>
        public Figure figure { get; private set; }
        /// <summary>
        /// откуда идет,
        /// </summary>
        public Square from { get; private set; }
        /// <summary>
        /// куда может пойти,
        /// </summary>
        public Square to { get; private set; }
        /// <summary>
        /// может ли превратиться.
        /// </summary>
        public Figure promotionFigure { get; private set; }
        /// <summary>
        /// Параметрический конструктор
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="promotionFigure"></param>
        public MotionFigure(Figure figure, Square from, Square to, Figure promotionFigure)
        {
            this.figure = figure;
            this.from = from;
            this.to = to;
            this.promotionFigure = promotionFigure;
        }
        /// <summary>
        /// Перемещение фигуры.
        /// Пример: Pa2a3, Pa7a8Q(с превращением)
        ///         01234  012345
        /// </summary>
        /// <param name="move"></param>
        public MotionFigure(string move)
        {
            this.figure = (Figure)move[0];
            this.from = new Square(move.Substring(1,2));
            this.to = new Square(move.Substring(3,2));
            if (move.Length == 6) this.promotionFigure = (Figure)move[5];
            else this.promotionFigure = Figure.none;
        }
    }
}
