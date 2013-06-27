using Db4objects.Db4o;
using System.Configuration;
using System;
using Model;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Defragment;

namespace DAL
{
    public class ObjectBase
    {
        private static IObjectContainer container = null;
        private static IEmbeddedConfiguration config = null;
        private static AppSettingsReader cap = null;
        
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ObjectBase()
        {
        }

        /// <summary>
        /// Abre conecção com o Banco,se não existir e retorna 
        /// </summary>
        protected IObjectContainer Container
        {            
            get
            {          
                if (container == null)
                {
                    Inicializa();
                }

                return container; 
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

        /// <summary>
        /// Metodo para habilitar a busca por alteracoes na estrutura das classes do DB4O
        /// </summary>
        /// <param name="flag">Verdadeiro para habilitar, falso para desabilitar</param>
        public void HabilitarVerificaoAlteracaoEstrutura(bool flag)
        {
            if (config == null)
            {
                Inicializa();
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
                Inicializa();
            }
            Defragment.Defrag(cap.GetValue("BasePath", typeof(String)).ToString());
        }
        
        public void Inicializa()
        {
            if (container == null)
            {
                //Cria configuração para container
                config = Db4oEmbedded.NewConfiguration();

                //TODO:REMOVER
                config.Common.DetectSchemaChanges = true;

                //Profundidade de ativacao
                config.Common.ActivationDepth = 8;

                //Cria Instância para leitura do arquivo de configuração
                cap = new AppSettingsReader();
                
                //Cria instância para container
                container = Db4oEmbedded.OpenFile(config, Config.BancoDadosURL);
            }
        }
    }
}
