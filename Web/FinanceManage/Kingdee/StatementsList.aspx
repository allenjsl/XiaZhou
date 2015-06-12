<%@ Page Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="StatementsList.aspx.cs" Inherits="EyouSoft.Web.FinanceManage.Kingdee.StatementsList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <span class="searchT">
                <form id="form1" method="get" action="StatementsList.aspx">
                <p>
                    <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
                    团号：<input type="text" class="inputtext formsize80" name="txtTourNo" value="<%= EyouSoft.Common.Utils.GetQueryStringValue("txtTourNo") %>">
                    凭证日期：<input type="text" class="inputtext formsize80" onclick="WdatePicker()" name="txtDateStart"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtDateStart") %>">
                    至
                    <input type="text" class="inputtext formsize80" onclick="WdatePicker()" name="txtDateEnd"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtDateEnd") %>">
                    是否已导：
                    <input type="radio" <%=EyouSoft.Common.Utils.GetQueryStringValue("radImportYes")=="true"?"checked=checked":"" %>
                        style="border: none;" value="true" name="radImportYes">是
                    <input type="radio" <%=EyouSoft.Common.Utils.GetQueryStringValue("radImportYes")=="true"?"":"checked=checked" %>
                        style="border: none;" value="false" name="radImportYes">否
                    <input id="serch" type="submit" class="search-btn" />
                </p>
                </form>
            </span>
        </div>
        <div id="tablehead" class="tablehead">
            <ul class="fixed">
                <li><s class="daochu"></s><a class="toolbar_daochu" hidefocus="true" onclick="DownXls()"
                    href="javascript:void(0);" id="selector_toolbar_toXls"><span>导出Excel</span></a></li>
            </ul>
            <span class="if_Stop" style="display: block; width: 200px; height: 30px; float: left;
                padding-top: 8px; margin-left: 5px;"></span>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th align="center" class="th-line">
                            团号
                        </th>
                        <th align="center" class="th-line">
                            凭证日期
                        </th>
                        <th align="center" class="th-line">
                            摘要
                        </th>
                        <th align="center" class="th-line">
                            科目
                        </th>
                        <th align="center" class="th-line">
                            借方
                        </th>
                        <th align="center" class="th-line">
                            贷方
                        </th>
                        <th align="center" class="th-line">
                            <span class="th-line h20">是否已导</span>
                        </th>
                    </tr>
                    <asp:Repeater ID="rpt_List" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <%# Eval("FTourCode")%>
                                </td>
                                <td align="center">
                                    <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("FDate"),ProviderToDate)%>
                                </td>
                                <td align="center">
                                    <%#Eval("FExplanation")%>
                                </td>
                                <td align="center">
                                    <%#Eval("FAccountNum")%>-<%#Eval("FAccountName")%>
                                </td>
                                <td align="right">
                                    <b class="fontgreen">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("FDebit"),ProviderToMoney)%></b>
                                </td>
                                <td align="right">
                                    <b class="fontred">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("FCredit"),ProviderToMoney)%></b>
                                </td>
                                <td align="center">
                                    <%#(bool)Eval("FIsExport")?"是":"否"%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Literal ID="litMsg" runat="server"></asp:Literal>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="border-top: 0 none;" class="tablehead">

            <script type="text/javascript">
                document.write(document.getElementById("tablehead").innerHTML);
			</script>

        </div>
    </div>

    <script type="text/javascript">
        //选中是否已导“是”的时候隐匿导出按钮//Start
        var Is_Visible = '<%=EyouSoft.Common.Utils.GetQueryStringValue("radImportYes")%>';
        $("ul[class=fixed]").css("visibility", Is_Visible == "true" ? "hidden" : "visible");
        //选中是否已导“是”的时候隐匿导出按钮//End
        function DownXls() {
            window.location.pathname;
            if (pagingRecordCount == 0) { alert("暂无任何数据供导出。"); return false; }

            var _win = window;
            var _params = _win.Boxy.getUrlParams([]);

            _params["toxls"] = "1";
            _params["toxlsrecordcount"] = pagingRecordCount;

            var url = _win.Boxy.createUri(window.location.pathname, _params);
            $.newAjax({
                type: "get",
                cache: false,
                url: url,
                dataType: "json",
                success: function(ret) {
                    if (ret.result == "1") {

                        window.window.location.href = ret.msg;
                        $(".if_Stop").html("<a style=\"color:Red;\" hidefocus=\"true\"  href=" + ret.msg + "><span>如果导出被浏览器阻止，请点击这里</a>");
                    }
                }
            });
        }
        
    </script>

</asp:Content>
