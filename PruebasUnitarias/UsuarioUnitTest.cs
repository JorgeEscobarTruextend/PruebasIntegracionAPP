using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebasUnitarias
{
    [TestClass]
    public class UsuarioUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestCorreoVacio()
        {
            PruebasClases.Helpers.Usuario.ValidarCorreo("");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestCorreoIncorrecto()
        {
            PruebasClases.Helpers.Usuario.ValidarCorreo("lalala");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestCorreoIncorrectoSinArroba()
        {
            PruebasClases.Helpers.Usuario.ValidarCorreo("lalalalalala.com");
        }

        [TestMethod]
        public void TestCorreoValido()
        {
            var resultado = PruebasClases.Helpers.Usuario.ValidarCorreo("lalala@lalala.com");
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void TestPasswordVacia()
        {
            var resultado = PruebasClases.Helpers.Usuario.ValidarPassword("");
            Assert.AreEqual(PruebasClases.Helpers.PasswordScore.Blank, resultado);
        }

        [TestMethod]
        public void TestPasswordMuyDebil()
        {
            var resultado = PruebasClases.Helpers.Usuario.ValidarPassword("123");
            Assert.AreEqual(PruebasClases.Helpers.PasswordScore.VeryWeak, resultado);
        }

        [TestMethod]
        public void TestPasswordCorrecta()
        {
            var resultado = PruebasClases.Helpers.Usuario.ValidarPassword("123.!prueba#123");
            Assert.AreEqual(PruebasClases.Helpers.PasswordScore.Weak, resultado);
        }
    }
}
