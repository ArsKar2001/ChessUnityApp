using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessLibrary
{
    /// <summary>
    /// Представление всех фигур для шахмат, включая пустую клетку none
    /// </summary>
    enum Figure
    {
        none,

        whiteKing = 'K',
        whiteQueen = 'Q',
        whiteRook = 'R',
        whiteBishop = 'B',
        whiteKnight = 'N',
        whitePawn = 'P',

        blackKing = 'k',
        blackQueen = 'q',
        blackRook = 'r',
        blackBishop = 'b',
        blackKnight = 'n',
        blackPawn = 'p'
    }
    /// <summary>
    /// Описывает свойства фигур.
    /// </summary>
    static class FigureMetods
    {
        /// <summary>
        /// Определяет какого цвета фигура.
        /// </summary>
        /// <param name="figure"></param>
        /// <returns></returns>
        public static Color GetColor(this Figure figure)
        {
            if (figure == Figure.none) return Color.none;

            return
                ((char)figure).ToString().ToLower() == ((char)figure).ToString() ?
                Color.black : Color.white;
        }
        /// <summary>
        /// Определяет возможные превращения для пешки.
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static IEnumerable<Figure> Promotions(this Figure figure, Square to)
        {
            switch (figure)
            {
                case Figure.whitePawn when to.X == 7:
                    yield return Figure.whiteQueen;
                    yield return Figure.whiteRook;
                    yield return Figure.whiteBishop;
                    yield return Figure.whiteKnight;
                    break;
                case Figure.blackPawn when to.X == 0:
                    yield return Figure.blackQueen;
                    yield return Figure.blackRook;
                    yield return Figure.blackBishop;
                    yield return Figure.blackKnight;
                    break;
                default:
                    yield return Figure.none;
                    break;
            }
        }
    }
}
