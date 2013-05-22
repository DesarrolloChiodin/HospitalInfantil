using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDePacientes.Entities
{
    public class Enfermedad
    {
        private System.Int32 idenfermedad;
        public System.Int32 Idenfermedad
        {
            get
            {
                return idenfermedad;
            }
            set
            {
                idenfermedad = value;
            }
        }
        private System.String enfermedadname;
        public System.String Enfermedadname
        {
            get
            {
                return enfermedadname;
            }
            set
            {
                enfermedadname = value;
            }
        }
        private System.Int32 codtipoenfermedad;
        public System.Int32 Codtipoenfermedad
        {
            get
            {
                return codtipoenfermedad;
            }
            set
            {
                codtipoenfermedad = value;
            }
        }
    }

}

