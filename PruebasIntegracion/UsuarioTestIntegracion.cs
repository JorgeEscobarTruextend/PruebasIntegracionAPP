using System;
using System.Configuration;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebasIntegracion
{
    [TestClass]
    public class UsuarioTestIntegracion
    {
        [TestInitialize]
        public void Setup()
        {
            var ruta = ConfigurationManager.AppSettings["rutaData"];

            var usuarios = Directory.GetFiles(ruta + @"\usuarios\");

            foreach (var usuario in usuarios)
            {
                File.Delete(usuario);
            }

        }
        [TestMethod]
        public void TestGrabarUsuario()
        {
            var usuarioGuid = PruebasClases.Business.Usuario.CrearUsuario(new PruebasClases.Models.Usuario()
            {
                NombreUsuario = "prueba@prueba.com",
                Password = "Clave#Segura.8762"
            });

            var usuario = PruebasClases.Business.Usuario.ObtenerUsuario(usuarioGuid);
            Assert.IsNotNull(usuario);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestErrorGrabarUsuarioDuplicado()
        {
            PruebasClases.Business.Usuario.CrearUsuario(new PruebasClases.Models.Usuario()
            {
                NombreUsuario = "prueba2@prueba.com",
                Password = "Clave#Segura.8762"
            });
            PruebasClases.Business.Usuario.CrearUsuario(new PruebasClases.Models.Usuario()
            {
                NombreUsuario = "prueba2@prueba.com",
                Password = "Clave#Segura.8762"
            });
        }
    }
}
