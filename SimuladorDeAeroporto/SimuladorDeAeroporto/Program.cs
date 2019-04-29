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
        static void Main(string[] args)
        {
            Console.WriteLine("----- [Simulador de Aeroporto] -----");
            Thread.Sleep(1000); 

            var filaParaPousar = new FilaParaPousar();
            Console.WriteLine(filaParaPousar.SetarGasolinaAviao());
        } 
    }
}