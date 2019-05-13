﻿using SimuladorDeAeroporto.Classes_De_Negócio;
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
        #region [variáveis globais]
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
        private static bool pousoEmergencialNaInteracao = false;
        private static bool aviaoDecolouPista1 = false;
        private static bool aviaoDecolouPista2 = false; 
        #endregion

        static void Main(string[] args)
        {
            Console.WriteLine("----- [Simulador de Aeroporto] -----");
            Thread.Sleep(1000);

            int iteracao = 1;

            #region [instanciando as pistas]
            var pista1 = new Pista();
            var pista2 = new Pista();
            var pista3 = new Pista();
            #endregion

            Console.WriteLine("Inicializando aeroporto...");
            Thread.Sleep(100);

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
            ProcessarIteracao(pista, identificaoPista);
            //ExibirLog();
        }

        private static void ProcessarIteracao(Pista pista, PistaEnum identificaoPista)
        {
            VerificarAvioesCaidos(pista, identificaoPista);
            RealizarPousoDeEmergencia(pista);
            var realizouPousoEmergencialPistaAtual = RealizarPousoEmergencialPistaAtual(pista); //não deveria chamar isso só se gasolina = 1?

            switch (identificaoPista)
            {
                case PistaEnum.Pista1:
                    if (!aviaoDecolouPista1)
                    {
                        if (realizouPousoEmergencialPistaAtual)
                            avioesPousadosPista1 += 1;
                        else
                        {
                            RemoverAviaoFila(pista.Decolar);
                            avioesDecoladosPista1 += 1;
                            aviaoDecolouPista1 = true;
                        }
                    }
                    else
                    {
                        if (pista.Pousar1.Count > pista.Pousar2.Count)
                        {
                            RemoverAviaoFila(pista.Pousar1);
                            avioesPousadosPista1 += 1;
                        }
                        else
                        {
                            RemoverAviaoFila(pista.Pousar2);
                            avioesPousadosPista1 += 1;
                        }

                        aviaoDecolouPista1 = false;
                    }
                    break;
                case PistaEnum.Pista2:
                    if (!aviaoDecolouPista2)
                    {
                        if (realizouPousoEmergencialPistaAtual)
                            avioesPousadosPista2 += 1;

                        else
                        {
                            RemoverAviaoFila(pista.Decolar);
                            avioesDecoladosPista2 += 1;
                            aviaoDecolouPista2 = true;
                        }
                    }
                    else
                    {
                        if (pista.Pousar1.Count > pista.Pousar2.Count)
                        {
                            RemoverAviaoFila(pista.Pousar1);
                            avioesPousadosPista2 += 1;
                        }
                        else
                        {
                            RemoverAviaoFila(pista.Pousar2);
                            avioesPousadosPista2 += 1;
                        }

                        aviaoDecolouPista2 = false;
                    }
                    break;
                case PistaEnum.Pista3:
                    if (!pousoEmergencialNaInteracao)
                    {
                        RemoverAviaoFila(pista.Decolar);
                        avioesDecoladosPista3 += 1;
                    }
                    break;
                default:
                    break;
            }
        }

        private static bool RealizarPousoEmergencialPistaAtual(Pista pista)
        {
            var pousoEmergencialPistaAtual = new Aviao();
            var isFila1 = true;

            pousoEmergencialPistaAtual = pista.Pousar1.FirstOrDefault(p => p.NivelGasolina == 1);
            if (pousoEmergencialPistaAtual == null)
            {
                pousoEmergencialPistaAtual = pista.Pousar2.FirstOrDefault(p => p.NivelGasolina == 1);
                isFila1 = false;
            }
            if (pousoEmergencialNaInteracao && pousoEmergencialPistaAtual != null)
            {
                if (isFila1)
                    RemoverAviaoFila(pista.Pousar1, pousoEmergencialPistaAtual);
                else
                    RemoverAviaoFila(pista.Pousar2, pousoEmergencialPistaAtual);

                return true;
            }
            else
                return false;

        }

        private static void RealizarPousoDeEmergencia(Pista pista)
        {
            var pousoDeEmergencia = pista.Pousar1.FirstOrDefault(p => p.NivelGasolina.Value == 1);

            if (!pousoEmergencialNaInteracao && pousoDeEmergencia != null)
            {
                RemoverAviaoFila(pista.Pousar1, pousoDeEmergencia);
                avioesPousadosPista3 += 1;
                pousoEmergencialNaInteracao = true;
            }
            else if (!pousoEmergencialNaInteracao && pousoDeEmergencia == null)
            {
                pousoDeEmergencia = pista.Pousar2.FirstOrDefault(p => p.NivelGasolina.Value == 1);
                if (pousoDeEmergencia != null)
                {
                    RemoverAviaoFila(pista.Pousar2, pousoDeEmergencia);
                    avioesPousadosPista3 += 1;
                    pousoEmergencialNaInteracao = true;
                }
            }
        }

        private static void VerificarAvioesCaidos(Pista pista, PistaEnum identificaoPista)
        {
            var _avioesCaidosAterrissar1 = new List<Aviao>();
            _avioesCaidosAterrissar1 = pista.Pousar1.Where(p => p.NivelGasolina.Value == 0).ToList();

            var _avioesCaidosAterrissar2 = new List<Aviao>();
            _avioesCaidosAterrissar2 = pista.Pousar2.Where(p => p.NivelGasolina.Value == 0).ToList();

            foreach (var aviao in _avioesCaidosAterrissar1)
            {
                RemoverAviaoFila(pista.Pousar1, aviao);
            }

            foreach (var aviao in _avioesCaidosAterrissar2)
            {
                RemoverAviaoFila(pista.Pousar2, aviao);
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
                    if (pista.Pousar1.Count > pista.Pousar2.Count)
                        pista.Pousar2.Enqueue(aviao);

                    else
                        pista.Pousar1.Enqueue(aviao);
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
                        queue_aux.Enqueue(aviao);
                }

                fila.Clear();
                fila = queue_aux;
            }
        }

        private static List<Aviao> GeraAvioes(FilaEnum tipo)
        {
            var lista = new List<Aviao>();
            int idAviao = tipo == FilaEnum.Pousar ? idMaximoAterrissagem : idMaximoDecolagem; //NÃO ENTENDI ESSA PARTE, o idAviao não tinha que começar de 1?
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