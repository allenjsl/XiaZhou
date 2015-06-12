<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkReportEdit.aspx.cs"
    Inherits="Web.UserCenter.WorkExchange.WorkReportEdit" %>

<%@ Register Src="../../UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/SellsSelect.ascx" TagName="SelectSection" TagPrefix="uc2" %>
<%@ Register Src="../../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery.boxy.js"></script>

    <script type="text/javascript" src="/js/table-toolbar.js"></script>

    <script type="text/javascript" src="/js/Newjquery.autocomplete.js"></script>

    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="13%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                   汇报标题：
                </td>
                <td width="41%" height="28" align="left">
                    <asp:TextBox ID="txtTitle" CssClass="inputtext formsize120" name="txtTitle" runat="server"
                        errmsg="请输入标题!" valid="required"></asp:TextBox><b class="fontred">*</b> 
                </td>
                <td width="13%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    汇报人：
                </td>
                <td width="33%" align="left">
                    <asp:TextBox ID="txtUserName" CssClass="inputtext formsize80" runat="server" errmsg="请输入汇报人!"
                        valid="required"></asp:TextBox><b class="fontred">*</b> 
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    汇报部门：
                </td>
                <td height="28" align="left">
                    <uc1:SelectSection ID="SelectSection1" runat="server" ReadOnly="true" SetTitle="汇报部门" /><b class="fontred">*</b> 
                </td>
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    汇报时间：
                </td>
                <td align="left">
                    <asp:TextBox ID="txtDateTime" CssClass="inputtext formsize80" runat="server" onfocus="WdatePicker()"
                        errmsg="请输入汇报时间!" valid="required"></asp:TextBox><b class="fontred">*</b> 
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    汇报内容：
                </td>
                <td height="50" colspan="3" align="left">
                    <asp:TextBox TextMode="MultiLine" Style="height: 50px;" cols="20" Rows="2" ID="txtContent"
                        CssClass="inputtext formsize450" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    附件上传：
                </td>
                <td colspan="3" align="left">
                    <asp:Literal ID="lclFile" runat="server"></asp:Literal>
                    <uc3:UploadControl ID="UploadControl1" runat="server" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    审批人：
                </td>
                <td align="left" class="kuang2" colspan="3">
                    <uc2:SelectSection ID="SelectSection2" runat="server" SetTitle="审批人" SMode="true" /><b class="fontred">*</b> 
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <asp:PlaceHolder ID="phdSave" runat="server"><a href="javascript:void(0)" hidefocus="true"
                id="btnSave" onclick="return WorkReportEditPage.CheckForm()"><s class="baochun">
                </s>保 存</a></asp:PlaceHolder>
            <a href="javascript:void(0)" hidefocus="true" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;">
                <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    <%--存储操作id--%>
    <asp:HiddenField ID="hidId" runat="server" />
    </form>

    <script type="text/javascript">
        $(function() {
            WorkReportEditPage.BindBtn();
        })

        var WorkReportEditPage = {
            data: {
                sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>'
            },
            CheckForm: function() {
                var form = $("#btnSave").closest("form").get(0);
                return ValiDatorForm.validator(form, "parent");
            },
            Save: function() {
                if (WorkReportEditPage.CheckForm()) {
                    $("#btnSave").html('<s class="baochun"></s>保存中').unbind("click");
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: "/UserCenter/WorkExchange/WorkReportEdit.aspx?doType=AjaxSave&sl=" + WorkReportEditPage.data.sl,
                        data: $("#btnSave").closest("form").serialize(),
                        dataType: "json",
                        success: function(ret) {
                            if (ret.result == "true") {
                                tableToolbar._showMsg(ret.msg, function() {
                                    window.parent.location.href = window.parent.location.href;
                                });
                            } else {
                                tableToolbar._showMsg(ret.msg);
                                WorkReportEditPage.BindBtn();
                                $("#btnSave").html('<s class="baochun"></s>保存');
                            }

                        },
                        error: function() {
                            WorkReportEditPage.BindBtn();
                            $("#btnSave").html('<s class="baochun"></s>保存');
                            tableToolbar._showMsg(tableToolbar.errorMsg);
                        }
                    });
                }
            },
            //删除签证附件
            RemoveVisaFile: function(obj) {
                $(obj).parent().remove();
            },
            BindBtn: function() {
                $("#btnSave").click(function() {
                    WorkReportEditPage.Save();
                    return false;
                })
            }
        }
    </script>

</body>
</html>
