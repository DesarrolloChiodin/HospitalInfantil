using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ControlDePacientes.Dal
{
    public class Paciente
    {
        public string connStr = ConfigurationManager.ConnectionStrings["ControlDePacientes.Properties.Settings.hospitalinfantilConnectionString"].ConnectionString;


        public DataTable GetPaciente()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("estado_getall", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }

        public int CountPaciente()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("paciente_countAll", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    int lTotalPaciente = Convert.ToInt32(cmd.ExecuteScalar());
                    return lTotalPaciente;

                }
            }
        }

        public string AddPaciente(ControlDePacientes.Entities.paciente pPaciente)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("paciente_add", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@pPacienteName", MySqlDbType.String).Value = pPaciente.Pacientename;
                    cmd.Parameters.Add("@pNombre", MySqlDbType.String).Value = pPaciente.Nombre;
                    cmd.Parameters.Add("@pApellido", MySqlDbType.String).Value = pPaciente.Apellido;
                    cmd.Parameters.Add("@pSegundoApellido", MySqlDbType.String).Value = pPaciente.Segundoapellido;
                    cmd.Parameters.Add("@pCodSexo", MySqlDbType.Int32).Value = pPaciente.Codsexo == 0 ? (object)DBNull.Value : pPaciente.Codsexo;
                    cmd.Parameters.Add("@pFechaNacimiento", MySqlDbType.DateTime).Value = pPaciente.Fechanacimiento == new DateTime() ? (object)DBNull.Value : pPaciente.Fechanacimiento;
                    cmd.Parameters.Add("@pLugarNacimiento", MySqlDbType.Int32).Value = pPaciente.Lugarnacimiento == 0 ? (object)DBNull.Value : pPaciente.Lugarnacimiento;
                    cmd.Parameters.Add("@pCodAlergias", MySqlDbType.Int32).Value = pPaciente.Codalergias == 0 ? (object)DBNull.Value : pPaciente.Codalergias;
                    cmd.Parameters.Add("@pCodTipoSangre", MySqlDbType.Int32).Value = pPaciente.Codtiposangre == 0 ? (object)DBNull.Value : pPaciente.Codtiposangre;
                    cmd.Parameters.Add("@pNombrePadre", MySqlDbType.String).Value = pPaciente.Nombrepadre == "" ? (object)DBNull.Value : pPaciente.Nombrepadre;
                    cmd.Parameters.Add("@pCodOcupacionPadre", MySqlDbType.Int32).Value = pPaciente.Codocupacionpadre == 0 ? (object)DBNull.Value : pPaciente.Codocupacionpadre;
                    cmd.Parameters.Add("@pNombreMadre", MySqlDbType.String).Value = pPaciente.Nombremadre == "" ? (object)DBNull.Value : pPaciente.Nombremadre;
                    cmd.Parameters.Add("@pCodOcupacion", MySqlDbType.Int32).Value = pPaciente.Codocupacion == 0 ? (object)DBNull.Value : pPaciente.Codocupacion;
                    cmd.Parameters.Add("@pDireccion", MySqlDbType.String).Value = pPaciente.Direccion == "" ? (object)DBNull.Value : pPaciente.Direccion;
                    cmd.Parameters.Add("@pCodMunicipio", MySqlDbType.Int32).Value = pPaciente.Codmunicipio == 0 ? (object)DBNull.Value : pPaciente.Codmunicipio;
                    cmd.Parameters.Add("@pCodDepartamento", MySqlDbType.Int32).Value = pPaciente.Coddepartamento == 0 ? (object)DBNull.Value : pPaciente.Coddepartamento;
                    cmd.Parameters.Add("@pAldea", MySqlDbType.String).Value = pPaciente.Aldea == "" ? (object)DBNull.Value : pPaciente.Aldea;
                    cmd.Parameters.Add("@pTelefonoCasa", MySqlDbType.String).Value = pPaciente.Telefonocasa == "" ? (object)DBNull.Value : pPaciente.Telefonocasa;
                    cmd.Parameters.Add("@pTelefonoEmergencia", MySqlDbType.String).Value = pPaciente.Telefonoemergencia == "" ? (object)DBNull.Value : pPaciente.Telefonoemergencia;
                    cmd.Parameters.Add("@pCelularPrincipal", MySqlDbType.String).Value = pPaciente.Celularprincipal == "" ? (object)DBNull.Value : pPaciente.Celularprincipal;
                    cmd.Parameters.Add("@pCelularSecundario", MySqlDbType.String).Value = DBNull.Value;
                    cmd.Parameters.Add("@pCorreoElectronico", MySqlDbType.String).Value = pPaciente.Correoelectronico == "" ? (object)DBNull.Value : pPaciente.Correoelectronico;
                   
                    cmd.Parameters.Add("@pCodEstado", MySqlDbType.Int32).Value = 1;
                    cmd.Parameters.Add("@pObservacion", MySqlDbType.String).Value = pPaciente.Observacion == "" ? (object)DBNull.Value : pPaciente.Observacion;
                    cmd.Parameters.Add("@pCodPais", MySqlDbType.String).Value = pPaciente.Codpais == 0 ? (object)DBNull.Value : pPaciente.Codpais;
                    cmd.Parameters.Add("@pRegistroMedico", MySqlDbType.String).Value = pPaciente.Registromedico == "" ? (object)DBNull.Value : pPaciente.Registromedico;
                    cmd.Parameters.Add("@pEdad", MySqlDbType.String).Value = pPaciente.Edad == "" ? (object)DBNull.Value : pPaciente.Edad;

                    cmd.Parameters.Add("@pCodGrupoEtnico", MySqlDbType.Int32).Value = pPaciente.CodGrupoEtnico == 0 ? (object)DBNull.Value : pPaciente.CodGrupoEtnico;
                    cmd.Parameters.Add("@pIgss", MySqlDbType.Bit).Value = pPaciente.Igss;
                    cmd.Parameters.Add("@pCodComunidadLinguistica", MySqlDbType.Int32).Value = pPaciente.CodComunidadLinguistica == 0 ? (object)DBNull.Value : pPaciente.CodComunidadLinguistica;
                    cmd.Parameters.Add("@pAgricolaMigrante", MySqlDbType.Bit).Value = pPaciente.AgricolaMigrante;
                    cmd.Parameters.Add("@pCodEscolaridad", MySqlDbType.Int32).Value = pPaciente.CodEscolaridad == 0 ? (object)DBNull.Value : pPaciente.CodEscolaridad;
                    cmd.Parameters.Add("@pCodDiscapacidad", MySqlDbType.Int32).Value = pPaciente.CodDiscapacidad == 0 ? (object)DBNull.Value : pPaciente.CodDiscapacidad;

                    cmd.Parameters.Add("@pPacienteDpi", MySqlDbType.String).Value = pPaciente.PacienteDpi == "" ? (object)DBNull.Value : pPaciente.PacienteDpi;
                    cmd.Parameters.Add("@pPadreDpi", MySqlDbType.String).Value = pPaciente.PadreDpi == "" ? (object)DBNull.Value : pPaciente.PadreDpi;
                    cmd.Parameters.Add("@pMadreDpi", MySqlDbType.String).Value = pPaciente.MadreDpi == "" ? (object)DBNull.Value : pPaciente.MadreDpi;

                    if (pPaciente.FotoPaciente == null)
                        cmd.Parameters.Add("@PFotoPaciente", MySqlDbType.Blob).Value = (object) DBNull.Value;
                    else cmd.Parameters.Add("@PFotoPaciente", MySqlDbType.Blob).Value = pPaciente.FotoPaciente;
                    cmd.Parameters.Add("@pFotoPath", MySqlDbType.String).Value = pPaciente.Fotopath == "" ? (object)DBNull.Value : pPaciente.Fotopath;

                    // cmd.Parameters.Add("@pNombreAcompanante", MySqlDbType.String).Value = pPaciente.NombreAcompanante == "" ? (object)DBNull.Value : pPaciente.NombreAcompanante;
                    //cmd.Parameters.Add("@pCodOcupacionacompanante", MySqlDbType.Int32).Value = pPaciente.CodOcupacionAcompanante == 0 ? (object)DBNull.Value : pPaciente.CodOcupacionAcompanante;



                    //Comentado para foto
                    //if (!string.IsNullOrEmpty(pPaciente.Fotopath))
                    //{
                    //    string filePath = pPaciente.Fotopath;

                    //    //A stream of bytes that represnts the binary file
                    //    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                    //    //The reader reads the binary data from the file stream
                    //    BinaryReader reader = new BinaryReader(fs);

                    //    //Bytes from the binary reader stored in BlobValue array
                    //    byte[] BlobValue = reader.ReadBytes((int)fs.Length);

                    //    fs.Close();
                    //    reader.Close();
                    //    cmd.Parameters.Add("@pFotoPaciente", MySqlDbType.LongBlob).Value = BlobValue;

                    //}
                   // else cmd.Parameters.Add("@pFotoPaciente", MySqlDbType.LongBlob).Value = (object)DBNull.Value;


                    conn.Open();
                    var lIdPaciente = Convert.ToString(cmd.ExecuteScalar());
                    return lIdPaciente;

                }
            }
        }

        public void UpdPaciente(ControlDePacientes.Entities.paciente pPaciente, string pPathAnterior)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("paciente_update_by_idpaciente", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdPaciente", MySqlDbType.String).Value = pPaciente.Idpaciente;
                    cmd.Parameters.Add("@pPacienteName", MySqlDbType.String).Value = pPaciente.Pacientename;
                    cmd.Parameters.Add("@pNombre", MySqlDbType.String).Value = pPaciente.Nombre;
                    cmd.Parameters.Add("@pApellido", MySqlDbType.String).Value = pPaciente.Apellido;
                    cmd.Parameters.Add("@pSegundoApellido", MySqlDbType.String).Value = pPaciente.Segundoapellido;
                    cmd.Parameters.Add("@pCodSexo", MySqlDbType.Int32).Value = pPaciente.Codsexo == 0 ? (object)DBNull.Value : pPaciente.Codsexo;
                    cmd.Parameters.Add("@pFechaNacimiento", MySqlDbType.DateTime).Value = pPaciente.Fechanacimiento == new DateTime() ? (object)DBNull.Value : pPaciente.Fechanacimiento;
                    cmd.Parameters.Add("@pLugarNacimiento", MySqlDbType.Int32).Value = pPaciente.Lugarnacimiento == 0 ? (object)DBNull.Value : pPaciente.Lugarnacimiento;
                    cmd.Parameters.Add("@pCodAlergias", MySqlDbType.Int32).Value = pPaciente.Codalergias == 0 ? (object)DBNull.Value : pPaciente.Codalergias;
                    cmd.Parameters.Add("@pCodTipoSangre", MySqlDbType.Int32).Value = pPaciente.Codtiposangre == 0 ? (object)DBNull.Value : pPaciente.Codtiposangre;
                    cmd.Parameters.Add("@pNombrePadre", MySqlDbType.String).Value = pPaciente.Nombrepadre == "" ? (object)DBNull.Value : pPaciente.Nombrepadre;
                    cmd.Parameters.Add("@pCodOcupacionPadre", MySqlDbType.Int32).Value = pPaciente.Codocupacionpadre == 0 ? (object)DBNull.Value : pPaciente.Codocupacionpadre;
                    cmd.Parameters.Add("@pNombreMadre", MySqlDbType.String).Value = pPaciente.Nombremadre == "" ? (object)DBNull.Value : pPaciente.Nombremadre;
                    cmd.Parameters.Add("@pCodOcupacion", MySqlDbType.Int32).Value = pPaciente.Codocupacion == 0 ? (object)DBNull.Value : pPaciente.Codocupacion;
                    cmd.Parameters.Add("@pDireccion", MySqlDbType.String).Value = pPaciente.Direccion == "" ? (object)DBNull.Value : pPaciente.Direccion;
                    cmd.Parameters.Add("@pCodMunicipio", MySqlDbType.Int32).Value = pPaciente.Codmunicipio == 0 ? (object)DBNull.Value : pPaciente.Codmunicipio;
                    cmd.Parameters.Add("@pCodDepartamento", MySqlDbType.Int32).Value = pPaciente.Coddepartamento == 0 ? (object)DBNull.Value : pPaciente.Coddepartamento;
                    cmd.Parameters.Add("@pAldea", MySqlDbType.String).Value = pPaciente.Aldea == "" ? (object)DBNull.Value : pPaciente.Aldea;
                    cmd.Parameters.Add("@pTelefonoCasa", MySqlDbType.String).Value = pPaciente.Telefonocasa == "" ? (object)DBNull.Value : pPaciente.Telefonocasa;
                    cmd.Parameters.Add("@pTelefonoEmergencia", MySqlDbType.String).Value = pPaciente.Telefonoemergencia == "" ? (object)DBNull.Value : pPaciente.Telefonoemergencia;
                    cmd.Parameters.Add("@pCelularPrincipal", MySqlDbType.String).Value = pPaciente.Celularprincipal == "" ? (object)DBNull.Value : pPaciente.Celularprincipal;
                    cmd.Parameters.Add("@pCelularSecundario", MySqlDbType.String).Value = DBNull.Value;
                    cmd.Parameters.Add("@pCorreoElectronico", MySqlDbType.String).Value = pPaciente.Correoelectronico == "" ? (object)DBNull.Value : pPaciente.Correoelectronico;
                   // cmd.Parameters.Add("@pFotoPath", MySqlDbType.String).Value = pPaciente.Fotopath;
                    cmd.Parameters.Add("@pCodEstado", MySqlDbType.Int32).Value = 1;
                    cmd.Parameters.Add("@pObservacion", MySqlDbType.String).Value = pPaciente.Observacion == "" ? (object)DBNull.Value : pPaciente.Observacion;
                    cmd.Parameters.Add("@pCodPais", MySqlDbType.String).Value = pPaciente.Codpais == 0 ? (object)DBNull.Value : pPaciente.Codpais;
                    cmd.Parameters.Add("@pEdad", MySqlDbType.String).Value = pPaciente.Edad == "" ? (object)DBNull.Value : pPaciente.Edad;
                    cmd.Parameters.Add("@pPacienteDpi", MySqlDbType.String).Value = pPaciente.PacienteDpi == "" ? (object)DBNull.Value : pPaciente.PacienteDpi;
                    cmd.Parameters.Add("@pPadreDpi", MySqlDbType.String).Value = pPaciente.PadreDpi == "" ? (object)DBNull.Value : pPaciente.PadreDpi;
                    cmd.Parameters.Add("@pMadreDpi", MySqlDbType.String).Value = pPaciente.MadreDpi == "" ? (object)DBNull.Value : pPaciente.MadreDpi;
                    cmd.Parameters.Add("@pCodGrupoEtnico", MySqlDbType.Int32).Value = pPaciente.CodGrupoEtnico == 0 ? (object)DBNull.Value : pPaciente.CodGrupoEtnico;
                    cmd.Parameters.Add("@pIgss", MySqlDbType.Bit).Value = pPaciente.Igss;
                    cmd.Parameters.Add("@pCodComunidadLinguistica", MySqlDbType.Int32).Value = pPaciente.CodComunidadLinguistica == 0 ? (object)DBNull.Value : pPaciente.CodComunidadLinguistica;
                    cmd.Parameters.Add("@pAgricolaMigrante", MySqlDbType.Bit).Value = pPaciente.AgricolaMigrante;
                    cmd.Parameters.Add("@pCodEscolaridad", MySqlDbType.Int32).Value = pPaciente.CodEscolaridad == 0 ? (object)DBNull.Value : pPaciente.CodEscolaridad;
                    cmd.Parameters.Add("@pCodDiscapacidad", MySqlDbType.Int32).Value = pPaciente.CodDiscapacidad == 0 ? (object)DBNull.Value : pPaciente.CodDiscapacidad;
                   // cmd.Parameters.Add("@pFotoPaciente", MySqlDbType.Blob).Value = pPaciente.FotoPaciente;
                    if (pPaciente.FotoPaciente == null)
                        cmd.Parameters.Add("@PFotoPaciente", MySqlDbType.Blob).Value = (object)DBNull.Value;
                    else cmd.Parameters.Add("@PFotoPaciente", MySqlDbType.Blob).Value = pPaciente.FotoPaciente;
                    cmd.Parameters.Add("@pFotoPath", MySqlDbType.String).Value = pPaciente.Fotopath == "" ? (object)DBNull.Value : pPaciente.Fotopath;
                    
                    conn.Open();
                    cmd.ExecuteNonQuery();


                }
            }
        }

        public void UpdRegistroMedico(string pRegistroMedico, int pIdPaciente)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("paciente_updateRegistroMedico", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pRegistroMedico", MySqlDbType.String).Value = pRegistroMedico;
                    cmd.Parameters.Add("@pIdPaciente", MySqlDbType.String).Value = pIdPaciente;

                    conn.Open();
                    cmd.ExecuteNonQuery();


                }
            }
        }

        public bool DeletePaciente(int pIdPaciente)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("paciente_delete_by_idpaciente", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pIdPaciente", MySqlDbType.Int32).Value = pIdPaciente;

                    conn.Open();


                    return Convert.ToBoolean(cmd.ExecuteScalar());

                }
            }

        }

        public DataTable GetPacientePorParametros(string pStringParameter)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("paciente_getparameters", conn);

                    if (pStringParameter != "")
                        cmd.Parameters.Add("@pStringParameter", MySqlDbType.String).Value = pStringParameter;
                    else cmd.Parameters.Add("@pStringParameter", MySqlDbType.String).Value = "*";
                    //if (pIntParameter != 0)
                    //    cmd.Parameters.Add("@pIntParameter", MySqlDbType.Int32).Value = pIntParameter;
                    //else cmd.Parameters.Add("@pIntParameter", MySqlDbType.Int32).Value = 0;


                    cmd.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }

        public DataTable GetPacienteByRegistroMedico(string pRegistroMedico)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("paciente_getRegistroMedico", conn);
                    cmd.Parameters.Add("@pRegistroMedico", MySqlDbType.String).Value = pRegistroMedico;
                    cmd.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }


        public DataTable GetPacientePorOldParameters(string pStringParameter)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("paciente_SearchOld", conn);

                    if (pStringParameter != "")
                        cmd.Parameters.Add("@pStringParameter", MySqlDbType.String).Value = pStringParameter;
                    else cmd.Parameters.Add("@pStringParameter", MySqlDbType.String).Value = "*";
                    //if (pIntParameter != 0)
                    //    cmd.Parameters.Add("@pIntParameter", MySqlDbType.Int32).Value = pIntParameter;
                    //else cmd.Parameters.Add("@pIntParameter", MySqlDbType.Int32).Value = 0;


                    cmd.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    conn.Open();

                    MySqlDataReader myDataReader = cmd.ExecuteReader();
                    dt.Load(myDataReader);

                    return dt;

                }
            }

        }



        public DataTable GetPacienteHistorialByEstado(int pIdPaciente)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                {
                    //ejecuto sp para la consulta de Usuarios
                    MySqlCommand cmd = new MySqlCommand("Historial_getAllByIdPacienteByState", conn);
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
