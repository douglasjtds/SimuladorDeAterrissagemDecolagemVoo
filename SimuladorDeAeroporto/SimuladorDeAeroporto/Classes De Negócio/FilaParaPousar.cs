using SimuladorDeAeroporto.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorDeAeroporto.Classes_De_Negócio
{
    public class FilaParaPousar : IFila
    {
        public int ReferenteAPista;

        private class Celula
        {
            internal Object item;
            internal Celula prox;
        }
        private Celula frente;
        private Celula tras;

        #region [Métodos herdados da interface base]
        public void CriaFilaVazia()
        {
            frente = new Celula();
            tras = frente;
            frente.prox = null;
        }

        public void Enfileira(object x)
        {
            tras.prox = new Celula();
            tras = tras.prox;
            tras.item = x;
            tras.prox = null;
        }

        public Object Desenfileira()
        {
            Object item = null;
            if (this.IsFilaVazia())
                throw new Exception("Erro: A fila está vazia!");
            frente = frente.prox;
            item = frente.item;
            return item;
        }


        public bool IsFilaVazia()
        {
            return (frente == tras);
        }
        #endregion

        public int SetarGasolinaAviao() //TODO: Criar método que seta um inteiro aleatório de 1 a 20 para o nível de gasolina de uma instância de Aviao
        {
            //throw new NotImplementedException();
            Random random = new Random();
            int randomNumber = random.Next(1, 21);
            return randomNumber;
        }
    }
}