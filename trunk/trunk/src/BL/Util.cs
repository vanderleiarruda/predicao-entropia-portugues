using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Windows.Forms;

namespace BL
{
    public static class Util
    {
        public static string SelecionaArquivo(String filtro)
        {
            OpenFileDialog openfileDialog = new OpenFileDialog();
            openfileDialog.Filter = filtro;

            DialogResult result = openfileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                return openfileDialog.FileName;
            }
            else
            {
                return "";
            }
        }

        public static void CriarDiretorio()
        {
            if (!System.IO.Directory.Exists(Path.GetDirectoryName(Config.BancoDadosURL)))
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(Config.BancoDadosURL));

            if (!System.IO.File.Exists(Config.BancoDadosURL))
                System.IO.File.Create(Config.BancoDadosURL);
        }

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

        public static ArrayList LeArquivoTexto(String url)
        {
            string sLine = "";
            ArrayList arrText = new ArrayList();

            StreamReader objReader = new StreamReader(url);

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                    arrText.Add(sLine);
            }
            objReader.Close();

            return arrText;
        }
        
        public static List<Frequencia> LerEstimativaFrequencia(string url)
        {
            List<Frequencia> lstFrequencia = new List<Frequencia>();            
            ArrayList arrText = Util.LeArquivoTexto(url);

            foreach (string sOutput in arrText){

                String[] freq = sOutput.Split(';');
                Frequencia f = new Frequencia(freq);
                lstFrequencia.Add(f);
            }

            return lstFrequencia; 
        }

        public static List<FrequenciaL> LerFrequenciaLetras(string url)
        {
            List<FrequenciaL> lstFrequencia = new List<FrequenciaL>();
            ArrayList arrText = Util.LeArquivoTexto(url);

            foreach (string sOutput in arrText)
            {

                String[] freq = sOutput.Split(';');
                FrequenciaL f = new FrequenciaL(freq);
                lstFrequencia.Add(f);
            }

            return lstFrequencia;
        }
    }
}
