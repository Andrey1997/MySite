<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestExampleKNN_my.aspx.cs" Inherits="MyWebApplication.Page.MachineLearning.TestExampleKNN_my" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .p_font_size {
            font-size: 14pt;
        }
    </style>
    <div style="background-color: white; min-height: 100vh;">
        <div class="container body-content" id="Machine_learning_style">
            <div style="text-align:center">
                <h2>Тестовый метод(MyNoName)</h2>
            </div>
            <br/>
            <p><a href="http://archive.ics.uci.edu/ml/datasets/Adult">Данные были скачены отсюда.</a></p>
            <p>Это данные переписи, задача состоит в предсказании дохода ( > 50000 в год или нет). 
                Данные с пропущенными значениями отбрасывались. 
                Всего 45222 объектос (учебных = 30162, тестовых = 15060) с процентым содержанием классов 75.22% и 24.78%.</p>
            <br />
            <p>Информация про свойства объектов:<br />
                &nbsp&nbsp&nbsp&nbsp возраст: непрерывно<br />
                &nbsp&nbsp&nbsp&nbsp работа: Private, Self-emp-not-inc, Self-emp-inc, Federal-gov, Local-gov, State-gov, Without-pay, Never-worked<br />
                &nbsp&nbsp&nbsp&nbsp fnlwgt: непрерывно<br />
                &nbsp&nbsp&nbsp&nbsp образование: Bachelors, Some-college, 11th, HS-grad, Prof-school, Assoc-acdm, Assoc-voc, 9th, 7th-8th, 12th, Masters, 1st-4th, 10th, Doctorate, 5th-6th, Preschool<br />
                &nbsp&nbsp&nbsp&nbsp образование число: непрерывно<br />
                &nbsp&nbsp&nbsp&nbsp семейный статус: Married-civ-spouse, Divorced, Never-married, Separated, Widowed, Married-spouse-absent, Married-AF-spouse<br />
                &nbsp&nbsp&nbsp&nbsp занятие/сфера деятельности: Tech-support, Craft-repair, Other-service, Sales, Exec-managerial, Prof-specialty, Handlers-cleaners, Machine-op-inspct, Adm-clerical, Farming-fishing, Transport-moving, Priv-house-serv, Protective-serv, Armed-Forces<br />
                &nbsp&nbsp&nbsp&nbsp отношения: Wife, Own-child, Husband, Not-in-family, Other-relative, Unmarried<br />
                &nbsp&nbsp&nbsp&nbsp раса:  White, Asian-Pac-Islander, Amer-Indian-Eskimo, Other, Black<br />
                &nbsp&nbsp&nbsp&nbsp пол: Female, Male<br />
                &nbsp&nbsp&nbsp&nbsp прирост капитала: непрерывно<br />
                &nbsp&nbsp&nbsp&nbsp потеря капитала: непрерывно<br />
                &nbsp&nbsp&nbsp&nbsp работает часов в неделю: непрерывно<br />
                &nbsp&nbsp&nbsp&nbsp родная страна:  United-States, Cambodia, England, Puerto-Rico, Canada, Germany, Outlying-US(Guam-USVI-etc), India, Japan, Greece, South, China, Cuba, Iran, Honduras, Philippines, Italy, Poland, Jamaica, Vietnam, Mexico, Portugal, Ireland, France, Dominican-Republic, Laos, Ecuador, Taiwan, Haiti, Columbia, Hungary, Guatemala, Nicaragua, Scotland, Thailand, Yugoslavia, El-Salvador, Trinadad&Tobago, Peru, Hong, Holand-Netherlands<p>
            
            <p>----</p>
            <p>Ниже представлены найденные оптимальные параметры при которых процент ошибки на всей тестовый выборки составляет ~17%.</p>
            <p>Был применен метод ближайших соседей (без нормализации). На сайте указаны результаты других методов, к примеру, лучшие результаты чуть больше 14%. При использовании метода ближайших соседей результат 20% и 21%, что является самыми плохими результатами среди всех методов.</p>
            <p>Также есть возможность попробовать задать параметры самому по представленому макету:</p>
            <p>----</p>
            <asp:TextBox Text="" TextMode="MultiLine" runat="server" ID="TextBoxExample" Rows="8"   Wrap="False" ReadOnly="true"></asp:TextBox>
            <br />
            <asp:TextBox Text="" TextMode="MultiLine" runat="server" ID="TextBoxUserParameter" Rows="8"   Wrap="False"></asp:TextBox>
            <p>Результат: <asp:Label runat="server" ID="label_1" Text=""></asp:Label></p>
            <br />
            <asp:Button runat="server" OnClick="KNNClick" Text="Запустить" />
            <asp:Button runat="server" OnClick="DefaultExampleClick" Text="Пример по умолчанию" />
            <br />
        </div>
    </div>
</asp:Content>
