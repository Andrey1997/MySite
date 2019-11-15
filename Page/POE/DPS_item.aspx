<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DPS_item.aspx.cs" Inherits="MyWebApplication.Page.POE.DPS_item" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="background-color: Black; min-height: 100vh;" id="POE_wiki_style">
        <div class="container body-content">
            <h2>Расчет DPS оружия</h2>
            <p>Средний урон = (нижняя граница урона + верхняя граница урона) * количество ударов в секунду</p>
            <p>
                Если к урону применяется понятие "удачи" и "неудачи", тогда его расчет происходит дважды, и для
                "удачливого" урона берется большее значение, а для "неудачливого" - меньшее. Обратите внимание,
                что понятие "удачи" применяется только к урону.
            </p>
            <br>
            <table>
                <tr>
                    <td><p>min dmg</p></td>
                    <td><asp:TextBox runat="server" Text="" ID="MinDamadge" autocomplete="off"/></td>
                </tr>
                <tr>
                    <td><p>max dmg</p></td>
                    <td><asp:TextBox runat="server" Text="" ID="MaxDamadge" autocomplete="off"/></td>
                </tr>
                <tr>
                    <td><p>APS</p></td>
                    <td><asp:TextBox runat="server" Text="" ID="APS" autocomplete="off"/></td>
                </tr>
                <tr>
                    <td><p style="color: #D77524;"><asp:Label runat="server" ID="ResultCalculate" /></p></td>
                    <td><asp:Button runat="server" onclick="ButtonClickCalculateDPS" Text="Рассчитать" /></td>
                </tr>
            </table>            
         </div>
    </div>
</asp:Content>
