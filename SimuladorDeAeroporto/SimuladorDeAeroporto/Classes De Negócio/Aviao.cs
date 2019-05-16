using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorDeAeroporto.Classes_De_Negócio
{
    public class Aviao
    {
        public Aviao(bool possuiAviaoAterrissando)
        {
            NivelGasolina = GeraValorAleatorioParaGasolina();
            IsAviaoDecolando = true;

        }

        public Aviao()
        {
            IsAviaoDecolando = false;
        }

        public int Id_Aviao { get; set; }
        public bool IsAviaoDecolando { get; set; }
        public int? NivelGasolina { get; set; }


        private int GeraValorAleatorioParaGasolina()
        {
            Random rnd = new Random();
            int valorCombustivel = rnd.Next(21);
            return valorCombustivel;
        }
    }
}