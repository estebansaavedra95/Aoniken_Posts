<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PostsEditor.aspx.cs" Inherits="Aoniken_Posts.Forms.PostsEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="card">
        <div class="card-title"></div>
        <div class="card-body">
                        <div class="row">
        
                            <h4>Posts a aprobar</h4>
                            <br />
                                   <asp:DropDownList runat="server" ID="ddlEscritores" DataTextField="APELLIDO_NOMBRE" AppendDataBoundItems="true"
                                    DataValueField="ID_USUARIO" AutoPostBack="true" CssClass="form-control col-md-3" DataSourceID="sqlEscritores" CausesValidation="false">
                                </asp:DropDownList>

                                <asp:SqlDataSource ID="sqlEscritores" runat="server"
                                    ConnectionString="<%$ ConnectionStrings:connectiondb %>"
                                    SelectCommand="">
                                </asp:SqlDataSource>
                            <br />


                            <br />
         <asp:ListView ID="lvPosts" runat="server" DataSourceID="sqlPosts" OnItemCommand="lvPosts_ItemCommand">   
            <LayoutTemplate>   
                <table id="Table1" runat="server" class="table">   
                    <tr id="Tr1" runat="server">   
                        <td id="Td1" runat="server">TÍTULO</td>   
                        <td id="Td2" runat="server">DESCRIPCIÓN</td>  
                        <td id="Td3" runat="server">FECHA CREACIÓN</td>
                        <td id="Td4" runat="server">AUTOR</td>
                        <td id="Td5" runat="server">ACCIONES</td>
 
                    </tr>   
                    <tr id="ItemPlaceholder" runat="server">   
                    </tr>   
                    <tr id="Tr2" runat="server">   
                        <td id="Td6" runat="server" colspan="5">   
                            <asp:DataPager ID="DataPager1" runat="server">   
                                <Fields>   
                                    <asp:NextPreviousPagerField ButtonType="Link" />   
                                    <asp:NumericPagerField />   
                                    <asp:NextPreviousPagerField ButtonType="Link" />   
                                </Fields>   
                            </asp:DataPager>   
                        </td>   
                    </tr>   
                </table>   
            </LayoutTemplate>   
            <ItemTemplate>   
                <tr>   
                    <td>   
                        <asp:Label  ID="Label1" runat="server" Text='<%# Eval("TITULO")%>'>   
                        </asp:Label>   
                    </td>   
                    <td>   
                        <asp:Label  ID="Label2" runat="server" Text='<%# Eval("DESC_POST")%>'>   
                        </asp:Label>   
                    </td> 
                    
                    <td>   
                        <asp:Label  ID="Label3" runat="server" Text='<%# Eval("F_CREACION")%>'>   
                        </asp:Label>   
                    </td> 

                    <td>   
                        <asp:Label  ID="Label4" runat="server" Text='<%# Eval("AUTOR")%>'>   
                        </asp:Label>   
                    </td> 
                    <td>
                        <asp:LinkButton runat="server"  ToolTip="Ver detalles" CommandArgument='<%# Eval("ID_POST")%>' CommandName="ver"><i class="fa-solid fa-eye"></i></asp:LinkButton>

                        <%--Se usa una concatenacion para poder hacer uso de una misma funcion js la cual aprueba, rechaza o elimina. A la funcion se le pasa parametros que nos serviran mas adelante.--%>
                         <asp:LinkButton ID="lnkAprobar" runat="server" ToolTip="Aprobar" Text="Aprobar" OnClientClick='<%# String.Concat("accion(", Eval("ID_POST"), ",", (Char)(39), Eval("F_CREACION"), (Char)(39), ",", (Char)(39), Eval("Autor"), (Char)(39), ",1,event);") %>'><i class="fa-solid fa-check"></i></asp:LinkButton>
                         <asp:LinkButton ID="lnkRechazar" runat="server" ToolTip="Rechazar" Text="Rechazar" OnClientClick='<%# String.Concat("accion(", Eval("ID_POST"), ",", (Char)(39), Eval("F_CREACION"), (Char)(39), ",", (Char)(39), Eval("Autor"), (Char)(39), ",2,event);") %>'><i class="fa-solid fa-ban"></i></asp:LinkButton>
                         <asp:LinkButton ID="lnkEliminar" runat="server" ToolTip="Eliminar" Text="Eliminar" OnClientClick='<%# String.Concat("accion(", Eval("ID_POST"), ",", (Char)(39), Eval("F_CREACION"), (Char)(39), ",", (Char)(39), Eval("Autor"), (Char)(39), ",3,event);") %>'><i class="fa-solid fa-trash"></i></asp:LinkButton>
                    </td>
                </tr>                   
            </ItemTemplate>   

          <EmptyDataTemplate>
        <div align="center">No se han encontrado posts.</div>
    </EmptyDataTemplate>
        </asp:ListView>  
        <asp:SqlDataSource ID="sqlPosts" runat="server" ConnectionString="<%$ConnectionStrings:connectiondb %>" SelectCommand=""></asp:SqlDataSource>  
    </div>
        </div>
        <asp:Button runat="server" ID="btnAprobar" Style="display:none" OnClick="btnAprobar_Click" />
        <asp:Button runat="server" ID="btnRechazar" Style="display:none" OnClick="btnRechazar_Click" />
        <asp:Button runat="server" ID="btnEliminar" Style="display:none" OnClick="btnEliminar_Click" />
        <asp:HiddenField runat="server" ID="hfPost" />
    </div>

<script type="text/javascript">
    function accion(cod, fecha, autor, codAccion, e) {
        //Primero detengo el evento, asi no hace un postback de la pagina.
        //Se usa el case para saber la accion y mandar antes una confirmacion correcta, luego dependiendo de que si es aprobar, rechazar, o eliminar, se  le pasa el id del post a un campo oculto, y por ultimo se realiza el clic del boton oculto correspondiente.
        e.preventDefault();
        var accion;
        switch (codAccion) {
            case 1:
                accion = "aprobar"
                break;
            case 2:
                accion = "rechazar"
                break;
            default:
                accion = "eliminar"
        }
        let desc = "¿Desea " + accion + " el post del " + autor + ", con fecha " + fecha + "?"
        if (confirm(desc)) {
            var btn;
            switch (codAccion) {
                case 1:
                    btn = document.getElementById("<%=btnAprobar.ClientID%>");
                    break;
                case 2:
                    btn = document.getElementById("<%=btnRechazar.ClientID%>");
                    break;
                default:
                    btn = document.getElementById("<%=btnEliminar.ClientID%>");
            }

            document.getElementById("<%=hfPost.ClientID %>").value = cod;
            btn.click();
        } 
    }
</script>
</asp:Content>
