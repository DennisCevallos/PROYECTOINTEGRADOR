using System;
using System.Collections.Generic;

namespace Entidades.Negocio
{
    public partial class Vehiculo
    {
        public Vehiculo()
        {
            Seguro = new HashSet<Seguro>();
            Siniestro = new HashSet<Siniestro>();
        }

        public int IdVehiculo { get; set; }
        public int IdMarca { get; set; }
        public int IdModelo { get; set; }
        public string Placa { get; set; }
        public string Chasis { get; set; }
        public int IdColor { get; set; }
        public string Observaciones { get; set; }
        public int IdTipoVehiculo { get; set; }
        public bool? Estado { get; set; }
        public string AnioDeFabricacion { get; set; }
        public string Url { get; set; }

        public Color IdColorNavigation { get; set; }
        public Marca IdMarcaNavigation { get; set; }
        public Modelo IdModeloNavigation { get; set; }
        public TipoVehiculo IdTipoVehiculoNavigation { get; set; }
        public ICollection<Seguro> Seguro { get; set; }
        public ICollection<Siniestro> Siniestro { get; set; }
    }
}
