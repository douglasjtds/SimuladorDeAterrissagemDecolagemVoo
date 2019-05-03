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
        private static FilaParaPousar Fila1;
        private static FilaParaPousar Fila2;
        private static FilaParaPousar Fila3;
        private static FilaParaPousar Fila4;

        private static FilaParaDecolar Fila5;
        private static FilaParaDecolar Fila6;
        private static FilaParaDecolar Fila7;

        static void Main(string[] args)
        {
            Console.WriteLine("----- [Simulador de Aeroporto] -----");
            Thread.Sleep(1000);

            #region [instanciando as filas]
            Fila1 = new FilaParaPousar();
            Fila1.ReferenteAPista = 1;

            Fila2 = new FilaParaPousar();
            Fila2.ReferenteAPista = 1;

            Fila3 = new FilaParaPousar();
            Fila3.ReferenteAPista = 2;

            Fila4 = new FilaParaPousar();
            Fila4.ReferenteAPista = 2;

            Fila5 = new FilaParaDecolar();
            Fila5.ReferenteAPista = 1;

            Fila6 = new FilaParaDecolar();
            Fila6.ReferenteAPista = 2;

            Fila7 = new FilaParaDecolar();
            Fila7.ReferenteAPista = 3;
            #endregion

            GeraAvioesPouso(GeraQuantidadeDeAvioesAleatoria());

            GeraAvioesDecolagem(GeraQuantidadeDeAvioesAleatoria());

            while (true)
            {

                //var filaParaPousar = new FilaParaPousar();
                //Console.WriteLine(filaParaPousar.SetarGasolinaAviao()); //TESTE

                //chamar todas as funções aqui
            }
        }

        //if (!aviao.IsAviaoDecolando(aviao.Id_Aviao))
        private static List<Aviao> GeraAvioesPouso(int quantidade)
        {
            for (int i = 0; i < quantidade; i++)
            {
                Aviao aviao = new Aviao();
                aviao.NivelGasolina = aviao.GeraValorAleatorioParaGasolina();
                //aviao.Id_Aviao = idAviao + 1;
                //idAviao++;
                //aviao.ReferenteAPista = ; //VER COMO VAI SER ESCOLHIDO ISSO
            }
        }

        int idAviao = 0;

        private static List<Aviao> GeraAvioesDecolagem(int quantidade)
        {
            for (int i = 0; i < quantidade; i++)
            {
                Aviao aviao = new Aviao();
                aviao.NivelGasolina = null;
                //aviao.Id_Aviao = idAviao + 1;
                //idAviao++;
                //aviao.ReferenteAPista = ; //VER COMO VAI SER ESCOLHIDO ISSO
            }
        }



        public static int GeraQuantidadeDeAvioesAleatoria()
        {
            Random rnd = new Random();
            int quantidadeAvioes = rnd.Next(4);
            return quantidadeAvioes;
        }
    }
}