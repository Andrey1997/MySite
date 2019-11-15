<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="min_max_DPS.aspx.cs" Inherits="MyWebApplication.Page.POE.min_max_DPS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .custom-select {
          position: relative;
          font-family: Arial;
          width: 250px;
          background-color: black;
          border: 1px solid #a38d6d;
        }

        .custom-select select {
          display: none;
          border: 1px solid #a38d6d;
        }

        .select-selected {

        }

        .select-selected:after {
          position: absolute;
          content: "";
          top: 14px;
          right: 10px;
          width: 0;
          height: 0;
          border: 6px solid transparent;
          border-color: #fff transparent transparent transparent;
        }

        .select-selected.select-arrow-active:after {
          border-color: transparent transparent #fff transparent;
          top: 7px;
        }

        .select-items div,.select-selected {
          color: #cd740f;
          padding: 2px 4px;
          border: 1px solid #a38d6d;
          cursor: pointer;
          user-select: none;
        }

        .select-items {
          position: absolute;
          background-color: black;
          top: 100%;
          left: 0;
          right: 0;
          z-index: 99;
        }

        .select-hide {
          display: none;
        }

        .select-items div:hover, .same-as-selected {
            background-color: #211f18;
        }
    </style>
    <div style="background-color: Black; min-height: 100vh;" id="POE_wiki_style">
        <div class="container body-content">
            <h2>Расчет минимально и максимального DPS оружия</h2>
            <p>Расчет нижнего и верхнего значения урона на оружии происходит следующим образом:</p>
            <p>(Базовый минимальный урон + Доп. физический урон(с префикса 1 значение)) *
                (1 + % увеличение физического урона(с префикса) + % Качество) = нижняя граница урона</p>
            <p>(Базовый максимальный урон + Доп. физический урон(с префикса 2 значение)) *
                (1 + % увеличение физического урона(с префикса) + % Качество) = верхняя граница урона</p>
            <p><a href="https://pathofexile-ru.gamepedia.com/%D0%9A%D0%B0%D1%87%D0%B5%D1%81%D1%82%D0%B2%D0%BE">посмотреть на POE WIKI</a></p>
            <br />
            <table>
                <tr>
                    <td><p>нижняя граница урона (база)</p></td>
                    <td><asp:TextBox runat="server" Text="0" ID="BaseMaxDMG" autocomplete="off"/></td>
                </tr>
                <tr>
                    <td><p>верхняя граница урона (база)</p></td>
                    <td><asp:TextBox runat="server" Text="0" ID="BaseMinDMG" autocomplete="off"/></td>
                </tr>
                <tr>
                    <td><p>скорость атаки (база)</p></td>
                    <td><asp:TextBox runat="server" Text="0" ID="BaseAttackSpeed" autocomplete="off"/></td>
                </tr>
                <tr>
                    <td><p>качество</p></td>
                    <td><asp:TextBox runat="server" Text="0" ID="QualityItem" autocomplete="off"/></td>
                </tr>
                <tr>
                    <td><p>увеличение физ урона</p></td>
                    <td>
                        <div class="custom-select">
                            <asp:DropDownList runat="server" ID="PreficsIncreasedPhysDMG">
                                <asp:ListItem>0 to 0</asp:ListItem>
                                <asp:ListItem>40 to 49</asp:ListItem>
                                <asp:ListItem>50 to 64</asp:ListItem>
                                <asp:ListItem>65 to 84</asp:ListItem>
                                <asp:ListItem>85 to 109</asp:ListItem>
                                <asp:ListItem>110 to 134</asp:ListItem>
                                <asp:ListItem>135 to 154</asp:ListItem>
                                <asp:ListItem>155 to 169</asp:ListItem>
                                <asp:ListItem>170 to 179</asp:ListItem>

                                <asp:ListItem>95 to 105</asp:ListItem>
                                <asp:ListItem>80 to 99</asp:ListItem>
                                <asp:ListItem>100 to 129</asp:ListItem>

                                <asp:ListItem>127 to 134</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td><p>гибридное увеличение физ урона</p></td>
                    <td>
                        <div class="custom-select">
                            <asp:DropDownList runat="server" ID="PreficsHybridIncreasedPhysDMG">
                                <asp:ListItem>0 to 0</asp:ListItem>
                                <asp:ListItem>15 to 19</asp:ListItem>
                                <asp:ListItem>20 to 24</asp:ListItem>
                                <asp:ListItem>25 to 34</asp:ListItem>
                                <asp:ListItem>35 to 44</asp:ListItem>
                                <asp:ListItem>45 to 54</asp:ListItem>
                                <asp:ListItem>55 to 64</asp:ListItem>
                                <asp:ListItem>65 to 74</asp:ListItem>
                                <asp:ListItem>75 to 79</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td><p>добавочный физ урон</p></td>
                    <td>
                        <div class="custom-select">
                            <asp:DropDownList runat="server" ID="PreficsAddPhysDMG">
                                <asp:ListItem>0 to 0 / 0 to 0</asp:ListItem>
                                <asp:ListItem>9 to 13 / 20 to 24</asp:ListItem>
                                <asp:ListItem>13 to 17 / 26 to 30</asp:ListItem>
                                <asp:ListItem>14 to 19 / 29 to 35</asp:ListItem>
                                <asp:ListItem>17 to 24 / 36 to 41</asp:ListItem>
                                <asp:ListItem>20 to 27 / 41 to 49</asp:ListItem>

                                <asp:ListItem>13 to 17 / 26 to 30</asp:ListItem>
                                <asp:ListItem>18 to 24 / 36 to 42</asp:ListItem>

                                <asp:ListItem>21 to 27 / 42 to 48</asp:ListItem>
                                <asp:ListItem>22 to 30 / 46 to 56</asp:ListItem>
                                <asp:ListItem>27 to 38 / 58 to 66</asp:ListItem>
                                <asp:ListItem>32 to 43 / 66 to 78</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td><p>увеличение скорости атаки</p></td>
                    <td>
                        <div class="custom-select">
                            <asp:DropDownList runat="server" ID="SuffixIncreasedAPS">
                                <asp:ListItem>0 to 0</asp:ListItem>
                                <asp:ListItem>14 to 16</asp:ListItem>
                                <asp:ListItem>17 to 19</asp:ListItem>
                                <asp:ListItem>20 to 22</asp:ListItem>
                                <asp:ListItem>23 to 25</asp:ListItem>
                                <asp:ListItem>26 to 27</asp:ListItem>

                                <asp:ListItem>8 to 10</asp:ListItem>
                                <asp:ListItem>11 to 13</asp:ListItem>
                                <asp:ListItem>14 to 16</asp:ListItem>

                                <asp:ListItem>8 to 10</asp:ListItem>
                                <asp:ListItem>11 to 12</asp:ListItem>

                                <asp:ListItem>11 to 15</asp:ListItem>
                                <asp:ListItem>16 to 20</asp:ListItem>

                                <asp:ListItem>17 to 19</asp:ListItem>
                                <asp:ListItem>20 to 21</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td><p>увеличение шанса крита(качество с крафта)</p></td>
                    <td>
                        <div class="custom-select">
                            <asp:DropDownList runat="server" ID="SuffixIncreasedCritChance">
                                <asp:ListItem>0 to 0</asp:ListItem>
                                <asp:ListItem>7 to 12</asp:ListItem>
                                <asp:ListItem>13 to 18</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td><p>увеличение физ урона(элдер)</p></td>
                    <td>
                        <div class="custom-select">
                            <asp:DropDownList runat="server" ID="SuffixEldeIncreasedPhysDMG">
                                <asp:ListItem>0 to 0</asp:ListItem>
                                <asp:ListItem>30 to 49</asp:ListItem>
                                <asp:ListItem>23 to 27</asp:ListItem>
                                <asp:ListItem>28 to 32</asp:ListItem>
                                <asp:ListItem>33 to 37</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td><p style="color: #D77524;"><asp:Label runat="server" ID="ResultCalculate" /></p></td>
                    <td><asp:Button runat="server" onclick="ButtonClickCalculateMinMaxDPS" Text="Рассчитать" /></td>
                </tr>
            </table>
            <br />
            <p>Внимание! Расчет проводится без промежуточных округлений и итоговые значения могут отчличатся до 2 DPS относительно других ресурсов!</p>
         </div>
    </div>
    <script>
        var x, i, j, selElmnt, a, b, c;
        /*look for any elements with the class "custom-select":*/
        x = document.getElementsByClassName("custom-select");
        for (i = 0; i < x.length; i++) {
          selElmnt = x[i].getElementsByTagName("select")[0];
          /*for each element, create a new DIV that will act as the selected item:*/
          a = document.createElement("DIV");
          a.setAttribute("class", "select-selected");
          a.innerHTML = selElmnt.options[selElmnt.selectedIndex].innerHTML;
          x[i].appendChild(a);
          /*for each element, create a new DIV that will contain the option list:*/
          b = document.createElement("DIV");
          b.setAttribute("class", "select-items select-hide");
          for (j = 0; j < selElmnt.length; j++) {
            /*for each option in the original select element,
            create a new DIV that will act as an option item:*/
            c = document.createElement("DIV");
            c.innerHTML = selElmnt.options[j].innerHTML;
            c.addEventListener("click", function(e) {
                /*when an item is clicked, update the original select box,
                and the selected item:*/
                var y, i, k, s, h;
                s = this.parentNode.parentNode.getElementsByTagName("select")[0];
                h = this.parentNode.previousSibling;
                for (i = 0; i < s.length; i++) {
                  if (s.options[i].innerHTML == this.innerHTML) {
                    s.selectedIndex = i;
                    h.innerHTML = this.innerHTML;
                    y = this.parentNode.getElementsByClassName("same-as-selected");
                    for (k = 0; k < y.length; k++) {
                      y[k].removeAttribute("class");
                    }
                    this.setAttribute("class", "same-as-selected");
                    break;
                  }
                }
                h.click();
            });
            b.appendChild(c);
          }
          x[i].appendChild(b);
          a.addEventListener("click", function(e) {
              /*when the select box is clicked, close any other select boxes,
              and open/close the current select box:*/
              e.stopPropagation();
              closeAllSelect(this);
              this.nextSibling.classList.toggle("select-hide");
              this.classList.toggle("select-arrow-active");
            });
        }
        function closeAllSelect(elmnt) {
          /*a function that will close all select boxes in the document,
          except the current select box:*/
          var x, y, i, arrNo = [];
          x = document.getElementsByClassName("select-items");
          y = document.getElementsByClassName("select-selected");
          for (i = 0; i < y.length; i++) {
            if (elmnt == y[i]) {
              arrNo.push(i)
            } else {
              y[i].classList.remove("select-arrow-active");
            }
          }
          for (i = 0; i < x.length; i++) {
            if (arrNo.indexOf(i)) {
              x[i].classList.add("select-hide");
            }
          }
        }
        /*if the user clicks anywhere outside the select box,
        then close all select boxes:*/
        document.addEventListener("click", closeAllSelect);
    </script>
</asp:Content>

