using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDePacientes.Entities
{
    public class tiposangre
    {
        private System.Int32 idtiposangre;
        public System.Int32 Idtiposangre
        {
            get
            {
                return idtiposangre;
            }
            set
            {
                idtiposangre = value;
            }
        }
        private System.String tiposangrename;
        public System.String Tiposangrename
        {
            get
            {
                return tiposangrename;
            }
            set
            {
                tiposangrename = value;
            }
        }
    }
}
