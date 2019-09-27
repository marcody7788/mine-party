using System;

namespace Common
{
    public static class GameRules
    {
        private const int MinefieldMaxColumns = 99;
        private const int MinefieldMinColumns = 1;

        private const int MinefieldMaxRows = 99;
        private const int MinefieldMinRows = 1;

        private const int MinefieldMinMines = 0;

        private const decimal minesCellsRatio = 2/3m;

        public static bool ValidateColumns(int cols, out string error)
        {
            if (cols < MinefieldMinColumns)
            {
                error = $"Columns must be greater or equal than {MinefieldMinColumns}";
                return false;
            }
            if (cols > MinefieldMaxColumns)
            {
                error = $"Columns must be less or equal than {MinefieldMaxColumns}";
                return false;
            }
            error = null;
            return true;
        }

        public static bool ValidateRows(int rows, out string error)
        {
            if (rows < MinefieldMinRows)
            {
                error = $"Rows must be greater or equal than {MinefieldMinRows}";
                return false;
            }

            if (rows > MinefieldMaxRows)
            {
                error = $"Rows must be less or equal than {MinefieldMaxRows}";
                return false;
            }
            error = null;
            return true;
        }

        public static bool ValidateMines(int mines, int cols, int rows, out string error)
        {
            var maxMinesAllowed = cols * rows * minesCellsRatio;

            if (mines < MinefieldMinMines)
            {
                error = $"Mines must be greater or equal than {MinefieldMinMines}";
                return false;
            }
            if (mines > maxMinesAllowed)
            {
                error = $"Mines must be less or equal than {Math.Floor(maxMinesAllowed)}";
                return false;
            }
            error = null;
            return true;
        }

        public static bool CanPutFlag(LayoutState[,] layout, Minefield minefield, int col, int row)
        {
            if (layout[col, row] == LayoutState.Discovered) return false;
            if (layout[col, row] == LayoutState.Flag) return true;

            var flagsInLayout = layout.Count(LayoutState.Flag);

            return flagsInLayout < minefield.Mines;
        }
    }
}