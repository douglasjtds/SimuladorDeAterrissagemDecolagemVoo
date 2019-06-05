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
        #region [variáveis globais]
        private static int numeroMaxIteracao = 100000;
        private static int idMaximoAterrissagem = 1;
        private static int idMaximoDecolagem = 2;
        private static bool pousoEmergencialPista3NaInteracao = false;
        private static bool aviaoDecolouPista1 = false;
        private static bool aviaoDecolouPista2 = false;
        private static List<Tuple<int, int, int?>> avioesCaidos = new List<Tuple<int, int, int?>>();
        private static List<Tuple<int, int, int?>> avioesDecolados = new List<Tuple<int, int, int?>>();
        private static List<Tuple<int, int, int?>> avioesPousados = new List<Tuple<int, int, int?>>();

        private const int idPista1 = 1;
        private const int idPista2 = 2;
        private const int idPista3 = 3;

        private static int iteracaoAtual = 1;
        #endregion

        static void Main(string[] args)
        {
            Console.WriteLine("----- [Simulador de Aeroporto] -----");
            Thread.Sleep(1000);


            #region [instanciando as pistas]
            var pista1 = new Pista(idPista1);
            var pista2 = new Pista(idPista2);
            var pista3 = new Pista(idPista3);
            #endregion

            Console.WriteLine("Inicializando aeroporto...");
            Thread.Sleep(500);

            while (iteracaoAtual <= numeroMaxIteracao)
            {
                pousoEmergencialPista3NaInteracao = false;

                InsereAviao(pista1);
                InsereAviao(pista2);
                InsereAviao(pista3, false);

                ProcessarIteracao(pista1);
                ProcessarIteracao(pista2);
                ProcessarIteracao(pista3);

                BaixarNivelGasolina(pista1);
                BaixarNivelGasolina(pista2);
                BaixarNivelGasolina(pista3);

                iteracaoAtual++;
                ImprimirPeriodicamente();
            }
        }

        private static void BaixarNivelGasolina(Pista pista)
        {
            foreach (var aviao in pista.Pousar1)
            {
                aviao.NivelGasolina -= 1;
            }

            foreach (var aviao in pista.Pousar2)
            {
                aviao.NivelGasolina -= 1;
            }
        }

        private static void ProcessarIteracao(Pista pista)
        {
            VerificarAvioesCaidos(pista);
            RealizarPousoDeEmergenciaPista3(pista);
            var realizouPousoEmergencialPistaAtual = RealizarPousoEmergencialPistaAtual(pista);

            switch (pista.Id_Pista)
            {
                case idPista1:
                    if (!realizouPousoEmergencialPistaAtual)
                    {
                        if (!aviaoDecolouPista1)
                        {
                            RemoverAviaoFila(pista.Decolar, pista.Id_Pista, FilaEnum.Decolar);
                            aviaoDecolouPista1 = true;
                        }
                        else
                        {
                            if (pista.Pousar1.Count > pista.Pousar2.Count)
                                RemoverAviaoFila(pista.Pousar1, pista.Id_Pista, FilaEnum.Pousar);
                            else
                                RemoverAviaoFila(pista.Pousar2, pista.Id_Pista, FilaEnum.Pousar);

                            aviaoDecolouPista1 = false;
                        }
                    }
                    break;
                case idPista2:
                    if (!realizouPousoEmergencialPistaAtual)
                    {
                        if (!aviaoDecolouPista2)
                        {
                            RemoverAviaoFila(pista.Decolar, pista.Id_Pista, FilaEnum.Decolar);
                            aviaoDecolouPista2 = true;
                        }
                        else
                        {
                            if (pista.Pousar1.Count > pista.Pousar2.Count)
                                RemoverAviaoFila(pista.Pousar1, pista.Id_Pista, FilaEnum.Pousar);
                            else
                                RemoverAviaoFila(pista.Pousar2, pista.Id_Pista, FilaEnum.Pousar);

                            aviaoDecolouPista2 = false;
                        }
                    }
                    break;
                case idPista3:
                    if (!pousoEmergencialPista3NaInteracao)
                        RemoverAviaoFila(pista.Decolar, pista.Id_Pista, FilaEnum.Decolar);
                    break;
                default:
                    break;
            }
        }

        private static bool RealizarPousoEmergencialPistaAtual(Pista pista)
        {
            if (pousoEmergencialPista3NaInteracao)
            {
                var aviaoSemGasolina = new Aviao();

                aviaoSemGasolina = pista.Pousar1.FirstOrDefault(p => p.NivelGasolina == 1);

                if (aviaoSemGasolina != null)
                {
                    RemoverAviaoFila(pista.Pousar1, pista.Id_Pista, FilaEnum.Pousar, aviaoSemGasolina);
                    return true;
                }
                else
                {
                    aviaoSemGasolina = pista.Pousar2.FirstOrDefault(p => p.NivelGasolina == 1);

                    if (aviaoSemGasolina != null)
                    {
                        RemoverAviaoFila(pista.Pousar2, pista.Id_Pista, FilaEnum.Pousar, aviaoSemGasolina);
                        return true;
                    }
                    else
                        return false;
                }
            }
            else
                return false;
        }

        private static void RealizarPousoDeEmergenciaPista3(Pista pista)
        {
            var aviaoSemGasolina = new Aviao();

            aviaoSemGasolina = pista.Pousar1.FirstOrDefault(p => p.NivelGasolina.HasValue && p.NivelGasolina.Value == 1);

            if (!pousoEmergencialPista3NaInteracao && aviaoSemGasolina != null)
            {
                RemoverAviaoFila(pista.Pousar1, idPista3, FilaEnum.Pousar, aviaoSemGasolina);
                pousoEmergencialPista3NaInteracao = true;
            }
            else if (!pousoEmergencialPista3NaInteracao && aviaoSemGasolina == null)
            {
                aviaoSemGasolina = pista.Pousar2.FirstOrDefault(p => p.NivelGasolina.HasValue && p.NivelGasolina.Value == 1);

                if (aviaoSemGasolina != null)
                {
                    RemoverAviaoFila(pista.Pousar2, idPista3, FilaEnum.Pousar, aviaoSemGasolina);
                    pousoEmergencialPista3NaInteracao = true;
                }
            }
        }

        private static void VerificarAvioesCaidos(Pista pista)
        {
            var _avioesCaidosAterrissar1 = new List<Aviao>();
            _avioesCaidosAterrissar1 = pista.Pousar1.Where(p => p.NivelGasolina.HasValue && p.NivelGasolina.Value < 1).ToList();

            var _avioesCaidosAterrissar2 = new List<Aviao>();
            _avioesCaidosAterrissar2 = pista.Pousar2.Where(p => p.NivelGasolina.HasValue && p.NivelGasolina.Value < 1).ToList();

            foreach (var aviao in _avioesCaidosAterrissar1)
            {
                avioesCaidos.Add(new Tuple<int, int, int?>(aviao.Id_Aviao, pista.Id_Pista, aviao.NivelGasolina));
                RemoverAviaoCaidoFila(pista.Pousar1, aviao);
            }

            foreach (var aviao in _avioesCaidosAterrissar2)
            {
                avioesCaidos.Add(new Tuple<int, int, int?>(aviao.Id_Aviao, pista.Id_Pista, aviao.NivelGasolina));
                RemoverAviaoCaidoFila(pista.Pousar2, aviao);
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

        private static void RemoverAviaoFila(Queue<Aviao> fila, int idPista, FilaEnum tipoFila, Aviao aviao = null)
        {
            if (fila.Any())
            {
                var aviaoAux = aviao != null ? aviao : fila.First();

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

                RemoverAviaoFila(idPista, tipoFila, aviaoAux);
            }
        }

        private static void RemoverAviaoFila(int idPista, FilaEnum tipoFila, Aviao aviaoAux)
        {
            switch (tipoFila)
            {
                case FilaEnum.Decolar:
                    avioesDecolados.Add(new Tuple<int, int, int?>(aviaoAux.Id_Aviao, idPista, aviaoAux.NivelGasolina));
                    break;
                case FilaEnum.Pousar:
                    avioesPousados.Add(new Tuple<int, int, int?>(aviaoAux.Id_Aviao, idPista, aviaoAux.NivelGasolina));
                    break;
                default:
                    break;
            }
        }

        private static void RemoverAviaoCaidoFila(Queue<Aviao> fila, Aviao aviao)
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

        private static List<Aviao> GeraAvioes(FilaEnum tipo)
        {
            var lista = new List<Aviao>();
            int idAviao = tipo == FilaEnum.Pousar ? idMaximoAterrissagem : idMaximoDecolagem;
            Random rnd = new Random();
            int quantidadeAvioes = rnd.Next(4);
            Aviao aviao;
            for (int i = 0; i < quantidadeAvioes; i++)
            {

                if (tipo == FilaEnum.Pousar)
                {
                    aviao = new Aviao(true)
                    {
                        Id_Aviao = idAviao
                    };

                    idMaximoAterrissagem = idAviao;
                }
                else
                {
                    aviao = new Aviao
                    {
                        Id_Aviao = idAviao
                    };

                    idMaximoDecolagem = idAviao;
                }

                lista.Add(aviao);
                idAviao += 2;
            }

            return lista;
        }

        /// <summary>
        /// Método para imprimir informações a cada iteração.
        /// a) o conteúdo de cada fila;
        /// b) o tempo médio de espera para decolagem;
        /// c) o tempo médio de espera para aterrissagem; e
        /// d) o número de aviões que aterrissaram sem reserva de combustível.
        /// </summary>
        private static void ImprimirPeriodicamente()
        {
            var logAvioesDecolados = avioesDecolados.GroupBy(p => p.Item2).Select(p => new { pista = p.Key, quantidade = p.Count() });
            var logAvioesPousados = avioesPousados.GroupBy(p => p.Item2).Select(p => new { pista = p.Key, quantidade = p.Count() });
            var logAvioesCaidos = avioesCaidos.GroupBy(p => p.Item2).Select(p => new { pista = p.Key, quantidade = p.Count() });

            var avioesDecoladosPista1 = logAvioesDecolados.FirstOrDefault(p => p.pista == idPista1)?.quantidade ?? 0;
            var avioesDecoladosPista2 = logAvioesDecolados.FirstOrDefault(p => p.pista == idPista2)?.quantidade ?? 0;
            var avioesDecoladosPista3 = logAvioesDecolados.FirstOrDefault(p => p.pista == idPista3)?.quantidade ?? 0;

            var avioesPousadosPista1 = logAvioesPousados.FirstOrDefault(p => p.pista == idPista1)?.quantidade ?? 0;
            var avioesPousadosPista2 = logAvioesPousados.FirstOrDefault(p => p.pista == idPista2)?.quantidade ?? 0;
            var avioesPousadosPista3 = logAvioesPousados.FirstOrDefault(p => p.pista == idPista3)?.quantidade ?? 0;

            var mediaIntercaoDecolagemPista1 = avioesDecoladosPista1 > 0 ? avioesDecoladosPista1 / iteracaoAtual : 0;
            var mediaIntercaoDecolagemPista2 = Math.Round(avioesDecoladosPista2 > 0 ? avioesDecoladosPista2 / (iteracaoAtual * 1.0) : 0, 2);
            var mediaIntercaoDecolagemPista3 = Math.Round(avioesDecoladosPista3 > 0 ? avioesDecoladosPista3 / (iteracaoAtual * 1.0) : 0, 2);

            var mediaIntercaoPousadosPista1 = Math.Round(avioesPousadosPista1 > 0 ? avioesPousadosPista1 / (iteracaoAtual * 1.0) : 0, 2);
            var mediaIntercaoPousadosPista2 = Math.Round(avioesPousadosPista2 > 0 ? avioesPousadosPista2 / (iteracaoAtual * 1.0) : 0, 2);
            var mediaIntercaoPousadosPista3 = Math.Round(avioesPousadosPista3 > 0 ? avioesPousadosPista3 / (iteracaoAtual * 1.0) : 0, 2);

            Console.WriteLine("\n");
            Console.WriteLine("Decolagem pista 1: {0} aviões", avioesDecoladosPista1);
            Console.WriteLine("Decolagem pista 2: {0} aviões", avioesDecoladosPista2);
            Console.WriteLine("Decolagem pista 3: {0} aviões", avioesDecoladosPista3);
            Console.WriteLine("\n");
            Console.WriteLine("Aterrissagem pista 1: {0} aviões", avioesPousadosPista1);
            Console.WriteLine("Aterrissagem pista 2: {0} aviões", avioesPousadosPista2);
            Console.WriteLine("\n");
            Console.WriteLine("Aviões que aterrissaram sem reserva de combustível na pista 3: {0} aviões", avioesPousadosPista3);
            Console.WriteLine("Aviões caídos: {0} aviões", avioesCaidos.Count());
            Console.WriteLine("\n");
            Console.WriteLine("O tempo médio de espera para decolagem pista 1: {0} unidades de tempo", mediaIntercaoDecolagemPista1);
            Console.WriteLine("O tempo médio de espera para decolagem pista 2: {0} unidades de tempo", mediaIntercaoDecolagemPista2);
            Console.WriteLine("O tempo médio de espera para decolagem pista 3: {0} unidades de tempo", mediaIntercaoDecolagemPista3);
            Console.WriteLine("\n");
            Console.WriteLine("O tempo médio de espera para aterrissagem pista 1: {0} unidades de tempo", mediaIntercaoPousadosPista1);
            Console.WriteLine("O tempo médio de espera para aterrissagem pista 2: {0} unidades de tempo", mediaIntercaoPousadosPista2);
            Console.WriteLine("O tempo médio de espera para aterrissagem pista 3: {0} unidades de tempo", mediaIntercaoPousadosPista3);
            Console.WriteLine("\n");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
        }
    }
}