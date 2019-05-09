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
        private static int avioesDecoladosPista1 = 0;
        private static int avioesDecoladosPista2 = 0;
        private static int avioesDecoladosPista3 = 0;
        private static int avioesPousadosPista1 = 0;
        private static int avioesPousadosPista2 = 0;
        private static int avioesPousadosPista3 = 0;
        private static int avioesCaidosPista1 = 0;
        private static int avioesCaidosPista2 = 0;
        private static int avioesCaidosPista3 = 0;
        private static bool pousoEmergencialNaInteracao = false;

        static void Main(string[] args)
        {
            Console.WriteLine("----- [Simulador de Aeroporto] -----");
            Thread.Sleep(1000);
            #region [instanciando as filas]
            #endregion

            Console.WriteLine("Inicializando aeroporto...");
            Thread.Sleep(100);

            int iteracao = 1;
            var pista1 = new Pista();
            var pista2 = new Pista();
            var pista3 = new Pista();

            while (iteracao <= numeroMaxIteracao)
            {
                pousoEmergencialNaInteracao = false;

                InsereAviao(pista1);
                InsereAviao(pista2);
                InsereAviao(pista3, false);

                PousarDecolarAvioes(pista1, PistaEnum.Pista1);
                PousarDecolarAvioes(pista2, PistaEnum.Pista2);
                PousarDecolarAvioes(pista3, PistaEnum.Pista3);

                iteracao++;
            }
        }

        private static void PousarDecolarAvioes(Pista pista, PistaEnum identificaoPista)
        {
            VarificarAvioesCaidos(pista, identificaoPista);
            RealizarPousoUrgencia(pista);
        }

        private static void RealizarPousoUrgencia(Pista pista)
        {
            var pousoUrgencia = pista.Aterrissar1.FirstOrDefault(p => p.NivelGasolina.Value == 1);

            if (!pousoEmergencialNaInteracao && pousoUrgencia != null)
            {
                RemoverAviaoFila(pista.Aterrissar1, pousoUrgencia);
                avioesPousadosPista3 += 1;
                pousoEmergencialNaInteracao = true;
            }
            else if (!pousoEmergencialNaInteracao && pousoUrgencia == null)
            {
                pousoUrgencia = pista.Aterrissar2.FirstOrDefault(p => p.NivelGasolina.Value == 1);
                if (pousoUrgencia != null)
                {
                    RemoverAviaoFila(pista.Aterrissar2, pousoUrgencia);
                    avioesPousadosPista3 += 1;
                    pousoEmergencialNaInteracao = true;
                }
            }
        }

        private static void VarificarAvioesCaidos(Pista pista, PistaEnum identificaoPista)
        {
            var _avioesCaidosAterrissar1 = new List<Aviao>();
            _avioesCaidosAterrissar1 = pista.Aterrissar1.Where(p => p.NivelGasolina.Value == 0).ToList();

            var _avioesCaidosAterrissar2 = new List<Aviao>();
            _avioesCaidosAterrissar2 = pista.Aterrissar2.Where(p => p.NivelGasolina.Value == 0).ToList();

            foreach (var aviao in _avioesCaidosAterrissar1)
            {
                RemoverAviaoFila(pista.Aterrissar1, aviao);
            }

            foreach (var aviao in _avioesCaidosAterrissar2)
            {
                RemoverAviaoFila(pista.Aterrissar2, aviao);
            }

            switch (identificaoPista)
            {
                case PistaEnum.Pista1:
                    avioesCaidosPista1 += _avioesCaidosAterrissar1.Count;
                    avioesCaidosPista1 += _avioesCaidosAterrissar2.Count;
                    break;
                case PistaEnum.Pista2:
                    avioesCaidosPista2 += _avioesCaidosAterrissar1.Count;
                    avioesCaidosPista2 += _avioesCaidosAterrissar2.Count;
                    break;
                case PistaEnum.Pista3:
                    avioesCaidosPista3 += _avioesCaidosAterrissar1.Count;
                    avioesCaidosPista3 += _avioesCaidosAterrissar2.Count;
                    break;
                default:
                    break;
            }
        }

        private static void InsereAviao(Pista pista, bool gerarAvioesFilaDePouso = true)
        {

            foreach (var aviao in GeraAvioes(FilaEnum.Decolar))
            {
                pista.Decolar.Enqueue(aviao);
            }

            if (gerarAvioesFilaDePouso)
            {
                foreach (var aviao in GeraAvioes(FilaEnum.Pousar))
                {
                    if (pista.Aterrissar1.Count > pista.Aterrissar2.Count)
                    {
                        pista.Aterrissar2.Enqueue(aviao);
                    }
                    else
                    {
                        pista.Aterrissar1.Enqueue(aviao);
                    }
                }
            }
        }

        private static void RemoverAviaoFila(Queue<Aviao> fila, Aviao aviao = null)
        {
            if (aviao == null)
                fila.Dequeue();
            else
            {
                var queue_aux = new Queue<Aviao>();

                foreach (var item in fila)
                {
                    if (item.Id_Aviao != aviao.Id_Aviao)
                    {
                        queue_aux.Enqueue(aviao);
                    }
                }

                fila.Clear();
                fila = queue_aux;
            }
        }

        private static List<Aviao> GeraAvioes(FilaEnum tipo)
        {
            var lista = new List<Aviao>();
            int idAviao = tipo == FilaEnum.Pousar ? idMaximoAterrissagem : idMaximoDecolagem;
            Random rnd = new Random();
            int quantidadeAvioes = rnd.Next(4);

            for (int i = 0; i < quantidadeAvioes; i++)
            {
                Aviao aviao = new Aviao
                {
                    Id_Aviao = idAviao
                };

                idAviao += 2;
                lista.Add(aviao);
            }

            if (tipo == FilaEnum.Pousar)
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