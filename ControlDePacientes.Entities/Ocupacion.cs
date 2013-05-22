using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDePacientes.Entities
{
    public class ocupacion
    {
        private System.Int32 idocupacion;
        public System.Int32 Idocupacion
        {
            get
            {
                return idocupacion;
            }
            set
            {
                idocupacion = value;
            }
        }
        private System.String ocupacionname;
        public System.String Ocupacionname
        {
            get
            {
                return ocupacionname;
            }
            set
            {
                ocupacionname = value;
            }
        }
    }
}
