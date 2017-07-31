using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Sintaxe
{
    /// <summary>
    /// Elementos para TABGRAFO
    /// </summary>
    class ElementoGrafo
    {
        /// <summary>
        /// Terminal
        /// </summary>
        public bool ter = true;

        /// <summary>
        /// Indice na tabela TABNT ou TABT
        /// </summary>
        public int sim;

        /// <summary>
        /// Alternativa
        /// </summary>
        public int alt;

        /// <summary>
        /// Sucessor
        /// </summary>
        public int suc;

        /// <summary>
        /// Rotina semantica
        /// </summary>
        public int sem;

        public ElementoGrafo()
        {
        }
    }
}
