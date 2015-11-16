using System;
using System.Linq;

namespace Model
{
    public class Frequencia
    {
        private char primeiro, segundo;
        private int aparicoes;

        public Char Primeiro { get { return primeiro; } set { primeiro = value; } }
        public Char Segundo { get { return segundo; } set { segundo = value; } }
        public int Aparicoes { get { return aparicoes; } set { aparicoes = value; } }
        
        public Frequencia(String[] text)
        {
            this.primeiro = text[0].ToCharArray()[0];
            this.segundo = text[1].ToCharArray()[0];
            this.aparicoes = Convert.ToInt32(text[2]);            
        }
    }
}
