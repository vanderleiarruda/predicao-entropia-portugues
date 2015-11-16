using BL;
using Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;

namespace Hangman
{
    /// <summary>
    /// Interaction logic for wCadastroSentecas.xaml
    /// </summary>
    public partial class wCadastroSentencas : Window
    {
        private static string arquivo;
        private static string mensagem;

        public wCadastroSentencas(bool sentenca100)
        {            
            InitializeComponent();
            if (sentenca100)
                rbtn100.IsChecked = true;
            else
                rbtn100.IsChecked = false;

            Atualizar();
        }

        public void ImportaSentencas()
        {
            //List<string> mensagemLinha = new List<string>();
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "";
            openFileDialog.InitialDirectory = @"c:\temp";
            openFileDialog.Filter = "Arquivos texto (*.txt)|*.txt| All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
                arquivo = openFileDialog.FileName;

            if (String.IsNullOrEmpty(arquivo))
            {
                MessageBox.Show("Arquivo Invalido", "Alerta", MessageBoxButton.OK);
            }
            else
            {
                using (StreamReader texto = new StreamReader(arquivo))
                {
                    while ((mensagem = texto.ReadLine()) != null)
                    {
                        //mensagemLinha.Add(mensagem);
                        CadastroSentenca.Adiciona(new Sentenca(0, mensagem, (bool)rbtn100.IsChecked));
                    }
                }
            }

            Atualizar();
        }

        private void Atualizar()
        {            
            dgSentencas.ItemsSource = null;
            dgSentencas.DisplayMemberPath = "Texto";
            dgSentencas.ItemsSource = CadastroSentenca.Recupera();
            txtSentenca.Text = String.Empty;         
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            CadastroSentenca.Adiciona(new Sentenca(0, txtSentenca.Text, (bool)rbtn100.IsChecked));
            Atualizar();            
        }

        private void txtSentenca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnSalvar_Click(null, null);
        }

        private void btnCarregar_Click(object sender, RoutedEventArgs e)
        {
            CadastroSentenca.Carregar((bool)rbtn100.IsChecked);
            Atualizar();
        }

        private void btnSalvarBD_Click(object sender, RoutedEventArgs e)
        {
            CadastroSentenca.Gravar((bool) rbtn100.IsChecked);
            MessageBox.Show("Salvo! =D");
        }

        private void btnImportar_Click(object sender, RoutedEventArgs e)
        {
            ImportaSentencas();
        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            CadastroSentenca.Limpar();
            dgSentencas.ItemsSource = null; 
        }

        private void rbtn25_Checked(object sender, RoutedEventArgs e)
        {
            CadastroSentenca.TamanhoSentenca = 25;
            CadastroSentenca.Carregar(false);
            Atualizar();
        }

        private void rbtn100_Checked(object sender, RoutedEventArgs e)
        {
            CadastroSentenca.TamanhoSentenca = 100;
            CadastroSentenca.Carregar(true);
            Atualizar();
        }
    }
}
