using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Sintaxe
{
    static class CarregadorSintatico
    {
        static int Maxt = 0, Maxnt = 0, Indprim = 1, Nomax = 0;
        static char Tipo;
        static string Nomer, Numno, Altr, Sucr, Semr;
        static Tabnt[] TABNT = new Tabnt[5];
        static string[] TABT = new string[7];
        static ElementoGrafo[] TABGRAFO = new ElementoGrafo[7];
        static int Indice;
        static bool Carregou = false;

        static StreamReader sr = new StreamReader(@"C:\Users\usuario\Desktop\Entrada.txt");

        /// <summary>
        /// Verifica a existencia do no na tabela TABNT
        /// </summary>
        private static bool ExisteTABNT(string Nomer)
        {
            for (int i = 0; i < 5; i++)
                if (Nomer == TABNT[i].Nome)
                    return true;

            return false;
        }

        /// <summary>
        /// Verifica a existencia do no na tabela TABT
        /// </summary>
        private static bool ExisteTABT(string Nomer)
        {
            for (int i = 0; i < 6; i++)
                if (Nomer == TABT[i])
                    return true;

            return false;
        }

        /// <summary>
        /// Tabela dos nos nao terminais
        /// </summary>
        public static Tabnt[] TabelaNaoTerminais()
        {
            Ler();

            return TABNT;
        }

        /// <summary>
        /// Tabela dos nos terminais
        /// </summary>
        public static string[] TabelaTerminais()
        {
            Ler();

            return TABT;
        }

        /// <summary>
        /// Tabela do grafo
        /// </summary>
        public static ElementoGrafo[] TabelaGrafo()
        {
            Ler();

            return TABGRAFO;
        }

        /// <summary>
        /// Le o arquivo descritivo dos grafos
        /// </summary>
        private static void Ler()
        {
            if (Carregou)
                return;

            Carregou = true;

            for (int i = 0; i < 5; i++)
                TABNT[i] = new Tabnt();

            for (int i = 0; i < 7; i++)
                TABGRAFO[i] = new ElementoGrafo();

            TABGRAFO[0].ter = false;
            TABGRAFO[0].sim = 1;
            TABGRAFO[0].suc = 0;
            TABGRAFO[0].sem = 0;

            while (!sr.EndOfStream)
            {
                string Linha = sr.ReadLine();

                try
                {
                    Tipo = Linha[0];

                    Nomer = Linha.Substring(2, 6);

                    Numno = Linha.Substring(9, 3);

                    Altr = Linha.Substring(13, 3);

                    Sucr = Linha.Substring(17, 3);

                    Semr = Linha.Substring(21, 3);

                    Console.WriteLine("Tipo: " + Tipo + " Nomer: " + Nomer + " Numno: " + Numno + " Altr: " + Altr + " Sucr: " + Sucr + " Semr: " + Semr);
                }
                catch (Exception)
                {
                }

                if (Tipo == 'c')
                {
                    Indprim = Indprim + Nomax;
                    Nomax = 0;

                    if (!ExisteTABNT(Nomer))
                    {
                        Maxnt = Maxnt + 1;

                        TABNT[Maxnt].Nome = Nomer;
                        TABNT[Maxnt].Prim = Indprim;
                    }

                    for (int i = 0; i < 5; i++)
                    {
                        if (TABNT[i].Nome == Nomer)
                        {
                            //if (TABNT[i].Prim == 0)
                            //{
                            TABNT[i].Prim = Indprim;
                            //}
                            //else
                            //{
                            //    Console.WriteLine("erro");
                            //    Console.ReadKey();
                            //}
                        }
                    }
                }
                else
                {
                    Indice = Indprim + int.Parse(Numno) - 1;

                    if (Tipo == 't' && Nomer.Trim() != "")
                    {
                        if (!ExisteTABT(Nomer))
                        {
                            Maxt = Maxt + 1;
                            TABT[Maxt] = Nomer;
                        }
                    }

                    if (Tipo == 'n')
                    {
                        if (!ExisteTABNT(Nomer))
                        {
                            Maxnt = Maxnt + 1;

                            TABNT[Maxnt].Nome = Nomer;
                            TABNT[Maxnt].Prim = 0;
                        }

                        TABGRAFO[Indice].ter = false;
                    }

                    if (Nomer.Trim() == "")
                        TABGRAFO[Indice].sim = 0;
                    else
                    {
                        if (Tipo == 't')
                        {
                            for (int i = 0; i < 7; i++)
                                if (TABT[i] == Nomer)
                                    TABGRAFO[Indice].sim = i;
                        }
                        else if (Tipo == 'n')
                        {
                            for (int i = 0; i < 3; i++)
                                if (TABNT[i].Nome == Nomer)
                                    TABGRAFO[Indice].sim = i;
                        }
                    }

                    if (int.Parse(Altr) != 0)
                        TABGRAFO[Indice].alt = Indprim + int.Parse(Altr) - 1;
                    else
                        TABGRAFO[Indice].alt = 0;

                    if (int.Parse(Sucr) != 0)
                        TABGRAFO[Indice].suc = Indprim + int.Parse(Sucr) - 1;
                    else
                        TABGRAFO[Indice].suc = 0;

                    //TABGRAFO[I].sem = int.Parse(Semr);

                    if (Nomax < int.Parse(Numno))
                        Nomax = int.Parse(Numno);
                }

            }

            Console.WriteLine();

            for (int i = 1; i < 7; i++)
            {
                Console.WriteLine(i + "\t" + TABGRAFO[i].ter + "\t" + TABGRAFO[i].sim + " " + TABGRAFO[i].alt + " " + TABGRAFO[i].suc + " " + TABGRAFO[i].sem);
            }

            Console.WriteLine();

            for (int i = 1; i < 7; i++)
            {
                Console.WriteLine(TABT[i]);
            }

            Console.WriteLine();

            for (int i = 1; i < 3; i++)
            {
                Console.WriteLine(TABNT[i].Nome + " " + TABNT[i].Prim);
            }
        }
    }
}
