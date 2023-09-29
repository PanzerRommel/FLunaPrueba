using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Direccion
    {
        public int IdDireccion { get; set; }

        [Required]

        public string Calle { get; set; }
        [Required]
        [DisplayName("Numero Interior:")]
        public string NumeroInterior { get; set; }
        [Required]
        [DisplayName("Numero Exterior:")]
        public string NumeroExterior { get; set; }

        public ML.Colonia Colonia { get; set; }
        public ML.Usuario Usuario { get; set; }
        public List<object> Direcciones { get; set; }
    }
}