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
        public int x { get; private set; }
        /// <summary>
        /// Принимает позицию фигуры по вертикали
        /// </summary>
        public int y { get; private set; }
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
            this.x = x;
            this.y = y;
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
                x = name[0] - 'a';
                y = name[1] - '1';
            }
            else this = none;
        }
        /// <summary>
        /// Принимает название позиции фигуры на игровом поле.
        /// Пример: (4, 1) -> e2;
        /// </summary>
        public string Name => ((char)('a' - this.x)).ToString() +
            (this.y + 1).ToString();

        public bool OnBoard()
        {
            return (this.x >= 0 && this.x < 8) &&
                   (this.y >= 0 && this.y < 8);
        }
    }
}
