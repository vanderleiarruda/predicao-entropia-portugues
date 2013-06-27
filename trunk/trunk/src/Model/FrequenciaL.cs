using System;
using System.Linq;

namespace Model
{
    public class FrequenciaL
    {
        private char letra;
        private int aparicoes;

        public Char Letra { get { return letra; } set { letra = value; } }        
        public int Aparicoes { get { return aparicoes; } set { aparicoes = value; } }

        public FrequenciaL(String[] text)
        {
            this.letra = text[0].ToCharArray()[0];
            this.aparicoes = Convert.ToInt32(text[1]);
        }
    }
}
