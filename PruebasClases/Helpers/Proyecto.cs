using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PruebasClases.Helpers
{

    public static class Proyecto
    {
        public static bool ValidarNombre(string nombre)
        {

            if (string.IsNullOrEmpty(nombre))
                throw new FormatException("Se debe indicar un correo");


            return true;

        }

        public static bool NombreProyectoExiste(string nombreProyecto, Guid usuarioGuid)
        {
            var lista = ObtieneProyectos();
            return (lista.Where(x => x.NombreProyecto == nombreProyecto && x.UsuarioPropietario == usuarioGuid).FirstOrDefault() != null ? throw new ArgumentException("Nombre de Proyecto ya existe") : true);
        }

        public static List<Models.Proyecto> ObtieneProyectos()
        {
            List<Models.Proyecto> resultado = new List<Models.Proyecto>();
            var ruta = ConfigurationManager.AppSettings["rutaData"];

            var Proyectos = Directory.GetFiles(ruta + @"\Proyectos\");

            foreach (var archivoProyecto in Proyectos)
            {
                resultado.Add(JsonConvert.DeserializeObject<Models.Proyecto>(File.ReadAllText(archivoProyecto)));
            }

            return resultado;
        }
    }
}
