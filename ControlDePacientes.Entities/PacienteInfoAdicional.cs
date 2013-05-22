using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlDePacientes.Entities
{
    public class PacienteInfoAdicional
    {
        public int Idpacienteinfoadicional { get; set; }
        public string Nombreacompanante { get; set; }
        public string Ceduladpiacompanante { get; set; }
        public string Telefonoacompanante { get; set; }
        public string Celularacompanante { get; set; }
        public string Direccion { get; set; }
        public string Personaretirapaciente { get; set; }
        public bool Retirapaciente { get; set; }
        public string Historiaacompanante { get; set; }
        public DateTime Fechaacompanante { get; set; }
        public int Codpaciente { get; set; }
        public int Codhistorialmedico { get; set; }
        public bool NuevoPaciente { get; set; }
        public int CodConsulta { get; set; }
        public int CodReferencia { get; set; }
    }
}
