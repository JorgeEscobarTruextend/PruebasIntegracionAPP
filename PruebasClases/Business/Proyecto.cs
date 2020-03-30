using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebasClases.Business
{
    public static class Proyecto
    {
        public static Guid CrearProyecto(Models.Proyecto proyecto)
        {
            Guid nuevoGuid = Guid.NewGuid();
            Helpers.Proyecto.ValidarNombre(proyecto.NombreProyecto);
            Helpers.Proyecto.NombreProyectoExiste(proyecto.NombreProyecto, proyecto.UsuarioPropietario);
            proyecto.Guid = nuevoGuid;
            
            var ruta = ConfigurationManager.AppSettings["rutaData"];

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(ruta + @"\Proyectos\" + nuevoGuid.ToString() + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, proyecto);
            }

            return proyecto.Guid;
        }

    }
}
