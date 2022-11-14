using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aoniken_Posts.Forms
{
    public partial class PostsVisitante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {

            if (!Page.IsPostBack) 
            {
                CargarEscritores();
                CargarListado();
            }
            else
            {
                CargarListado();
            }
        }

        private void CargarListado()
        {
            //Carga el listado de posts aprobado, el usuario anonimo tambien puede filtrar por escritor
            string sql = "SELECT ID_POST, TITULO, LEFT(DESC_POST,73) + '...' DESC_POST, CONVERT(VARCHAR,F_APROBACION,103) F_APROBACION, APELLIDO_NOMBRE AS AUTOR  FROM POSTS P ";
            sql += "  INNER JOIN USUARIOS U ON P.ID_USUARIO_CREA = U.ID_USUARIO WHERE ID_ESTADO_POST = 2";
           
            
            if (ddlEscritores.SelectedValue != "-1" && ddlEscritores.SelectedValue != "")
            {
                sql += $" AND P.ID_USUARIO_CREA = {ddlEscritores.SelectedValue} ";
            }
            sql += "  ORDER BY F_APROBACION DESC, ID_POST DESC ";
            sqlPosts.SelectCommand = sql;
            lvPosts.DataBind();
        }
       
        private void CargarEscritores()
        {   
         string sql = "SELECT -1 ID_USUARIO,'(Todos los escritores)' APELLIDO_NOMBRE UNION ";
         sql += " SELECT ID_USUARIO, APELLIDO_NOMBRE FROM USUARIOS U WHERE ID_ROL = 2 AND ID_USUARIO IN (SELECT ID_USUARIO_CREA FROM POSTS  WHERE ID_ESTADO_POST = 2 ) ";
         sqlEscritores.SelectCommand = sql;

            if (Request.QueryString["est"] != null)
            {
                ddlEscritores.SelectedValue = Request.QueryString["est"].ToString();
                ddlEscritores.DataBind();

            }
        }

        protected void lvPosts_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "ver")
            {
                Response.Redirect($"~/Forms/Visitante/Ver.aspx?id={e.CommandArgument}&ver=1&est={ddlEscritores.SelectedValue}");
            }

        }
    }
}
