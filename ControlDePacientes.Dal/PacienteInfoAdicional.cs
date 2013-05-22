using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ControlDePacientes.Dal
{
    public class PacienteInfoAdicional
    {
        public string connStr = ConfigurationManager.ConnectionStrings["ControlDePacientes.Properties.Settings.hospitalinfantilConnectionString"].ConnectionString;

        public DataTable GetPacienteInfoAdicionalPorHistorial(int pCodHistorial)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {

                    MySqlCommand cmd = new MySqlCommand("pacienteinfoadicional_getAllByHistorial", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pCodHistorial", MySqlDbType.String).Value = pCodHistorial;

                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;
                    
                }
            }
        }


        public void AddPacienteInfoAdicional(ControlDePacientes.Entities.PacienteInfoAdicional pPacienteInfoAdicional)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {

                    MySqlCommand cmd = new MySqlCommand("pacienteinfoadicional_add", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pNombreAcompanante", MySqlDbType.String).Value = pPacienteInfoAdicional.Nombreacompanante;
                    cmd.Parameters.Add("@pCedulaDpiAcompanante", MySqlDbType.String).Value = pPacienteInfoAdicional.Ceduladpiacompanante;
                    cmd.Parameters.Add("@pTelefonoAcompanante", MySqlDbType.String).Value = pPacienteInfoAdicional.Telefonoacompanante;
                    cmd.Parameters.Add("@pCelularAcompanante", MySqlDbType.String).Value = pPacienteInfoAdicional.Celularacompanante;
                    cmd.Parameters.Add("@pDireccion", MySqlDbType.String).Value = pPacienteInfoAdicional.Direccion;
                    cmd.Parameters.Add("@pPersonaRetiraPaciente", MySqlDbType.String).Value = pPacienteInfoAdicional.Personaretirapaciente;
                    cmd.Parameters.Add("@pRetiraPaciente", MySqlDbType.Bit).Value = pPacienteInfoAdicional.Retirapaciente;
                    cmd.Parameters.Add("@pHistoriaAcompanante", MySqlDbType.String).Value = pPacienteInfoAdicional.Historiaacompanante;
                    cmd.Parameters.Add("@pFechaAcompanante", MySqlDbType.DateTime).Value = pPacienteInfoAdicional.Fechaacompanante;
                    cmd.Parameters.Add("@pCodPaciente", MySqlDbType.String).Value = pPacienteInfoAdicional.Codpaciente;
                    cmd.Parameters.Add("@pCodHistorialMedico", MySqlDbType.String).Value = pPacienteInfoAdicional.Codhistorialmedico;
                    cmd.Parameters.Add("@pNuevoPaciente", MySqlDbType.Bit).Value = pPacienteInfoAdicional.NuevoPaciente;

                    if (pPacienteInfoAdicional.CodConsulta == 0)
                        cmd.Parameters.Add("@pCodConsulta", MySqlDbType.Int32).Value = DBNull.Value;
                    else cmd.Parameters.Add("@pCodConsulta", MySqlDbType.Int32).Value = pPacienteInfoAdicional.CodConsulta;

                    if(pPacienteInfoAdicional.CodReferencia == 0)
                        cmd.Parameters.Add("@pCodReferido", MySqlDbType.Int32).Value = DBNull.Value;
                    else cmd.Parameters.Add("@pCodReferido", MySqlDbType.Int32).Value = pPacienteInfoAdicional.CodReferencia;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }


        public void UpdPacienteInfoAdicional(ControlDePacientes.Entities.PacienteInfoAdicional pPacienteInfoAdicional)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {

                    MySqlCommand cmd = new MySqlCommand("pacienteinfoadicional_update_byId", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pidpacienteinfoadicional", MySqlDbType.String).Value = pPacienteInfoAdicional.Idpacienteinfoadicional;
                    cmd.Parameters.Add("@pNombreAcompanante", MySqlDbType.String).Value = pPacienteInfoAdicional.Nombreacompanante;
                    cmd.Parameters.Add("@pCedulaDpiAcompanante", MySqlDbType.String).Value = pPacienteInfoAdicional.Ceduladpiacompanante;
                    cmd.Parameters.Add("@pTelefonoAcompanante", MySqlDbType.String).Value = pPacienteInfoAdicional.Telefonoacompanante;
                    cmd.Parameters.Add("@pCelularAcompanante", MySqlDbType.String).Value = pPacienteInfoAdicional.Celularacompanante;
                    cmd.Parameters.Add("@pDireccion", MySqlDbType.String).Value = pPacienteInfoAdicional.Direccion;
                    cmd.Parameters.Add("@pPersonaRetiraPaciente", MySqlDbType.String).Value = pPacienteInfoAdicional.Personaretirapaciente;
                    cmd.Parameters.Add("@pRetiraPaciente", MySqlDbType.Bit).Value = pPacienteInfoAdicional.Retirapaciente;
                    cmd.Parameters.Add("@pHistoriaAcompanante", MySqlDbType.String).Value = pPacienteInfoAdicional.Historiaacompanante;
                    cmd.Parameters.Add("@pFechaAcompanante", MySqlDbType.DateTime).Value = pPacienteInfoAdicional.Fechaacompanante;
                    cmd.Parameters.Add("@pCodPaciente", MySqlDbType.String).Value = pPacienteInfoAdicional.Codpaciente;
                    cmd.Parameters.Add("@pCodHistorialMedico", MySqlDbType.String).Value = pPacienteInfoAdicional.Codhistorialmedico;
                    cmd.Parameters.Add("@pNuevoPaciente", MySqlDbType.Bit).Value = pPacienteInfoAdicional.NuevoPaciente;

                    if (pPacienteInfoAdicional.CodConsulta == 0)
                        cmd.Parameters.Add("@pCodConsulta", MySqlDbType.Int32).Value = DBNull.Value;
                    else cmd.Parameters.Add("@pCodConsulta", MySqlDbType.Int32).Value = pPacienteInfoAdicional.CodConsulta;

                    if (pPacienteInfoAdicional.CodReferencia == 0)
                        cmd.Parameters.Add("@pCodReferido", MySqlDbType.Int32).Value = DBNull.Value;
                    else cmd.Parameters.Add("@pCodReferido", MySqlDbType.Int32).Value = pPacienteInfoAdicional.CodReferencia;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }
    }

}