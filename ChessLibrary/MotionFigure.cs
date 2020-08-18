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
        public Figure Figure { get; private set; }
        /// <summary>
        /// откуда идет,
        /// </summary>
        public Square From { get; private set; }
        /// <summary>
        /// куда может пойти,
        /// </summary>
        public Square To { get; private set; }
        /// <summary>
        /// может ли превратиться.
        /// </summary>
        public Figure PromotionFigure { get; private set; }
        /// <summary>
        /// Параметрический конструктор
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="promotionFigure"></param>
        public MotionFigure(FigureOnSquare figure, Square to, Figure promotionFigure = Figure.none)
        {
            this.Figure = figure.Figure;
            this.From = figure.Square;
            this.To = to;
            this.PromotionFigure = promotionFigure;
        }
        /// <summary>
        /// Перемещение фигуры.
        /// Пример: Pa2a3, Pa7a8Q(с превращением)
        ///         01234  012345
        /// </summary>
        /// <param name="move"></param>
        public MotionFigure(string move)
        {
            this.Figure = (Figure)move[0];
            this.From = new Square(move.Substring(1,2));
            this.To = new Square(move.Substring(3,2));
            if (move.Length == 6) this.PromotionFigure = (Figure)move[5];
            else this.PromotionFigure = Figure.none;
        }
        /// <summary>
        /// Возвращает строковое представление объекта MotionFigure
        /// </summary>
        /// <returns></returns>
        public override string ToString() => 
            ((char)Figure).ToString() + From.Name + To.Name;

        public int DeltaX =>
            To.X - From.X;
        public int DeltaY =>
            To.Y - From.Y;
        public int AbsDeltaX => Math.Abs(DeltaX);
        public int AbsDeltaY => Math.Abs(DeltaY);
    }
}
