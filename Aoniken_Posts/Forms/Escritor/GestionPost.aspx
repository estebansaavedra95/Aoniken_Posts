<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionPost.aspx.cs" Inherits="Aoniken_Posts.Forms.Editor.GestionPost" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


        <div class="row">
            <div class="col-sm-12">
            <label>Título:</label>
            <asp:TextBox runat="server" ID="txtTitulo" CssClass="form-control" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvTitulo" CssClass="text-danger" ControlToValidate="txtTitulo" ErrorMessage="* Ingrese el título." ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                 <label class="form-control-label">Descripción:</label>
                 <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" TextMode="MultiLine"  Rows ="10"></asp:TextBox>
                 <asp:RequiredFieldValidator runat="server" ID="rfvDescripcion" CssClass="text-danger" ControlToValidate="txtDescripcion" ErrorMessage="* Ingrese la descripción." ValidationGroup="vgGuardar" ></asp:RequiredFieldValidator>
                <br />
                 <asp:Button CssClass="btn btn-sm btn-success"  ID="btnGuardar" runat="server" Text="Guardar" ValidationGroup="vgGuardar" OnClick="btnGuardar_Click"/>
                <asp:Button CssClass="btn btn-sm btn-danger" OnClick="btnCancelar_Click" ID="btnCancelar" runat="server" Text="Cancelar"/>
            </div>
           
        </div>
</asp:Content>