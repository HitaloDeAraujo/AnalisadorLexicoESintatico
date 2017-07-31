using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto
{
    /// <summary>
    /// Classe para verificacao se a palavra e um termo reservado
    /// </summary>
    static class SimbolosReservados
    {
        //simbolos reservados
        private static string[] SimbolosEspeciais = { "+", "-", "*", ":=", ".", ",", ";", ":", "'", "<>", "<", "<=", ">=", ">", "(", ")", "[", "]", "{", "}" };
        private static string[] PalavrasReservadas = {"and", "array", "begin", "case", "const", "div", "do", "downto", "else", "end", "file", "for",
                                          "func", "goto", "if", "in", "label", "mod", "nil", "not", "of", "or", "packed", "proc",
                                          "progr", "record", "repeat", "set", "then", "to", "type", "until", "var", "while", "with"};

        //colecao de simbolos reservados
        private static HashSet<string> hs = new HashSet<string>();

        private static string To6(string Palavra)
        {
            if (Palavra.Length > 6)
                Palavra = Palavra.Substring(0, 6);
            else if (Palavra.Length < 6)
            {
                int aux = 6 - Palavra.Length;

                for (int i = 0; i < aux; i++)
                    Palavra += " ";
            }

            return Palavra;
        }

        //adiciona simbolos reservados a hs
        private static void AddSimbolosReservados()
        {
            for (int i = 0; i < SimbolosEspeciais.Length; i++)
            {
                SimbolosEspeciais[i] = To6(SimbolosEspeciais[i]);
                hs.Add(SimbolosEspeciais[i]);
            }

            for (int i = 0; i < PalavrasReservadas.Length; i++)
            {
                PalavrasReservadas[i] = To6(PalavrasReservadas[i]);
                hs.Add(PalavrasReservadas[i]);
            }
        }

        /// <summary>
        /// Verifica se a palavra e um simbolo reservado
        /// </summary>
        //verifica se a palavra e um simbolo reservado
        public static bool Existe(string palavra)
        {
            if (hs.Count() == 0)            //preenche a colecao vazia
                AddSimbolosReservados();

            if (hs.Contains(To6(palavra)))
                return true;
            else
                return false;
        }

        public static void ImprimirSimbolosReservados()
        {
            foreach (var item in SimbolosEspeciais)
                Console.WriteLine(item);

            foreach (var item in PalavrasReservadas)
                Console.WriteLine(item);
        }
    }
}
