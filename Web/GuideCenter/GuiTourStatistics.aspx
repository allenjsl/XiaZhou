<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuiTourStatistics.aspx.cs"
    Inherits="Web.GuideCenter.GuiTourStatistics" MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ContentPlaceHolderID="head" ID="Content2" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">
    <div class="mainbox">

        <script src="../Js/datepicker/WdatePicker.js" type="text/javascript"></script>

        <form id="formSearch" action="GuiTourStatistics.aspx" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
                    导游姓名：<input class="inputtext" type="text" size="20" name="txtGuidName" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtGuidName") %>" />
                    上团时间：
                    <input name="txtStartTime" onfocus="WdatePicker();" type="text" class="inputtext formsize80"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtStartTime") %>" />
                    -
                    <input name="txtEndTime" type="text" onfocus="WdatePicker();" class="inputtext formsize80"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtEndTime") %>" />
                    <button type="submit" class="search-btn">
                        搜索</button></p>
            </span>
        </div>
        </form>
        <div class="tablehead">
            <ul class="fixed" id="actionBtn">
                <li><s class="dayin"></s><a href="javascript:" hidefocus="true" class="toolbar_dayin"
                    onclick="PrintPage('liststyle')"><span>打印</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a class="toolbar_daochu" onclick="toXls1();return false;"
                    hidefocus="true" href="javascript:void(0)"><span>导出</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th width="30" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th align="center" class="th-line">
                        导游姓名
                    </th>
                    <th align="center" class="th-line">
                        团队数
                    </th>
                    <th align="center" class="th-line">
                        团天数
                    </th>
                    <th align="center" class="th-line">
                        评分（质量管理中导游总分/评分次数）
                    </th>
                </tr>
                <asp:Repeater ID="replist" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" value="<%# Eval("GuideId") %>" />
                            </td>
                            <td align="center">
                                <%# Eval("Name")%>
                            </td>
                            <td align="center">
                                <a href="javascript:" data-class="groupCount" data-id="<%# Eval("GuideId") %>">
                                    <%# Eval("GroupCount")%></a>
                            </td>
                            <td align="center">
                                <%# Eval("DayCount")%>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.Utils.FilterEndOfTheZeroString( Convert.ToDecimal(Eval("Score") .ToString()).ToString("0.00")) %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="tablehead">
            <ul class="fixed">

                <script type="text/javascript">
                   document.write(document.getElementById("actionBtn").innerHTML);
                </script>

            </ul>
            <div class="pages">
                <asp:Label ID="lab_text" runat="server"></asp:Label>
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var tourStatisTicsPage = {
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            _BindBtn: function() {
                $("#liststyle").find("[data-class='groupCount']").unbind("click");
                $("#liststyle").find("[data-class='groupCount']").click(function() {
                    var id = $(this).attr("data-ID");
                    var com = '<%=this.SiteUserInfo.CompanyId %>';
                    tourStatisTicsPage._OpenBoxy("团队数查看", "/GuideCenter/TourStatisticsNums.aspx?ID=" + id + "&companyID=" + com, "630px", "390px", true);
                });
                
            },
            _PageInit: function() {
                tourStatisTicsPage._BindBtn();
                //toXls.init({ "selector": ".toolbar_daochu" });
               
            }
        }
        $(function() {
            tableToolbar.init({
                tableContainerSelector: ".tablelist-box"
            });
            tourStatisTicsPage._PageInit();
        });
    </script>

</asp:Content>
