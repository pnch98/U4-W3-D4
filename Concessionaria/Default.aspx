<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Concessionaria._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="d-flex flex-column align-items-center">
        <asp:DropDownList ID="ddlExample" runat="server">
            <asp:ListItem Value="null">Seleziona un'auto</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="Button1" class="btn btn-primary mt-2 mb-3" runat="server" Text="Dettagli" OnClick="SelectedCar" />
        <div id="divDettaglio" class="d-none" runat="server">
            <img id="selectedImg" runat="server" height="400"/>
            <p id="selectedModel" class="fs-2 mb-0" runat="server"></p>
            <p id="selectedBasePrice" runat="server"></p>
            <span>Garanzia: </span>
            <asp:DropDownList ID="garanzia" runat="server">
                <asp:ListItem Value="2">2 anni</asp:ListItem>
                <asp:ListItem Value="3">3 anni</asp:ListItem>
                <asp:ListItem Value="4">4 anni</asp:ListItem>
                <asp:ListItem Value="5">5 anni</asp:ListItem>
            </asp:DropDownList>
            <div class="d-flex flex-column">
                <div>
                    <span>Climatizzatore </span>
                    <asp:CheckBox ID="climatizzatore" runat="server" />
                </div>
                <div>
                    <span>ABS </span>
                    <asp:CheckBox ID="ABS" runat="server" />
                </div>
                <div>
                    <span>Cerchi in lega </span>
                    <asp:CheckBox ID="cerchi" runat="server" />
                </div>
                <div>
                    <span>Fari LED </span>
                    <asp:CheckBox ID="fariLED" runat="server" />
                </div>
            </div>
            <asp:Button ID="Calcola" runat="server" Text="Calcola Prezzo" OnClick="CalcolaPrezzo" OnClientClick="return false"/>
            <p id="prezzoTotaleFinaleUAU" class="display-5" runat="server"></p>
        </div>
    </main>

</asp:Content>
