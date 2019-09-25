using Common;
using System;

namespace Minesweeper
{
    public class Program
    {
        private static int cols;
        private static int rows;
        private static int mines;
        private static GameManager manager = GameManager.GetInstance();

        static void Main(string[] args)
        {
            GetInputs();
            GenerateMineField();
            Console.WriteLine(Environment.NewLine + "Generate again? (Y = yes, other = exit)");
            while ("y" == char.ConvertFromUtf32(Console.ReadKey().KeyChar).ToLower())
            {
                GenerateMineField();
                Console.WriteLine();
                Console.WriteLine(Environment.NewLine + "Generate again? (Y = yes, other = exit)");
            }
        }

        private static void GetInputs()
        {

            Console.WriteLine("Enter number of columns...");
            string validationMessage = null;
            while (!int.TryParse(Console.ReadLine(), out cols) || !GameRules.ValidateColumns(cols, out validationMessage))
            {
                Console.WriteLine($"Invalid input. {validationMessage}");
                validationMessage = null;
            }

            Console.WriteLine("Enter number of rows...");
            while (!int.TryParse(Console.ReadLine(), out rows) || !GameRules.ValidateRows(rows, out validationMessage))
            {
                Console.WriteLine($"Invalid input. {validationMessage}");
                validationMessage = null;
            }

            Console.WriteLine("Enter number of mines...");
            while (!int.TryParse(Console.ReadLine(), out mines) || !GameRules.ValidateMines(mines, cols, rows, out validationMessage))
            {
                Console.WriteLine($"Invalid input. {validationMessage}");
                validationMessage = null;
            }
        }

        private static void GenerateMineField()
        {
            var minefieldOptions = new MinefieldOptions(cols, rows, mines);
            var minefield = new Minefield(minefieldOptions);

            manager.PopulateMinefield(minefield);
            MinefieldPainter.PaintMinefield(minefield);
        }
    }
}
