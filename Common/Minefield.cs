namespace Common
{
    public class Minefield
    {
        private MinefieldOptions options;
        public State[,] FieldState { get; }
        public int Columns { get { return options.Cols; } }
        public int Rows { get { return options.Rows; } }
        public int Mines { get { return options.Mines; } }

        public Minefield(MinefieldOptions options)
        {
            this.options = options;
            FieldState = new State[options.Cols, options.Rows];
        }
    }
}