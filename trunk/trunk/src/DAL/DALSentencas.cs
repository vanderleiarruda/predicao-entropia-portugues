using System;
using System.Collections.Generic;
using System.Linq;
using Model;

namespace DAL
{
    public class DALSentencas : DALBase<Sentenca>
    {
        public List<Sentenca> RetornaSentencas(bool sentenca100)
        {
            IList<Sentenca> lstSentencas = base.RetornaTodos();

            List<Sentenca> resultado1 = (from e in lstSentencas where e.CEM == sentenca100
                                                                         select e).ToList();
            return resultado1;
        }
    }
}
