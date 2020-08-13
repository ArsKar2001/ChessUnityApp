using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    static class FigureMetods
    {
        public static Color GetColor (this Figure figure)
        {
            if (figure == Figure.none) return Color.none;

            return
                ((char)figure).ToString().ToLower() == ((char)figure).ToString() ?
                Color.black : Color.white;
        }
    }
}
