using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessLibrary
{
    /// <summary>
    /// Перечисление цветов фигур
    /// </summary>
    enum Color
    {
        none,
        white,
        black
    }

    static class ColorMetods
    {
        /// <summary>
        /// Смена цвета на каждый ход
        /// </summary>
        /// <param name="color">Входящий свет фигуры</param>
        /// <returns></returns>
        public static Color FlipColor(this Color color)
        {
            switch (color)
            {
                case Color.black:
                    return Color.white;
                case Color.white:
                    return Color.black;
                default:
                    return color;
            }
        }
    }
}
