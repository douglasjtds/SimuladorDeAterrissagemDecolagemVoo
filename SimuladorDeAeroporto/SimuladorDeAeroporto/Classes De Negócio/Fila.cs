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
            tras = new Celula();
            frente = new Celula();
        }

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

        private class Celula
        {
            internal Object item;
            internal Celula prox;
        }
    }
}
