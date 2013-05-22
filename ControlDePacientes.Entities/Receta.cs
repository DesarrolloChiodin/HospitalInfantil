using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDePacientes.Entities
{
    public class receta
    {
        private System.Int32 idreceta;
        public System.Int32 Idreceta
        {
            get
            {
                return idreceta;
            }
            set
            {
                idreceta = value;
            }
        }
        private System.String recetaname;
        public System.String Recetaname
        {
            get
            {
                return recetaname;
            }
            set
            {
                recetaname = value;
            }
        }
        private System.Int32 codpaciente;
        public System.Int32 Codpaciente
        {
            get
            {
                return codpaciente;
            }
            set
            {
                codpaciente = value;
            }
        }
        private System.DateTime fechareceta;
        public System.DateTime Fechareceta
        {
            get
            {
                return fechareceta;
            }
            set
            {
                fechareceta = value;
            }
        }
        private System.Int32 codestado;
        public System.Int32 Codestado
        {
            get
            {
                return codestado;
            }
            set
            {
                codestado = value;
            }
        }
    }
}
