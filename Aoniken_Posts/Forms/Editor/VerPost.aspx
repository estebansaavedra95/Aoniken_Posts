<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerPost.aspx.cs" Inherits="Aoniken_Posts.Forms.Editor.VerPost" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


        <div class="row">
            <div class="col-sm-12">
            <label>Título:</label>
            <asp:Label runat="server" ID="lblTitulo"></asp:Label>
                </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                 <label class="form-control-label">Descripción:</label>
                 <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" TextMode="MultiLine" Enabled="False" Rows ="10"></asp:TextBox>
                <asp:LinkButton runat="server" ID="lnkVolver" OnClick="lnkVolver_Click">Volver</asp:LinkButton>
            </div>
           
        </div>
</asp:Content>