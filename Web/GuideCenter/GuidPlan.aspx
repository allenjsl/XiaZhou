<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuidPlan.aspx.cs" Inherits="Web.GuideCenter.GuidPlan" %>

<%@ Register Src="../UserControl/LineSelect.ascx" TagName="LineSelect" TagPrefix="uc1" %>
<%@ Register TagName="Guid" TagPrefix="uc4" Src="~/UserControl/selectGuid.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/js/jquery.boxy.js"></script>

    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>

    <script type="text/javascript" src="/Js/Newjquery.autocomplete.js"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <script type="text/javascript" src="/Js/ValiDatorForm.js"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <div style="padding-left: 20px; font-size: 14px; font-weight: bold; padding-bottom: 5px;
            height: 20px;">
            【<asp:Label ID="labName" runat="server" Text=""></asp:Label>】的出团安排</div>
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center">
            <tbody>
                <tr>
                    <td width="14%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        线路名称：
                    </td>
                    <td align="left">
                        <uc1:LineSelect ID="LineSelect1" runat="server" CallBackFun="callBackFun" />
                    </td>
                    <td bgcolor="#B7E0F3" align="right">
                        <span class="alertboxTableT">团号：</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtTourCode" class="inputtext formsize120" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        <span class="fontred">*</span>导游姓名：
                    </td>
                    <td width="44%" bgcolor="#E0E9EF" align="left">
                        <uc4:Guid  ID="GuidControl1" runat="server" CallBackFun="GuidPlanPage._AjaxContectInfo" errmsg="请选择导游!"
                        valid="required"/>
                    </td>
                    <td width="11%" height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                        导游电话：
                    </td>
                    <td width="31%" height="28" bgcolor="#E0E9EF">
                        <asp:TextBox ID="txttel" runat="server" class="inputtext formsize120"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        <span class="fontred">*</span>上团地点：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtStartAddress" runat="server" class="inputtext formsize120" errmsg="请输入上团地点!"
                            valid="required"></asp:TextBox>
                    </td>
                    <td height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                        <span class="fontred">*</span>上团时间：
                    </td>
                    <td height="28">
                        <asp:TextBox ID="txtDate_Start" name="txtDate_Start" runat="server" onfocus="WdatePicker({onpicked:function(){$('#txtDate_Start').focus();},minDate:'%y-%M-#{%d}'})"
                            class="inputtext formsize120" errmsg="请输入上团时间!" valid="required"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        <span class="fontred">*</span>下团地点：
                    </td>
                    <td bgcolor="#E0E9EF" align="left">
                        <asp:TextBox ID="txtEndAddress" runat="server" class="inputtext formsize120" errmsg="请输入下团地点!"
                            valid="required"></asp:TextBox>
                    </td>
                    <td height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                        <span class="fontred">*</span>下团时间：
                    </td>
                    <td height="28" bgcolor="#E0E9EF">
                        <asp:TextBox ID="txtDate_End" name="txtDate_End" runat="server" class="inputtext formsize120"
                            onfocus="WdatePicker({onpicked:function(){$('#txtDate_End').focus();},minDate:'%y-%M-#{%d}'})" errmsg="请输入下团时间!"
                            valid="required"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        <span class="fontred">*</span>任务类型：
                    </td>
                    <td height="28" align="left" colspan="3">
                        <%=GetGuidJobHtml(EyouSoft.Common.Utils.GetQueryStringValue("seleJobType"))%>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        行程接待：
                    </td>
                    <td height="28" bgcolor="#E0E9EF" align="left" colspan="5">
                        <asp:TextBox TextMode="MultiLine" ID="txttravel" name="txttravel" Style="height: 35px;"
                            class="inputtext formsize600" Rows="6" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        服务标准：
                    </td>
                    <td height="28" align="left" colspan="5">
                        <asp:TextBox TextMode="MultiLine" ID="txtService" name="txtService" Style="height: 35px;"
                            class="inputtext formsize600" Rows="6" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        其他备注：
                    </td>
                    <td height="28" bgcolor="#E0E9EF" align="left" colspan="5">
                        <asp:TextBox TextMode="MultiLine" ID="txtrmark" name="txtrmark" Style="height: 35px;"
                            Rows="6" runat="server" class="inputtext formsize600"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        费用明细：
                    </td>
                    <td height="28" align="left" colspan="5">
                        <asp:TextBox TextMode="MultiLine" ID="txtCostDesc" name="txtCostDesc" Style="height: 35px;"
                            Rows="6" runat="server" class="inputtext formsize600"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        <span class="fontred">*</span>结算费用：
                    </td>
                    <td height="28" bgcolor="#E0E9EF" align="left">
                        <asp:TextBox ID="txtPaidCost" name="txtPaidCost" class="inputtext formsize120" runat="server"
                            errmsg="请输入结算费用!" valid="required"></asp:TextBox>
                    </td>
                    <td height="28" bgcolor="#B7E0F3" align="right">
                        <span class="fontred">*</span>状态：
                    </td>
                    <td height="28" bgcolor="#E0E9EF" align="left">
                        <asp:DropDownList ID="ddlState" name="selectState" runat="server" CssClass="inputselect">
                            <asp:ListItem Value="3">未落实</asp:ListItem>
                            <asp:ListItem Value="4">已落实</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:void(0);" id="BtnSave"><s class="baochun"></s>保
                存</a><a hidefocus="true" href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();"><s
                    class="chongzhi"></s>关 闭</a>
        </div>
        <input  type="hidden" name="hidTourID" value=""/>
    </div>
    </form>

    <script type="text/javascript">        
        $(function() {
            GuidPlanPage.PageInit();
        });
        function callBackFun(obj) {
            $("#<%=txtTourCode.ClientID %>").val(obj.tourcode);
            $("#<%=txtDate_Start.ClientID %>").val(obj.ldate.substring(0, 9));
            $("#<%=txtDate_End.ClientID %>").val(obj.rdate.substring(0, 9));
            $("#<%=LineSelect1.LineNameClient %>").val(obj.text);
            $("input[type='hidden'][name='hidTourID']").val(obj.tourID);       
        }
        var GuidPlanPage = {
            GoAjax: function(url) {
                $.ajax({
                    type: "post",
                    cache: false,
                    url: url,
                    dataType: "json",
                    data: $("#BtnSave").closest("form").serialize(),
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() {
                                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                                parent.location.href = parent.location.href;
                            });
                        }
                        else {
                            tableToolbar._showMsg(ret.msg, function() {
                                $("#BtnSave").html("保存");
                                $("#BtnSave").bind("click");
                                $("#BtnSave").css("background-position", "0 0px");
                                $("#BtnSave").click(function() {
                                    $("#BtnSave").html("提交中...");
                                    $("#BtnSave").unbind("click");
                                    $("#BtnSave").css("background-position", "0 -55px");
                                    GuidPlanPage.GoAjax('/GuideCenter/GuidPlan.aspx?save=save&name=<%=EyouSoft.Common.Utils.GetQueryStringValue("name") %>&guideid=<%=EyouSoft.Common.Utils.GetQueryStringValue("guideid") %>&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>');
                                });
                            });
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            },
            _AjaxContectInfo: function(obj) {
                $('#<%=GuidControl1.GuidIDClient %>').val(obj.value);
                $('#<%=GuidControl1.GuidNameClient %>').val(obj.text);
                $("#<%=txttel.ClientID %>").val(obj.phone);
            },
            PageInit: function() {
                if ('<%=EyouSoft.Common.Utils.GetQueryStringValue("type") %>' == "停职") {
                    $("#BtnSave").html("已停职");
                    $("#BtnSave").unbind("click");
                }
                if ('<%=EyouSoft.Common.Utils.GetQueryStringValue("type") %>' == "假期") {
                    $("#BtnSave").html("请假中");
                    $("#BtnSave").unbind("click");
                }

                $("#BtnSave").click(function() {
                    if (!ValiDatorForm.validator($("#BtnSave").closest("form").get(0), "alert")) {
                        return false;
                    }
                    else {
                        $("#BtnSave").html("提交中...");
                        $("#BtnSave").unbind("click");
                        $("#BtnSave").css("background-position", "0 -55px");
                        GuidPlanPage.GoAjax('/GuideCenter/GuidPlan.aspx?save=save&name=<%=EyouSoft.Common.Utils.GetQueryStringValue("name") %>&guideid=<%=EyouSoft.Common.Utils.GetQueryStringValue("guideid") %>&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>');
                    }
                    return false;
                });
            }
        }
    </script>

</body>
</html>
