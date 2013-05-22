using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDePacientes.Entities
{
    public class tipousuario
    {
        private System.Int32 idtipousuario;
        public System.Int32 Idtipousuario
        {
            get
            {
                return idtipousuario;
            }
            set
            {
                idtipousuario = value;
            }
        }
        private System.String tipousuarioname;
        public System.String Tipousuarioname
        {
            get
            {
                return tipousuarioname;
            }
            set
            {
                tipousuarioname = value;
            }
        }
    }
}
