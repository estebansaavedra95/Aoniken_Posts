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
    public partial class VerPost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Se comprueba si la sesion sigue activa o en caso de que quiera entrar sin un logueo no lo permitiria.

            if (Session["ID"].ToString() == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            //Cambio un label de la MasterPage para diferenciar entre editor y escritor
            ContentPlaceHolder mpContentPlaceHolder;
            Label Editor;
            mpContentPlaceHolder = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            if (mpContentPlaceHolder != null)
            {
                Editor = (Label)mpContentPlaceHolder.FindControl("lblEditor");
                if (Editor != null)
                {
                    Editor.Text = $"¡BIENVENIDO, EDITOR {Session["Nombre"]}!";
                }
            }
            BuscarPost();
        }


        private void BuscarPost()
        {
            //Se realiza la busqueda del post a traves del query enviado en la pagina PostsEditor y luego, con su informacion, se completan los campos con los datos traidos de la bd.
            string IdPost = Request.QueryString["id"].ToString();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectiondb"].ToString()))
            {

                cn.Open();
                
                SqlCommand cmd = new SqlCommand($"SELECT TITULO, DESC_POST FROM POSTS WHERE ID_POST ="+ IdPost, cn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    lblTitulo.Text = dr["TITULO"].ToString();
                    txtDescripcion.Text = dr["DESC_POST"].ToString();
                }
                else
                {
                    lblTitulo.Text = "Post no encontrado.";
                }
                cn.Close();
            }
        }

        protected void lnkVolver_Click(object sender, EventArgs e)
        {
            //Vuelve a la pagina anterior manteniendo los filtros.
            Response.Redirect($"~/Forms/Editor/PostsEditor.aspx?{ Request.QueryString.ToString() }");
        }
    }
}