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
    public static class Usuario
    {
        public static Guid CrearUsuario(Models.Usuario usuario)
        {
            Guid nuevoGuid = Guid.NewGuid();
            Helpers.Usuario.ValidarCorreo(usuario.NombreUsuario);
            Helpers.Usuario.ValidarPassword(usuario.Password);
            Helpers.Usuario.NombreUsuarioExiste(usuario.NombreUsuario);
            usuario.Guid = nuevoGuid;
            
            var ruta = ConfigurationManager.AppSettings["rutaData"];

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(ruta + @"\usuarios\" + nuevoGuid.ToString() + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, usuario);
            }

            return usuario.Guid;
        }

        public static Models.Usuario ObtenerUsuario(Guid guid)
        {
            var ruta = ConfigurationManager.AppSettings["rutaData"];

            try
            {
                var usuario = JsonConvert.DeserializeObject<Models.Usuario>(File.ReadAllText(ruta + @"\usuarios\" + guid.ToString() + ".json"));

                return usuario;
            }
            catch (FileNotFoundException ex)
            {
                throw new ArgumentException("Usuario no existe");
            }
        }

    }
}
