using System;

namespace Common
{
    public enum State
    {
        Zero,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Mine,
    }

    public static class StateExtensions
    {
        public static State ToState(this int number)
        {
            switch (number)
            {
                case 0: return State.Zero;
                case 1: return State.One;
                case 2: return State.Two;
                case 3: return State.Three;
                case 4: return State.Four;
                case 5: return State.Five;
                case 6: return State.Six;
                case 7: return State.Seven;
                case 8: return State.Eight;

                default: throw new ArgumentException();
            }
        }
    }
}
