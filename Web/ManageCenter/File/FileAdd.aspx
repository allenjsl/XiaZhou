<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileAdd.aspx.cs" Inherits="Web.ManageCenter.File.FileAdd" %>

<%@ Register Src="~/UserControl/HrSelect.ascx" TagName="HrSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc2" %>
<%@ Register Src="../../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <style type="text/css">
        .alertbox-outbox table .progressWrapper
        {
            overflow: hidden;
            width: 210px;
        }
        .alertbox-outbox table .progressContainer
        {
            overflow: hidden;
            width: 190px;
        }
        .alertbox-outbox table .progressName
        {
            width: 140px;
        }
        .errmsg
        {
            color: #f00;
            font-size: 12px;
        }
    </style>
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>文件字号：
                </td>
                <td height="28" colspan="3" align="left">
                    <asp:TextBox runat="server" ID="txtfileSize" valid="required" errmsg="文件字号不能为空！"
                        class="inputtext formsize120"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hidKeyId" />
                </td>
            </tr>
            <tr>
                <td width="18%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>文件发布单位：
                </td>
                <td width="35%" height="28" align="left">
                    <asp:TextBox runat="server" ID="txtcompany" valid="required" errmsg="发布单位不能为空！" class="inputtext formsize180"></asp:TextBox>
                </td>
                <td width="15%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    文件标题：
                </td>
                <td width="32%" align="left">
                    <span class="errmsg">*</span>
                    <asp:TextBox runat="server" ID="txttitle" valid="required" errmsg="文件标题不能为空！" class="inputtext formsize180"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    文件上传：
                </td>
                <td height="28" align="left">
                    <uc2:UploadControl runat="server" ID="SingleFileUpload1" IsUploadMore="true" IsUploadSelf="true" />
                    <asp:Label runat="server" ID="lbFiles" CssClass="labelFiles"></asp:Label>
                </td>
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    审批或传阅：
                </td>
                <td align="left">
                    <asp:RadioButton runat="server" ID="shenpi" GroupName="isPass" Text="审批" />
                    <asp:RadioButton runat="server" ID="chuanyue" GroupName="isPass" Text="传阅" Checked="true" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>审批/传阅人：
                </td>
                <td height="28" align="left">
                    <uc1:SellsSelect ID="SellsSelect1" runat="server" SetTitle="审批人" SMode="true" />
                </td>
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    <span class="errmsg">*</span>经办人：
                </td>
                <td align="left">
                    <uc2:HrSelect runat="server" ID="HrSelect1" ReadOnly="true" SetTitle="经办人" />
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

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

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
                if (PageJsData.FormCheck()) {
                    $("#btnSave").unbind("click").addClass("alertbox-btn_a_active").html("<s class=\"baochun\"></s> 提交中...");
                    var url = "/ManageCenter/File/FileAdd.aspx?";
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
                $("#btnSave").attr("class", "").html("<s class=\"baochun\"></s>保    存");
            }
        }
        $(function() {
            PageJsData.BindBtn();
        });
        
    </script>

</body>
</html>
