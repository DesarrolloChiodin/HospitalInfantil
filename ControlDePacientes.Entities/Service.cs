using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDePacientes.Entities
{
    public class Service
    {
        private System.Int32 idservicio;
        public System.Int32 Idservicio
        {
            get
            {
                return idservicio;
            }
            set
            {
                idservicio = value;
            }
        }
        private System.String servicioname;
        public System.String Servicioname
        {
            get
            {
                return servicioname;
            }
            set
            {
                servicioname = value;
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
