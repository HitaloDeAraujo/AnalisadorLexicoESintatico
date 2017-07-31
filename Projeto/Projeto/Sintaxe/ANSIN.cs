using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Sintaxe
{
    /// <summary>
    /// Classe para analise Sintatica
    /// </summary>
    static class ANSIN
    {
        static int MAXK = 50, MAXPS = 100;

        static Tabnt[] TABNT;
        static string[] TABT;
        static ElementoGrafo[] TABGRAFO;

        static ElementoK[] k = new ElementoK[MAXK];
        static string[] PS = new string[MAXPS];
        static int TopPS;
        static int Topo;

        static void Desempilha(int p)
        {
            p = k[Topo].No;
            TopPS = k[Topo].R;
            Topo = Topo - 1;
            PS[TopPS] = TABNT[TABGRAFO[p].sim].Nome;
        }

        static void Empilha(int p)
        {
            Topo = Topo + 1;
            k[Topo].No = p;
            k[Topo].R = TopPS + 1;
        }

        static void Carregador()
        {
            TABNT = CarregadorSintatico.TabelaNaoTerminais();
            TABT = CarregadorSintatico.TabelaTerminais();
            TABGRAFO = CarregadorSintatico.TabelaGrafo();
        }

        public static void Inicio(int OBJETIVO, bool SUCESSO)
        {
            for (int j = 0; j < MAXK; j++)
                k[j] = new ElementoK();

            Carregador();

            k[1].No = 0;
            k[1].R = 1;

            bool Continue;
            int i, iu;
            string Ent;

            ANALEX.Inicio();
            Ent = ANALEX.P1();

            //+
            Topo = 1; k[1].No = 0; k[1].R = 1;
            TopPS = 0;

            i = TABNT[OBJETIVO].Prim;
            iu = i;

            Continue = true;

            while (Continue)
            {
                if (i != 0)
                {
                    if (TABGRAFO[i].ter)
                    {
                        if (TABGRAFO[i].sim == 0)
                        {
                            i = TABGRAFO[i].suc;
                            iu = i;
                        }
                        else
                        {
                            if (TABT[TABGRAFO[i].sim] == Ent)//++
                            {
                                TopPS = TopPS + 1;
                                PS[TopPS] = Ent;

                                ANALEX.Inicio();
                                Ent = ANALEX.P1();

                                i = TABGRAFO[i].suc;

                                iu = i;
                            }
                            else if (TABGRAFO[i].alt != 0)
                            {
                                i = TABGRAFO[i].alt;
                            }
                        }
                    }
                    else
                    {
                        Empilha(i);
                        i = TABNT[TABGRAFO[i].sim].Prim;
                    }
                }
                else
                {
                    if (Topo != 0)
                    {
                        Desempilha(i);
                        i = TABGRAFO[i].suc;

                        iu = i;
                    }
                    else
                    {
                        if (Ent == "$")
                        {
                            SUCESSO = true;
                            Console.WriteLine("Cadeia de caracteres reconhecida");
                        }
                        else
                        {
                            SUCESSO = false;
                        }

                        Continue = false;
                    }
                }
            }
        }
    }
}
