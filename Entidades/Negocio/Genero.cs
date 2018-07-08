using System;
using System.Collections.Generic;

namespace Entidades.Negocio
{
    public partial class Genero
    {
        public Genero()
        {
            Persona = new HashSet<Persona>();
        }

        public int IdGenero { get; set; }
        public string Descripcion { get; set; }
        public bool? Estado { get; set; }

        public ICollection<Persona> Persona { get; set; }
    }
}
