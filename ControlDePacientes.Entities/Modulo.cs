using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDePacientes.Entities
{
    public class modulo
    {
        private System.Int32 idmodulo;
        public System.Int32 Idmodulo
        {
            get
            {
                return idmodulo;
            }
            set
            {
                idmodulo = value;
            }
        }
        private System.String moduloname;
        public System.String Moduloname
        {
            get
            {
                return moduloname;
            }
            set
            {
                moduloname = value;
            }
        }
        private System.Int32 codtipousuario;
        public System.Int32 Codtipousuario
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
    }
}
