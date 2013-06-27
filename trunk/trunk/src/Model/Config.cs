using System;
using System.Linq;

namespace Model
{
    public static class Config
    {
        private static readonly String bancoDados = @"c:\temp\Hangman.uni";

        public static String BancoDadosURL { get { return bancoDados; } }
    }
}
