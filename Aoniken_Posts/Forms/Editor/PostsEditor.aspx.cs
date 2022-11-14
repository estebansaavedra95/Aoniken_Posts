using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aoniken_Posts.Forms
{
    public partial class PostsEditor : System.Web.UI.Page
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

        }
        protected void Page_PreRender(object sender, EventArgs e)
            
        {
            //Se cargan los escritores para filtrar los posts y tambien se carga el listado de posts.
            if (!Page.IsPostBack) //Primera vez que carga la pagina
            {

                CargarEscritores();
                CargarListado();
            } else
            {
                CargarListado();
            }
         
        }


        protected void btnAprobar_Click(object sender, EventArgs e)
        {
            var IdPost = hfPost.Value;
            AprobarPost(Convert.ToInt32(IdPost));
            lvPosts.DataBind();
        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            var IdPost = hfPost.Value;
            RechazarPost(Convert.ToInt32(IdPost));
            lvPosts.DataBind();

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            var IdPost = hfPost.Value;
            EliminarPost(Convert.ToInt32(IdPost));
            lvPosts.DataBind();
        }

        private void AprobarPost(int Id)
        {
            //Se actualiza el post en estado 2(aprobado) y se guarda la fecha en que se hace, con un id que está oculto.
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectiondb"].ToString()))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand($"UPDATE POSTS SET ID_ESTADO_POST = 2, F_APROBACION = '{DateTime.Now.ToString("yyyy-MM-dd")}' WHERE ID_POST = {Id}", cn);
                cmd.ExecuteNonQuery();

            }
        }
        private void RechazarPost (int Id)
        {
            //Se actualiza el post en estado 3(rechazado) y se guarda la fecha en que se hace, con un id que está oculto.
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectiondb"].ToString()))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand($"UPDATE POSTS SET ID_ESTADO_POST = 3, F_RECHAZO = '{DateTime.Now.ToString("yyyy-MM-dd")}' WHERE ID_POST = {Id}", cn);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        private void EliminarPost(int Id)
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectiondb"].ToString()))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand($"DELETE FROM POSTS WHERE ID_POST = {Id}", cn);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        private void CargarListado()
        {
            //Se realiza la carga de los posts, si se elige un escritor, se filtra por el mismo. Busca los posts con estado 1 (pendientes de aprobacion)
            string sql = "SELECT ID_POST, LEFT(DESC_POST,73) + '...' DESC_POST, TITULO, CONVERT(VARCHAR,F_CREACION,103)F_CREACION , APELLIDO_NOMBRE AS AUTOR FROM POSTS P ";
                   sql +=" INNER JOIN USUARIOS U ON P.ID_USUARIO_CREA = U.ID_USUARIO WHERE ID_ESTADO_POST = 1 ";


            if (ddlEscritores.SelectedValue != "-1" && ddlEscritores.SelectedValue != "")
            {
                sql += $" AND P.ID_USUARIO_CREA = {ddlEscritores.SelectedValue} ";
            }
            sql += "  ORDER BY F_CREACION DESC, ID_POST DESC ";
            sqlPosts.SelectCommand = sql;
            lvPosts.DataBind();

        }
        private void CargarEscritores()
        {
            //Se cargan los escritores que tienen blogs creados y aprobados (estado 2)
            string sql = "SELECT -1 ID_USUARIO,'(Todos los escritores)' APELLIDO_NOMBRE UNION ";
            sql += " SELECT ID_USUARIO, APELLIDO_NOMBRE FROM USUARIOS U WHERE ID_ROL = 2 AND ID_USUARIO IN (SELECT ID_USUARIO_CREA FROM POSTS  WHERE ID_ESTADO_POST = 2 ) ";
            sqlEscritores.SelectCommand = sql;

            if (Request.QueryString["esc"] != null)
            {
                ddlEscritores.SelectedValue = Request.QueryString["esc"].ToString();
                ddlEscritores.DataBind();

            }
        }

        protected void lvPosts_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //Item command del listado, redirige con el id correspondiente y con el filtro de escritores para que, en caso de volver, se mantenga el filtro
            if (e.CommandName == "ver")
            {
                Response.Redirect($"~/Forms/Editor/VerPost.aspx?id={e.CommandArgument}&esc={ddlEscritores.SelectedValue}");
            }
        }
    }
}