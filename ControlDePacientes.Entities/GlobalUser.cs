using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDePacientes.Entities
{
    public static class GlobalUser
    {
        private static System.Int32 idusuario;
        public static System.Int32 Idusuario
        {
            get
            {
                return idusuario;
            }
            set
            {
                idusuario = value;
            }
        }
        private static System.String usuarioname;
        public static System.String Usuarioname
        {
            get
            {
                return usuarioname;
            }
            set
            {
                usuarioname = value;
            }
        }
        private static System.String nombreusuario;
        public static System.String Nombreusuario
        {
            get
            {
                return nombreusuario;
            }
            set
            {
                nombreusuario = value;
            }
        }
        private static System.String apellidousuario;
        public static System.String Apellidousuario
        {
            get
            {
                return apellidousuario;
            }
            set
            {
                apellidousuario = value;
            }
        }
        private static System.String usuario;
        public static System.String Usuario
        {
            get
            {
                return usuario;
            }
            set
            {
                usuario = value;
            }
        }
        private static System.String password;
        public static System.String Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        private static System.Int32 codtipousuario;
        public static System.Int32 Codtipousuario
        {
            get
            {
                return codtipousuario;
            }
            set
            {
                codtipousuario = value;
            }
        }
        private static System.String telefonocasa;
        public static System.String Telefonocasa
        {
            get
            {
                return telefonocasa;
            }
            set
            {
                telefonocasa = value;
            }
        }
        private static System.String celular;
        public static System.String Celular
        {
            get
            {
                return celular;
            }
            set
            {
                celular = value;
            }
        }
        private static System.Int32 codestado;
        public static System.Int32 Codestado
        {
            get
            {
                return codestado;
            }
            set
            {
                codestado = value;
            }
        }
        private static System.Int32 codcargo;
        public static System.Int32 Codcargo
        {
            get
            {
                return codcargo;
            }
            set
            {
                codcargo = value;
            }
        }
    }
}
