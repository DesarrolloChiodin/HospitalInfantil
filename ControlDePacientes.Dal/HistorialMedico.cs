using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ControlDePacientes.Dal
{
    public class HistorialMedico
    {
        public string connStr = ConfigurationManager.ConnectionStrings["ControlDePacientes.Properties.Settings.hospitalinfantilConnectionString"].ConnectionString;

        public DataTable GetHistorialMedicoByIdPaciente()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }

        public DataTable HistorialMedicoGetAll(int pIdHistorialMedico)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("historialmedico_get_by_idhistorialmedico", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdHistorialMedico", MySqlDbType.Int32).Value = pIdHistorialMedico;
                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }

        public DataTable HistorialMedicoVerify(int pIdPaciente, int pIdEstado)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("historialmedico_Verify", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdPaciente", MySqlDbType.Int32).Value = pIdPaciente;
                    cmd.Parameters.Add("@pIdEstado", MySqlDbType.Int32).Value = pIdEstado;

                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }

        public DataTable HistorialMedicoByPacienteByState(string pRegistroMedico)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("historialmedico_getbyRegistroMedicoEstadoFinal", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pRegistroMedico", MySqlDbType.String).Value = pRegistroMedico;


                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }

        public int AddHistorialMedicoUpdExamenFisico(ControlDePacientes.Entities.historialmedico phistorialmedico)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("historialmedico_add", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdHistorialMedico", MySqlDbType.Int32).Value = phistorialmedico.Idhistorialmedico;
                    cmd.Parameters.Add("@pFECHA", MySqlDbType.DateTime).Value = phistorialmedico.Fecha;
                    cmd.Parameters.Add("@pDxIngreso", MySqlDbType.Int32).Value = phistorialmedico.Dxingreso;
                    cmd.Parameters.Add("@pAntecedentesImportancia", MySqlDbType.String).Value = phistorialmedico.Antecedentesimportancia;
                    cmd.Parameters.Add("@pMotivoConsulta", MySqlDbType.String).Value = phistorialmedico.Motivoconsulta;
                    cmd.Parameters.Add("@pExamenFisico", MySqlDbType.String).Value = phistorialmedico.Examenfisico;
                    cmd.Parameters.Add("@pPresionArterial", MySqlDbType.String).Value = phistorialmedico.Presionarterial;
                    cmd.Parameters.Add("@pFrecuenciaCardiaca", MySqlDbType.String).Value = phistorialmedico.Frecuenciacardiaca;
                    cmd.Parameters.Add("@pFrecuenciaRespiratora", MySqlDbType.String).Value = phistorialmedico.Frecuenciarespiratora;
                    cmd.Parameters.Add("@pTemperatura", MySqlDbType.String).Value = phistorialmedico.Temperatura;
                    cmd.Parameters.Add("@pCC", MySqlDbType.String).Value = phistorialmedico.Cc;
                    cmd.Parameters.Add("@pPeso", MySqlDbType.String).Value = phistorialmedico.Peso;
                    cmd.Parameters.Add("@pTalla", MySqlDbType.String).Value = phistorialmedico.Talla;
                    cmd.Parameters.Add("@pMasaCorporal", MySqlDbType.String).Value = phistorialmedico.Masacorporal;
                    cmd.Parameters.Add("@pTE", MySqlDbType.String).Value = phistorialmedico.Te;
                    cmd.Parameters.Add("@pPE", MySqlDbType.String).Value = phistorialmedico.Pe;
                    cmd.Parameters.Add("@pPesoTalla", MySqlDbType.String).Value = phistorialmedico.Pesotalla;
                    cmd.Parameters.Add("@pSaturacionO2", MySqlDbType.String).Value = phistorialmedico.Saturaciono2;
                    cmd.Parameters.Add("@pRayosX", MySqlDbType.String).Value = phistorialmedico.Rayosx;
                    cmd.Parameters.Add("@pCodServicio", MySqlDbType.Int32).Value = phistorialmedico.Codservicio;
                    cmd.Parameters.Add("@pCodUsuario", MySqlDbType.Int32).Value = phistorialmedico.Codusuario;
                    cmd.Parameters.Add("@pCodEstado", MySqlDbType.Int32).Value = phistorialmedico.Codestado;



                    conn.Open();
                    int lIdHistorialMedico = Convert.ToInt32(cmd.ExecuteScalar());
                    return lIdHistorialMedico;

                }
            }
        }

        public int AddNewHistorialMedico(ControlDePacientes.Entities.historialmedico phistorialmedico)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("historialmedicoAdmision_add", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pHistorialMedicoName", MySqlDbType.String).Value = phistorialmedico.Historialmediconame;
                    cmd.Parameters.Add("@pIdEstado", MySqlDbType.Int32).Value = phistorialmedico.Codestado;
                    cmd.Parameters.Add("@pIdPaciente", MySqlDbType.Int32).Value = phistorialmedico.Codpaciente;
                    cmd.Parameters.Add("@pCorrelativo", MySqlDbType.String).Value = phistorialmedico.Correlativo;
                    cmd.Parameters.Add("@pFECHA", MySqlDbType.DateTime).Value = phistorialmedico.Fecha;

                    conn.Open();
                    int lIdHistorialMedico = Convert.ToInt32(cmd.ExecuteScalar());
                    return lIdHistorialMedico;


                }
            }
        }

        public int UpdHistorialMedico(ControlDePacientes.Entities.historialmedico phistorialmedico)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("historialmedico_update", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pIdHistorialMedico", MySqlDbType.Int32).Value = phistorialmedico.Idhistorialmedico;
                    if (phistorialmedico.Dxingreso != 0)
                        cmd.Parameters.Add("@pDxIngreso", MySqlDbType.Int32).Value = phistorialmedico.Dxingreso;
                    else cmd.Parameters.Add("@pDxIngreso", MySqlDbType.Int32).Value = DBNull.Value;
                    if (phistorialmedico.Dxegreso != 0)
                        cmd.Parameters.Add("@pDxEgreso", MySqlDbType.Int32).Value = phistorialmedico.Dxegreso;
                    else cmd.Parameters.Add("@pDxEgreso", MySqlDbType.Int32).Value = DBNull.Value;
                    cmd.Parameters.Add("@pEstadoNutricinal", MySqlDbType.String).Value = phistorialmedico.Estadonutricinal;
                    cmd.Parameters.Add("@pEstudiosIniciales", MySqlDbType.String).Value = phistorialmedico.Estudiosiniciales;
                    cmd.Parameters.Add("@pImpresionClinica", MySqlDbType.String).Value = phistorialmedico.Impresionclinica;
                    cmd.Parameters.Add("@pTratamientoConsultaExterna", MySqlDbType.String).Value = phistorialmedico.Tratamientoconsultaexterna;
                    if (phistorialmedico.Coddestinocaso != 0)
                        cmd.Parameters.Add("@pCodDestinoCaso", MySqlDbType.Int32).Value = phistorialmedico.Coddestinocaso;
                    else cmd.Parameters.Add("@pCodDestinoCaso", MySqlDbType.Int32).Value = DBNull.Value;
                    cmd.Parameters.Add("@pObservaciones", MySqlDbType.String).Value = phistorialmedico.Observaciones;
                    cmd.Parameters.Add("@pNotasDescargo", MySqlDbType.String).Value = phistorialmedico.Notasdescargo;
                    cmd.Parameters.Add("@pCodEstado", MySqlDbType.Int32).Value = phistorialmedico.Codestado;
                    cmd.Parameters.Add("@pCodDoctor", MySqlDbType.Int32).Value = phistorialmedico.Coddoctor;



                    conn.Open();
                    cmd.ExecuteScalar();
                    return 1;

                }
            }
        }

        public bool DeleteHistorialMedico(int pIdHistorialMedico)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("historialmedico_delete_by_idhistorialmedico", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdHistorialMedico", MySqlDbType.Int32).Value = pIdHistorialMedico;

                    conn.Open();


                    return Convert.ToBoolean(cmd.ExecuteScalar());

                }
            }

        }

        public DataTable GetDoctorsNotes(int pIdHistorialMedico)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("historialmedicogetDocotorNotes", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdHistorialMedico", MySqlDbType.Int32).Value = pIdHistorialMedico;
                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }

        public DataTable GetHistorialMedicoForReport(int pIdHistorialMedico)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("RptHistorialmedicoGetAllById", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdHistorialMedico", MySqlDbType.Int32).Value = pIdHistorialMedico;


                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }

        public int HistorialMedicoCountByYear(int pIdPaciente)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("HistorialMedicoCountByYear", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pIdPaciente", MySqlDbType.String).Value = pIdPaciente;


                    conn.Open();
                    int lIdHistorialMedico = Convert.ToInt32(cmd.ExecuteScalar());
                    return lIdHistorialMedico;

                }
            }
        }

        public DataTable HistorialMedicoByIdPaciente(string pRegistroMedico)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("historialmedico_getbyRegistroMedicoEstadoFinal", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pRegistroMedico", MySqlDbType.String).Value = pRegistroMedico;


                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }

        public DataTable HistorialMedicoGetAllForPrint(int pIdHistorialMedico)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("HistorialMedico_getAllForPrint", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdHistorialMedico", MySqlDbType.Int32).Value = pIdHistorialMedico;


                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }

        public DataTable HistorialMedicoGetAllByIdPaciente(int pIdPaciente)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("Historial_getAllByIdPaciente", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdPaciente", MySqlDbType.String).Value = pIdPaciente;


                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }

        public DataTable HistorialMedicoPendienteCheck(int pIdPaciente)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("Historial_PendienteCheck", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdPaciente", MySqlDbType.Int32).Value = pIdPaciente;


                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }
    }
}
