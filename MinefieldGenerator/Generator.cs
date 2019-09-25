using Common;
using System;

namespace MinefieldGenerator
{
    public static class Generator
    {
        public static Minefield NewMinefield(MinefieldOptions options)
        {
            return new Minefield(options);
        }
    }
}