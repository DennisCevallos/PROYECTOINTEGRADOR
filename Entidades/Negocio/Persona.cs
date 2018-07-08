using System;
using System.Collections.Generic;

namespace Entidades.Negocio
{
    public partial class Persona
    {
        public Persona()
        {
            Login = new HashSet<Login>();
            Poliza = new HashSet<Poliza>();
        }

        public int IdPersona { get; set; }
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public bool? Estado { get; set; }
        public int IdGenero { get; set; }

        public Genero IdGeneroNavigation { get; set; }
        public ICollection<Login> Login { get; set; }
        public ICollection<Poliza> Poliza { get; set; }
    }
}
