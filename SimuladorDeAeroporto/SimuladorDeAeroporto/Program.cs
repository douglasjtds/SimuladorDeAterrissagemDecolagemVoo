using SimuladorDeAeroporto.Classes_De_Negócio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimuladorDeAeroporto
{
    public class Program
    {
        private static Fila Fila1;
        private static Fila Fila2;
        private static Fila Fila3;
        private static Fila Fila4;

        private static Fila Fila5;
        private static Fila Fila6;
        private static Fila Fila7;

        static void Main(string[] args)
        {
            Console.WriteLine("----- [Simulador de Aeroporto] -----");
            Thread.Sleep(1000);

            #region [instanciando as filas]
            Fila1 = new Fila();
            Fila1.ReferenteAPista = 1;

            Fila2 = new Fila();
            Fila2.ReferenteAPista = 1;

            Fila3 = new Fila();
            Fila3.ReferenteAPista = 2;

            Fila4 = new Fila();
            Fila4.ReferenteAPista = 2;

            Fila5 = new Fila();
            Fila5.ReferenteAPista = 1;

            Fila6 = new Fila();
            Fila6.ReferenteAPista = 2;

            Fila7 = new Fila();
            Fila7.ReferenteAPista = 3;
            #endregion

            //GeraAvioesPouso(GeraQuantidadeDeAvioesAleatoria());

            //GeraAvioesDecolagem(GeraQuantidadeDeAvioesAleatoria());
            Console.WriteLine("Inicializando aeroporto...");
            Thread.Sleep(100);

            while (true)
            {

                //var filaParaPousar = new FilaParaPousar();
                //Console.WriteLine(filaParaPousar.SetarGasolinaAviao()); //TESTE

                //chamar todas as funções aqui

                GeraAvioes(GeraQuantidadeDeAvioesAleatoria());

                

            }
        }

        ////if (!aviao.IsAviaoDecolando(aviao.Id_Aviao))
        //private static List<Aviao> GeraAvioesPouso(int quantidade)
        //{
        //    for (int i = 0; i < quantidade; i++)
        //    {
        //        Aviao aviao = new Aviao();
        //        aviao.NivelGasolina = aviao.GeraValorAleatorioParaGasolina();
        //        //aviao.Id_Aviao = idAviao + 1;
        //        //idAviao++;
        //        //aviao.ReferenteAPista = ; //VER COMO VAI SER ESCOLHIDO ISSO
        //    }
        //}

        //int idAviao = 0;

        //private static List<Aviao> GeraAvioesDecolagem(int quantidade)
        //{
        //    for (int i = 0; i < quantidade; i++)
        //    {
        //        Aviao aviao = new Aviao();
        //        aviao.NivelGasolina = null;
        //        //aviao.Id_Aviao = idAviao + 1;
        //        //idAviao++;
        //        //aviao.ReferenteAPista = ; //VER COMO VAI SER ESCOLHIDO ISSO
        //    }
        //}


        private static List<Aviao> GeraAvioes(int quantidade)
        {
            int idAviao = 1;
            for (int i = 0; i >= GeraQuantidadeDeAvioesAleatoria(); i++)
            {
                Aviao aviao = new Aviao();
                aviao.Id_Aviao = idAviao;
                idAviao++;
                if (!aviao.IsAviaoDecolando(aviao.Id_Aviao))
                    aviao.NivelGasolina = null;
                else
                    aviao.GeraValorAleatorioParaGasolina(); //gera quantidade de gasolina
                for (int j = 0; j >= 3; j++)
                    aviao.ReferenteAPista = j; //VER COMO VAI SER ESCOLHIDO ISSO
            }
            //return GeraAvioes(); //ISSO MESMO?
            //quantidade = GeraQuantidadeDeAvioesAleatoria();
            //return quantidade;

            return new List<Aviao>();
        }


        private static int GeraQuantidadeDeAvioesAleatoria()
        {
            Random rnd = new Random();
            int quantidadeAvioes = rnd.Next(4);
            return quantidadeAvioes;
        }

        /// <summary>
        /// a) o conteúdo de cada fila;
        /// b) o tempo médio de espera para decolagem;
        /// c) o tempo médio de espera para aterrissagem; e
        /// d) o número de aviões que aterrissaram sem reserva de combustível.
        /// </summary>
        private static void ImprimirPeriodicamente()
        {

        }

        private static void PousarAviao(Aviao Aviao)
        {

        }
    }
}