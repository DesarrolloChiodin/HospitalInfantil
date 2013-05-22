using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDePacientes.Entities
{
    public class sexo
    {
        private System.Int32 idsexo;
        public System.Int32 Idsexo
        {
            get
            {
                return idsexo;
            }
            set
            {
                idsexo = value;
            }
        }
        private System.String sexoname;
        public System.String Sexoname
        {
            get
            {
                return sexoname;
            }
            set
            {
                sexoname = value;
            }
        }
    }
}
