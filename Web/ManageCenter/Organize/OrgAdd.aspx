<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrgAdd.aspx.cs" Inherits="Web.ManageCenter.Organize.OrgAdd" %>

<%@ Register Src="~/UserControl/HrSelect.ascx" TagName="HrSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .errmsg
        {
            color: #f00;
            font-size: 12px;
        }
    </style>

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" method="post" runat="server" enctype="multipart/form-data">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="18%" height="25" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>部门名称：
                </td>
                <td width="35%" height="28" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox runat="server" ID="txtDepartName" class="inputtext formsize120" valid="required"
                        errmsg="部门名称不能为空！"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hidDepartId" />
                </td>
                <td width="16%" align="right" bgcolor="#B7E0F3">
                    部门主管：
                </td>
                <td width="31%" align="left" bgcolor="#e0e9ef" class="UserInfo">
                    <uc2:HrSelect runat="server" ID="HrSelect1" ReadOnly="true" SModel="1" IsValid="false" />
                </td>
            </tr>
            <tr>
                <td height="25" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    上级部门：
                </td>
                <td align="left" colspan="3" bgcolor="#e0e9ef">
                    <asp:TextBox ID="txtUpSection" runat="server" Enabled="false" CssClass="inputtext formsize120"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hidupsectionId" />
                </td>
            </tr>
            <tr>
                <td height="25" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    联系电话：
                </td>
                <td align="left" bgcolor="#e0e9ef">
                    <asp:TextBox runat="server" ID="txtContact" class="inputtext formsize120"></asp:TextBox>
                </td>
                <td align="right" bgcolor="#B7E0F3">
                    传真：
                </td>
                <td align="left" bgcolor="#e0e9ef">
                    <asp:TextBox runat="server" ID="txtFaxa" class="inputtext formsize120" valid="isPhone"
                        errmsg="传真格式不正确！"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="25" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    打印页眉：
                </td>
                <td height="28" colspan="3" align="left" bgcolor="#e0e9ef">
                    <uc1:UploadControl runat="server" ID="uc_Head" IsUploadMore="false" IsUploadSelf="true" />
                    <asp:Label runat="server" ID="lbTxtHead" CssClass="labelFiles"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="25" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    打印页脚：
                </td>
                <td height="28" colspan="3" align="left" bgcolor="#e0e9ef"">
                    <uc1:UploadControl runat="server" ID="uc_Foot" IsUploadMore="false" IsUploadSelf="true" />
                    <asp:Label runat="server" ID="lbTxtFoot" CssClass="labelFiles"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="25" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    打印模板：
                </td>
                <td height="28" colspan="3" align="left" bgcolor="#e0e9ef">
                    <uc1:UploadControl runat="server" ID="uc_Temp" IsUploadMore="false" IsUploadSelf="true" />
                    <asp:Label runat="server" ID="lbTxtTemp" CssClass="labelFiles"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="25" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    部门公章：
                </td>
                <td height="28" colspan="3" align="left" bgcolor="#e0e9ef">
                    <uc1:UploadControl runat="server" ID="uc_Seal" IsUploadMore="false" IsUploadSelf="true" />
                    <asp:Label runat="server" ID="lbTxtSeal" CssClass="labelFiles"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    备注：
                </td>
                <td colspan="3" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox runat="server" ID="txtRemark" Height="100" class="inputtext formsize450"
                        TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" hidefocus="true" id="btnSave"><s class="baochun"></s>保
                存</a> <a href="javascript:void(0);" onclick="PageJsData.ResetForm()" hidefocus="true">
                    <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script type="text/javascript">
        var PageJsData = {
            Query: {/*URL参数对象*/
                sl: '<%=Request.QueryString["sl"] %>',
                doType: '<%=Request.QueryString["doType"] %>'
            },
            DelFile: function(obj) {
                var self = $(obj);
                self.closest("span").hide();
                self.next(":hidden").val("");
            },
            ResetForm: function() {
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
            },
            Form: null,
            FormCheck: function() {
                this.Form = $("#btnSave").closest("form").get(0)
                FV_onBlur.initValid(this.Form);
                return ValiDatorForm.validator(this.Form, "parent");
            },
            Save: function() {
                var that = this;
                if (that.FormCheck()) {
                    $("#btnSave").unbind("click").addClass("alertbox-btn_a_active").html("<s class=\"baochun\"></s> 提交中...");
                    var url = "/ManageCenter/Organize/OrgAdd.aspx?";
                    url += $.param({
                        doType: that.Query.doType,
                        save: "save",
                        sl: that.Query.sl
                    });
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: url,
                        data: $(that.Form).serialize().replace(),
                        dataType: "json",
                        success: function(result) {
                            if (result.result == "1") {
                                parent.tableToolbar._showMsg(result.msg, function() {
                                    if (that.Query.doType == 'add') {
                                        if (result.id != 0) {
                                            window.parent.DM.callbackAddD(result.id);
                                        }
                                        else {
                                            parent.location.reload();
                                        }
                                    }
                                    else {
                                        window.parent.DM.callbackUpdateD(result.id, result.nowName, result.updateP)
                                    }
                                    parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                                });

                            }
                            else { parent.tableToolbar._showMsg(result.msg, function() { PageJsData.BindBtn(); }); }
                        },
                        error: function() {
                            parenttabl.eToolbar._showMsg(tableToolbar.errorMsg, function() { PageJsData.BindBtn(); });
                        }
                    });
                }
            },
            BindBtn: function() {
                $("#btnSave").click(function() {
                    PageJsData.Save();
                    return false;
                })
                $("#btnSave").attr("class", "").html("<s class=\"baochun\"></s>保 存");
            }
        }

        $(function() {
            PageJsData.BindBtn();
        });
    </script>

</body>
</html>
