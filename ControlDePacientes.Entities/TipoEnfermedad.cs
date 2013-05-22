using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDePacientes.Entities
{
    public class TipoEnfermedad
    {
        private System.Int32 idtipoenfermedad;
        public System.Int32 Idtipoenfermedad
        {
            get
            {
                return idtipoenfermedad;
            }
            set
            {
                idtipoenfermedad = value;
            }
        }
        private System.String tipoenfermedadname;
        public System.String Tipoenfermedadname
        {
            get
            {
                return tipoenfermedadname;
            }
            set
            {
                tipoenfermedadname = value;
            }
        }

    }
}
