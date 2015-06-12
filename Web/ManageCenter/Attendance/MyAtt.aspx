<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAtt.aspx.cs" Inherits="Web.ManageCenter.Attendance.MyAtt" %>
<%--<%@ Register Src="~/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1"%>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>
    <script type="text/javascript" src="/js/table-toolbar.js"></script>
    <style type="text/css">
        .red
        {
            color: #FF0000;
            font-weight: bold;
            font-size: 12px;
        }
        .searchBtn{ background:url(/images/ptjh_21.gif) no-repeat 0 0; width:65px; height:23px; line-height:23px; text-indent:-9999px; border:0 none; margin-left:5px;}
        </style>
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox02">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td height="28" align="left" class="alertboxTableT">
                    <form method="get" id="form1">
                        <input type="hidden" name="id" value='<%=Request.QueryString["id"]%>' />
                        年份：
                        <select name="selYear" id="selYear"  runat="server"></select>
                        月份：
                        <select name="selMonth" id="selMonth">
                        <option value="-1">请选择</option>
                        <option value="01">01</option>
                        <option value="02">02</option>
                        <option value="03">03</option>
                        <option value="04">04</option>
                        <option value="05">05</option>
                        <option value="06">06</option>
                        <option value="07">07</option>
                        <option value="08">08</option>
                        <option value="09">09</option>
                        <option value="10">10</option>
                        <option value="11">11</option>
                        <option value="12">12</option>
                        </select>
  <%--                         &nbsp; 员工编号：
                        <input name="txtNum"  type="text" class="formsize80" id="txtNum" runat="server"/>
                     &nbsp;&nbsp; 姓名：
                        <input name="txtName" type="text" class="formsize80" id="txtName" runat="server"/>
                        &nbsp; 部门：
                        <uc1:SelectSection ID="SelectSection1" runat="server" SModel="1" Width="80" PModel="true"/>
                        &nbsp;&nbsp;--%>
                        <button type="submit" class="searchBtn">搜 索</button>
                      <script type="text/javascript">
                            function setValue(obj, v) {
                                for (var i = 0; i < obj.options.length; i++) {
                                    if (obj.options[i].value == v) {
                                        obj.options[i].selected = true;
                                    }
                                }
                            }
                            setValue($("#selYear")[0], '<%=Request.QueryString["selYear"] %>');
                            setValue($("#selMonth")[0], '<%=Request.QueryString["selMonth"] %>');
                    </script>
                    </form>        
                </td>
            </tr>
            <tr>
                <td height="28" align="left" class="alertboxTableT">
                    <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#85C1DD">
                        <tr>
                            <td width="6%" bgcolor="#b7e0f3">
                                月/日
                            </td>
                            <asp:Repeater ID="RepDateList" runat="server">
                                <ItemTemplate>
                                    <td width="3%" bgcolor="#b7e0f3"><%#Container.ItemIndex+1%></td>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tr>
                        <tr>
                            <td bgcolor="#E9F4F9">
                                <%=Request.QueryString["selMonth"] == "-1" ? "" : Request.QueryString["selMonth"]%>月
                            </td>
                            <asp:Repeater ID="RepList" runat="server">
                                <ItemTemplate>
                                    <td bgcolor="#E9F4F9"><%#this.getUnit(Container.ItemIndex+1)%></td>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="28" align="center" class="alertboxTableT">
                    <asp:Label runat="server" ID="lbAttInfo"></asp:Label>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn" style="text-align: center;">
            <a href="#" hidefocus="true"><s class="baochun"></s>打 印</a><a href="#" hidefocus="true"><s
                class="baochun"></s>导 出</a>
        </div>
    </div>
</body>
</html>