using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FCT
{
    public class Validaciones
    {
        public bool validarDNI(string campoDNI)
        {
            Regex regex = new Regex("^[0-9]{7,8}[A-Z a-z]$");
            return regex.Match(campoDNI).Success;
        }

        public bool validarEmail(string campoEmail)
        {
            Regex regex = new Regex("[_A-Za-z0-9-]+(?:\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9]+(?:\\.[A-Za-z0-9]+)*(?:\\.[A-Za-z]{2,})");
            return regex.Match(campoEmail).Success;
        }

        public bool validarCP(string campoCP)
        {
            Regex regex = new Regex("^[0-9]{5}$");
            return regex.Match(campoCP).Success;
        }

        public bool validarCIF(string campoCIF)
        {
            Regex regex = new Regex("^[A-Z a-z][0-9]{8}$");
            return regex.Match(campoCIF).Success;
        }

        public bool validarTelefono(string campoTelefono)
        {
            Regex regex = new Regex("^[0-9]{9}$");
            return regex.Match(campoTelefono).Success;
        }

        public bool validarCamposTextos(string campoTexto)
        {
            Regex regex = new Regex("^[A-Z a-z]");
            return regex.Match(campoTexto).Success;
        }

        public bool validarCamposNumericos(string campoNumerico)
        {
            Regex regex = new Regex("^[0-9]");
            return regex.Match(campoNumerico).Success;
        }
    }
}
