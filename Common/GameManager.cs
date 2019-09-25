using System;
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
                var location = GetMineFreeRandomLocation(minefield);
                PlantMine(minefield, location.Item1, location.Item2);
            }
        }

        private Tuple<int, int> GetMineFreeRandomLocation(Minefield minefield)
        {
            int col = randomGenerator.Next(0, minefield.Columns);
            int row = randomGenerator.Next(0, minefield.Rows);
            if (HasMine(minefield, col, row))
            {
                return GetMineFreeRandomLocation(minefield);
            }
            return new Tuple<int, int>(col, row);
        }

        private bool HasMine(Minefield minefield, int col, int row)
        {
            return minefield.FieldState[col, row] == State.Mine;
        }

        private void PlantMine(Minefield minefield, int col, int row)
        {
            minefield.FieldState[col, row] = State.Mine;
        }
    }
}
