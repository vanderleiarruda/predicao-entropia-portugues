using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;

namespace Hangman
{
    /// <summary>
    /// Interaction logic for wTabelaEstatistica.xaml
    /// </summary>
    public partial class wTabelaEstatistica : Window
    {
        public wTabelaEstatistica()
        {
            InitializeComponent();
            MontaTabela();

        }

        private void MontaTabela()
        {
            List<double[]> lstTabela = BL.MontaTabela.Tabela;

            DataTable dt = new DataTable();

            int nrColunas = lstTabela.Count > 0 ? lstTabela[0].Length : 15;            

            DataColumn dcT = new DataColumn();
            dcT.DataType = Type.GetType("System.String");
            dcT.ColumnName = "T";
            dt.Columns.Add(dcT);

            //Cria as colunas
            for (int i = 1; i <= nrColunas; i++)
            {
                DataColumn dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dc.ColumnName = i.ToString();
                dt.Columns.Add(dc);
            }

            //Cria as linhas e celulas
            for (int i = 0; i < lstTabela.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = (i + 1).ToString();

                //Atualiza colunas
                for (int j = 1; j <= nrColunas; j++)
                    dr[j] = lstTabela[i][j - 1];

                dt.Rows.Add(dr);
            }

            //Atualiza grid
            dgEstatistica.ItemsSource = dt.AsDataView();
        }

        private void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            dgEstatistica.ItemsSource = null;
            MontaTabela();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            BL.MontaTabela.Salvar();
            MessageBox.Show("Salvo! =D");
        }

        private void btnCarregar_Click(object sender, RoutedEventArgs e)
        {
            BL.MontaTabela.Carregar();
            MontaTabela();
        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            BL.MontaTabela.Limpar();
            MontaTabela();
        }
    }
}
