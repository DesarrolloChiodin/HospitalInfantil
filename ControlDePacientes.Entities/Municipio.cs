using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDePacientes.Entities
{
    public class municipio
    {
        private System.Int32 idmunicipio;
        public System.Int32 Idmunicipio
        {
            get
            {
                return idmunicipio;
            }
            set
            {
                idmunicipio = value;
            }
        }
        private System.String municipioname;
        public System.String Municipioname
        {
            get
            {
                return municipioname;
            }
            set
            {
                municipioname = value;
            }
        }
        private System.Int32 coddepartamento;
        public System.Int32 Coddepartamento
        {
            get
            {
                return coddepartamento;
            }
            set
            {
                coddepartamento = value;
            }
        }
    }
}
