using System;
using System.Collections.Generic;

namespace Entidades.Negocio
{
    public partial class Login
    {
        public int IdLogin { get; set; }
        public DateTime? FechaCambio { get; set; }
        public string Clave { get; set; }
        public string Usuario { get; set; }
        public bool? Estado { get; set; }
        public int IdPersona { get; set; }
        public int IdPerfil { get; set; }

        public Perfil IdPerfilNavigation { get; set; }
        public Persona IdPersonaNavigation { get; set; }
    }
}
