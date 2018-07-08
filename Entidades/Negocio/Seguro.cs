using System;
using System.Collections.Generic;

namespace Entidades.Negocio
{
    public partial class Seguro
    {
        public int IdSeguro { get; set; }
        public int IdPoliza { get; set; }
        public int IdVehiculo { get; set; }
        public decimal? ValAsegurado { get; set; }
        public decimal? Tasa { get; set; }
        public decimal? PrimaSeguro { get; set; }

        public Poliza IdPolizaNavigation { get; set; }
        public Vehiculo IdVehiculoNavigation { get; set; }
    }
}
