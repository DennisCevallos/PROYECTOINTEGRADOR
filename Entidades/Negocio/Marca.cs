using System;
using System.Collections.Generic;

namespace Entidades.Negocio
{
    public partial class Marca
    {
        public Marca()
        {
            Vehiculo = new HashSet<Vehiculo>();
        }

        public int IdMarca { get; set; }
        public string Descripcion { get; set; }
        public bool? Estado { get; set; }

        public ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
