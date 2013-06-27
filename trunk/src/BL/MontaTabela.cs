using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public static class MontaTabela
    {
        private static List<double[]> lstTabela = new List<double[]>();
        
        public static void AtualizaTabela(int nrTentativa, int nrLetrasConhecidas)
        {           
            for (int i = 0; i < nrTentativa; i++)
            {
                if(lstTabela.Count <= i)
                {
                    lstTabela.Add(new double[CadastroSentenca.TamanhoSentenca]);                    
                }
            }

            if (lstTabela.Count > (nrTentativa-1))
                lstTabela[nrTentativa-1][nrLetrasConhecidas]++;
        }

        public static List<double[]> Tabela
        {
            get
            {
                return lstTabela;
            }
        }

        public static void Carregar()
        {
            DALTabela dal = new DALTabela();
            List<Tabela> lstTab = dal.RetornaTodos();

            if (lstTab.Count > 0)
                lstTabela = lstTab[0].Conteudo;

            dal.CloseBase();
        }

        public static void Salvar()
        {
            DALTabela dal = new DALTabela();
            List<Tabela> lstTab = dal.RetornaTodos();

            //Apaga se existir
            foreach (Tabela t in lstTab)
                dal.Excluir(t);

            //Cria tabela
            Tabela tab = new Tabela(lstTabela);

            //Grava tabela
            dal.Gravar(tab);

            //Fecha conexão
            dal.CloseBase();
        }

        public static void Limpar()
        {
            lstTabela = new List<double[]>();
        }
    }
}
