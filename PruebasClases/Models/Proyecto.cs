using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebasClases.Models
{
    public class Proyecto
    {
        public Guid Guid { get; set; }
        public string NombreProyecto { get; set; }
        public Guid UsuarioPropietario { get; set; }
    }
}
