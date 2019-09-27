using Common;
using System;

namespace Minesweeper
{
    public class Program
    {
        private static GameManager manager = GameManager.GetInstance();
        private static MinefieldOptions minefieldOptions;
        private static Minefield minefield;
        private static MinefieldPainter painter;

        public static int FieldColumns = 20;
        public static int FieldRows = 20;
        public static int FieldMines = 65;

        static void Main(string[] args)
        {
            GetInputs();
            NewGame();

            var exit = false;
            do
            {
                var action = AskActions();
                PerformAction(action);
            }
            while (!exit);
        }

        private static void PerformAction(char action)
        {

            switch (action)
            {
                case '1': Clear(); AskDiscover(putFlag: false); break;
                case '2': Clear(); AskDiscover(putFlag: true); break;
                case '3': NewGame(); break;
                case '4': Environment.Exit(0); break;
                default:
                    break;
            }

            void Clear()
            {
                Console.Clear();
                painter.PaintMinefield();
            }
        }

        private static void AskDiscover(bool putFlag)
        {
            Console.WriteLine();
            Console.WriteLine("What cell?");
            Console.WriteLine("Enter column number");
            int col;
            while (!int.TryParse(Console.ReadLine(), out col) || col < 1 || col > FieldColumns)
            {
                Console.WriteLine($"Invalid input");
            }

            Console.WriteLine("Enter row number");
            int row;
            while (!int.TryParse(Console.ReadLine(), out row) || row < 1 || row > FieldRows)
            {
                Console.WriteLine($"Invalid input");
            }

            DoDiscover(col - 1, row - 1, putFlag);
        }

        private static char AskActions()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Choose action:" + Environment.NewLine +
                "1- Discover" + Environment.NewLine +
                "2- Flag" + Environment.NewLine +
                "3- New game" + Environment.NewLine +
                "4- Exit");
            return Console.ReadKey().KeyChar;
        }

        private static void GetInputs()
        {
            Console.WriteLine("Enter number of columns...");
            string validationMessage = null;
            while (!int.TryParse(Console.ReadLine(), out FieldColumns) || !GameRules.ValidateColumns(FieldColumns, out validationMessage))
            {
                Console.WriteLine($"Invalid input. {validationMessage}");
                validationMessage = null;
            }

            Console.WriteLine("Enter number of rows...");
            while (!int.TryParse(Console.ReadLine(), out FieldRows) || !GameRules.ValidateRows(FieldRows, out validationMessage))
            {
                Console.WriteLine($"Invalid input. {validationMessage}");
                validationMessage = null;
            }

            Console.WriteLine("Enter number of mines...");
            while (!int.TryParse(Console.ReadLine(), out FieldMines) || !GameRules.ValidateMines(FieldMines, FieldColumns, FieldRows, out validationMessage))
            {
                Console.WriteLine($"Invalid input. {validationMessage}");
                validationMessage = null;
            }
        }

        private static void NewGame()
        {
            minefieldOptions = new MinefieldOptions(FieldColumns, FieldRows, FieldMines);
            minefield = new Minefield(minefieldOptions);

            manager.PopulateMinefield(minefield);
            painter = new MinefieldPainter(minefield);
        }

        private static void DoDiscover(int col, int row, bool putFlag)
        {
            if (putFlag)
            {
                if (GameRules.CanPutFlag(painter.Layout, minefield, col, row))
                {
                    painter.PutFlag(col, row);
                }
            }
            else
            {
                var mines = GameManager.SurroundingCells(minefield, col, row);
                painter.Discover(col, row, mines);
            }

            var situation = manager.GetGameSituation(painter.Layout, minefield, col, row);
            if (situation != GameSituation.Ongoing)
            {
                FinishGame(situation);
                return;
            }
        }

        private static void FinishGame(GameSituation situation)
        {
            painter.DiscoverAll();
            Console.WriteLine();
            string text = null;

            if (situation == GameSituation.Win)
                text = "WINNER :))))))))) WELL DONE DUDE !!!";
            if (situation == GameSituation.Lose)
                text = "LOSER :(((( YOU GOT A MINE DINNER!!!";

            Console.WriteLine(text);
            var ask = true;
            do
            {
                Console.WriteLine();
                Console.WriteLine("New game? (y/n)");
                var key = Console.ReadKey().KeyChar;
                ask = (key != 'y' && key != 'n');
                if (key == 'y') NewGame();
                if (key == 'n') Environment.Exit(0);
            }
            while (ask);
        }

    }
}