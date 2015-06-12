<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZiXun.aspx.cs" Inherits="EyouSoft.YlWeb.Corp.ZiXun"
    MasterPageFile="~/MasterPage/M1.Master" Title="资讯" %>
<%@ MasterType VirtualPath="~/MasterPage/M1.Master" %>
<%@ Register Src="~/UserControl/FLDH.ascx" TagName="FLDH" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/GuanZhu.ascx" TagName="GuanZhu" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/BangZhu.ascx" TagName="BangZhu" TagPrefix="uc3" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">   
    <div class="step_mainbox fixed">
        <div class="leftside">
            <uc3:FLDH ID="FLDH1" runat="server" />
            <uc3:GuanZhu ID="GuanZhu1" runat="server" />
            <uc3:BangZhu ID="BangZhu1" runat="server" />
        </div>
        <div class="rightside">
            <div class="R_basicbox">
                <div class="basic_rightT">
                    <h5><asp:Literal runat="server" ID="ltr0"></asp:Literal></h5>
                </div>
                <div class="news_list">
                    <ul class="user_list">
                        <asp:Repeater runat="server" ID="rpt">
                        <ItemTemplate>
                        <li><span><%#Eval("IssueTime","{0:yyyy-MM-dd}") %></span><a href="zixunxx.aspx?s=<%#Eval("ZiXunId") %>"><%#Eval("BiaoTi") %></a></li>
                        </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
            <div>
                <div id="page_change" style="width: 100%; text-align: right; margin: 0px auto 0px;
                    margin: 0; clear: both">
                </div>
            </div>
        </div>
    </div>
    
    <script type="text/javascript" src="/js/ajaxpagecontrols.js"></script>
    <script type="text/javascript">
        var pConfig = { pageSize: 15, pageIndex: 1, recordCount: 0, showPrev: true, showNext: true, showDisplayText: false, cssClassName: 'page_change' }
        $(document).ready(function() {
            if (pConfig.recordCount > 0) AjaxPageControls.replace("page_change", pConfig);
        });
    </script>
    
    <form id="form1" runat="server"></form>
</asp:Content>
