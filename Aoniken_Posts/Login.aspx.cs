using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aoniken_Posts
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            LimpiarSesion();
        }


        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (BuscarUsuario())
                {
                    if (Convert.ToInt32( Session["ROL"]) == 1)
                    {
                        Response.Redirect("~/Forms/Editor/PostsEditor.aspx");
                    } 
                    else
                    {
                        Response.Redirect("~/Forms/Escritor/PostsEscritor.aspx");
                    }
                }
                else
                {

                    cvError.IsValid = false;
                }

            }
        }

        private  bool BuscarUsuario()
        {
            bool condicion;
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectiondb"].ToString()))
            {

                cn.Open();

                SqlCommand cmd = new SqlCommand($"SELECT ID_USUARIO, APELLIDO_NOMBRE, USUARIO, ID_ROL FROM USUARIOS WHERE USUARIO = '{txtUsuario.Text}' AND PASSWORD = '{txtPass.Text}'", cn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    LoguearUsuario(Convert.ToInt32( dr["ID_USUARIO"]), dr["APELLIDO_NOMBRE"].ToString(), Convert.ToInt32( dr["ID_ROL"]));
                    condicion = true;
                }
                else
                {
                    condicion = false;
                }
                cn.Close();
                return condicion;
            }
        }

        private void LoguearUsuario(int ID, string NOMBRE, int ROL)
        {
            Session.Add("ID", ID);
            Session.Add("NOMBRE", NOMBRE);
            Session.Add("ROL", ROL);
            Session.Timeout = 240;
        }
        private void LimpiarSesion()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
        }
    }
}