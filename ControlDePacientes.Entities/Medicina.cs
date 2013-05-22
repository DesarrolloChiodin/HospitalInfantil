using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDePacientes.Entities
{
    public class medicina
    {
        private System.Int32 idmedicina;
        public System.Int32 Idmedicina
        {
            get
            {
                return idmedicina;
            }
            set
            {
                idmedicina = value;
            }
        }
        private System.String medicinaname;
        public System.String Medicinaname
        {
            get
            {
                return medicinaname;
            }
            set
            {
                medicinaname = value;
            }
        }
        private System.String laboratorio;
        public System.String Laboratorio
        {
            get
            {
                return laboratorio;
            }
            set
            {
                laboratorio = value;
            }
        }
        private System.DateTime fechacaducidad;
        public System.DateTime Fechacaducidad
        {
            get
            {
                return fechacaducidad;
            }
            set
            {
                fechacaducidad = value;
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
        private System.Int32 codproveedor;
        public System.Int32 Codproveedor
        {
            get
            {
                return codproveedor;
            }
            set
            {
                codproveedor = value;
            }
        }
    }
}
