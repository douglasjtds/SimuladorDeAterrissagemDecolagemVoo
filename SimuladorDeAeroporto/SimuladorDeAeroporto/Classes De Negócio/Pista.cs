using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorDeAeroporto.Classes_De_Negócio
{
    public class Pista
    {
        public int Id_Pista;
        public Pista(int id)
        {
            this.Id_Pista = id;
            Decolar = new Queue<Aviao>();
            Pousar1 = new Queue<Aviao>();
            Pousar2 = new Queue<Aviao>();
        }

        public Queue<Aviao> Decolar { get; set; }
        public Queue<Aviao> Pousar1 { get; set; }
        public Queue<Aviao> Pousar2 { get; set; }

    }
}