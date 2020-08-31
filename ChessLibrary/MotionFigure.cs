using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessLibrary
{
    /// <summary>
    /// Представляет перемещение фигуры на доске
    /// </summary>
    class MotionFigure
    {
        /// <summary>
        /// Выбранная фигура.
        /// </summary>
        public Figure Figure { get; private set; }

        public Figure PlacedFigure => promotionFigure == Figure.none ? Figure : promotionFigure;
        /// <summary>
        /// Откуда идет,...
        /// </summary>
        public Square From { get; private set; }
        /// <summary>
        /// ...куда может пойти,...
        /// </summary>
        public Square To { get; private set; }
        /// <summary>
        /// ...может ли превратиться.
        /// </summary>
        public Figure promotionFigure { get; private set; }
        /// <summary>
        /// Статический пустой экземпляр класса MotionFigure.
        /// </summary>
        public static MotionFigure none = new MotionFigure();
        /// <summary>
        /// Конструктор, создающий пустой экземпляр класса MotionFigure.
        /// </summary>
        public MotionFigure()
        {
            Figure = Figure.none;
            From = Square.none;
            To = Square.none;
            promotionFigure = Figure.none;
        }
        /// <summary>
        /// Параметрический конструктор
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="to"></param>
        /// <param name="promotionFigure"></param>
        public MotionFigure(FigureOnSquare figure, Square to, Figure promotionFigure = Figure.none)
        {
            this.Figure = figure.Figure;
            this.From = figure.Square;
            this.To = to;
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
            this.Figure = (Figure)move[0];
            this.From = new Square(move.Substring(1,2));
            this.To = new Square(move.Substring(3,2));
            if (move.Length == 6) this.promotionFigure = (Figure)move[5];
            else this.promotionFigure = Figure.none;
        }
        /// <summary>
        /// Возвращает строковое представление объекта MotionFigure
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            ((char)Figure).ToString() +
            From.Name +
            To.Name +
            (promotionFigure == Figure.none ? "" : ((char)promotionFigure).ToString());
        /// <summary>
        /// Смещение по вертикали доски.
        /// </summary>
        public int DeltaX =>
            To.X - From.X;
        /// <summary>
        /// Смещение по горизонтали доски.
        /// </summary>
        public int DeltaY =>
            To.Y - From.Y;

        public int AbsDeltaX => Math.Abs(DeltaX);
        public int AbsDeltaY => Math.Abs(DeltaY);

        public int SignX => Math.Sign(DeltaX);
        public int SignY => Math.Sign(DeltaY);
    }
}
