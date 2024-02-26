using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using Button = System.Web.UI.WebControls.Button;

namespace anteproyecto
{
    public partial class index : System.Web.UI.Page
    {
        MySqlConnection conexion = new MySqlConnection("DataBase=anteproyecto;DataSource=localhost;user=root;Port=3306");

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioActual"] != null)
            {
                lblNombreUsuario.Text = Session["UsuarioActual"].ToString();
            }
            if (Session["UsuarioActual"] != null)
            {
                lblNombreUsuario2.Text = Session["UsuarioActual"].ToString();
            }


            lnkLogout.ServerClick += lnkLogout_Click;
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("index.aspx");
        }


        protected void limpiar()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            string nombreUsuario = TextBox1.Text;
            string contraseña = TextBox2.Text;

            try
            {
                using (MySqlConnection conexion = new MySqlConnection("DataBase=anteproyecto;DataSource=localhost;user=root;Port=3306"))
                {
                    conexion.Open();

                    string consulta = "SELECT COUNT(*) FROM usuarios WHERE nombreUsuario = @nombreUsuario AND contraseña = @contraseña";

                    using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                        comando.Parameters.AddWithValue("@contraseña", contraseña);

                        int count = Convert.ToInt32(comando.ExecuteScalar());

                        if (count > 0)
                        {
                            Session["UsuarioActual"] = nombreUsuario;
                            MultiView1.ActiveViewIndex = 2;
                        }
                        else
                        {
                            Response.Write("<script>alert('Nombre de usuario y/o contraseña incorrectos.');</script>");
                        }
                    }
                }
            }
            catch (Exception falloInicioSesion)
            {
                Response.Write("<script>alert('Error al intentar iniciar sesión.');</script>");
            }
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1; 

        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensajeError.Text = "";

                string nombreUsuario = TextBox3.Text.Trim();
                string contraseña = TextBox4.Text.Trim();

                if (UsuarioExiste(nombreUsuario))
                {
                    TextBox3.CssClass = "error-textbox";
                    lblMensajeError.Text = "El nombre de usuario ya está registrado.";
                    return;
                }

                if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contraseña))
                {
                    TextBox3.CssClass = "error-textbox";
                    TextBox4.CssClass = "error-textbox";
                    lblMensajeError.Text = "El nombre de usuario o contraseña está vacío.";
                    return;
                }

                if (!ValidarContraseña(contraseña))
                {
                    TextBox4.CssClass = "error-textbox";
                    lblMensajeError.Text = "La contraseña debe tener al menos 4 caracteres, incluyendo al menos una mayúscula, una minúscula y un número.";
                    return;
                }

                MySqlCommand comando1 = new MySqlCommand("INSERT INTO usuarios VALUES(NULL, @nombreUsuario, @contraseña)", conexion);
                MySqlParameter nombre = comando1.Parameters.Add("@nombreUsuario", MySqlDbType.String);
                MySqlParameter dni = comando1.Parameters.Add("@contraseña", MySqlDbType.String);

                if (!TextBox5.Text.Equals(TextBox4.Text))
                {
                    lblMensajeError.Text = "Las contraseñas no coinciden.";
                    return;
                }

                nombre.Value = nombreUsuario;
                dni.Value = contraseña;

                conexion.Open();
                comando1.ExecuteNonQuery();
                conexion.Close();
                MultiView1.ActiveViewIndex = 0;
            }
            catch (Exception ex)
            {
                lblMensajeError.Text = "Error al registrar el usuario.";
            }
            finally
            {
                limpiar();
            }
        }



        private bool ValidarContraseña(string contraseña)
        {
            string expresionRegular = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{4,}$";
            return Regex.IsMatch(contraseña, expresionRegular);
        }

        private bool UsuarioExiste(string nombreUsuario)
        {
            MySqlCommand comando = new MySqlCommand("SELECT COUNT(*) FROM usuarios WHERE nombreUsuario = @nombreUsuario", conexion);
            comando.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
            conexion.Open();
            int count = Convert.ToInt32(comando.ExecuteScalar());
            conexion.Close();
            return count > 0;
        }


        protected void Button4_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;

        }


        protected void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UsuarioActual"] != null)
                {
                    string nombreUsuario = Session["UsuarioActual"].ToString();
                    string contenido = TextBox6.Text;

                    int idUsuario = ObtenerIdUsuario(nombreUsuario);

                    DateTime fechaActual = DateTime.Now;

                    using (MySqlConnection conexion = new MySqlConnection("DataBase=anteproyecto;DataSource=localhost;user=root;Port=3306"))
                    {
                        conexion.Open();

                        string consultaExistencia = "SELECT id_entrada FROM entradas WHERE id_usuario = @idUsuario AND DATE(fecha) = @fechaActual";
                        using (MySqlCommand comandoExistencia = new MySqlCommand(consultaExistencia, conexion))
                        {
                            comandoExistencia.Parameters.AddWithValue("@idUsuario", idUsuario);
                            comandoExistencia.Parameters.AddWithValue("@fechaActual", fechaActual.ToString("yyyy-MM-dd"));

                            object idEntradaExistente = comandoExistencia.ExecuteScalar();

                            if (idEntradaExistente != null)
                            {
                                int idEntrada = Convert.ToInt32(idEntradaExistente);
                                string consultaActualizar = "UPDATE entradas SET contenido = @contenido WHERE id_entrada = @idEntrada";
                                using (MySqlCommand comandoActualizar = new MySqlCommand(consultaActualizar, conexion))
                                {
                                    comandoActualizar.Parameters.AddWithValue("@idEntrada", idEntrada);
                                    comandoActualizar.Parameters.AddWithValue("@contenido", contenido);

                                    comandoActualizar.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string consultaInsercion = "INSERT INTO entradas (id_usuario, fecha, contenido) VALUES (@idUsuario, @fecha, @contenido)";
                                using (MySqlCommand comandoInsercion = new MySqlCommand(consultaInsercion, conexion))
                                {
                                    comandoInsercion.Parameters.AddWithValue("@idUsuario", idUsuario);
                                    comandoInsercion.Parameters.AddWithValue("@fecha", fechaActual);
                                    comandoInsercion.Parameters.AddWithValue("@contenido", contenido);
                                    comandoInsercion.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                }
                else
                {
                    Response.Write("<script>alert('Usuario no autenticado.');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al intentar guardar la entrada. Detalles: {ex.Message}');</script>");
            }
            finally
            {
                if (Session["UsuarioActual"] != null)
                {
                    string nombreUsuario = Session["UsuarioActual"].ToString();
                    lblNombreUsuario2.Text = nombreUsuario;


                    int idUsuario = ObtenerIdUsuario(nombreUsuario);


                    MostrarEntradasUsuario(idUsuario);

                    MultiView1.ActiveViewIndex = 3;
                }
                else
                {
                    Response.Write("<script>alert('Usuario no autenticado.');</script>");
                }
            }

        }



        private int ObtenerIdUsuario(string nombreUsuario)
        {
            int idUsuario = 0;

            try
            {
                using (MySqlConnection conexion = new MySqlConnection("DataBase=anteproyecto;DataSource=localhost;user=root;Port=3306"))
                {
                    conexion.Open();

                    string consulta = "SELECT id_usuario FROM usuarios WHERE nombreUsuario = @nombreUsuario";

                    using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

                        object resultado = comando.ExecuteScalar();

                        if (resultado != null)
                        {
                            idUsuario = Convert.ToInt32(resultado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al obtener el id_usuario.');</script>");
            }

            return idUsuario;
        }


        protected void Button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UsuarioActual"] != null)
                {
                    string nombreUsuario = Session["UsuarioActual"].ToString();
                    lblNombreUsuario2.Text = nombreUsuario;

                    int idUsuario = ObtenerIdUsuario(nombreUsuario);

                    MostrarEntradasUsuario(idUsuario);

                    MultiView1.ActiveViewIndex = 3;
                }
                else
                {
                    Response.Write("<script>alert('Usuario no autenticado.');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar las entradas.');</script>");
            }
        }


        private void MostrarEntradasUsuario(int idUsuario)
        {
            Literal1.Text = "";
            try
            {
                using (MySqlConnection conexion = new MySqlConnection("DataBase=anteproyecto;DataSource=localhost;user=root;Port=3306"))
                {
                    conexion.Open();

                    string consulta = "SELECT e.id_entrada, e.fecha, e.contenido, f.imagen FROM entradas e LEFT JOIN fotos f ON e.id_entrada = f.id_entrada WHERE e.id_usuario = @idUsuario ORDER BY e.fecha DESC";

                    using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@idUsuario", idUsuario);

                        using (MySqlDataReader reader = comando.ExecuteReader())
                        {
                            Dictionary<DateTime, Tuple<string, List<string>>> entradasFotosContenido = new Dictionary<DateTime, Tuple<string, List<string>>>();

                            while (reader.Read())
                            {
                                int idEntrada = Convert.ToInt32(reader["id_entrada"]);
                                DateTime fecha = Convert.ToDateTime(reader["fecha"]);
                                string contenido = reader["contenido"].ToString();
                                string rutaImagen = reader["imagen"] as string;

                                if (!entradasFotosContenido.ContainsKey(fecha))
                                {
                                    entradasFotosContenido[fecha] = Tuple.Create(contenido, new List<string>());
                                }

                                if (!string.IsNullOrEmpty(rutaImagen))
                                {
                                    entradasFotosContenido[fecha].Item2.Add(rutaImagen);
                                }
                            }

                            foreach (var entradaFotoContenido in entradasFotosContenido)
                            {
                                DateTime fecha = entradaFotoContenido.Key;
                                Tuple<string, List<string>> contenidoYRutas = entradaFotoContenido.Value;

                                string fechaFormateada = fecha.ToString("dd/MM/yyyy");
                                Literal1.Text += $"<div class='mi-clase-especial'>";
                                Literal1.Text += $"<p>{fechaFormateada}: <br />{contenidoYRutas.Item1}</p>";

                                foreach (var rutaImagen in contenidoYRutas.Item2)
                                {
                                    Literal1.Text += $"<img src='{rutaImagen}' alt='Imagen asociada' loading='lazy'>";
                                }

                                Literal1.Text += $"</div>";
                            }
                        }
                    }
                }
                Literal1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al mostrar las entradas del usuario. Detalles: {ex.Message}');</script>");
            }
        }


        protected void EventoDelBoton(object sender, EventArgs e)
        {
        }


        protected void Button7_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["UsuarioActual"] != null)
                {

                    DateTime fechaSeleccionada = Calendar1.SelectedDate;


                    string nombreUsuario = Session["UsuarioActual"].ToString();


                    int idUsuario = ObtenerIdUsuario(nombreUsuario);

                    int idEntrada = ObtenerIdEntradaPorFechaYUsuario(fechaSeleccionada, idUsuario);
                    if (Calendar1.SelectedDate != DateTime.MinValue)
                    {
                        if (FileUpload1.HasFile)
                        {
                            GuardarImagenEnBaseDeDatos(FileUpload1, idEntrada);
                            meterImagenEnProyecto(FileUpload1);
                        }
                        else
                        {
                            Response.Write("<script>alert('Selecciona una imagen.');</script>");

                        }

                    }
                    else
                    {
                        Response.Write("<script>alert('Selecciona una dia en el calendario.');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Usuario no autenticado.');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }


        protected void GuardarImagenEnBaseDeDatos(FileUpload fileUpload, int idEntrada)
        {

            string nombreUsuario = Session["UsuarioActual"].ToString();
            using (MySqlConnection conexion = new MySqlConnection("DataBase=anteproyecto;DataSource=localhost;user=root;Port=3306"))
            {
                conexion.Open();

                using (MySqlCommand comandoInsertar = new MySqlCommand("INSERT INTO fotos (id_entrada, imagen) VALUES (@idEntrada, @rutaImagen)", conexion))
                {
                    comandoInsertar.Parameters.AddWithValue("@idEntrada", idEntrada);
                    comandoInsertar.Parameters.AddWithValue("@rutaImagen", ObtenerRutaImagen(FileUpload1));

                    comandoInsertar.ExecuteNonQuery();
                }
            }

        }


        private string ObtenerRutaImagen(FileUpload fileUpload)
        {
            string nombreArchivo = Path.GetFileName(fileUpload.FileName);

            return "imagenesProyecto/" + nombreArchivo;
        }

        protected void LinkButtonView1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;

        }


        protected void LinkButtonView2_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;

        }


        protected void LinkButtonView3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UsuarioActual"] != null)
                {
                    MultiView1.ActiveViewIndex = 2;
                }
                else
                {
                    Response.Write("<script>alert('Usuario no autenticado.');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar las entradas.');</script>");
            }

        }


        protected void LinkButtonView4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UsuarioActual"] != null)
                {
                    MultiView1.ActiveViewIndex = 3;
                    try
                    {
                        if (Session["UsuarioActual"] != null)
                        {
                            string nombreUsuario = Session["UsuarioActual"].ToString();
                            lblNombreUsuario2.Text = nombreUsuario;

                            int idUsuario = ObtenerIdUsuario(nombreUsuario);

                            MostrarEntradasUsuario(idUsuario);

                            MultiView1.ActiveViewIndex = 3;
                        }
                        else
                        {
                            Response.Write("<script>alert('Usuario no autenticado.');</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error al cargar las entradas.');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Usuario no autenticado.');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar las entradas.');</script>");
            }

        }


        protected int ObtenerIdEntradaPorFechaYUsuario(DateTime fechaSeleccionada, int IdnombreUsuario)
        {
            int idEntrada = 0;

            try
            {
                using (MySqlConnection conexion = new MySqlConnection("DataBase=anteproyecto;DataSource=localhost;user=root;Port=3306"))
                {
                    conexion.Open();

                    string consulta = "SELECT `id_entrada` FROM `entradas` where`fecha` = @fechaSeleccionada AND `id_usuario` = @idNombreUsuario";

                    using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@fechaSeleccionada", fechaSeleccionada);
                        comando.Parameters.AddWithValue("@idNombreUsuario", IdnombreUsuario);

                        object resultado = comando.ExecuteScalar();

                        if (resultado != null)
                        {
                            idEntrada = Convert.ToInt32(resultado);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al obtener el id_entrada.');</script>");
            }

            return idEntrada;
        }

        protected void meterImagenEnProyecto(FileUpload archivo)
        {
            try
            {
                if (archivo.HasFile)
                {
                    string rutaDestino = Server.MapPath("~/imagenesProyecto");

                    if (!Directory.Exists(rutaDestino))
                    {
                        Directory.CreateDirectory(rutaDestino);
                    }

                    string nombreArchivo = Path.GetFileName(archivo.FileName);

                    string rutaCompletaDestino = Path.Combine(rutaDestino, nombreArchivo);

                    archivo.SaveAs(rutaCompletaDestino);

                    if (Session["UsuarioActual"] != null)
                    {
                        string nombreUsuario = Session["UsuarioActual"].ToString();
                        int idUsuario = ObtenerIdUsuario(nombreUsuario);
                        MostrarEntradasUsuario(idUsuario);
                    }

                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al subir la imagen. Detalles: {ex.Message}');</script>");
            }
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UsuarioActual"] != null)
                {
                    DateTime fechaSeleccionada = Calendar1.SelectedDate;

                    string nombreUsuario = Session["UsuarioActual"].ToString();

                    int idUsuario = ObtenerIdUsuario(nombreUsuario);

                    int idEntrada = ObtenerIdEntradaPorFechaYUsuario(fechaSeleccionada, idUsuario);

                    if (Calendar1.SelectedDate != DateTime.MinValue)
                    {
                        BorrarImagen(idEntrada);

                    }
                    else
                    {
                        Response.Write("<script>alert('Selecciona un dia para borrar la imagen.');</script>");


                    }
                    MostrarEntradasUsuario(idUsuario);

                }
                else
                {
                    Response.Write("<script>alert('Usuario no autenticado.');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }

        }

        protected void BorrarImagen(int idEntrada)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection("DataBase=anteproyecto;DataSource=localhost;user=root;Port=3306"))
                {
                    conexion.Open();

                    string consultaEliminarImagen = "DELETE FROM fotos WHERE id_entrada = @idEntrada";

                    using (MySqlCommand comandoEliminarImagen = new MySqlCommand(consultaEliminarImagen, conexion))
                    {
                        comandoEliminarImagen.Parameters.AddWithValue("@idEntrada", idEntrada);
                        comandoEliminarImagen.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al intentar eliminar la imagen. Detalles: {ex.Message}');</script>");
            }
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UsuarioActual"] != null)
                {
                    DateTime fechaSeleccionada = Calendar1.SelectedDate;

                    string nombreUsuario = Session["UsuarioActual"].ToString();

                    int idUsuario = ObtenerIdUsuario(nombreUsuario);

                    int idEntrada = ObtenerIdEntradaPorFechaYUsuario(fechaSeleccionada, idUsuario);

                    if (Calendar1.SelectedDate != DateTime.MinValue)
                    {

                        BorrarEntrada(idEntrada);
                    }
                    else
                    {
                        Response.Write("<script>alert('Selecciona un dia para borrar la entrada.');</script>");

                    }

                    MostrarEntradasUsuario(idUsuario);
                }
                else
                {
                    Response.Write("<script>alert('Usuario no autenticado.');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        protected void BorrarEntrada(int idEntrada)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection("DataBase=anteproyecto;DataSource=localhost;user=root;Port=3306"))
                {
                    conexion.Open();

                    string consultaEliminarEntrada = "DELETE FROM entradas WHERE id_entrada = @idEntrada";

                    using (MySqlCommand comandoEliminarEntrada = new MySqlCommand(consultaEliminarEntrada, conexion))
                    {
                        comandoEliminarEntrada.Parameters.AddWithValue("@idEntrada", idEntrada);
                        comandoEliminarEntrada.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error al intentar borrar la entrada. Detalles: {ex.Message}');</script>");
            }
        }


    }
}