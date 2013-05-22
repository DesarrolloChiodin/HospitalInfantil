using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDePacientes.Entities
{
    public class recetadetalle
    {
        private System.Int32 idrecetadetalle;
        public System.Int32 Idrecetadetalle
        {
            get
            {
                return idrecetadetalle;
            }
            set
            {
                idrecetadetalle = value;
            }
        }
        private System.String recetadetallename;
        public System.String Recetadetallename
        {
            get
            {
                return recetadetallename;
            }
            set
            {
                recetadetallename = value;
            }
        }
        private System.Int32 codreceta;
        public System.Int32 Codreceta
        {
            get
            {
                return codreceta;
            }
            set
            {
                codreceta = value;
            }
        }
        private System.Int32 cantidad;
        public System.Int32 Cantidad
        {
            get
            {
                return cantidad;
            }
            set
            {
                cantidad = value;
            }
        }
        private System.Int32 codmedicina;
        public System.Int32 Codmedicina
        {
            get
            {
                return codmedicina;
            }
            set
            {
                codmedicina = value;
            }
        }
        private System.String indicaciones;
        public System.String Indicaciones
        {
            get
            {
                return indicaciones;
            }
            set
            {
                indicaciones = value;
            }
        }
    }
}
