
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BL
{
    public static class CadastroSentenca
    {
        private static List<Sentenca> lstSentencas = new List<Sentenca>();
        private static int tamanhoSentenca = 0;
       
        public static void Limpar()
        {
            lstSentencas.Clear();
        }

        public static int TamanhoSentenca { get { return tamanhoSentenca; } set { tamanhoSentenca = value; } }

        public static string RemoverAcentos(string texto)
        {
            string s = texto.Normalize(NormalizationForm.FormD);

            StringBuilder sb = new StringBuilder();

            for (int k = 0; k < s.Length; k++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(s[k]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(s[k]);
                }
            }
            return sb.ToString();
        }

        public static void Adiciona(Sentenca sentenca)
        {            
            lstSentencas.Add(sentenca);
        }

        public static List<Sentenca> Recupera()
        {
            return lstSentencas;
        }

        public static Sentenca Recupera(int indice)
        {
            if (indice < 0)
                return null;

            if (lstSentencas.Count > indice)
                return lstSentencas[indice];
            else
                return null;
        }

        public static void Carregar(bool sentencas100)
        {
            DALSentencas dal = new DALSentencas();
            
            lstSentencas = dal.RetornaSentencas(sentencas100);
            dal.CloseBase();

            if (sentencas100)
                tamanhoSentenca = 100;
            else
                tamanhoSentenca = 25;
        }

        public static void Gravar(bool sentenca100)
        {
            DALSentencas dal = new DALSentencas();
            
            //Busca todos
            List<Sentenca> lstTemp = dal.RetornaSentencas(sentenca100);
            
            //Apaga um por um
            foreach (Sentenca s in lstTemp)
                dal.Excluir(s);

            int temp = 1;
            
            //Grava um por um
            foreach (Sentenca s in lstSentencas)
            {
                s.Codigo = temp;                

                s.Texto = s.Texto.Replace(",", "");
                s.Texto = s.Texto.Replace(".", "");
                s.Texto = s.Texto.Replace("!", "");
                s.Texto = s.Texto.Replace("-", "");
                s.Texto = s.Texto.Replace(";", "");
                s.Texto = s.Texto.Replace(":", "");
                s.Texto = s.Texto.Replace("?", "");
                s.Texto = s.Texto.Replace("%", "");

                s.Texto = s.Texto.Replace("@", "");
                s.Texto = s.Texto.Replace("#", "");
                s.Texto = s.Texto.Replace("$", "");
                s.Texto = s.Texto.Replace("*", "");
                s.Texto = s.Texto.Replace("(", "");
                s.Texto = s.Texto.Replace(")", "");
                s.Texto = s.Texto.Replace("+", "");
                s.Texto = s.Texto.Replace("-", "");
                s.Texto = s.Texto.Replace("–", "");
                s.Texto = s.Texto.Replace("/", "");

                s.Texto = s.Texto.Replace('"'.ToString(), "");

                s.Texto = s.Texto.Trim();
                s.Texto = s.Texto.Replace("0", "");
                s.Texto = s.Texto.Replace("1", "");
                s.Texto = s.Texto.Replace("2", "");
                s.Texto = s.Texto.Replace("3", "");
                s.Texto = s.Texto.Replace("4", "");
                s.Texto = s.Texto.Replace("5", "");
                s.Texto = s.Texto.Replace("6", "");
                s.Texto = s.Texto.Replace("7", "");
                s.Texto = s.Texto.Replace("8", "");
                s.Texto = s.Texto.Replace("9", "");
                s.Texto = s.Texto.Replace("ª", "");

                s.Texto = s.Texto.Replace("  ", " ");
                s.Texto = s.Texto.Replace("   ", " ");
                s.Texto = s.Texto.Replace("    ", " ");
                s.Texto = s.Texto.Replace("     ", " ");
                s.Texto = s.Texto.Replace("      ", " ");
                
                //Regex reg = new Regex("[^a-zA-Z ]");
                //s.Texto = reg.Replace(s.Texto, "");

                //while (s.Texto.Length < tamanhoSentenca)
                //    s.Texto += " ";

                if (s.Texto.Length >= tamanhoSentenca)
                {
                    s.Texto = s.Texto.Substring(0, tamanhoSentenca);
                    s.Texto = s.Texto.ToUpper();

                    dal.Gravar(s);
                    temp++;
                }
            }

            dal.CloseBase();
        }        
    }
}
