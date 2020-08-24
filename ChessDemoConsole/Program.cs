using ChessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessDemoConsole
{
    class Program
    {
        static void Main()
        {
            Chess chess = new Chess("8/3PPPP1/8/8/8/8/3pppp1/8 w - - 0 1");
            while (true)
            {
                Console.Clear();
                Console.WriteLine(chess.Fen);
                OutBoardPrintColor(ChessWriteAscii(chess));
                foreach (var item in chess.YieldvalidMoves())
                {
                    Console.WriteLine(item);
                }
                string move = Console.ReadLine();
                if (move == "") break;
                chess = chess.Move(move);
            }
        }
        static string ChessWriteAscii(Chess chess)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("  +-----------------+ ");
            for (int y = 7; y >= 0; y--)
            {
                stringBuilder.Append(y + 1 + " | ");
                for (int x = 0; x < 8; x++)
                {
                    stringBuilder.Append(chess.GetFigure(y, x) + " ");
                }
                stringBuilder.Append("| ");
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendLine("  +-----------------+ ");
            stringBuilder.AppendLine("    a b c d e f g h ");
            return stringBuilder.ToString();
        }
        public static void OutBoardPrintColor(string chess)
        {
            ConsoleColor color = Console.ForegroundColor;
            foreach (var item in chess)
            {
                if (item >= 'a' && item <= 'z')
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (item >= 'A' && item <= 'Z')
                    Console.ForegroundColor = ConsoleColor.Blue;
                else
                    Console.ForegroundColor = ConsoleColor.White;
                Console.Write(item);
            }
            Console.ForegroundColor = color;
        }
    }
}
