using Common;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Minesweeper
{
    public class MinefieldPainter
    {
        private readonly Minefield _minefield;
        public LayoutState[,] Layout { get; private set; }

        public MinefieldPainter(Minefield minefield)
        {
            _minefield = minefield;
            Layout = new LayoutState[minefield.Columns, minefield.Rows];
            PaintMinefield();
        }

        public void PaintMinefield()
        {
            Console.Clear();
            Console.WriteLine();


            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Write(new string(' ', 3));
            for (int c = 1; c <= _minefield.Columns; c++)
            {
                Console.Write(new string(' ', 3 - c.ToString().Length) + c);
            }
            Console.WriteLine();


            for (int rows = 1; rows <= _minefield.Rows; rows++)
            {
                Console.WriteLine();
                Console.Write(rows);
                Console.Write(new string(' ', 3 - rows.ToString().Length));
                for (int cols = 1; cols <= _minefield.Columns; cols++)
                {
                    Console.Write(" ");
                    var symbol =
                        Layout[cols - 1, rows - 1] == LayoutState.Discovered ?
                        GetSymbol(_minefield.FieldState[cols - 1, rows - 1]) :
                        GetSymbol(Layout[cols - 1, rows - 1]);

                    Console.ForegroundColor = GetForeColorForSymbol(symbol);
                    Console.BackgroundColor = GetBackColorForSymbol(symbol);

                    Console.Write(symbol);

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;

                }
                Console.Write(" ");
            }
        }

        private ConsoleColor GetBackColorForSymbol(string symbol)
        {
            switch (symbol)
            {
                case Symbols.Tile: return ConsoleColor.Gray;
                case Symbols.Flag: return ConsoleColor.Gray;
                case Symbols.Mine: return ConsoleColor.Red;
            }
            return ConsoleColor.Black;
        }

        private ConsoleColor GetForeColorForSymbol(string symbol)
        {
            switch (symbol)
            {
                case Symbols.Mine: return ConsoleColor.Black;
                case Symbols.Tile: return ConsoleColor.Black;
                case Symbols.Flag: return ConsoleColor.DarkRed;
                case Symbols.Zero: return ConsoleColor.White;
                case Symbols.One: return ConsoleColor.Blue;
                case Symbols.Two: return ConsoleColor.Green;
                case Symbols.Three: return ConsoleColor.Red;
                case Symbols.Four: return ConsoleColor.DarkBlue;
                case Symbols.Five: return ConsoleColor.DarkRed;
                case Symbols.Six: return ConsoleColor.Yellow;
                case Symbols.Seven: return ConsoleColor.DarkMagenta;
                case Symbols.Eight: return ConsoleColor.DarkYellow;
            }
            return ConsoleColor.White;
        }

        public void PutFlag(int col, int row)
        {
            if (Layout[col, row] == LayoutState.Flag)
            {
                Layout[col, row] = LayoutState.Tile;
                PaintMinefield();
                return;
            }

            if (Layout[col, row] == LayoutState.Tile)
            {
                Layout[col, row] = LayoutState.Flag;
                PaintMinefield();
                return;
            }
        }

        public void Discover(int col, int row, IList<Tuple<int, int>> surroundingCells)
        {
            RecursiveDiscover(col, row, surroundingCells);
            PaintMinefield();
        }

        private void RecursiveDiscover(int col, int row, IList<Tuple<int, int>> surroundingCells)
        {
            Layout[col, row] = LayoutState.Discovered;

            if (_minefield.FieldState[col, row] != State.Zero) return;

            foreach (var t in surroundingCells)
            {
                var c = t.Item1;
                var r = t.Item2;

                if (_minefield.FieldState[c, r] != State.Mine && Layout[c, r] != LayoutState.Flag)
                {
                    if (_minefield.FieldState[c, r] == State.Zero && Layout[c, r] == LayoutState.Tile)
                    {
                        RecursiveDiscover(c, r, GameManager.SurroundingCells(_minefield, c, r));
                    }
                    Layout[c, r] = LayoutState.Discovered;
                }
            }
        }

        private string GetSymbol(State state)
        {
            switch (state)
            {
                case State.Zero: return Symbols.Zero;
                case State.One: return Symbols.One;
                case State.Two: return Symbols.Two;
                case State.Three: return Symbols.Three;
                case State.Four: return Symbols.Four;
                case State.Five: return Symbols.Five;
                case State.Six: return Symbols.Six;
                case State.Seven: return Symbols.Seven;
                case State.Eight: return Symbols.Eight;
                case State.Mine: return Symbols.Mine;
                default: return "ERR";
            }
        }
        private string GetSymbol(LayoutState state)
        {
            switch (state)
            {
                case LayoutState.Tile: return Symbols.Tile;
                case LayoutState.Flag: return Symbols.Flag;
            }
            return "ERR";
        }

        internal void DiscoverAll()
        {
            for (int cols = 0; cols < _minefield.Columns; cols++)
            {
                for (int rows = 0; rows < _minefield.Rows; rows++)
                {
                    Layout[cols, rows] = LayoutState.Discovered;
                }
            }

            PaintMinefield();
        }
    }
}