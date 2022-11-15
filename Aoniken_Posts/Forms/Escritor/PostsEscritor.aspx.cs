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
    public partial class PostsEscritor : System.Web.UI.Page
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
                    Editor.Text = $"¡BIENVENIDO, ESCRITOR {Session["Nombre"]}!";
                }
            }
        }


        protected void Page_PreRender(object sender, EventArgs e)
        {
            //Se cargan los estados para filtrar los posts y tambien se carga el listado de posts.
            if (!Page.IsPostBack) //Pasa la primera vez que carga la pagina
            {
                CargarEstados();
                CargarListado();
            }
            else
            {
                CargarListado();
            }

        }


        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            var IdPost = hfPost.Value;
            ModificarEstado(Convert.ToInt32(IdPost), 1);
            lvPosts.DataBind();
        }
        private void ModificarEstado(int Id, int Accion)
        {
            try 
            {
                using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectiondb"].ToString()))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand($"UPDATE POSTS SET ID_ESTADO_POST = {Accion} WHERE ID_POST = {Id}", cn);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Ocurrió un error al intentar enviar el post.');", true);
            }

          
        }

        private void CargarEstados()
        {
            //Se carga un listado de post traido de la bd que nos sirvira para filtrar los posts del escritor autenticado
            string sql = "SELECT -1 ID_ESTADO_POST, '(Todos los estados)' DESC_ESTADO UNION SELECT ID_ESTADO_POST, DESC_ESTADO FROM ESTADOS_POSTS";
            sqlEstados.SelectCommand = sql;
            if (Request.QueryString["est"] != null)
            {
                ddlEstados.SelectedValue = Request.QueryString["est"].ToString();
                ddlEstados.DataBind();
            }
        }
        private void CargarListado()
        {
            //Quizas la consulta mas compleja, pero en pocas palabras diferencio los estados y con esa informacion la utilizo a mi favor para ver si se puede editar, enviar, modificar o no se pueden hacer estas acciones.
            //Tambien le agregué un filtro de estados.
            //El listado son posts del escritor autenticada, en cualquier estado.
            string sql = " SELECT ID_POST, TITULO, LEFT(DESC_POST, 73) +'..' DESC_POST, CONVERT(VARCHAR, F_CREACION, 103) F_CREACION, DESC_ESTADO, ";
            sql += " CASE WHEN P.ID_ESTADO_POST = 2 OR P.ID_ESTADO_POST = 1 THEN 'false' ELSE 'true' END AS PUEDE_M, CASE WHEN P.ID_ESTADO_POST = 2 THEN 'GREEN' WHEN P.ID_ESTADO_POST = 3 THEN 'RED' ELSE 'BLACK'  END AS COLOR, CASE WHEN P.ID_ESTADO_POST = 2 OR P.ID_ESTADO_POST = 1 THEN 'No se puede editar, pues el post está aprobado o enviado' ELSE 'Editar post' END AS TOOLTIP_M, ";
            sql += " CASE WHEN P.ID_ESTADO_POST <> 2 and P.ID_ESTADO_POST <> 1 THEN 'true'  ELSE 'false' END AS PUEDE_E, CASE WHEN P.ID_ESTADO_POST = 2 or P.ID_ESTADO_POST = 1 THEN 'No se puede enviar, pues el post está aprobado o enviado' ELSE 'Enviar post' END AS TOOLTIP_E, P.ID_ESTADO_POST ";
            sql += " FROM POSTS P ";
            sql += " INNER JOIN ESTADOS_POSTS EP ON P.ID_ESTADO_POST = EP.ID_ESTADO_POST ";
            sql += $" WHERE ID_USUARIO_CREA = {Session["ID"].ToString()} ";
            
            if (ddlEstados.SelectedValue != "-1" && ddlEstados.SelectedValue != "")
            {
                sql += $" AND P.ID_ESTADO_POST = {ddlEstados.SelectedValue} ";
            }
            sql += "  ORDER BY F_CREACION DESC, ID_POST DESC ";

            sqlPosts.SelectCommand = sql;
            lvPosts.DataBind();
        }

        protected void lvPosts_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //Redirige a otra pagina manteniendo los filtros
            if (e.CommandName == "ver")
            {
                Response.Redirect($"~/Forms/Escritor/GestionPost.aspx?id={e.CommandArgument}&ver=1&est={ddlEstados.SelectedValue.ToString()}");
            }
            else
            {
                Response.Redirect($"~/Forms/Escritor/GestionPost.aspx?id={e.CommandArgument}&ver=0&est={ddlEstados.SelectedValue.ToString()}");
            }
        }
    }
}
