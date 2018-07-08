using System;
using System.Collections.Generic;

namespace Entidades.Negocio
{
    public partial class Perfil
    {
        public Perfil()
        {
            ItemMenu = new HashSet<ItemMenu>();
            Login = new HashSet<Login>();
        }

        public int IdPerfil { get; set; }
        public string Descripcion { get; set; }
        public bool? Estado { get; set; }

        public ICollection<ItemMenu> ItemMenu { get; set; }
        public ICollection<Login> Login { get; set; }
    }
}
