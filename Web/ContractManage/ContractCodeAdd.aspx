<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractCodeAdd.aspx.cs"
    Inherits="Web.ContractManage.ContractCodeAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>合同管理-国内合同-登记合同</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: 0 none;">
    <form id="form1" method="post" runat="server">
    <div class="alertbox-outbox">
        <table width="100%" height="64" border="0" align="center" cellpadding="0" cellspacing="0">            
            <tr>
                <td width="14%" height="32" align="right" bgcolor="#b7e0f3">
                    <span class="alertboxTableT">合同类型：</span>
                </td>
                <td width="86%" height="32" align="left">
                    <%=HeTongLeiXing%>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3">
                    <label><span class="alertboxTableT">合同号段：</span></label>
                </td>
                <td height="32" align="left">
                    前缀&nbsp;<asp:TextBox ID="txtPrefix" runat="server" class="formsize100 inputtext" valid="required"
                        errmsg="合同号前缀不能为空！"></asp:TextBox>
                    &nbsp;起始序号&nbsp;<asp:TextBox ID="txtStart" runat="server" Width="40" CssClass="inputtext"
                        valid="RegInteger" errmsg="起始序号必须是数字!"></asp:TextBox>
                    &nbsp;截止序号&nbsp;<asp:TextBox ID="txtEnd" runat="server" Width="40" CssClass="inputtext"
                        valid="RegInteger" errmsg="截止序号必须是数字!"></asp:TextBox>
                    &nbsp;序号长度&nbsp;<asp:TextBox ID="txtXuHaoChangDu" runat="server" Width="40" CssClass="inputtext"
                        valid="required|RegInteger" errmsg="请输入序号长度|序号长度必须是数字!" MaxLength="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 32px; color:#666; line-height:24px;">
                    说明：按指定的起止序号生成前缀+序号长度位数字序列的合同号，不足序号长度的补<span style="color: Red">0</span>。<br/>
                    如前缀HT，起序号99，止序号101，序号长度4，将会生成以下合同号：HT<span style="color:Red">00</span>99，HT<span style="color: Red">0</span>100，HT<span
                        style="color: Red">0</span>101。
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" hidefocus="true" id="btnSave"><s class="baochun"></s>保
                存</a> <a href="javascript:void(0);" onclick="ContractCodeAdd.ResetForm()" hidefocus="true">
                    <s class="chongzhi"></s>重 置</a>
        </div>
    </div>
    </form>

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script type="text/javascript">
        var ContractCodeAdd = {
            Query: {/*URL参数对象*/
                sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                type: '<%=EyouSoft.Common.Utils.GetQueryStringValue("type") %>'
            },
            ResetForm: function() {
                document.getElementById('form1').reset();
            },
            Form: null,
            FormCheck: function() {
                var errorMsg = CheckConNum();
                if (errorMsg != "") {
                    parent.tableToolbar._showMsg(errorMsg);
                    return false;
                }

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
                    var url = "/ContractManage/ContractCodeAdd.aspx?";
                    url += $.param({
                        doType: 'save',
                        type: this.Query.type,
                        sl: this.Query.sl
                    });
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: url,
                        data: $(that.Form).serialize().replace(),
                        dataType: "json",
                        success: function(result) {
                            if (result.result == 'True') {
                                parent.tableToolbar._showMsg(result.msg, function() {
                                    parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                                    parent.window.document.getElementById("btnSubmit").click();
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

        //检查合同号是否规范
        function CheckConNum() {
            var errorMsg = "";
            var start = $("#txtStart").val().replace(/\s+/g, "");
            var end = $("#txtEnd").val().replace(/\s+/g, "");
            if (start != "" && end != "") {
                if (parseInt(start) - parseInt(end) < -200 || parseInt(start) - parseInt(end) > 200) {
                    errorMsg = "起始序号和截止序号只能相差200！";
                }
            }
            if (start == "" && end == "") {
                errorMsg = "起始序号和截止序号不能同时为空！";
            }
            return errorMsg;
        }
    </script>

</body>
</html>
