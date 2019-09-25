using System;

namespace Common
{
    public static class GameRules
    {
        private const int MinefieldMaxColumns = 100;
        private const int MinefieldMinColumns = 1;

        private const int MinefieldMaxRows = 100;
        private const int MinefieldMinRows = 1;

        private const int MinefieldMinMines = 0;

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
            var maxMinesAllowed = cols * rows * 0.75;

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
    }
}