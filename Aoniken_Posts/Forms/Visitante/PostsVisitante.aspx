<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PostsVisitante.aspx.cs" Inherits="Aoniken_Posts.Forms.PostsVisitante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="card">
        <div class="card-title"></div>
        <div class="card-body">
                        <div class="row">
        
                            <h4>Mis posts</h4>
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
                        <td id="Td3" runat="server">FECHA</td>
                        <td id="Td4" runat="server">ESCRITOR</td>
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
                        <asp:Label  ID="Label3" runat="server" Text='<%# Eval("F_APROBACION")%>'>   
                        </asp:Label>   
                    </td> 

                     <td>   
                        <asp:Label  ID="Label4" runat="server" Text='<%# Eval("AUTOR")%>'>   
                        </asp:Label>   
                    </td> 

                    <td>
                       <%-- PostBackUrl='<%# String.Concat("~/Forms/Visitante/Ver.aspx?id=",Eval("ID_POST"),"&ver=1") %>' --%>
                        <asp:LinkButton runat="server"  ToolTip="Ver detalles" CommandArgument='<%#Eval("ID_POST") %>' CommandName="ver"   ><i class="fa-solid fa-eye"></i></asp:LinkButton>
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
    </div>

<script type="text/javascript">
</script>
</asp:Content>
