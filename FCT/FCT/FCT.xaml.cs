using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;


namespace FCT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string id = "";
        private int intRow = 0;

        public MainWindow()
        {
            InitializeComponent();
            resetMe();

            rellenarComboAlumno();
            rellenarComboEmpresa();
            rellenarComboTutor();

        }
        private void resetMe()
        {

            this.id = "";

            texto_codigo.Clear();
            texto_nombre_empresa.Clear();
            texto_CIF.Clear();
            texto_direccion.Clear();
            texto_localidad.Clear();
            texto_CP.Clear();
            texto_nombre_resp.Clear();
            texto_nombre_tutor.Clear();
            texto_apellido_tutor.Clear();
            texto_DNI_tutor.Clear();

            if (combobox_jornada.Items.Count > 0)
            {
                combobox_jornada.SelectedIndex = 0;
            }
        }

        private void loadData()
        {

            CRUD.dsEmpresa.Clear();

            CRUD.sql = "SELECT * FROM empresa";
            CRUD.cmd = new MySqlCommand(CRUD.sql, CRUD.con);
            CRUD.adaptador.SelectCommand = CRUD.cmd;
            CRUD.adaptador.Fill(CRUD.dsEmpresa, "empresa");

        }

        private void loadDataAlumno()
        {

            CRUD.dsAlumno.Clear();

            CRUD.sql = "SELECT * FROM alumnos";
            CRUD.cmd = new MySqlCommand(CRUD.sql, CRUD.con);
            CRUD.adaptador.SelectCommand = CRUD.cmd;
            CRUD.adaptador.Fill(CRUD.dsAlumno, "alumnos");

        }

        private void loadDataTutor()
        {

            CRUD.dsTutor.Clear();

            CRUD.sql = "SELECT * FROM tutoresdocentes";
            CRUD.cmd = new MySqlCommand(CRUD.sql, CRUD.con);
            CRUD.adaptador.SelectCommand = CRUD.cmd;
            CRUD.adaptador.Fill(CRUD.dsTutor, "tutoresdocentes");

        }

        private void onLoad(object sender, RoutedEventArgs e)
        {

            dv1.ItemsSource = null;
            loadData();
            dv1.ItemsSource = CRUD.dsEmpresa.Tables["empresa"].DefaultView;

            dv1_alumno.ItemsSource = null;
            loadDataAlumno();
            dv1_alumno.ItemsSource = CRUD.dsAlumno.Tables["alumnos"].DefaultView;

            dv1_tutor.ItemsSource = null;
            loadDataTutor();
            dv1_tutor.ItemsSource = CRUD.dsTutor.Tables["tutoresdocentes"].DefaultView;
        }

        private void boton_insertar_Click(object sender, RoutedEventArgs e)
        {
            Empresa e1 = new Empresa();
            Validaciones valid = new Validaciones();

            try
            {
                e1 = recuperaDatos();
                string sql = "Insert into empresa(COD_EMPRESA, NOM_EMPRESA, CIF, DIREC, LOCALI, CP, TIP_JORNADA, DNI_RESP, NOM_RESP, APELLIDO_RESP, DNI_TUTOR, NOM_TUTOR, APE_TUTOR, MAIL_TUTOR, TLF_TUTOR)values('" + e1.codEmpresa + "','" + e1.nomEmpresa + "','" + e1.cifEmpresa + "','" + e1.direccion + "','" + e1.localidad + "','" + e1.codPostal + "','" + e1.tipoJornada + "','" + e1.dniResp + "','" + e1.nombreResp + "','" + e1.apeResp + "','" + e1.dniTutor + "','" + e1.nomTutor + "','" + e1.apeTutor + "','" + e1.mailTutor + "','" + e1.tlfTutor + "')";

                if (valid.validarDNI(texto_DNI_resp.Text) && valid.validarDNI(texto_DNI_tutor.Text))
                {
                    if (valid.validarEmail(texto_email_tutor.Text))
                    {
                        if (valid.validarCP(texto_CP.Text))
                        {
                            if (valid.validarCIF(texto_CIF.Text))
                            {
                                if (valid.validarTelefono(texto_tlf_tutor.Text))
                                {
                                    if (valid.validarCamposTextos(texto_nombre_empresa.Text) && valid.validarCamposTextos(texto_nombre_empresa.Text) && valid.validarCamposTextos(texto_localidad.Text) && valid.validarCamposTextos(texto_nombre_resp.Text) && valid.validarCamposTextos(texto_nombre_tutor.Text) && valid.validarCamposTextos(texto_apellido_resp.Text) && valid.validarCamposTextos(texto_apellido_tutor.Text))
                                    {
                                        if (valid.validarCamposNumericos(texto_codigo.Text))
                                        {
                                            inserta(sql);
                                        } else
                                        {
                                            MessageBox.Show("Error de formato: Código de Empresa tiene carácteres");
                                        }
                                        
                                    } else
                                    {
                                        MessageBox.Show("Error de formato: Números en campos de texto");
                                    }
                                    
                                } else
                                {
                                    MessageBox.Show("Error de formato: Teléfono");
                                }
                                
                            } else
                            {
                                MessageBox.Show("Error de formato: CIF");
                            }
                            
                        } else
                        {
                            MessageBox.Show("Error de formato: CP");
                        }
                        
                    } else
                    {
                        MessageBox.Show("Error de formato: Email");
                    }
                    
                } else
                {
                    MessageBox.Show("Error de formato: DNI");
                }
                
                loadData();

                rellenarComboEmpresa();
                //vaciarCamposEmpresa();
            } catch (Exception)
            {

            }
        }

        private void boton_modificar_Click(object sender, RoutedEventArgs e)
        {
            Empresa e1 = new Empresa();
            e1 = recuperaDatos();
            string sql = "UPDATE empresa SET NOM_EMPRESA = '" + e1.nomEmpresa + "', CIF = '" + e1.cifEmpresa + "', DIREC = '" + e1.direccion + "', LOCALI = '" + e1.localidad + "', CP = '" + e1.codPostal + "', TIP_JORNADA = '" + e1.tipoJornada + "', DNI_RESP = '" + e1.dniResp + "', NOM_RESP = '" + e1.nombreResp + "', APELLIDO_RESP = '" + e1.apeResp + "', DNI_TUTOR = '" + e1.dniTutor + "', NOM_TUTOR = '" + e1.nomTutor + "', APE_TUTOR = '" + e1.apeTutor + "', MAIL_TUTOR = '" + e1.mailTutor + "', TLF_TUTOR = '" + e1.tlfTutor + "' WHERE COD_EMPRESA = " + e1.codEmpresa;

            modificar(sql);
            loadData();

            rellenarComboEmpresa();
            vaciarCamposEmpresa();
        }

        private void boton_borrar_Click(object sender, RoutedEventArgs e)
        {
            Empresa e1 = new Empresa();
            e1 = recuperaDatos();
            string sql = "DELETE FROM empresa WHERE COD_EMPRESA = " + e1.codEmpresa;

            eliminar(sql);
            loadData();

            rellenarComboEmpresa();
            vaciarCamposEmpresa();
        }

        private void boton_insertar_alumno1(object sender, RoutedEventArgs e)
        {
            Alumno a1 = new Alumno();
            a1 = recuperarDatosAlumnos();
            string sql = "Insert into alumnos(Codigo, DNI, Nombre, Apellidos, FechaNacimiento)values(" + a1.codAlumno + ",'" + a1.dniAlumno + "','" + a1.nombreAlumno + "','" + a1.apellidosAlumno + "','" + a1.fechaNacAlumno + "')";

            inserta(sql);
            loadDataAlumno();

            rellenarComboAlumno();
            vaciarCamposAlumno();
        }

        private void boton_modificar_alumno1(object sender, RoutedEventArgs e)
        {
            Alumno a1 = new Alumno();
            a1 = recuperarDatosAlumnos();
            string sql = "UPDATE alumnos SET DNI = '" + a1.dniAlumno + "', Nombre = '" + a1.nombreAlumno + "', Apellidos = '" + a1.apellidosAlumno + "', FechaNacimiento = '" + a1.fechaNacAlumno + "' WHERE codigo = " + a1.codAlumno;

            modificar(sql);
            loadDataAlumno();

            rellenarComboAlumno();
            vaciarCamposAlumno();
        }

        private void boton_borrar_alumno1(object sender, RoutedEventArgs e)
        {
            Alumno a1 = new Alumno();
            a1 = recuperarDatosAlumnos();
            string sql = "DELETE FROM alumnos WHERE codigo = " + a1.codAlumno;

            eliminar(sql);
            loadDataAlumno();

            rellenarComboAlumno();
            vaciarCamposAlumno();
        }

        private void boton_insertar_tutor1(object sender, RoutedEventArgs e)
        {
            Tutor t1 = new Tutor();
            t1 = recuperarDatosTutor();
            string sql = "Insert into tutoresdocentes(CodigoTutor, NombreApellido, Correo, Telefono) values (" + t1.codigoTutor + ",'" + t1.nombreApelTutor + "','" + t1.correoTutor + "'," + t1.telefonoTutor + ")";

            inserta(sql);
            loadDataTutor();

            rellenarComboTutor();
            vaciarCamposTutor();
        }

        private void boton_modificar_tutor1(object sender, RoutedEventArgs e)
        {
            Tutor t1 = new Tutor();
            t1 = recuperarDatosTutor();
            string sql = "UPDATE tutoresdocentes SET NombreApellido = '" + t1.nombreApelTutor + "', Correo = '" + t1.correoTutor + "', Telefono = " + t1.telefonoTutor + " WHERE CodigoTutor = " + t1.codigoTutor;

            modificar(sql);
            loadDataTutor();

            rellenarComboTutor();
            vaciarCamposTutor();
        }

        private void boton_borrar_tutor1(object sender, RoutedEventArgs e)
        {
            Tutor t1 = new Tutor();
            t1 = recuperarDatosTutor();
            string sql = "DELETE FROM tutoresdocentes WHERE CodigoTutor = " + t1.codigoTutor;

            eliminar(sql);
            loadDataTutor();

            rellenarComboTutor();
            vaciarCamposTutor();
        }


        public Empresa recuperaDatos()
        {
            Empresa emp = new Empresa();
            try
            {
                emp.codEmpresa = Convert.ToInt32(texto_codigo.Text);
                emp.nomEmpresa = texto_nombre_empresa.Text.Trim();
                emp.cifEmpresa = texto_CIF.Text.Trim();
                emp.direccion = texto_direccion.Text.Trim();
                emp.localidad = texto_localidad.Text.Trim();
                emp.codPostal = texto_CP.Text.Trim();
                emp.tipoJornada = combobox_jornada.Text.Trim();
                emp.dniResp = texto_DNI_resp.Text.Trim();
                emp.nombreResp = texto_nombre_resp.Text.Trim();
                emp.apeResp = texto_apellido_resp.Text.Trim();
                emp.dniTutor = texto_DNI_tutor.Text.Trim();
                emp.nomTutor = texto_nombre_tutor.Text.Trim();
                emp.apeTutor = texto_apellido_tutor.Text.Trim();
                emp.mailTutor = texto_email_tutor.Text.Trim();
                emp.tlfTutor = texto_tlf_tutor.Text.Trim();

            } catch (Exception e)
            {
                MessageBox.Show("Error durante la ejecución del programa");
            }

            return emp;
        }

        public Alumno recuperarDatosAlumnos()
        {
            Alumno alum = new Alumno();
            try
            {
                alum.codAlumno = Convert.ToInt32(texto_codigo_alumno.Text);
                alum.dniAlumno = texto_dni_alumno.Text.Trim();
                alum.nombreAlumno = texto_nombre_alumno.Text.Trim();
                alum.apellidosAlumno = texto_apellidos_alumno.Text.Trim();
                alum.fechaNacAlumno = texto_fechanacimiento_alumno.Text.Trim();
            } catch (Exception e)
            {
                MessageBox.Show("Error durante la ejecución del programa");
            }

            return alum;
        }

        public Tutor recuperarDatosTutor() 
        {
            Tutor tutor = new Tutor();
            try 
            {
                tutor.codigoTutor = Convert.ToInt32(texto_codigo_tutor.Text);
                tutor.nombreApelTutor = texto_nombreApel_tutor.Text.Trim();
                tutor.correoTutor = texto_correo_tutor.Text.Trim();
                tutor.telefonoTutor = texto_telefono_tutor.Text.Trim();
            } catch(Exception e) 
            {
                MessageBox.Show("Error durante la ejecución del programa");
            }

            return tutor;
        }

        public Boolean inserta(string sql)
        {
            MySqlConnection conexionBD = CRUD.con;
            conexionBD.Open();
            Boolean insertado = true;
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro guardado");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guardar el registro");
                insertado = false;

            }
            conexionBD.Close();
            return insertado;
        }
        
        public Boolean insertaAsignado(string sql)
        {
            MySqlConnection conexionBD = CRUD.con;
            conexionBD.Open();
            Boolean insertado = true;
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro guardado");

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("El alumno ya está asignado");
                insertado = false;

            }
            conexionBD.Close();
            return insertado;
        }

        public Boolean modificar(string sql)
        {
            MySqlConnection conexionBD = CRUD.con;
            conexionBD.Open();

            Boolean modificado = true;
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro modificado");

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al modificar el registro");
                modificado = false;

            }
            conexionBD.Close();
            return modificado;
        }

        public Boolean eliminar(string sql)
        {
            MySqlConnection conexionBD = CRUD.con;
            conexionBD.Open();

            Boolean eliminado = true;
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro eliminado");

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al eliminar el registro");
                eliminado = false;

            }
            conexionBD.Close();
            return eliminado;
        }

        public void rellenarComboAlumno()
        {
            CRUD.sql = "SELECT Codigo, Nombre, Apellidos FROM alumnos";
            CRUD.cmd = new MySqlCommand(CRUD.sql, CRUD.con);

            MySqlConnection conexionBD = CRUD.con;
            conexionBD.Open();
            MySqlDataReader sqlReader = CRUD.cmd.ExecuteReader();

            combobox_alumno.Items.Clear();
            while (sqlReader.Read())
            {
                combobox_alumno.Items.Add(Convert.ToString(sqlReader["Nombre"] + " " + sqlReader["Apellidos"]));
            }

            sqlReader.Close();
            conexionBD.Close();
        }
        
        public void rellenarComboEmpresa()
        {
            CRUD.sql = "SELECT NOM_EMPRESA FROM empresa";
            CRUD.cmd = new MySqlCommand(CRUD.sql, CRUD.con);

            MySqlConnection conexionBD = CRUD.con;
            conexionBD.Open();
            MySqlDataReader sqlReader = CRUD.cmd.ExecuteReader();

            combobox_empresa.Items.Clear();
            while (sqlReader.Read())
            {
                combobox_empresa.Items.Add(Convert.ToString(sqlReader["NOM_EMPRESA"]));
            }

            sqlReader.Close();
            conexionBD.Close();
        }
        public void rellenarComboTutor()
        {
            CRUD.sql = "SELECT NombreApellido FROM tutoresdocentes";
            CRUD.cmd = new MySqlCommand(CRUD.sql, CRUD.con);

            MySqlConnection conexionBD = CRUD.con;
            conexionBD.Open();
            MySqlDataReader sqlReader = CRUD.cmd.ExecuteReader();

            combobox_tutor.Items.Clear();
            while (sqlReader.Read())
            {
                combobox_tutor.Items.Add(Convert.ToString(sqlReader["NombreApellido"]));
            }

            sqlReader.Close();
            conexionBD.Close();
        }

        private void boton_asignar_Click(object sender, RoutedEventArgs e)
        {
            string alumnoSelec = combobox_alumno.Text.Trim();
            string empresaSelec = combobox_empresa.Text.Trim();
            string tutorSelec = combobox_tutor.Text.Trim();

            string salidaTexto = "El alumno " + alumnoSelec + ", queda asignado a la empresa " + empresaSelec + " supervisado por el tutor " + tutorSelec + ".";
            texto_asignacion.Clear();
            texto_asignacion.AppendText(salidaTexto);
          
            string sql = "Insert into asignados (NombreAlumno, NombreTutor, NombreEmpresa) values ('"+alumnoSelec+"', '"+tutorSelec+"', '"+empresaSelec+"')";

            insertaAsignado(sql);
        }

        public void vaciarCamposEmpresa()
        {
            texto_codigo.Clear();
            texto_nombre_empresa.Clear();
            texto_CIF.Clear();
            texto_direccion.Clear();
            texto_localidad.Clear();
            texto_CP.Clear();
            texto_nombre_resp.Clear();
            texto_apellido_resp.Clear();
            texto_DNI_resp.Clear();
            texto_nombre_tutor.Clear();
            texto_apellido_tutor.Clear();
            texto_DNI_tutor.Clear();
            texto_email_tutor.Clear();
            texto_tlf_tutor.Clear();
        }

        public void vaciarCamposAlumno()
        {
            texto_codigo_alumno.Clear();
            texto_dni_alumno.Clear();
            texto_nombre_alumno.Clear();
            texto_apellidos_alumno.Clear();
            texto_fechanacimiento_alumno.Clear();

        }

        public void vaciarCamposTutor()
        {
            texto_codigo_tutor.Clear();
            texto_nombreApel_tutor.Clear();
            texto_correo_tutor.Clear();
            texto_telefono_tutor.Clear();
        }

    }
}
