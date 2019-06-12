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

        /// <summary>
        /// Id_Aviao: números pares são aviões decolando, números ímpares são aviões pousando.
        /// </summary>
        public int Id_Aviao { get; set; }
        public bool IsAviaoDecolando { get; set; }
        public int? NivelGasolina { get; set; }


        private int GeraValorAleatorioParaGasolina()
        {
            Random rnd = new Random();
            int valorCombustivel = rnd.Next(1, 21); //deve gerar valor aleatório para a gasolina, podendo ser de 1 a 20
            return valorCombustivel;
        }
    }
}