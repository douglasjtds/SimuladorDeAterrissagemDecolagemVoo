using SimuladorDeAeroporto.Domain;
using SimuladorDeAeroporto.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorDeAeroporto.Classes_De_Negócio
{
    public class Fila : IFila
    {
        private Celula frente;
        private Celula tras;
        public FilaEnum tipo;

        public Fila()
        {
            frente = new Celula();
            tras = frente;
            frente.prox = null;
        }

        #region [Métodos herdados da interface base]

        public void Enfileira(object x)
        {
            this.tras.prox = new Celula();
            this.tras = this.tras.prox;
            this.tras.item = x;
            this.tras.prox = null;
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

        private class Celula
        {
            internal Object item;
            internal Celula prox;
        }
    }
}
