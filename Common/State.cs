namespace Common
{
    public enum State
    {
        None,
        Mine,
        Flag,
        Number
    }

    public static class StateExtensions
    {
        public static string Symbol(this State state)
        {
            switch (state)
            {
                case State.None: return " ";
                case State.Mine: return "ò";
                case State.Flag: return "p";
                case State.Number: return "3";
                default:
                    return "ERR";
            }
        }
    }
}
