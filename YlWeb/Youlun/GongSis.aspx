<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GongSis.aspx.cs" Inherits="EyouSoft.YlWeb.Youlun.GongSis"
    MasterPageFile="~/MasterPage/M1.Master" Title="长江游轮合作公司" %>

<%@ MasterType VirtualPath="~/MasterPage/M1.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">

    <div class="step_mainbox">
        <div class="basicT">
            您的位置：维诗达游轮 &gt; <%=LeiXing2 %></div>
        <div class="basic_mainT">
            <h5>
                <%=LeiXing2 %></h5>
        </div>
        <div class="tg_list company_pic">
            <h3><%=LeiXing1 %></h3>
            <ul>
                <asp:Repeater runat="server" ID="rpt"><ItemTemplate>
                <li <%#(Container.ItemIndex+1)%3==0? "class=\"marginR\"" :""%> title="<%#Eval("MingCheng") %>"><a class="tg_img"
                    href="<%#GongSiUrl+"?id="+Eval("GongSiId") %>">
                    <img src="<%#EyouSoft.YlWeb.TuPian.F1( ErpFilepath+Eval("Logo") ,300,113)%>"></a>
                    <p>
                        <a href="<%#GongSiUrl+"?id="+Eval("GongSiId") %>"><%#Eval("MingCheng") %></a></p>
                </li>
                </ItemTemplate></asp:Repeater>
            </ul>
            <div class="clear">
            </div>
        </div>
        <%--<div class="page">
            <ul>
                <li><span class="disabled">首页</span></li>
                <li><a class="current">1</a></li>
                <li><a href="#">2</a></li>
                <li><a href="#">3</a></li>
                <li><a href="#">下一页</a></li>
                <li><a href="#">尾页</a></li>
            </ul>
        </div>--%>
        <div style="padding-top: 20px;">
            <div id="page_change" style="width: 100%; text-align: center; margin: 0px auto 0px;
                margin: 0; clear: both">
            </div>
        </div>
    </div>
    

    <script type="text/javascript">
        var pConfig = { pageSize: 15, pageIndex: 1, recordCount: 0, showPrev: true, showNext: true, showDisplayText: false, cssClassName: 'page_change' }

        $(document).ready(function() {
            if (pConfig.recordCount > 0) { AjaxPageControls.replace("page_change", pConfig); }

            if ("<%=(int)LeiXing %>" == "0") setNav(1); else setNav("2");
        });
    </script>

    <script type="text/javascript" src="/js/ajaxpagecontrols.js"></script>

    <form runat="server" id="form1"></form>
</asp:Content>