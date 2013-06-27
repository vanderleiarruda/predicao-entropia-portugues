using Db4objects.Db4o.Linq;
using System;
using System.Collections.Generic;

namespace DAL
{    
    public abstract class DALBase<T>: ObjectBase
    {
        #region Métodos padrão para todas as DAL, genéricos        

        /// <summary>
        /// Retorna ultimo registro
        /// </summary>
        /// <returns></returns>
        public T RetornaUltimo()
        {
            T t = (T)Activator.CreateInstance(typeof(T));

            List<T> lista = RetornaTodos();

            if (lista.Count > 0)
                t = lista[lista.Count - 1];
                       
            return t;
        }

        /// <summary>
        /// Retorna todos os objetos de tipo T do banco
        /// </summary>
        /// <returns>Lista de objetos de tipo T</returns>
        public List<T> RetornaTodos()
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
        public int ContaRegistros()
        {
            return Container.Query<T>().Count;
        }

        /// <summary>
        /// Grava objeto do tipo T no Banco
        /// </summary>
        /// <param name="entidade">Objeto, T é genérico</param>
        public void Gravar(T entidade)
        {
            try
            {
                Container.Store(entidade);
                Container.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception("@Gravar - " + ex.Message);
            }
            finally
            {
                //CloseBase();
            }
        }

        /// <summary>
        /// Exclui objeto do tipo T do banco
        /// </summary>
        /// <param name="entidade">Objeto, T é genérico</param>
        public void Excluir(T entidade)
        {
            Container.Delete(entidade);
            Container.Commit();
        }
        
        public DALBase() : base()
        {
               
        }
        #endregion
    }
}
