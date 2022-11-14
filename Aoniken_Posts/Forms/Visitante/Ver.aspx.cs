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
    public partial class Ver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BuscarPost();
        }


        private void BuscarPost()
        {
            string IdPost = Request.QueryString["id"].ToString();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectiondb"].ToString()))
            {

                cn.Open();
                
                SqlCommand cmd = new SqlCommand($" SELECT TITULO, DESC_POST, APELLIDO_NOMBRE FROM POSTS P  INNER JOIN USUARIOS U ON P.ID_USUARIO_CREA = U.ID_USUARIO WHERE ID_POST =" + IdPost, cn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    lblTitulo.Text = dr["TITULO"].ToString();
                    lblAutor.Text = dr["APELLIDO_NOMBRE"].ToString();
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
            Response.Redirect($"~/Forms/Visitante/PostsVisitante.aspx?{Request.QueryString.ToString()}");
        }
    }
}