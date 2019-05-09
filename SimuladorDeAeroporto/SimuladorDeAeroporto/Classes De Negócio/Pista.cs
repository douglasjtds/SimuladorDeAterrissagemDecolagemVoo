using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorDeAeroporto.Classes_De_Negócio
{
    public class Pista
    {
        public Pista()
        {
            Decolar = new Queue<Aviao>();
            Aterrissar1 = new Queue<Aviao>();
            Aterrissar2 = new Queue<Aviao>();
        }

        public Queue<Aviao> Decolar { get; set; }
        public Queue<Aviao> Aterrissar1 { get; set; }
        public Queue<Aviao> Aterrissar2 { get; set; }
    }
}
