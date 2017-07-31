using Projeto.Sintaxe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto
{
    /// <summary>
    /// Classe para analise lexica
    /// </summary>
    static class ANALEX
    {
        //p1, p2 e p3 serao as saidas
        private static string p1, p2;
        private static int p3 = -1, indice, j;
        private static char[] entrada = new char[80];

        #region Acoes Semanticas
        /// <summary>
        /// Acao semantica a1
        /// </summary>
        private static void a1()
        {
            p2 = "      ";
            j = 0;

            List<char> aux = new List<char>();
            aux.AddRange(p2.ToList());
            aux[j] = entrada[indice];

            p2 = null;

            foreach (char item in aux)
                p2 += item;
        }

        /// <summary>
        /// Acao semantica a2
        /// </summary>
        private static void a2()
        {
            j = j + 1;

            if (j <= 6)
            {
                List<char> aux = new List<char>();
                aux.AddRange(p2.ToList());
                aux[j] = entrada[indice];

                p2 = null;

                foreach (char item in aux)
                    p2 += item;
            }
        }

        /// <summary>
        /// Acao semantica a3
        /// </summary>
        private static void a3()
        {
            if (SimbolosReservados.Existe(p2))
                p1 = p2;
            else
                p1 = "iden  ";

            indice = indice - 1;
        }

        /// <summary>
        /// Acao semantica a4
        /// </summary>
        private static void a4()
        {
            p3 = int.Parse(entrada[indice].ToString());
        }

        /// <summary>
        /// Acao semantica a5
        /// </summary>
        private static void a5()
        {
            p3 = p3 * 10 + int.Parse(entrada[indice].ToString());
        }

        /// <summary>
        /// Acao semantica a6
        /// </summary>
        private static void a6()
        {
            p1 = "numb  ";
            indice = indice - 1;
        }

        /// <summary>
        /// Acao semantica a7
        /// </summary>
        private static void a7()
        {
            List<char> aux = new List<char>();
            aux.AddRange(p2.ToList());
            aux[1] = entrada[indice];

            p2 = null;

            foreach (char item in aux)
                p2 += item;

            if (!SimbolosReservados.Existe(p2)) //Verifica se p2 pertence aos simbolos reservados
            {
                p2 = aux[0].ToString();     //exclui o segundo caractere

                indice = indice - 1;        //diminui o indice
            }

            p1 = p2;
        }

        /// <summary>
        /// Acao semantica a8
        /// </summary>
        private static void a8()
        {
            p1 = p2;
            indice = indice - 1;
        }

        /// <summary>
        /// Acao semantica a9
        /// </summary>
        private static void a9()
        {
            p1 = "";
            p2 = "";
        }

        /// <summary>
        /// Acao semantica a10
        /// </summary>
        private static void a10()
        {
            p1 = "string";
        }

        /// <summary>
        /// Acao semantica a11
        /// </summary>
        private static void a11()
        {
            switch (p2)
            {
                case "SIMRES": Simres(); break;
                case "LISTON": Liston(); break;
                case "LISTOF": Listof(); break;
                case "ITLEON": Itleon(); break;
                case "ITLEOF": Itleof(); break;
            }
        }

        /// <summary>
        /// Identifica a acao semantica
        /// </summary>
        private static void AcaoSemantica(string Acao)
        {
            switch (Acao)
            {
                case "a1": a1(); break;
                case "a2": a2(); break;
                case "a3": a3(); break;
                case "a4": a4(); break;
                case "a5": a5(); break;
                case "a6": a6(); break;
                case "a7": a7(); break;
                case "a8": a8(); break;
                case "a9": a9(); break;
                case "a10": a10(); break;
                case "a11": a11(); break;
                default: break;
            }

            indice++;    //Novo caractere
        }
        #endregion

        #region Comandos de controle do compilador

        /// <summary>
        /// Parametro p1
        /// </summary>
        public static string P1()
        {
            return p1;
        }

        /// <summary>
        /// Parametro p2
        /// </summary>
        private static string P2()
        {
            return p2;
        }

        /// <summary>
        /// Parametro p3
        /// </summary>
        private static string P3()
        {
            if (p3 == -1)
                return "";
            else
            {
                Stack<int> Pilha = new Stack<int>();
                int Numero, Resto;
                string aux = "";

                Numero = p3;

                while (Numero >= 2)
                {
                    Resto = Numero % 2;
                    Numero = Numero / 2;
                    Pilha.Push(Resto);
                }

                Pilha.Push(Numero);

                foreach (int x in Pilha)
                    aux += x.ToString();

                return aux;
            }
        }

        /// <summary>
        /// Imprime os simbolos reservados
        /// </summary>
        private static void Simres()
        {
            SimbolosReservados.ImprimirSimbolosReservados();
        }

        private static bool AtivarListon = false;

        /// <summary>
        /// Imprime registros do programa fonte
        /// </summary>
        private static void Liston()
        {
            AtivarListon = true;
        }

        /// <summary>
        /// Encerra impressao dos registros do programa fonte
        /// </summary>
        private static void Listof()
        {
            AtivarListon = false;
        }

        private static bool AtivarItleon = false;

        /// <summary>
        /// Habilita impressao dos parametros p1, p2 e p3
        /// </summary>
        private static void Itleon()
        {
            AtivarItleon = true;
        }

        /// <summary>
        /// Desabilita impressao dos parametros p1, p2 e p3
        /// </summary>
        private static void Itleof()
        {
            AtivarItleon = false;
        }

        private static void ImprimirParametros()
        {
            if (P1() != "" && AtivarItleon)
                Console.WriteLine("p1: " + ANALEX.P1() + " p2: " + ANALEX.P2() + " p3: " + ANALEX.P3());
        }
        #endregion

        #region Padroes
        /// <summary>
        /// Verifica se o caractere e uma letra
        /// </summary>
        private static bool l(char caractere)
        {
            string palavra = caractere.ToString().ToLower();

            if ((palavra == "a" || palavra == "z") || (string.Compare(palavra, "a", false) > 0 && string.Compare(palavra, "z", false) < 0))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Verifica se o caractere e um digito
        /// </summary>
        private static bool d(char caractere)
        {
            string palavra = caractere.ToString().ToLower();

            if ((palavra == "0" || palavra == "9") || (string.Compare(palavra, "0", false) > 0 && string.Compare(palavra, "9", false) < 0))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Identifica se o caractere se encaixa no padrao
        /// </summary>
        private static bool Simbolo(char caractere, string padrao)
        {
            if (caractere == ' ' && padrao == " ")
                return true;
            else if (l(caractere) && padrao == "l")
                return true;
            else if (d(caractere) && padrao == "d")
                return true;
            else if (!d(caractere) && !l(caractere) && caractere != ' ' && caractere != '/' && caractere != ',' && caractere.ToString() != "'" && caractere != '$' && padrao == "c-d-l-' '-/-,-'-$")
                return true;
            else if (!d(caractere) && !l(caractere) && caractere != ' ' && padrao == "c-d-l-' '")
                return true;
            else if (!d(caractere) && !l(caractere) && padrao == "c-d-l")
                return true;
            else if (caractere == '/' && padrao == "/")
                return true;
            else if (!d(caractere) && padrao == "c-d")
                return true;
            else if ((d(caractere) || l(caractere) || caractere == ' ') && padrao == "l|d|' '")
                return true;
            else if (caractere != '*' && padrao == "c-*")
                return true;
            else if (caractere == '*' && padrao == "*")
                return true;
            else if (caractere != '/' && caractere != '*' && padrao == "c-/-*")
                return true;
            else if (caractere.ToString() == "'" && padrao == "'")
                return true;
            else if (caractere.ToString() == "," && padrao == ",")
                return true;
            else if (caractere == '$' && padrao == "$")
                return true;
            else if (padrao == "c")
                return true;
            else if (caractere.ToString() != "'" && padrao == "c-'")
                return true;
            else
                return false;
        }
        #endregion

        #region Automato finito
        //Vetor para tabela de transicoes
        private static string[,] TabelaDeTransicoes = new string[32, 5];

        /// <summary>
        /// Carregar tabela de transicoes
        /// </summary>
        private static void CarregarTabelaDeTransicoes()
        {
            //Estado Atual                     //Simbolo Lido                                 //Proximo Estado                    //Proxima Transicao                   //Acoes Semanticas
            TabelaDeTransicoes[0, 0] = "s0"; TabelaDeTransicoes[0, 1] = " "; TabelaDeTransicoes[0, 2] = "s0"; TabelaDeTransicoes[0, 3] = "0"; TabelaDeTransicoes[0, 4] = "";
            TabelaDeTransicoes[1, 0] = "s0"; TabelaDeTransicoes[1, 1] = "l"; TabelaDeTransicoes[1, 2] = "s1"; TabelaDeTransicoes[1, 3] = "7"; TabelaDeTransicoes[1, 4] = "a1";
            TabelaDeTransicoes[2, 0] = "s0"; TabelaDeTransicoes[2, 1] = "d"; TabelaDeTransicoes[2, 2] = "s2"; TabelaDeTransicoes[2, 3] = "10"; TabelaDeTransicoes[2, 4] = "a4";
            TabelaDeTransicoes[3, 0] = "s0"; TabelaDeTransicoes[3, 1] = "c-d-l-' '-/-,-'-$"; TabelaDeTransicoes[3, 2] = "s3"; TabelaDeTransicoes[3, 3] = "12"; TabelaDeTransicoes[3, 4] = "a1";
            TabelaDeTransicoes[4, 0] = "s0"; TabelaDeTransicoes[4, 1] = "/"; TabelaDeTransicoes[4, 2] = "s4"; TabelaDeTransicoes[4, 3] = "14"; TabelaDeTransicoes[4, 4] = "a1";
            TabelaDeTransicoes[5, 0] = "s0"; TabelaDeTransicoes[5, 1] = "'"; TabelaDeTransicoes[5, 2] = "s8"; TabelaDeTransicoes[5, 3] = "21"; TabelaDeTransicoes[5, 4] = "";
            TabelaDeTransicoes[6, 0] = "s0"; TabelaDeTransicoes[6, 1] = "$"; TabelaDeTransicoes[6, 2] = "s11"; TabelaDeTransicoes[6, 3] = "27"; TabelaDeTransicoes[6, 4] = "";
            TabelaDeTransicoes[7, 0] = "s1"; TabelaDeTransicoes[7, 1] = "d"; TabelaDeTransicoes[7, 2] = "s1"; TabelaDeTransicoes[7, 3] = "7"; TabelaDeTransicoes[7, 4] = "a2";
            TabelaDeTransicoes[8, 0] = "s1"; TabelaDeTransicoes[8, 1] = "l"; TabelaDeTransicoes[8, 2] = "s1"; TabelaDeTransicoes[8, 3] = "7"; TabelaDeTransicoes[8, 4] = "a2";
            TabelaDeTransicoes[9, 0] = "s1"; TabelaDeTransicoes[9, 1] = "c-d-l"; TabelaDeTransicoes[9, 2] = "s7"; TabelaDeTransicoes[9, 3] = "nenhum"; TabelaDeTransicoes[9, 4] = "a3";
            TabelaDeTransicoes[10, 0] = "s2"; TabelaDeTransicoes[10, 1] = "d"; TabelaDeTransicoes[10, 2] = "s2"; TabelaDeTransicoes[10, 3] = "10"; TabelaDeTransicoes[10, 4] = "a5";
            TabelaDeTransicoes[11, 0] = "s2"; TabelaDeTransicoes[11, 1] = "c-d"; TabelaDeTransicoes[11, 2] = "s7"; TabelaDeTransicoes[11, 3] = "nenhum"; TabelaDeTransicoes[11, 4] = "a6";
            TabelaDeTransicoes[12, 0] = "s3"; TabelaDeTransicoes[12, 1] = "l|d|' '"; TabelaDeTransicoes[12, 2] = "s7"; TabelaDeTransicoes[12, 3] = "nenhum"; TabelaDeTransicoes[12, 4] = "a8";
            TabelaDeTransicoes[13, 0] = "s3"; TabelaDeTransicoes[13, 1] = "c-d-l-' '"; TabelaDeTransicoes[13, 2] = "s7"; TabelaDeTransicoes[13, 3] = "nenhum"; TabelaDeTransicoes[13, 4] = "a7";
            TabelaDeTransicoes[14, 0] = "s4"; TabelaDeTransicoes[14, 1] = "c-*"; TabelaDeTransicoes[14, 2] = "s7"; TabelaDeTransicoes[14, 3] = "nenhum"; TabelaDeTransicoes[14, 4] = "a8";
            TabelaDeTransicoes[15, 0] = "s4"; TabelaDeTransicoes[15, 1] = "*"; TabelaDeTransicoes[15, 2] = "s5"; TabelaDeTransicoes[15, 3] = "16"; TabelaDeTransicoes[15, 4] = "";
            TabelaDeTransicoes[16, 0] = "s5"; TabelaDeTransicoes[16, 1] = "*"; TabelaDeTransicoes[16, 2] = "s6"; TabelaDeTransicoes[16, 3] = "18"; TabelaDeTransicoes[16, 4] = "";
            TabelaDeTransicoes[17, 0] = "s5"; TabelaDeTransicoes[17, 1] = "c-*"; TabelaDeTransicoes[17, 2] = "s5"; TabelaDeTransicoes[17, 3] = "16"; TabelaDeTransicoes[17, 4] = "";
            TabelaDeTransicoes[18, 0] = "s6"; TabelaDeTransicoes[18, 1] = "c-/-*"; TabelaDeTransicoes[18, 2] = "s5"; TabelaDeTransicoes[18, 3] = "16"; TabelaDeTransicoes[18, 4] = "";
            TabelaDeTransicoes[19, 0] = "s6"; TabelaDeTransicoes[19, 1] = "*"; TabelaDeTransicoes[19, 2] = "s6"; TabelaDeTransicoes[19, 3] = "18"; TabelaDeTransicoes[19, 4] = "";
            TabelaDeTransicoes[20, 0] = "s6"; TabelaDeTransicoes[20, 1] = "/"; TabelaDeTransicoes[20, 2] = "s0"; TabelaDeTransicoes[20, 3] = "0"; TabelaDeTransicoes[20, 4] = "a9";
            TabelaDeTransicoes[21, 0] = "s8"; TabelaDeTransicoes[21, 1] = "c"; TabelaDeTransicoes[21, 2] = "s9"; TabelaDeTransicoes[21, 3] = "23"; TabelaDeTransicoes[21, 4] = "a1";
            TabelaDeTransicoes[22, 0] = "s8"; TabelaDeTransicoes[22, 1] = "'"; TabelaDeTransicoes[22, 2] = "s10"; TabelaDeTransicoes[22, 3] = "25"; TabelaDeTransicoes[22, 4] = "";
            TabelaDeTransicoes[23, 0] = "s9"; TabelaDeTransicoes[23, 1] = "c-'"; TabelaDeTransicoes[23, 2] = "s9"; TabelaDeTransicoes[23, 3] = "23"; TabelaDeTransicoes[23, 4] = "a2";
            TabelaDeTransicoes[24, 0] = "s9"; TabelaDeTransicoes[24, 1] = "'"; TabelaDeTransicoes[24, 2] = "s10"; TabelaDeTransicoes[24, 3] = "25"; TabelaDeTransicoes[24, 4] = "";
            TabelaDeTransicoes[25, 0] = "s10"; TabelaDeTransicoes[25, 1] = "'"; TabelaDeTransicoes[25, 2] = "s9"; TabelaDeTransicoes[25, 3] = "23"; TabelaDeTransicoes[25, 4] = "a2";
            TabelaDeTransicoes[26, 0] = "s10"; TabelaDeTransicoes[26, 1] = "c-'"; TabelaDeTransicoes[26, 2] = "s7"; TabelaDeTransicoes[26, 3] = "nenhum"; TabelaDeTransicoes[26, 4] = "a10";
            TabelaDeTransicoes[27, 0] = "s11"; TabelaDeTransicoes[27, 1] = "l"; TabelaDeTransicoes[27, 2] = "s12"; TabelaDeTransicoes[27, 3] = "28"; TabelaDeTransicoes[27, 4] = "a1";
            TabelaDeTransicoes[28, 0] = "s12"; TabelaDeTransicoes[28, 1] = "l"; TabelaDeTransicoes[28, 2] = "s12"; TabelaDeTransicoes[28, 3] = "28"; TabelaDeTransicoes[28, 4] = "a2";
            TabelaDeTransicoes[29, 0] = "s12"; TabelaDeTransicoes[29, 1] = ","; TabelaDeTransicoes[29, 2] = "s13"; TabelaDeTransicoes[29, 3] = "31"; TabelaDeTransicoes[29, 4] = "a11";
            TabelaDeTransicoes[30, 0] = "s12"; TabelaDeTransicoes[30, 1] = "$"; TabelaDeTransicoes[30, 2] = "s7"; TabelaDeTransicoes[30, 3] = "nenhum"; TabelaDeTransicoes[30, 4] = "a11";
            TabelaDeTransicoes[31, 0] = "s13"; TabelaDeTransicoes[31, 1] = "l"; TabelaDeTransicoes[31, 2] = "s12"; TabelaDeTransicoes[31, 3] = "28"; TabelaDeTransicoes[31, 4] = "a1";
        }

        /// <summary>
        /// Automato finito
        /// </summary>
        private static bool AutomatoFinito(string Entrada)
        {
            //Carrega a tabela uma vez
            if (TabelaDeTransicoes[0, 0] == null)
                CarregarTabelaDeTransicoes();

            //Define fim da entrada
            if (Entrada == "")
            {
                p1 = "$";
                p2 = "$";
                p3 = -1;

                ImprimirParametros();

                return true;
            }

            //Volta as variaveis ao valor inicial
            p1 = "";
            p2 = "      ";
            p3 = -1;
            indice = 0;
            j = 0;
            //entrada.DefaultIfEmpty();

            //Transforma a entrada em um vetor de caracteres
            for (int i = 0; i < Entrada.Length; i++)
                entrada[i] = Entrada[i];

            //Lista de estados finais
            List<string> EstadosFinais = new List<string>();
            EstadosFinais.Add("s7");

            //EstadosFinais.Add("s0");    //Sem caracteres ou com somente comentarios

            int Linha = 0;
            char EntradaAtual;
            string EstadoAtual = "s0";

            //Para cada caractere
            for (int i = 0; i < Entrada.Length; i++)
            {
                if (AtivarListon)
                {
                    for (int k = indice; k < Entrada.Length - 1; k++)
                    {
                        Console.Write(Entrada[k]);

                        if (AtivarListon == false)
                        {
                            Console.WriteLine();
                            break;
                        }
                    }

                    Console.WriteLine();
                }

                EntradaAtual = Entrada[i];

                if (Simbolo(EntradaAtual, TabelaDeTransicoes[Linha, 1]))    //Verifica se a EntradaAtual corresponde ao padrao
                {
                    AcaoSemantica(TabelaDeTransicoes[Linha, 4]);    //Usa acao semantica especifica

                    EstadoAtual = TabelaDeTransicoes[Linha, 2];     //Novo estado atual

                    if (TabelaDeTransicoes[Linha, 3] != "nenhum")           //Verifica se existe estado posterior
                        Linha = int.Parse(TabelaDeTransicoes[Linha, 3]);    //Linha passa a ser a linha do proximo estado
                }
                else
                {
                    bool teste = false; //Variavel auxiliar

                    do  //Enquanto a linha estiver no estado atual
                    {
                        Linha = Linha + 1;  //Passa para a proxima linha

                        //Verifica se o proximo estado e igual ao estado atual e se a EntradaAtual corresponde ao padrao
                        if (EstadoAtual == TabelaDeTransicoes[Linha, 0] && Simbolo(EntradaAtual, TabelaDeTransicoes[Linha, 1]))
                        {
                            AcaoSemantica(TabelaDeTransicoes[Linha, 4]);    //Usa acao semantica especifica

                            EstadoAtual = TabelaDeTransicoes[Linha, 2];     //Novo estado atual

                            if (TabelaDeTransicoes[Linha, 3] != "nenhum")           //Verifica se existe estado posterior
                                Linha = int.Parse(TabelaDeTransicoes[Linha, 3]);    //Linha passa a ser a linha do proximo estado

                            teste = false;
                        }
                        else if ((Linha + 1 < 32) && EstadoAtual == TabelaDeTransicoes[Linha + 1, 0])   //Verifica se a proxima linha existe e se o seu estado e o atual
                            teste = true;
                        else
                            return false;
                    } while (teste);    //Enquanto a linha estiver no estado atual
                }

                if (EstadosFinais.Contains(EstadoAtual))        //Verifica se o estado atual e algum dos estados finais
                {
                    ImprimirParametros();

                    return true;
                }
            }

            if (EstadosFinais.Contains(EstadoAtual))            //Verifica se o estado atual e algum dos estados finais
            {
                ImprimirParametros();

                return true;
            }
            else
                return false;
        }
        #endregion

        static string TextoCodigoFonte = CodigoFonte.Codigo();

        /// <summary>
        /// Inicia tratamento lexico
        /// </summary>
        public static void Inicio()
        {
            entrada.DefaultIfEmpty();

            if (CodigoFonte.Codigo().Length <= 80)
            {
                if (AutomatoFinito(TextoCodigoFonte))
                {

                    if (TextoCodigoFonte == "")
                        return;

                    if (indice + 1 <= TextoCodigoFonte.Length)
                        TextoCodigoFonte = TextoCodigoFonte.Substring(indice).TrimStart();
                    else
                        TextoCodigoFonte = "";
                }
            }
        }
    }
}
