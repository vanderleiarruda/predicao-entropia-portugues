
#region Referências

using Db4objects.Db4o.Linq;
using System;
using System.Collections.Generic;

#endregion Referências

namespace DAL.Mappers.Base
{    
    public abstract class BaseMapper<T>: ObjectBase
    {
        #region Construtor

        public BaseMapper()
            : base()
        {

        }

        #endregion Construtor

        #region Rotinas

        #region Consulta

        /// <summary>
        /// Retorna ultimo registro
        /// </summary>
        /// <returns></returns>
        public virtual T RetornaUltimo()
        {
            T t = (T)Activator.CreateInstance(typeof(T));

            IList<T> lista = RetornaTodos();

            if (lista.Count > 0)
                t = lista[lista.Count - 1];

            return t;
        }

        /// <summary>
        /// Retorna primeiro registro
        /// </summary>
        /// <returns></returns>
        public virtual T RetornaPrimeiro()
        {
            T t = (T)Activator.CreateInstance(typeof(T));

            IList<T> lista = RetornaTodos();

            if (lista.Count > 0)
                t = lista[0];

            return t;
        }

        /// <summary>
        /// Retorna todos os objetos de tipo T do banco
        /// </summary>
        /// <returns>Lista de objetos de tipo T</returns>
        public virtual IList<T> RetornaTodos()
        {
            IDb4oLinqQuery<T> result = null;
            List<T> lista = new List<T>();

            result = from T c in Container select c;

            foreach (T p in result)
            {
                lista.Add(p);
            }

            return lista;
        }

        /// <summary>
        /// Retorna quantidade de registros para o tipo T
        /// </summary>
        /// <returns></returns>
        public virtual int ContaRegistros()
        {
            return Container.Query<T>().Count;
        }
        

        #endregion Consulta

        #region Manipulação

        /// <summary>
        /// Grava objeto do tipo T no Banco
        /// </summary>
        /// <param name="entidade">Objeto, T é genérico</param>
        public virtual void Gravar(T entidade)
        {
            Container.Store(entidade);
            Container.Commit();
        }

        /// <summary>
        /// Exclui objeto do tipo T do banco
        /// </summary>
        /// <param name="entidade">Objeto, T é genérico</param>
        public virtual void Excluir(T entidade)
        {
            Container.Delete(entidade);
            Container.Commit();
        }

        #endregion Manipulação

        #region Conexão

        /// <summary>
        /// Fecha conexão com banco de dados
        /// </summary>
        public void FecharConexao()
        {
            base.CloseBase();
        }

        #endregion Conexão

        #endregion Rotinas
    }
}
