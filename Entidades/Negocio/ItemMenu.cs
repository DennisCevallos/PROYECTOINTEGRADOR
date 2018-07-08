using System;
using System.Collections.Generic;

namespace Entidades.Negocio
{
    public partial class ItemMenu
    {
        public int IdSubMenu { get; set; }
        public int IdPerfil { get; set; }
        public int IdMenu { get; set; }
        public bool? Estado { get; set; }

        public Menu IdMenuNavigation { get; set; }
        public Perfil IdPerfilNavigation { get; set; }
    }
}
