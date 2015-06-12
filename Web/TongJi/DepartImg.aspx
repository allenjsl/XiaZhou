<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="DepartImg.aspx.cs" Inherits="EyouSoft.Web.TongJi.DepartImg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="/js/FusionChartsFree/FusionCharts.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <div id="divMap" class="addContent-box tongjitu">
        </div>
        <div class="hr_5">
        </div>
        <div class="hr_5">
        </div>
    </div>

    <script type="text/javascript">
        //设置统计图
        function SetDepartImg() {
            var dataXml = "<%= FlashDataXml %>";
            if (dataXml != "") {
                var departImg = new FusionCharts("/js/FusionChartsFree/Charts/FCF_MSColumn3D.swf", "chartId", "940", "500", "0", "1");
                departImg.setDataXML(dataXml);
                departImg.render("divMap");
            }
        }

        $(function() {
            SetDepartImg();
        });
    </script>

</asp:Content>
