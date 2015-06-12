<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttStatistic.aspx.cs" Inherits="Web.ManageCenter.Attendance.AttStatistic" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script type="text/javascript" src="/js/jquery.boxy.js"></script>

    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/js/table-toolbar.js"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div style="font-size: 18px; font-weight: bold; color: #000; text-align: center;">
            考勤汇总表</div>
        <div class="hr_5">
        </div>
        <form>
        <table width="99%" border="0" align="center">
            <tr>
                <td height="28" align="left" bgcolor="#C1E5F5">
                    年份：
                    <select name="selYear" id="selYear" class="inputselect">
                    </select>
                    月份：
                    <select name="selMonth" id="selMonth" class="inputselect">
                    </select>
                    部门：
                    <uc1:SelectSection ID="SelectSection1" runat="server" SModel="2" Width="80" SetTitle="部门选择" />
                    姓名：
                    <input name="txtName" type="text" class="inputtext formsize80" id="txtName" value='<%=Request.QueryString["txtName"] %>' />
                    员工编号：
                    <input name="txtNum" type="text" class="inputtext formsize80" id="txtNum" value='<%=Request.QueryString["txtNum"]%>' />
                    <input type="hidden" name="iframeId" value='<%=Request.QueryString["iframeId"]%>' />
                    <button type="submit" style="width: 64px; height: 24px; background: url(/images/cx.gif) no-repeat center center;
                        border: 0 none; margin-left: 5px;">
                        查 询</button>
                </td>
            </tr>
        </table>
        <input type="hidden" name="sl" value='<%=Request.QueryString["sl"]%>' />
        </form>
        <div class="hr_5">
        </div>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9">
            <tr>
                <td width="12%" height="28" align="center" bgcolor="#b7e0f3">
                    员工编号
                </td>
                <td width="12%" align="center" bgcolor="#b7e0f3">
                    部门
                </td>
                <td width="12%" align="center" bgcolor="#b7e0f3">
                    姓名
                </td>
                <%=this.getMonthDays()%>
            </tr>
            <asp:Repeater ID="RepList" runat="server">
                <ItemTemplate>
                    <tr>
                        <td width="12%" height="28" align="center" bgcolor="#E9F4F9">
                            <%#Eval("FileNumber")%>
                        </td>
                        <td width="12%" align="center" bgcolor="#E9F4F9">
                            <%#Eval("DepartName")%>
                        </td>
                        <td width="12%" align="center" bgcolor="#E9F4F9">
                            <%#Eval("Name")%>
                        </td>
                        <%#this.getTables(Eval("AttendanceList"))%>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div class="pages" style="display: none">
            <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
        </div>
        <div class="alertbox-btn">
            <a href="javascript:window.print()" hidefocus="true"><s class="baochun"></s>打 印</a><a
                href="javascript:void(0)" id="selector_toolbar_toXls" hidefocus="true"><s class="baochun"></s>导
                出</a>
        </div>
    </div>

    <script type="text/javascript">

        var AttStatistic = {
            data: {
                sl: '<%=Request.QueryString["sl"] %>',
                iframeId: '<%=Request.QueryString["iframeId"]%>'
            },
            initYear: function() {
                var date = new Date();
                var year = date.getFullYear();
                for (var i = 0; i < 10; i++) {
                    var opt = new Option(year - i, year - i)
                    document.getElementById("selYear").options.add(opt);
                }
            },
            initMonth: function() {
                for (var i = 1; i < 13; i++) {
                    var opt = new Option(i, i)
                    document.getElementById("selMonth").options.add(opt);
                }
            },
            setSelectValue: function(ObjName, v) {
                var obj = document.all[ObjName];
                for (var i = 0; i < obj.length; i++) {
                    if (obj.options.value == v) { obj.value = v; }
                }
            },
            initToolBar: function() {
                toXls.init({ "selector": "#selector_toolbar_toXls" });
            }
        }
        $(function() {
            AttStatistic.initToolBar();
            AttStatistic.initYear();
            AttStatistic.initMonth();
            var date = new Date();
            $("#selYear").val(Boxy.queryString("selYear") || date.getFullYear());
            $("#selMonth").val(Boxy.queryString("selMonth") || date.getMonth() + 1)
        })
    </script>

</body>
</html>
