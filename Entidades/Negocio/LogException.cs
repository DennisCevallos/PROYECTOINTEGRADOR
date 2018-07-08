using System;
using System.Collections.Generic;

namespace Entidades.Negocio
{
    public partial class LogException
    {
        public int IdLog { get; set; }
        public DateTime? FechaLog { get; set; }
        public string Metodo { get; set; }
        public string Mensaje { get; set; }
        public string Trace { get; set; }
    }
}
