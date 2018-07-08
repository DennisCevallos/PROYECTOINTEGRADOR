using System;
using System.Collections.Generic;

namespace Entidades.Negocio
{
    public partial class Poliza
    {
        public Poliza()
        {
            Seguro = new HashSet<Seguro>();
        }

        public int IdPoliza { get; set; }
        public DateTime? FechaCoverturaI { get; set; }
        public DateTime? FechaCoverturaF { get; set; }
        public string NumPoliza { get; set; }
        public string Factura { get; set; }
        public decimal? TotValAsegurado { get; set; }
        public decimal? TotValPrima { get; set; }
        public int IdPersona { get; set; }

        public Persona IdPersonaNavigation { get; set; }
        public ICollection<Seguro> Seguro { get; set; }
    }
}
