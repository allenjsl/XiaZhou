<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PackAdd.aspx.cs" Inherits="Web.ManageCenter.Pack.PackAdd" %>

<%@ Register Src="~/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/HrSelect.ascx" TagName="HrSelect" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <style type="text/css">
        .errmsg
        {
            color: #f00;
            font-size: 12px;
        }
        .alertbox-outbox table .progressWrapper
        {
            overflow: hidden;
            width: 190px;
        }
        .alertbox-outbox table .progressName
        {
            width: 130px;
        }
    </style>
</head>
<body style="background: 0 none;"> 
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>合同编号：
                </td>
                <td width="30%" height="28" align="left">
                    <asp:TextBox runat="server" ID="txtNumber" class="inputtext formsize120" valid="required|custom"
                        custom="PageJsData.CheckNum" errmsg="合同编号不能为空！|对不起，该编号已存在！"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hidKeyId" />
                </td>
                <td width="15%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    <span class="errmsg">*</span>合同类型：
                </td>
                <td width="40%" align="left">
                    <asp:TextBox runat="server" ID="txtType" valid="required" errmsg="合同编号不能为空！"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>合同单位：
                </td>
                <td align="left">
                    <asp:TextBox runat="server" ID="txtCompany" class="inputtext formsize120" valid="required"
                        errmsg="合同单位不能为空！"></asp:TextBox>
                </td>
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    <span class="errmsg">*</span>合同有效期：
                </td>
                <td align="left">
                    <asp:TextBox runat="server" ID="txtStartTime" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'txtEndTime\')}',dateFmt:'yyyy-MM-dd'})"
                        valid="required" errmsg="合同有效期不能为空！">
                    </asp:TextBox>-<asp:TextBox runat="server" ID="txtEndTime" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtStartTime\')}',dateFmt:'yyyy-MM-dd'})"
                        valid="required" errmsg="合同有效期不能为空！"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    合同描述：
                </td>
                <td colspan="3" align="left">
                    <asp:TextBox runat="server" ID="txtContent" TextMode="MultiLine" Width="350" Height="65"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    合同附件上传：
                </td>
                <td height="28" align="left">
                    <uc2:UploadControl runat="server" ID="SingleFileUpload1" IsUploadMore="true" IsUploadSelf="true" />
                    <asp:Label runat="server" ID="lbFiles" CssClass="labelFiles"></asp:Label><br />
                </td>
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    是否到期提醒：
                </td>
                <td align="left">
                    <asp:RadioButton runat="server" ID="warn" Text="是" GroupName="Iswarn" />
                    <asp:RadioButton runat="server" ID="nowarn" Text="否" GroupName="Iswarn" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>签订人：
                </td>
                <td height="28" align="left">
                    <uc3:HrSelect runat="server" ID="HrSelect1" ReadOnly="true" SetTitle="签订人" SModel="1" />
                </td>
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    <span class="errmsg">*</span>签订部门：
                </td>
                <td align="left">
                    <uc1:SelectSection runat="server" ID="SelectSection1" ReadOnly="true" SetTitle="签订部门"
                        SModel="1" />
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <asp:PlaceHolder runat="server" ID="ph_Save"><a href="javascript:void(0);" hidefocus="true"
                id="btnSave"><s class="baochun"></s>保 存</a> </asp:PlaceHolder>
            <a href="javascript:void(0);" onclick="PageJsData.ResetForm()" hidefocus="true"><s
                class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

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
            CheckNum: function(obj) {
                var s = "";
                var url = "/ManageCenter/Pack/PackAdd.aspx?";
                url += $.param({
                    sl: this.Query.sl,
                    doType: "checkNum",
                    num: $(obj).val(),
                    id: '<%=Request.QueryString["id"]%>'
                })
                $.newAjax({
                    type: "get",
                    url: url,
                    success: function(data) { s = data; },
                    async: false
                });
                return s != "1" ? true : false;
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
                if (PageJsData.FormCheck()) {
                    $("#btnSave").unbind("click").addClass("alertbox-btn_a_active").html("<s class=\"baochun\"></s> 提交中...");
                    var url = "/ManageCenter/Pack/PackAdd.aspx?";
                    url += $.param({
                        doType: PageJsData.Query.doType,
                        save: "save",
                        sl: PageJsData.Query.sl
                    });
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: url,
                        data: $(PageJsData.Form).serialize().replace(),
                        dataType: "json",
                        success: function(result) {
                            if (result.result == "1") {
                                parent.tableToolbar._showMsg(result.msg, function() {
                                    parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                                    parent.window.document.getElementById("btnSubmit").click();
                                });

                            }
                            else { parent.tableToolbar._showMsg(result.msg, function() { PageJsData.BindBtn(); }); }
                            return false;
                        },
                        error: function() {
                            //ajax异常--你懂得
                            parent.tableToolbar._showMsg(tableToolbar.errorMsg, function() { PageJsData.BindBtn(); });
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
