
#region Referências

using Db4objects.Db4o;
using System.Configuration;
using System;
using Model;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Defragment;

#endregion Referências

//Classes para coneção com banco de dados
namespace DAL.Connection
{    
    public class ObjectBase
    {
        #region Atributos

        private static IObjectContainer container = null;
        private static IEmbeddedConfiguration config = null;
        private static AppSettingsReader cap = null;

        #endregion Atributos

        #region Construtor

        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ObjectBase()
        {

        }

        #endregion Construtor
      
        #region Propriedades

        /// <summary>
        /// Abre conecção com o Banco,se não existir e retorna 
        /// </summary>
        protected IObjectContainer Container
        {            
            get
            {                
                if (container == null)
                {
                    Conectar();
                }

                return container; 
            }
        }

        #endregion Propriedades

        #region Rotinas

        #region Conexão

        /// <summary>
        /// Conecta com base de dados, caso não exista conexão ativa
        /// </summary>
        public void Conectar()
        {
            if (container == null)
            {
                //Cria configuração para container
                config = Db4oEmbedded.NewConfiguration();

                //TODO:REMOVER
                config.Common.DetectSchemaChanges = true;

                //Configura indices paras as models
                //ConfigurarIndices();

                //Profundidade de ativacao
                config.Common.ActivationDepth = 8;

                //Cria Instância para leitura do arquivo de configuração
                cap = new AppSettingsReader();

                //Cria instância para container TODO:: cap.GetValue("BasePath", typeof(String)).ToString()
                container = Db4oEmbedded.OpenFile(config, Config.BancoDadosURL);
            }
        }

        /// <summary>
        /// Fecha conexão
        /// </summary>
        public void CloseBase()
        {
            if (container != null)
                container.Close();
            container = null;
        }

        #endregion Inicialização

        #region Manutenção

        /// <summary>
        /// Metodo para habilitar a busca por alteracoes na estrutura das classes do DB4O
        /// </summary>
        /// <param name="flag">Verdadeiro para habilitar, falso para desabilitar</param>
        public void HabilitarVerificaoAlteracaoEstrutura(bool flag)
        {
            if (config == null)
            {
                Conectar();
            }
            config.Common.DetectSchemaChanges = flag;
        }

        /// <summary>
        /// Metodo para desfragmentar uma base de dados
        /// </summary>
        public void Desfragmentar()
        {
            if (cap == null)
            {
                Conectar();
            }
            Defragment.Defrag(cap.GetValue("BasePath", typeof(String)).ToString());
        }
       
        #endregion Manutenção

        
        #region SalvarComo

        /// <summary>
        /// Abre/Cria Banco DB4O informado
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IObjectContainer OpenDataBaseExterno(String path)
        {
            IObjectContainer containerTemp = null;
            IEmbeddedConfiguration config = null;

            //Cria configuração para container
            config = Db4oEmbedded.NewConfiguration();

            //Profundidade de ativacao
            config.Common.ActivationDepth = 8;

            //Abre arquivo
            containerTemp = Db4oEmbedded.OpenFile(config, path);

            return containerTemp;
        }

        #endregion Salvar Como

        #endregion
    }
}
