using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorDeAeroporto.Interfaces
{
    public interface IFila
    {
        //void CriaFilaVazia();

        void Enfileira(Object x);

        Object Desenfileira();

        bool IsFilaVazia();
    }
}