using System;
using System.Configuration;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebasIntegracion
{
    [TestClass]
    public class ProyectoTestIntegracion
    {
        [TestInitialize]
        public void Setup()
        {
            var ruta = ConfigurationManager.AppSettings["rutaData"];

            var proyectos = Directory.GetFiles(ruta + @"\Proyectos\");

            foreach (var proyecto in proyectos)
            {
                File.Delete(proyecto);
            }

            var usuarios = Directory.GetFiles(ruta + @"\Usuarios\");

            foreach (var usuario in usuarios)
            {
                File.Delete(usuario);
            }
        }
        [TestMethod]
        public void TestGrabarProyecto()
        {
            var usuarioGuid = PruebasClases.Business.Usuario.CrearUsuario(new PruebasClases.Models.Usuario()
            {
                NombreUsuario = "prueba@prueba.com",
                Password = "Clave#Segura.8762"
            });

            PruebasClases.Business.Proyecto.CrearProyecto(new PruebasClases.Models.Proyecto()
            {
                NombreProyecto = "Proyecto 1",
                UsuarioPropietario = usuarioGuid
            });
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestErrorGrabarProyectoDuplicado()
        {
            var usuarioGuid = PruebasClases.Business.Usuario.CrearUsuario(new PruebasClases.Models.Usuario()
            {
                NombreUsuario = "prueba@prueba.com",
                Password = "Clave#Segura.8762"
            });

            PruebasClases.Business.Proyecto.CrearProyecto(new PruebasClases.Models.Proyecto()
            {
                NombreProyecto = "Proyecto 2",
                UsuarioPropietario = usuarioGuid
            });
            PruebasClases.Business.Proyecto.CrearProyecto(new PruebasClases.Models.Proyecto()
            {
                NombreProyecto = "Proyecto 2",
                UsuarioPropietario = usuarioGuid
            });
        }

        [TestMethod]
        public void TestErrorGrabarProyectoDuplicadoDiferentesUsuarios()
        {
            var usuarioGuid = PruebasClases.Business.Usuario.CrearUsuario(new PruebasClases.Models.Usuario()
            {
                NombreUsuario = "prueba@prueba.com",
                Password = "Clave#Segura.8762"
            });

            var usuario2Guid = PruebasClases.Business.Usuario.CrearUsuario(new PruebasClases.Models.Usuario()
            {
                NombreUsuario = "prueba2@prueba.com",
                Password = "Clave#Segura.8762"
            });

           var guidProyecto1 = PruebasClases.Business.Proyecto.CrearProyecto(new PruebasClases.Models.Proyecto()
            {
                NombreProyecto = "Proyecto 2",
                UsuarioPropietario = usuarioGuid
            });

            var guidProyecto2 = PruebasClases.Business.Proyecto.CrearProyecto(new PruebasClases.Models.Proyecto()
            {
                NombreProyecto = "Proyecto 2",
                UsuarioPropietario = usuario2Guid
            });
            Assert.AreNotEqual(guidProyecto1, guidProyecto2);
        }
    }
}
