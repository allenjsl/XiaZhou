<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RuleAdd.aspx.cs" Inherits="Web.ManageCenter.Rule.RuleAdd" %>

<%@ Register Src="~/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/HrSelect.ascx" TagName="HrSelect" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <style type="text/css">
        .errmsg
        {
            font-size: 12px;
            color: Red;
        }
        .alertbox-outbox table .progressWrapper
        {
            overflow: hidden;
            width: 300px;
        }
    </style>
</head>
<body style="background: 0 none;">
    <form id="form1" method="post" runat="server" enctype="multipart/form-data">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>制度编号：
                </td>
                <td width="39%" height="28" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox runat="server" ID="txtRuleId" CssClass="inputtext formsize180" valid="required|custom"
                        custom="PageJsData.CheckNum" errmsg="编号不能为空！|对不起，该编号已存在！"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hidRuleId" />
                </td>
                <td height="11" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>制度标题：
                </td>
                <td height="39" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox runat="server" ID="txtRuleTitle" CssClass="inputtext formsize180" valid="required"
                        errmsg="制度标题不能为空！"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    制度内容：
                </td>
                <td align="left" bgcolor="#e0e9ef" colspan="3">
                    <asp:TextBox runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"
                        ID="txtRuleContent" Width="700" Height="300"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    附件上传：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <uc2:UploadControl runat="server" ID="SingleFileUpload1" IsUploadMore="true" IsUploadSelf="true" />
                    <asp:Label runat="server" ID="lbFiles" CssClass="labelFiles"></asp:Label>
                </td>
                <td align="right" bgcolor="#B7E0F3">
                    <span class="errmsg">*</span>适用部门：
                </td>
                <td align="left">
                    <uc1:SelectSection runat="server" ID="SelectSection1" ReadOnly="true" SetTitle="适用部门"
                        SModel="2" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>发布部门：
                </td>
                <td height="28" align="left">
                    <uc1:SelectSection runat="server" ID="SelectSection2" ReadOnly="true" SetTitle="发布部门"
                        SModel="1" />
                </td>
                <td align="right" bgcolor="#B7E0F3">
                    <span class="errmsg">*</span>发布人：
                </td>
                <td align="left">
                    <uc3:HrSelect runat="server" ID="HrSelect1" ReadOnly="true" SetTitle="发布人" SModel="1" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    发布时间：
                </td>
                <td height="28" colspan="3" align="left">
                    <asp:TextBox runat="server" CssClass="inputtext formsize120" ID="IssueTime" onfocus="WdatePicker()"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <asp:PlaceHolder runat="server" ID="ph_save"><a href="javascript:void(0);" hidefocus="true"
                id="btnSave"><s class="baochun"></s>保 存</a> </asp:PlaceHolder>
            <a href="javascript:void(0);" onclick="PageJsData.CloseForm()" hidefocus="true"><s
                class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

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
            CloseForm: function() {
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
            },
            CheckNum: function(obj) {
                var s = "";
                var url = "RuleAdd.aspx?";
                url += $.param({
                    sl: this.Query.sl,
                    doType: "checkDutyName",
                    num: $(obj).val(),
                    id: '<%=Request.QueryString["id"] %>'
                })
                $.newAjax({
                    type: "get",
                    url: url,
                    success: function(data) {
                        s = data;
                    },
                    async: false
                })
                return s != "1" ? true : false;
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
                    var url = "/ManageCenter/Rule/RuleAdd.aspx?";
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
