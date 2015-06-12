<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveAdd.aspx.cs" Inherits="Web.ManageCenter.Leave.LeaveAdd" %>

<%@ Register Src="~/UserControl/HrSelect.ascx" TagName="HrSelect" TagPrefix="uc2" %>
<%@ Register Src="../../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/js/jquery.boxy.js"></script>

    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/js/table-toolbar.js"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <style type="text/css">
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
                <td width="9%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>申请人：
                </td>
                <td width="35%" height="28" align="left" bgcolor="#e0e9ef" class="UserInfo">
                    <uc2:HrSelect runat="server" ID="HrSelect1" ReadOnly="true" SetTitle="离职申请人" SModel="1" />
                    <asp:HiddenField runat="server" ID="hidKeyId" />
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    离职申请原因：
                </td>
                <td height="65" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox runat="server" TextMode="MultiLine" class="inputtext formsize450" ID="txtApplyCause"
                        Height="60" Width="450"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    希望离职时间：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox runat="server" ID="txtWantTime" class="inputtext formsize80" onfocus="WdatePicker()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>审批人：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <uc1:SellsSelect ID="SellsSelect1" runat="server" SetTitle="审批人" SMode="true" />
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
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script type="text/javascript">

        var PageJsData = {
            Query: {/*URL参数对象*/
                sl: '<%=Request.QueryString["sl"] %>'
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
                    var url = "/ManageCenter/Leave/LeaveAdd.aspx?";
                    url += $.param({
                        doType: '<%=Request.QueryString["doType"] %>',
                        save: "save",
                        sl: this.Query.sl
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
                                    parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                                    parent.window.document.getElementById("btnSubmit").click();
                                });
                            }
                            else {
                                parent.tableToolbar._showMsg(result.msg, function() {
                                    PageJsData.BindBtn();
                                });
                            }
                            return false;
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
                $("#btnSave").unbind("click");
                $("#btnSave").click(function() {
                    PageJsData.Save();
                    return false;
                })
                $("#btnSave").attr("class", "").html("<s class=\"baochun\"></s>保 存");
            }
        }

        $(function() {
            PageJsData.BindBtn();
        })
    </script>

</body>
</html>
