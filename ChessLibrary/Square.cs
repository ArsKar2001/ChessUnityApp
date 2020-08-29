using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    struct Square
    {
        /// <summary>
        /// Принимает позицию фигуры по горизонтали
        /// </summary>
        public int X { get; private set; }
        /// <summary>
        /// Принимает позицию фигуры по вертикали
        /// </summary>
        public int Y { get; private set; }
        /// <summary>
        /// Объявление фигуры ненаходящейся на поле 
        /// </summary>
        public static Square none = new Square(-1, -1);
        /// <summary>
        /// Параментрический конструктор
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Square(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        /// <summary>
        /// Параметрический конструктор с проверкой на присутствие фигуры на поле.
        /// </summary>
        /// <param name="name"></param>
        public Square(string name)
        {
            if (name.Length == 2 &&
                name[0] >= 'a' && name[0] <= 'h' &&
                name[1] >= '1' && name[1] <= '8')
            {
                Y = name[0] - 'a';
                X = name[1] - '1';
            }
            else this = none;
        }
        /// <summary>
        /// Принимает название позиции фигуры на игровом поле.
        /// Пример: (4, 1) -> e2;
        /// </summary>
        public string Name => (OnBoard()) ? ((char)('a' + Y)).ToString() +
            (X + 1).ToString() : "";

        public bool OnBoard()
        {
            return (this.X >= 0 && this.X < 8) &&
                   (this.Y >= 0 && this.Y < 8);
        }
        /// <summary>
        /// Формирует перечисление всех клеток на доске.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Square> YieldBoardMoves()
        {
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    yield return new Square(x, y);
        }

        public static bool operator ==(Square a, Square b) =>
            a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Square a, Square b) =>
            !(a == b);
    }
}
