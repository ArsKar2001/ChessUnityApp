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
            Chess chess = new Chess("r3k2r/pppppppp/8/8/8/8/PPPPPPPP/R3K2R w KQkq - 0 1");
            while (true)
            {
                Console.Clear();
                Console.WriteLine(chess.Fen);
                OutBoardPrintColor(ChessWriteAscii(chess));
                foreach (var item in chess.YieldValidMoves())
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
            if (chess.IsCheckShahk)
                stringBuilder.AppendLine("Вам шах!");
            if (chess.IsCheckMat)
                stringBuilder.AppendLine("Мат. Game Over!");
            if (chess.IsCheckPat)
                stringBuilder.AppendLine("Патовая ситуация!");
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
