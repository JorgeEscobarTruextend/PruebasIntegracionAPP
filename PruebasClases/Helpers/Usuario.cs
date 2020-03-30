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
    public enum PasswordScore
    {
        Blank = 0,
        VeryWeak = 1,
        Weak = 2,
        Medium = 3,
        Strong = 4,
        VeryStrong = 5
    }

    public static class Usuario
    {
        public static bool ValidarCorreo(string correo)
        {

            if (string.IsNullOrEmpty(correo))
                throw new FormatException("Se debe indicar un correo");

            new MailAddress(correo);

            return true;

        }

        public static PasswordScore ValidarPassword(string password)
        {
            int score = 0;

            if (password.Length < 1)
                return PasswordScore.Blank;
            if (password.Length < 4)
                return PasswordScore.VeryWeak;

            if (password.Length >= 8)
                score++;
            if (password.Length >= 12)
                score++;
            if (Regex.Match(password, @"/\d+/", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(password, @"/[a-z]/", RegexOptions.ECMAScript).Success &&
              Regex.Match(password, @"/[A-Z]/", RegexOptions.ECMAScript).Success)
                score++;
            if (Regex.Match(password, @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/", RegexOptions.ECMAScript).Success)
                score++;

            return (PasswordScore)score;
        }

        public static bool NombreUsuarioExiste(string nombreUsuario)
        {
            var lista = ObtieneUsuarios();
            return (lista.Where(x => x.NombreUsuario == nombreUsuario).FirstOrDefault() != null ? throw new ArgumentException("Nombre de usuario ya existe") : true);
        }

        public static List<Models.Usuario> ObtieneUsuarios()
        {
            List<Models.Usuario> resultado = new List<Models.Usuario>();
            var ruta = ConfigurationManager.AppSettings["rutaData"];

            var usuarios = Directory.GetFiles(ruta + @"\usuarios\");

            foreach (var archivoUsuario in usuarios)
            {
                resultado.Add(JsonConvert.DeserializeObject<Models.Usuario>(File.ReadAllText(archivoUsuario)));
            }

            return resultado;
        }
    }
}
