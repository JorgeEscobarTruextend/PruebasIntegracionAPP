using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebasClases.Models
{
    public class Usuario
    {
        public Guid Guid { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }

    }
}
