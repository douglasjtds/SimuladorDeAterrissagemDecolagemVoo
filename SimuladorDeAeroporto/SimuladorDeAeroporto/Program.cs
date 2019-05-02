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
            while (true)
            {
                Console.WriteLine("----- [Simulador de Aeroporto] -----");
                Thread.Sleep(1000);

                //var filaParaPousar = new FilaParaPousar();
                //Console.WriteLine(filaParaPousar.SetarGasolinaAviao()); //TESTE

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
            }
        }
    }
}