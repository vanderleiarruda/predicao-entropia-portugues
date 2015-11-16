using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Tabela
    {
        private List<double[]> lstTabela;

        public List<double[]> Conteudo { get { return lstTabela; } set { lstTabela = value; } }

        public Tabela(List<double[]> lstTabela)
        {
            this.lstTabela = lstTabela;
        }
    }
}
