using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Projeto.Sintaxe;

namespace Projeto
{
    class Program
    {
        static void Main(string[] args)
        {
            CodigoFonte.Codigo("as ");
            ANSIN.Inicio(1, false);

            Console.ReadKey();
        }
    }
}
