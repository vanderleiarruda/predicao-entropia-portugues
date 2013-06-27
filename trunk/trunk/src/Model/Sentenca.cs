using System;
using System.Linq;

namespace Model
{
    public class Sentenca
    {
        private String texto;
        private int codigo;
        private bool cem;

        public int Codigo { get { return codigo; } set { codigo = value; } }

        public Sentenca(int cod, String texto,bool cem = false)
        {
            this.codigo = cod;
            this.texto = texto;
            this.cem = cem;
        }
       
        public String Texto
        {
            get { return texto; }
            set { texto = value; }
        }

        public bool CEM { get { return cem; } set { cem = value; } }
    }
}
