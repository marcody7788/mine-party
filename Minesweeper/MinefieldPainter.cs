using Common;
using System;

namespace Minesweeper
{
    public static class MinefieldPainter
    {
        public static void PaintMinefield(Minefield minefield)
        {
            Console.WriteLine();
            for (int rows = 1; rows <= minefield.Rows; rows++)
            {
                Console.WriteLine();
                Console.Write(rows);
                Console.Write(new string(' ', 3 - rows.ToString().Length));
                for (int cols = 1; cols <= minefield.Columns; cols++)
                {
                    Console.Write("|");
                    Console.Write(minefield.FieldState[cols - 1, rows - 1].Symbol());
                }
                Console.Write("|");
            }
        }
    }
}