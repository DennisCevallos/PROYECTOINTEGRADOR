using System;
using System.Collections.Generic;

namespace Entidades.Negocio
{
    public partial class Modelo
    {
        public Modelo()
        {
            Vehiculo = new HashSet<Vehiculo>();
        }

        public int IdModelo { get; set; }
        public string Descripcion { get; set; }
        public bool? Estado { get; set; }

        public ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
