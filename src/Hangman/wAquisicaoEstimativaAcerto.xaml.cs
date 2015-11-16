using BL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Hangman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class wAquisicaoEstimativaAcerto : Window
    {
        private int indiceAtual = 0, indiceTela = 1, erros = 0, acertos = 0, indiceSentenca = 0, nrTentativa = 1;
        private Sentenca sent = null;
        private char letraAnterior = ' ';

        public wAquisicaoEstimativaAcerto()
        {
            CadastroSentenca.Carregar(false);
            BL.MontaTabela.Limpar();
            InitializeComponent();            
            lblSentenca.Content = "";            
            Util.CriarDiretorio();
            AtivaTeclado(false);            
            InicializaSenteca(indiceSentenca);
        }

        /// <summary>
        /// Inicializa sentença a ser adivinhada
        /// </summary>
        private void InicializaSenteca(int indice)
        {
            AtualizaPainelSenteca();
            indiceTela = 1;
            indiceAtual = 0;
            sent = CadastroSentenca.Recupera(indice);            

            if (sent == null)
            {
                MessageBox.Show("Não existem senteças cadastradas!");
                lblSentenca.Content = "";
                return;
            }

            AtivaTeclado(true);
            lblSentenca.Content = ""; //limpa rotulo

            foreach (char c in sent.Texto)
            {
                lblSentenca.Content += " _ ";
            }

            lblSentenca.Content += " _ ";

            letraAnterior = ' ';
        }

        private void btnTabela_Click(object sender, RoutedEventArgs e)
        {
            wTabelaEstatistica tabela = new wTabelaEstatistica();
            tabela.Show();            
        }
        
        private bool Tentativa(Char c)
        {
            char[] temp;

            if (indiceAtual >= sent.Texto.Length)
                return false;

            if (Util.RemoverAcentos(sent.Texto[indiceAtual].ToString().ToUpper()) ==  c.ToString().ToUpper())
            {
                temp = lblSentenca.Content.ToString().ToCharArray();
                temp[indiceTela] = sent.Texto[indiceAtual];
                lblSentenca.Content = new String(temp);

                //+ 1 é da regra de shannon
                MontaTabela.AtualizaTabela(nrTentativa, indiceAtual);
                indiceAtual += 1;
                indiceTela += 3;
                acertos += 1;
                nrTentativa = 1;
                lblLetrasConhecidas.Content = indiceAtual;
                lblTentativas.Content = nrTentativa;

                if (sent.Texto.Length == indiceAtual)
                    AtivaTeclado(false);
                else
                    AtivaTeclado(true);
                
                letraAnterior = c;
                
                return true;
            }
            else
            {
                erros += 1;
                lblTentativas.Content = nrTentativa;
                nrTentativa++;
                return false;
            }
        }

        private void AtualizaPainelSenteca()
        {
            lblSentença.Content = String.Format("Sentença {0} de um total de {1}. ", indiceSentenca + 1, CadastroSentenca.Recupera().Count);
        }

        private void DesabilitaBotao(Button b)
        {
            b.IsEnabled = false;
        }

        private bool Adivinhar(string url)
        {
            List<Frequencia> lstFrequencia = Util.LerEstimativaFrequencia(url);

            Boolean res = true;

            for (int j = indiceAtual; j < sent.Texto.Length; j++)
            {
                if (!res)
                {
                    MessageBox.Show(String.Format("Não foi possivel encontrar uma combinação para as letras: {0} e {1}", letraAnterior, sent.Texto[j - 1].ToString().ToLower()));

                    return false;
                }
                else
                    res = false;

                for (int i = 0; i < lstFrequencia.Count; i++)
                {
                    if (Util.RemoverAcentos(letraAnterior.ToString().ToUpper()) == Util.RemoverAcentos(lstFrequencia[i].Primeiro.ToString().ToUpper()))
                        res = Tentativa(lstFrequencia[i].Segundo);

                    if (res)
                    {
                        letraAnterior = lstFrequencia[i].Segundo;
                        i = lstFrequencia.Count;
                    }
                }
            }

            return true;
        }

        private void btnAdivinhar_Click(object sender, RoutedEventArgs e)
        {
            String arq = Util.SelecionaArquivo("Arquivos de planilha (*.csv)|*.csv");
            Adivinhar(arq);
        }

        #region Teclado

        private void btnA_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('A'))
                DesabilitaBotao(sender as Button);
        }

        private void btnB_Click(object sender, RoutedEventArgs e)
        {
            if (!Tentativa('B'))
                DesabilitaBotao(sender as Button);
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            if (!Tentativa('C'))
                DesabilitaBotao(sender as Button);
        }

        private void btnD_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('D'))
                DesabilitaBotao(sender as Button);
        }

        private void btnE_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('E'))
                DesabilitaBotao(sender as Button);
        }

        private void btnF_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('F'))
                DesabilitaBotao(sender as Button);
        }

        private void btnG_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('G'))
                DesabilitaBotao(sender as Button);
        }

        private void btnH_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('H'))
                DesabilitaBotao(sender as Button);
        }

        private void btnI_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('I'))
            DesabilitaBotao(sender as Button);
        }

        private void btnJ_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('J'))
            DesabilitaBotao(sender as Button);
        }

        private void btnK_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('K'))
            DesabilitaBotao(sender as Button);
        }

        private void btnL_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('L'))
            DesabilitaBotao(sender as Button);
        }

        private void btnW_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('W'))
            DesabilitaBotao(sender as Button);
        }

        private void btnM_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('M'))
            DesabilitaBotao(sender as Button);
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('X'))
            DesabilitaBotao(sender as Button);
        }

        private void btnN_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('N'))
            DesabilitaBotao(sender as Button);
        }

        private void btnY_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('Y'))
            DesabilitaBotao(sender as Button);
        }

        private void btnO_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('O'))
            DesabilitaBotao(sender as Button);
        }

        private void btnZ_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('Z'))
            DesabilitaBotao(sender as Button);
        }

        private void btnP_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('P'))
            DesabilitaBotao(sender as Button);
        }

        private void btnQ_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('Q'))
            DesabilitaBotao(sender as Button);
        }

        private void btnR_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('R'))
            DesabilitaBotao(sender as Button);
        }

        private void btnS_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('S'))
            DesabilitaBotao(sender as Button);
        }

        private void btnT_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('T'))
            DesabilitaBotao(sender as Button);
        }

        private void btnU_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('U'))
            DesabilitaBotao(sender as Button);
        }

        private void btnV_Click(object sender, RoutedEventArgs e)
        {
            if(!Tentativa('V'))
            DesabilitaBotao(sender as Button);
        }

        private void btnEspaco_Click(object sender, RoutedEventArgs e)
        {
            if (!Tentativa(' '))
                DesabilitaBotao(sender as Button);
        }
        #endregion Teclado        

        private void btnProximo_Click(object sender, RoutedEventArgs e)
        {
            indiceSentenca++;
            InicializaSenteca(indiceSentenca);            
        }

        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            indiceSentenca--;
            InicializaSenteca(indiceSentenca);
        }

        private void AtivaTeclado(bool abilitado)
        {
            foreach (Button b in cvTeclado.Children)
            {
                if (b is Button)
                {
                    b.IsEnabled = abilitado;
                }
            }
        }

        private void btnSentencas_Click(object sender, RoutedEventArgs e)
        {
            wCadastroSentencas sent = new wCadastroSentencas(false);
            sent.ShowDialog();
            InicializaSenteca(indiceSentenca);
        }

        private void btnAdivinharTodos_Click(object sender, RoutedEventArgs e)
        {
            int qtd = CadastroSentenca.Recupera().Count;
            
            //@"C:\temp\reprojeto\Tabela_de_combinacoes_JOR.csv"            
            String arq = Util.SelecionaArquivo("Arquivos de planilha (*.csv)|*.csv");

            while (indiceSentenca < qtd)
            {
                if (Adivinhar(arq))
                {
                    if (indiceSentenca + 1 < qtd)
                    {
                        btnProximo_Click(null, null);
                    }
                    else
                        break;
                }
                else
                    break;
            }
        }
    }
}
