using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Sintaxe
{
    static class CodigoFonte
    {
        static string Texto;

        public static void Codigo(string NovoCodigoFonte)
        {
            Texto = NovoCodigoFonte;
        }

        public static string Codigo()
        {
            return Texto;
        }
    }
}
