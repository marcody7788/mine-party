namespace Common
{
    public class MinefieldOptions
    {
        public int Cols { get; private set; }
        public int Rows { get; private set; }
        public int Mines { get; private set; }

        public MinefieldOptions(int cols, int rows, int mines)
        {
            this.Cols = cols;
            this.Rows = rows;
            this.Mines = mines;
        }
    }
}