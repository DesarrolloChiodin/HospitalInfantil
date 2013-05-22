using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDePacientes.Entities
{
    public class Estado
    {
        private System.Int32 idestado;
        public System.Int32 Idestado
        {
            get
            {
                return idestado;
            }
            set
            {
                idestado = value;
            }
        }
        private System.String estadoname;
        public System.String Estadoname
        {
            get
            {
                return estadoname;
            }
            set
            {
                estadoname = value;
            }
        }
        private System.Int32 codtipoestado;
        public System.Int32 Codtipoestado
        {
            get
            {
                return codtipoestado;
            }
            set
            {
                codtipoestado = value;
            }
        }
    }
}
