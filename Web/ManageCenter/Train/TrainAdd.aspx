<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainAdd.aspx.cs" Inherits="Web.ManageCenter.Train.TrainAdd" %>

<%@ Register Src="~/UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
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

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="19%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>培训时间：
                </td>
                <td width="39%" height="28" align="left">
                    <asp:TextBox runat="server" ID="txtSTime" class="inputtext formsize120" valid="required"
                        errmsg="培训时间不能为空！" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"></asp:TextBox>-
                    <asp:TextBox runat="server" ID="txtETime" class="inputtext formsize120" valid="required"
                        errmsg="培训时间不能为空！" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"></asp:TextBox>
                </td>
                <td width="12%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    <span class="errmsg">*</span>主题：
                </td>
                <td width="30%" align="left">
                    <asp:TextBox runat="server" ID="txtTheme" valid="required" errmsg="培训主题不能为空！"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hidKeyId" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>参加人员：
                </td>
                <td colspan="3" align="left">
                    <asp:TextBox runat="server" ID="txtJoinpeo" class="inputtext formsize450" Style="width: 501px"
                        valid="required" errmsg="参与培训人员不能为空！"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>培训地点：
                </td>
                <td height="28" align="left">
                    <asp:TextBox runat="server" ID="txtPlace" valid="required" errmsg="培训地点不能为空！"></asp:TextBox>
                </td>
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    <span class="errmsg">*</span>培训人：
                </td>
                <td align="left">
                    <asp:TextBox runat="server" valid="required" errmsg="培训人不能为空！" ID="txtTrainpeo"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="65" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    培训内容：
                </td>
                <td height="65" colspan="3" align="left">
                    <asp:TextBox runat="server" ID="txtTraincon" TextMode="MultiLine" class="inputtext formsize450"
                        Height="60"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    培训通知附件上传
                </td>
                <td height="28" colspan="3" align="left">
                    <uc1:UploadControl runat="server" ID="SingleFileUpload1" IsUploadMore="true" IsUploadSelf="true" />
                    <asp:Label runat="server" ID="lbNotice" CssClass="labelFiles"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    培训记录附件上传
                </td>
                <td height="28" colspan="3" align="left">
                    <uc1:UploadControl runat="server" ID="SingleFileUpload2" IsUploadMore="true" IsUploadSelf="true" />
                    <asp:Label runat="server" ID="lbRemark" CssClass="labelFiles"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    附件上传：
                </td>
                <td height="28" colspan="3" align="left">
                    <uc1:UploadControl runat="server" ID="SingleFileUpload3" IsUploadMore="true" IsUploadSelf="true" />
                    <asp:Label runat="server" ID="lbFiles" CssClass="labelFiles"></asp:Label><br />
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <asp:PlaceHolder runat="server" ID="ph_Save"><a href="javascript:void(0);" hidefocus="true"
                id="btnSave"><s class="baochun"></s>保 存</a></asp:PlaceHolder>
            <a href="javascript:void(0);" onclick="PageJsData.ResetForm()" hidefocus="true"><s
                class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

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
            ResetForm: function() {
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
            },
            Form: null,
            FormCheck: function() {
                this.Form = $("#btnSave").closest("form").get(0)
                FV_onBlur.initValid(this.Form);
                return ValiDatorForm.validator(this.Form, "alert");
            },
            Save: function() {
                if (PageJsData.FormCheck()) {
                    $("#btnSave").unbind("click").addClass("alertbox-btn_a_active").html("<s class=\"baochun\"></s> 提交中...");
                    var url = "/ManageCenter/Train/TrainAdd.aspx?";
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
                            parent.tableToolbar._showMsg(tableToolbar.errorMsg, function() {
                                PageJsData.BindBtn();
                            });

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
            },
            CallBackFun: function(data) {
                newToobar.backFun(data);
            }
        }
        $(function() {
            PageJsData.BindBtn();
        });
    </script>

</body>
</html>
