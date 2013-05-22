using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDePacientes.Entities
{
    public class departamento
    {
        private System.Int32 iddepartamento;
        public System.Int32 Iddepartamento
        {
            get
            {
                return iddepartamento;
            }
            set
            {
                iddepartamento = value;
            }
        }
        private System.String departamentoname;
        public System.String Departamentoname
        {
            get
            {
                return departamentoname;
            }
            set
            {
                departamentoname = value;
            }
        }
    }
}
