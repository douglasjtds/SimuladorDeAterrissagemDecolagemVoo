using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorDeAeroporto.Classes_De_Negócio
{
    public class Aviao
    {
        public int Id_Aviao;

        public int? NivelGasolina; //quantidade de unidades de tempo que pode permanecer no ar (de 1 a 20)

        public int ReferenteAPista;

        /// <summary>
        /// Referente ao Id_Aviao: números pares são aviões decolando, números ímpares são aviões pousando.
        /// </summary>
        /// <returns></returns>
        public bool IsAviaoDecolando {
            get {
                if (this.Id_Aviao % 2 == 0)
                {
                    NivelGasolina = null;
                    return true;
                }
                else
                {
                    NivelGasolina = GeraValorAleatorioParaGasolina();
                    return false;
                }
            }
            private set {; }
        }

        private int GeraValorAleatorioParaGasolina()
        {
            Random rnd = new Random();
            int valorCombustivel = rnd.Next(21);
            return valorCombustivel;
        }
    }
}