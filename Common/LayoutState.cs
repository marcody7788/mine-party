namespace Common
{
    public enum LayoutState
    {
        Tile,
        Discovered,
        Flag
    }

    public static class LayoutExtensions
    {

        public static int Count(this LayoutState[,] layout, LayoutState state)
        {
            int flagsInLayout = 0;
            int columns = layout.GetUpperBound(0) + 1;
            int rows = layout.GetUpperBound(1) + 1;

            for (int col = 0; col < columns; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    if (layout[col, row] == state)
                    {
                        flagsInLayout++;
                    }
                }
            }
            return flagsInLayout;
        }
    }
}
