using BL;
using System;
using System.Linq;
using System.Windows;

namespace Hangman
{
    /// <summary>
    /// Interaction logic for wMenu.xaml
    /// </summary>
    public partial class wMenu : Window
    {
        public wMenu()
        {
            InitializeComponent();            
        }

        private void Inicializa()
        {
            try
            {
                Util.CriarDiretorio();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnSentecas_Click(object sender, RoutedEventArgs e)
        {
            wAquisicaoEstimativaAcerto aq = new wAquisicaoEstimativaAcerto();
            aq.Show();
        }

        private void btnAquisicao_Click(object sender, RoutedEventArgs e)
        {
            wAquisicaoEstimativaAcerto100 aquisicao = new wAquisicaoEstimativaAcerto100();
            aquisicao.Show();
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            WAbout ab = new WAbout();
            ab.Show();
        }

    }
}