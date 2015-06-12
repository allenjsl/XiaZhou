<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetContract.aspx.cs" Inherits="EyouSoft.Web.SellCenter.Order.SetContract" %>
<%@ Register Src="~/UserControl/HeTongHao.ascx" TagName="HeTongHao" TagPrefix="uc9" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
 <title>登记合同</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>
    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>
    <link href="/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="100%" height="64" border="0" align="center" cellpadding="0" cellspacing="0">            
            <tr>
                <td width="14%" height="32" align="right" bgcolor="#b7e0f3">
                    订<span class="alertboxTableT">单号：</span>
                </td>
                <td width="86%" height="32" align="left">
                    <asp:Literal ID="lblOrderNum" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td width="14%" height="32" align="right" bgcolor="#b7e0f3">
                    客源单位：</td>
                <td width="86%" height="32" align="left">
                    <asp:Literal ID="ltrBuyCompanyName" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3">
                    <label><span class="alertboxTableT">合同号：</span></label>
                </td>
                <td height="32" align="left">
                    <uc9:hetonghao runat="server" id="txtHeTongHao"></uc9:hetonghao></td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" hidefocus="true" id="btnSave"><s class="baochun"></s>保
                存</a> <a href="javascript:void(0);" onclick="ContractCodeAdd.ResetForm()" hidefocus="true">
                    <s class="chongzhi"></s>重 置</a>
        </div>
    </div>
    </form>  
    <script type="text/javascript">
        var ContractCodeAdd = {
            Query: {/*URL参数对象*/
                sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                orderId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("orderId") %>'
            },
            ResetForm: function() {
                document.getElementById('form1').reset();
            },
            Form: null,
            FormCheck: function() {
                this.Form = $("#btnSave").closest("form").get(0);
                FV_onBlur.initValid(this.Form);
                return ValiDatorForm.validator(this.Form, "parent");
            },
            Save: function() {
                var that = this;
                if (that.FormCheck()) {
                    $("#btnSave").unbind("click");
                    $("#btnSave").addClass("alertbox-btn_a_active");
                    $("#btnSave").html("<s class=\"baochun\"></s> 提交中...");
                    var url = "/SellCenter/Order/SetContract.aspx?";
                    url += $.param({
                        doType: 'save',
                        orderId: this.Query.orderId,
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
                                    parent.window.location = parent.window.location;
                                });
                            }
                            else {
                                $("#btnSave").bind("click", function() {
                                    ContractCodeAdd.Save();
                                });
                                $("#btnSave").attr("class", "");
                                $("#btnSave").html("<s class=\"baochun\"></s>保 存");
                                parent.tableToolbar._showMsg(result.msg);
                            }
                        },
                        error: function() {
                            //ajax异常--你懂得
                            parent.tableToolbar._showMsg("服务器忙！");
                            ContractCodeAdd.BindBtn();
                        }
                    });
                }
            },
            BindBtn: function() {
                $("#btnSave").click(function() {
                    ContractCodeAdd.Save();
                    return false;
                })
                $("#btnSave").attr("class", "");
                $("#btnSave").html("<s class=\"baochun\"></s>保 存");
            }
        }
        $(function() {
            ContractCodeAdd.BindBtn();
        })
    </script>
</body>
</html>
