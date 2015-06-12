<%@ Page Title="游轮列表" Language="C#" MasterPageFile="~/MasterPage/Boxy.Master" AutoEventWireup="true"
    CodeBehind="HangQiList.aspx.cs" Inherits="EyouSoft.YlWeb.Hangqi.HangQiList" %>

<%@ Register Src="/UserControl/FLDH.ascx" TagName="FLDH" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <div class="mainbox fixed">
        <div class="leftside">
            <uc3:FLDH ID="FLDH1" runat="server" />
        </div>
        <div class="rightside">
            <div class="R_basicbox">
                <div class="basic_rightT">
                    <h5>
                        特价船票</h5>
                </div>
                <div class="R_side_02">
                    <ul>
                        <asp:Repeater ID="rpt_HangQi" runat="server">
                            <ItemTemplate>
                                <li>
                                    <div class="line_leftT">
                                        <a href="<%#GetHqUrl(Eval("LeiXing")) %>?id=<%# Eval("HangQiId")%>">
                                            <%# Eval("MingCheng")%></a><p>
                                                出发时间：<em><%#ChuGangTimeHtml(Eval("RiQis"))%></em></p>
                                    </div>
                                    <div class="line_rightbtn">
                                        <span>
                                            <%#Eval("QiShiJiaGe","{0:C2}")%>
                                        </span><a href="<%#GetHqUrl(Eval("LeiXing")) %>?id=<%# Eval("HangQiId")%>">立即预订</a></div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
            <%--<div class="page">
                <span class="disabled">首页</span><a class="current">1</a><a href="#">2</a><a href="#">3</a><a
                    href="#">下一页</a><a href="#">尾页</a></div>--%>
            <div style="padding-top:20px;">
                <div id="page_change" style="width: 100%; text-align: right; margin: 0px auto 0px;
                    margin: 0; clear: both">
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var pConfig = { pageSize: 15, pageIndex: 1, recordCount: 0, showPrev: true, showNext: true, showDisplayText: false, cssClassName: 'page_change' }
    </script>
    
    <form runat="server" id="form1"></form>
    <script type="text/javascript" src="/js/ajaxpagecontrols.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            if (pConfig.recordCount > 0) {
                AjaxPageControls.replace("page_change", pConfig);
            }
        });
    </script>
</asp:Content>
