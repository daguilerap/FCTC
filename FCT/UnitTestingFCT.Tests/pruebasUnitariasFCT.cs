using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

using FCT;

namespace UnitTestingFCT.Tests
{
    public class pruebasUnitariasFCT
    {
        [Fact]
        public void ValidadorCampos()
        {
            var fctValidator = new Validaciones();
            string dni = "12345678A";
            string email = "ejemplo1@email.com";
            string cp = "12345";
            string cif = "A12345678";
            string telefono = "123456789";
            string texto = "abc";
            string numeros = "123";

            bool isValidDni = fctValidator.validarDNI(dni);
            Assert.True(isValidDni);

            bool isValidEmail = fctValidator.validarEmail(email);
            Assert.True(isValidEmail);
            
            bool isValidCp = fctValidator.validarCP(cp);
            Assert.True(isValidCp);
            
            bool isValidCif = fctValidator.validarCIF(cif);
            Assert.True(isValidCif);
            
            bool isValidTelefono = fctValidator.validarTelefono(telefono);
            Assert.True(isValidTelefono);
            
            bool isValidTexto = fctValidator.validarCamposTextos(texto);
            Assert.True(isValidTexto);
            
            bool isValidNumeros = fctValidator.validarCamposNumericos(numeros);
            Assert.True(isValidNumeros);

        }
    }
}
