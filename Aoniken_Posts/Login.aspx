<%@ Page Title="" Language="C#" MasterPageFile="~/SiteLogin.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Aoniken_Posts.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


                            <div class="card-body mb-6">
                                
                                  
                                        <div class="row">
                                            <div class="col-sm-12">

                                           
                                        <div class="form-group">
                                            <label>Usuario</label>
                                            <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control form-control-lg" MaxLength="20" placeholder="Ingrese su usuario" required="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvUsuario" CssClass="text-danger" ControlToValidate="txtPass" ErrorMessage="* Ingrese el usuario." ValidationGroup="vgIngresar"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <label>Contraseña</label>

                                            <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control form-control-lg" placeholder="Ingrese su contraseña" MaxLength="20" required="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvPassword" CssClass="text-danger" ControlToValidate="txtPass" ErrorMessage="* Ingrese la contraseña." ValidationGroup="vgIngresar"></asp:RequiredFieldValidator>

                                            <div class="mt-3">
                                                <asp:LinkButton runat="server" Style="color: #af7ac5" ID="btnVerPosts" PostBackUrl="~/Forms/Visitante/PostsVisitante.aspx">Ingresar a ver posts</asp:LinkButton>
                                            </div>
                                        </div>

                                        <div class="mt-3">
                                            <asp:Button CssClass="btn btn-sm btn-success" ID="btnIngresar" runat="server" Text="Ingresar" OnClick="btnIngresar_Click" ValidationGroup="vgIngresar"/>
                                        </div>
                                        <asp:CustomValidator runat="server" CssClass="mt-3 text-danger" ID="cvError" ErrorMessage="* Usuario y/o contraseña incorrectos." Display="Dynamic" />
                                    </div>
                                 </div>
                               </div>

                            

</asp:Content>
