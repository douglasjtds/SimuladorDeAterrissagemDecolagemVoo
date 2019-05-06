using SimuladorDeAeroporto.Classes_De_Negócio;
using SimuladorDeAeroporto.Domain;
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
        private static int numeroMaxIteracao = 10000;
        private static int idMaximoAterrissagem = 1;
        private static int idMaximoDecolagem = 2;

        static void Main(string[] args)
        {
            Console.WriteLine("----- [Simulador de Aeroporto] -----");
            Thread.Sleep(1000);
            #region [instanciando as filas]


            var pista1 = new Pista
            {
                filas = new List<Fila>
                {
                    new Fila { tipo = FilaEnum.Aterrissar },
                    new Fila { tipo = FilaEnum.Aterrissar  },
                    new Fila { tipo = FilaEnum.Decolar  }
                }
            };
            var pista2 = new Pista
            {
                filas = new List<Fila>
                {
                    new Fila { tipo = FilaEnum.Aterrissar },
                    new Fila { tipo = FilaEnum.Aterrissar  },
                    new Fila { tipo = FilaEnum.Decolar  }
                }
            };
            var pista3 = new Pista
            {
                filas = new List<Fila>
                {
                    new Fila { tipo = FilaEnum.Aterrissar  },
                    new Fila { tipo = FilaEnum.Decolar  }
                }
            };

            #endregion

            Console.WriteLine("Inicializando aeroporto...");
            Thread.Sleep(100);

            int iteracao = 1;

            while (iteracao <= numeroMaxIteracao)
            {
                InsereAviao(pista1);

                iteracao++;
            }
        }

        private static void InsereAviao(Pista pista)
        {
            var listaAvioes = new List<Aviao>();
            listaAvioes.AddRange(GeraAvioes(FilaEnum.Decolar));
            listaAvioes.AddRange(GeraAvioes(FilaEnum.Aterrissar));

            var filaDecolar = pista.filas.Where(p => p.tipo == FilaEnum.Decolar).First();
            var filaAterrissar = pista.filas.Where(p => p.tipo == FilaEnum.Aterrissar);

            foreach (var aviao in listaAvioes)
            {
                if (aviao.IsAviaoDecolando)
                    filaDecolar.Enfileira(aviao);
                else
                {
     
                }
            }
        }

        private static List<Aviao> GeraAvioes(FilaEnum tipo)
        {
            var lista = new List<Aviao>();
            int idAviao = tipo == FilaEnum.Aterrissar ? idMaximoAterrissagem : idMaximoDecolagem;
            Random rnd = new Random();
            int quantidadeAvioes = rnd.Next(4);

            for (int i = 0; i < quantidadeAvioes; i++)
            {
                Aviao aviao = new Aviao();
                aviao.Id_Aviao = idAviao;
                idAviao += 2;
                if (!aviao.IsAviaoDecolando)
                    aviao.NivelGasolina = null;
                else
                    aviao.GeraValorAleatorioParaGasolina(); //gera quantidade de gasolina

                lista.Add(aviao);
            }

            if (tipo == FilaEnum.Aterrissar)
                idMaximoAterrissagem = idAviao;
            else
                idMaximoDecolagem = idAviao;

            return lista;
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