<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ObjLendAdd.aspx.cs" Inherits="Web.ManageCenter.ObjectManage.ObjLendAdd" %>

<%@ Register Src="~/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/HrSelect.ascx" TagName="HrSelect" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

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
    <form id="form1" method="post" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td width="31%" height="28" align="right" class="alertboxTableT">
                    <span class="errmsg">*</span>物品名称：
                </td>
                <td width="69%" height="28" align="left">
                    <asp:Label runat="server" ID="lbName"></asp:Label>
                    <asp:HiddenField runat="server" ID="hidName" />
                    <asp:HiddenField runat="server" ID="hidGoodId" />
                    <asp:HiddenField runat="server" ID="hidPrice" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" class="alertboxTableT">
                    <span class="errmsg">*</span>借阅时间：
                </td>
                <td height="28" align="left">
                    <asp:TextBox runat="server" ID="txtOutTime" class="inputtext formsize120" valid="required"
                        errmsg="借出时间不能为空！" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'txtInTime\')}',dateFmt:'yyyy-MM-dd HH:mm'})"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" class="alertboxTableT">
                    <span class="errmsg">*</span>借阅部门：
                </td>
                <td height="28" align="left">
                    <uc1:SelectSection ID="SelectSection1" runat="server" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" class="alertboxTableT">
                    <span class="errmsg">*</span>借 阅 人：
                </td>
                <td height="28" align="left">
                    <uc1:HrSelect runat="server" ID="HrSelect1" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" class="alertboxTableT">
                    <span class="errmsg">*</span>借阅数量：
                </td>
                <td height="28" align="left">
                    <asp:TextBox runat="server" ID="txtCount" class="inputtext formsize40" valid="required|range"
                        min="1" errmsg="物品数量不能为空！|物品数量最小为1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" class="alertboxTableT">
                    用&nbsp;&nbsp;&nbsp; 途：
                </td>
                <td height="28" align="left">
                    <asp:TextBox runat="server" ID="txtUse" TextMode="MultiLine" class="inputtext formsize400"
                        Width="300" Height="40"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" class="alertboxTableT">
                    经 办 人：
                </td>
                <td height="28" align="left">
                    <asp:Label runat="server" ID="lbHandler"></asp:Label>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" hidefocus="true" id="btnSave"><s class="baochun"></s>保
                存</a> <a href="javascript:void(0);" onclick="PageJsData.CloseBox()" hidefocus="true">
                    <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript">
        var PageJsData = {
            Query: {/*URL参数对象*/
                sl: '<%=Request.QueryString["sl"] %>'
            },
            ResetForm: function() {
                document.getElementById('form1').reset();
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
                    var url = "/ManageCenter/ObjectManage/ObjLendAdd.aspx?";
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
                            if (result.result == "0") {
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
            },
            CloseBox: function() {
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
            }
        }
        $(function() {
            PageJsData.BindBtn();
        });
    </script>

</body>
</html>
