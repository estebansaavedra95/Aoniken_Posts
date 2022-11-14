using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aoniken_Posts.Forms.Editor
{
    public partial class GestionPost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ID"].ToString() == "")
            {
                Response.Redirect("~/Login.aspx");
            }
            ContentPlaceHolder mpContentPlaceHolder;
            Label Editor;
            mpContentPlaceHolder = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (mpContentPlaceHolder != null)
            {
                Editor = (Label)mpContentPlaceHolder.FindControl("lblEditor");
                if (Editor != null)
                {
                    Editor.Text = $"¡BIENVENIDO, ESCRITOR {Session["Nombre"]}!";
                }
            }

           
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //Se utiliza la misma pagina para ver y editar los datos, dependiendo siempre del estado del post.
            string ver = Request.QueryString["ver"].ToString();
            BuscarPost();
            if (ver == "1")
            {
                DeshabilitarCampos();
            }
        }



        private void BuscarPost()
        {
            string IdPost = Request.QueryString["id"].ToString();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectiondb"].ToString()))
            {

                cn.Open();
                
                SqlCommand cmd = new SqlCommand($"SELECT TITULO, DESC_POST FROM POSTS WHERE ID_POST ="+ IdPost, cn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtTitulo.Text = dr["TITULO"].ToString();
                    txtDescripcion.Text = dr["DESC_POST"].ToString();
                    btnGuardar.Text = "Modificar";
                }
                else
                {
                    btnGuardar.Text = "Guardar";
                }
                cn.Close();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string IdPost = Request.QueryString["id"].ToString();
            if (IdPost == "0")
            {
                GuardarPost();
            }
            else
            {
                ModificarPost(IdPost);
            }
            
            Response.Redirect($"~/Forms/Escritor/PostsEscritor.aspx?{Request.QueryString.ToString()}");
        }

        private void GuardarPost()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectiondb"].ToString()))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand($"INSERT INTO POSTS (DESC_POST, TITULO, ID_USUARIO_CREA) VALUES ('{txtDescripcion.Text}', '{txtTitulo.Text}', {Session["ID"]} )", cn);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        private void ModificarPost(string Id)
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectiondb"].ToString()))
            {
                cn.Open();
                var sql = $"UPDATE POSTS SET TITULO = '{txtTitulo.Text}', DESC_POST = '{txtDescripcion.Text}' WHERE ID_POST = {Id}";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        private void DeshabilitarCampos()
        {
            txtDescripcion.Enabled = false;
            txtTitulo.Enabled = false;
            btnGuardar.Visible = false;

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect($"~/Forms/Escritor/PostsEscritor.aspx?{Request.QueryString.ToString()}");
        }
    }
}