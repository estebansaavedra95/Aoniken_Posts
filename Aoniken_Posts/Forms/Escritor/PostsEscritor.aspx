<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PostsEscritor.aspx.cs" Inherits="Aoniken_Posts.Forms.PostsEscritor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="card">
        <div class="card-title"></div>
        <div class="card-body">
                        <div class="row">
        
                            <h4>Mis posts</h4>
                            <br />
                              <asp:DropDownList runat="server" ID="ddlEstados" DataTextField="DESC_ESTADO" AppendDataBoundItems="true"
                                    DataValueField="ID_ESTADO_POST" AutoPostBack="true" CssClass="form-control col-md-3" DataSourceID="sqlEstados" CausesValidation="false">
                                </asp:DropDownList>

                                <asp:SqlDataSource ID="sqlEstados" runat="server"
                                    ConnectionString="<%$ ConnectionStrings:connectiondb %>"
                                    SelectCommand="">
                                </asp:SqlDataSource>
                            <br />
                            <br />
                             <asp:Button CssClass="btn btn-sm btn-success" ID="btnNuevoPost" runat="server" PostBackUrl="~/Forms/Escritor/GestionPost.aspx?id=0&ver=0" Text="Nuevo Post"/>
                            <br />


         <asp:ListView ID="lvPosts" runat="server" DataSourceID="sqlPosts" OnItemCommand="lvPosts_ItemCommand">   
            <LayoutTemplate>   
                <table id="Table1" runat="server" class="table">   
                    <tr id="Tr1" runat="server">   
                        <td id="Td1" runat="server">TÍTULO</td>   
                        <td id="Td2" runat="server">DESCRIPCIÓN</td>  
                        <td id="Td3" runat="server">FECHA CREACIÓN</td>
                        <td id="Td4" runat="server">ESTADO</td>
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

                     <td style='<%# String.Concat("color:",Eval("COLOR" ),";") %>'>   
                        <asp:Label  ID="Label4" runat="server"  Text='<%# Eval("DESC_ESTADO")%>'>   
                        </asp:Label>   
                    </td> 

                    <td>
                        <asp:LinkButton runat="server"  ToolTip="Ver detalles" CommandArgument='<%# Eval("ID_POST")%>' CommandName="ver"><i class="fa-solid fa-eye"></i></asp:LinkButton>

                         <asp:LinkButton ID="lnkEditar" runat="server" CommandArgument='<%# Eval("ID_POST")%>' CommandName="modificar" ToolTip='<%# Eval("TOOLTIP_M") %>' Enabled='<%# Convert.ToBoolean(Eval("PUEDE_M")) %>'  ><i class="fa-solid fa-pen-to-square"></i></asp:LinkButton>
                         <%--Se concatena una funcion js para realizar el envio a aprobar del post (cambio de estado)--%>
                         <asp:LinkButton ID="lnkEnviar" runat="server" ToolTip='<%# Eval("TOOLTIP_E") %>' Enabled='<%# Convert.ToBoolean(Eval("PUEDE_E")) %>' OnClientClick='<%# String.Concat("accion(", Eval("ID_POST"), ",", (Char)(39), Eval("TITULO"), (Char)(39), ",", (Char)(39), Eval("ID_POST"), (Char)(39), ",", Eval("ID_ESTADO_POST"), ",event);") %>'><i class="fa-solid fa-paper-plane"></i></asp:LinkButton>
                    </td>
                </tr>                   
            </ItemTemplate>   

          <EmptyDataTemplate>
        <div align="center">No se han encontrado posts.</div>
    </EmptyDataTemplate>
        </asp:ListView>  
        <asp:SqlDataSource ID="sqlPosts" runat="server" ConnectionString="<%$ConnectionStrings:connectiondb %>" SelectCommand="">
        </asp:SqlDataSource>  

    </div>
        </div>
             <asp:Button runat="server" ID="btnEnviar" Style="display:none" OnClick="btnEnviar_Click" />
        <asp:HiddenField runat="server" ID="hfPost" />
    </div>

<script type="text/javascript">
    function accion(cod, fecha, autor, estado, e) {
        e.preventDefault();
        if (estado != 2 && estado != 1) {
            let desc = "¿Desea enviar a aprobar el post con título:'" + fecha + "'?"
            if (confirm(desc)) {
                var btn = document.getElementById("<%=btnEnviar.ClientID%>");
                document.getElementById("<%=hfPost.ClientID %>").value = cod;
                btn.click();
            }
        }
    }
</script>
</asp:Content>
