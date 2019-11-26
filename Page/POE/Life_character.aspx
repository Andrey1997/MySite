<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Life_character.aspx.cs" Inherits="MyWebApplication.Page.POE.Life_character" MaintainScrollPositionOnPostback="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="background-color: Black; min-height: 100vh;" id="POE_wiki_style">
        <div class="container body-content">
            <h2>Расчет здоровья и сравнение эффективности</h2>
            <p>Расчет итогового значения здоровья проводится по следующей формуле:</p>
            <p>Максимальное здоровье = (38 + (Уровень * 12) + (Сила / 2) + Плоский бонус к здоровью)) * (1 + Процентные модификаторы) * (1 + Больше здоровья) * (1 - Меньше здоровья)</p>
            <p>2 силы = 1 хп</p>
            <p><a href="https://pathofexile-ru.gamepedia.com/%D0%97%D0%B4%D0%BE%D1%80%D0%BE%D0%B2%D1%8C%D0%B5">посмотреть на POE WIKI</a></p>
            <table>
                <tr>
                    <td><p>уровень персонажа</p></td>
                    <td><asp:TextBox runat="server" Text="0" ID="TextBox_lvl" autocomplete="off"/></td>
                </tr>
                <tr>
                    <td><p>итоговое значение силы</p></td>
                    <td><asp:TextBox runat="server" Text="0" ID="TextBox_strength" autocomplete="off"/></td>
                </tr>
                <tr>
                    <td><p>плоский бонус к здоровью</p></td>
                    <td><asp:TextBox runat="server" Text="0" ID="TextBox_add_life" autocomplete="off"/></td>
                </tr>
                <tr>
                    <td><p>% увеличение здоровья</p></td>
                    <td><asp:TextBox runat="server" Text="0" ID="TextBox_inc_life" autocomplete="off"/></td>
                </tr>
                <tr>
                    <td><p>% больше здоровья</p></td>
                    <td><asp:TextBox runat="server" Text="0" ID="TextBox_more_life" autocomplete="off"/></td>
                </tr>
                <tr>
                    <td><p>% уменьшния здоровья</p></td>
                    <td><asp:TextBox runat="server" Text="0" ID="TextBox_less_life" autocomplete="off"/></td>
                </tr>
            </table>
            <asp:Button runat="server" onclick="ButtonClickCalculateLife" Text="Рассчитать" />
            <br />
            <br />
            <p style="color: #D77524;"><asp:Label runat="server" ID="ResultCalculate" /></p>
            <p style="color: #D77524;"><asp:Label runat="server" ID="ResultCalculate_1_inc" /></p>
            <p style="color: #D77524;"><asp:Label runat="server" ID="ResultCalculate_5_inc" /></p>
            <p>Внимание! Расчет проводится без округлений! Рекомендуется вводить четные значения силы!<br />Если известно значение первой скобки полностью то запишите его в плоский бонус к здоровью, а в силу "-76"!</p>
        </div>
    </div>
</asp:Content>

