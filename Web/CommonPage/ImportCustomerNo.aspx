<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportCustomerNo.aspx.cs"
    Inherits="EyouSoft.Web.CommonPage.ImportCustomerNo" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="/css/style.css">

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox02">
        <table width="99%" height="79" cellspacing="0" cellpadding="0" class="alertboxbk1"
            border="0" bgcolor="#FFFFFF" align="center" style="margin: 0 auto; border-collapse: collapse;"
            id="liststyle">
            <tbody>
                <form id="form1" method="get">
                <tr>
                    <td height="30" align="left" colspan="5">
                    <input id="hidIFrmaeId" name="iframeid" type="hidden" value=<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeid")%>/>
                        类型：<select name="selCrmType" class="inputselect"><option value="-1">-请选择-</option>
                            <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SmsStructure.DaoRuKeHuType)), EyouSoft.Common.Utils.GetQueryStringValue("selCrmType"))%></select>
                        国家：
                        <select id="sltCountry" name="sltCountry" class="inputselect">
                        </select>
                        省份:
                        <select id="sltProvince" name="sltProvince" class="inputselect">
                        </select>
                        城市:
                        <select id="sltCity" name="sltCity" class="inputselect">
                        </select>
                        <input id="btnSearch" type="submit" style="width: 64px; height: 24px; background: url(&quot;../images/cx.gif&quot;) no-repeat scroll center center transparent;
                            border: 0pt none; margin-left: 5px;" value="查 询" />
                    </td>
                </tr>
                </form>
                <tr>
                    <td height="25" bgcolor="#B7E0F3" align="center">
                        <input type="checkbox" name="checkbox" onclick="ImportCustomerNo.SelAll(this);">
                    </td>
                    <td height="25" bgcolor="#B7E0F3" align="center">
                        单位名称
                    </td>
                    <td height="25" bgcolor="#B7E0F3" align="center">
                        类型
                    </td>
                    <td height="25" bgcolor="#B7E0F3" align="center">
                        所在地
                    </td>
                    <td bgcolor="#B7E0F3" align="center">
                        手机号码
                    </td>
                </tr>
                <cc1:CustomRepeater ID="repList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox1">
                            </td>
                            <td align="center">
                                <%#!string.IsNullOrEmpty(Eval("DanWeiName").ToString())?Eval("DanWeiName").ToString()+"-":""%><%#Eval("LxrName")%>
                            </td>
                            <td align="center">
                                <%#Eval("DanWeiType")%>
                            </td>
                            <td align="center">
                                <span class="pandl3">
                                    <%# GetCompanyAddress(Eval("CPCD"))%></span>
                            </td>
                            <td align="center" name="content">
                                <%#Eval("Mobile").ToString()%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </cc1:CustomRepeater>
            </tbody>
        </table>
        <div style="position: relative; height: 20px;">
            <div class="pages">
                <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:" id="btnSelect" onclick="return ImportCustomerNo.Select();">
                <s class="xuanzhe"></s>选 择</a><a hidefocus="true" href="javascript:" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"><s
                    class="chongzhi"></s>关 闭</a></div>
    </div>

    <script type="text/javascript">
        var ImportCustomerNo = {
            PWindow: parent.document,
            Select: function() {
                var smsArr = [];
                $("[name='checkbox1']:checked").each(function() {
                    smsArr.push($.trim($(this).parent().parent().find("td[name='content']").text()));
                });
                var txtSendMobile = ImportCustomerNo.PWindow.getElementById("txtSendMobile");
                if (txtSendMobile.value == "") {
                    txtSendMobile.value +=smsArr.join(',');
                } else {
                    txtSendMobile.value +=","+smsArr.join(',');
                }
                window.parent.SendSms.fontNum(txtSendMobile);
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide()
                return false;
            },

            //全选
            SelAll: function(obj) {
                $("input:checkbox").attr("checked", $(obj).attr("checked"));
            }
        };
        $(function() {
                pcToobar.init({ gID: "#sltCountry", pID: "#sltProvince", cID: "#sltCity",gSelect:"<%=EyouSoft.Common.Utils.GetQueryStringValue("sltCountry") %>",pSelect:"<%=EyouSoft.Common.Utils.GetQueryStringValue("sltProvince") %>",cSelect:"<%=EyouSoft.Common.Utils.GetQueryStringValue("sltCity") %>", comID: "<%=SiteUserInfo.CompanyId %>" });
        });
    </script>

</body>
</html>
