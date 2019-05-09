using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorDeAeroporto.Classes_De_Negócio
{
    public class Pista
    {

        public Fila Decolar { get; set; }
        public Fila Aterrissar1 { get; set; }
        public Fila Aterrissar2 { get; set; }
    }
}
