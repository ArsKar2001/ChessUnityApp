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
        static void Main(string[] args)
        {
            Chess chess = new Chess();
            while (true)
            {
                Console.WriteLine(chess.fen);
                Console.WriteLine(ChessWriteAscii(chess));
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
                    stringBuilder.Append(chess.GetFigure(x, y) + " ");
                }
                stringBuilder.Append("| ");
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendLine("  +-----------------+ ");
            stringBuilder.AppendLine("    a b c d i f g h ");
            return stringBuilder.ToString();
        }
    }
}
