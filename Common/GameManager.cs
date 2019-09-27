using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class GameManager
    {
        private static readonly GameManager _gameManager = new GameManager();
        Random randomGenerator = new Random();

        private GameManager()
        {

        }

        public static GameManager GetInstance()
        {
            return _gameManager;
        }

        public void PopulateMinefield(Minefield minefield)
        {
            for (int i = 0; i < minefield.Mines; i++)
            {
                var location = GetRandomMineFreeLocation(minefield);
                PlantMine(minefield, location.Item1, location.Item2);
            }
            for (int cols = 0; cols < minefield.Columns; cols++)
            {
                for (int rows = 0; rows < minefield.Rows; rows++)
                {
                    if (minefield.FieldState[cols, rows] == State.Mine) continue;
                    var numSurroundingMines = SurroundingCells(minefield, cols, rows).Where(t => HasMine(minefield, t.Item1, t.Item2)).Count();
                    minefield.FieldState[cols, rows] = numSurroundingMines.ToState();
                }
            }
        }

        private Tuple<int, int> GetRandomMineFreeLocation(Minefield minefield)
        {
            int col = randomGenerator.Next(0, minefield.Columns);
            int row = randomGenerator.Next(0, minefield.Rows);
            if (HasMine(minefield, col, row))
            {
                return GetRandomMineFreeLocation(minefield);
            }
            return new Tuple<int, int>(col, row);
        }

        public bool HasMine(Minefield minefield, int col, int row)
        {
            return minefield.FieldState[col, row] == State.Mine;
        }

        private void PlantMine(Minefield minefield, int col, int row)
        {
            minefield.FieldState[col, row] = State.Mine;
        }

        public static IList<Tuple<int, int>> SurroundingCells(Minefield minefield, int col, int row)
        {
            var surroundingCells = new List<Tuple<int, int>>();

            AddToListIfInBounds(col - 1, row - 1);
            AddToListIfInBounds(col, row - 1);
            AddToListIfInBounds(col + 1, row - 1);

            AddToListIfInBounds(col - 1, row);
            AddToListIfInBounds(col + 1, row);

            AddToListIfInBounds(col - 1, row + 1);
            AddToListIfInBounds(col, row + 1);
            AddToListIfInBounds(col + 1, row + 1);

            return surroundingCells;

            void AddToListIfInBounds(int c, int r)
            {
                if (IsInBounds(minefield, c, r)) surroundingCells.Add(new Tuple<int, int>(c, r));
            }

        }

        private static bool IsInBounds(Minefield minefield, int col, int row)
        {
            return (col >= 0 && col < minefield.Columns)
                && ((row >= 0 && row < minefield.Rows));
        }

        public GameSituation GetGameSituation(LayoutState[,] layout, Minefield minefield, int discoveredColumn, int discoveredRow)
        {
            if (HasMine(minefield, discoveredColumn, discoveredRow) && layout[discoveredColumn, discoveredRow] == LayoutState.Discovered) return GameSituation.Lose;

            var undiscovered = 0;
            for (int col = 0; col < minefield.Columns; col++)
            {
                for (int row = 0; row < minefield.Rows; row++)
                {
                    if (layout[col, row] != LayoutState.Discovered /*&& minefield.FieldState[col, row] == State.Mine*/)
                    {
                        undiscovered++;
                    }
                }
            }
            if (undiscovered == minefield.Mines) return GameSituation.Win;

            return GameSituation.Ongoing;
        }
    }
}