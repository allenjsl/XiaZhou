<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ObjAdd.aspx.cs" Inherits="Web.ManageCenter.ObjectManage.ObjAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

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
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="15%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>物品名称：
                </td>
                <td width="38%" height="28" align="left">
                    <asp:TextBox runat="server" ID="txtName" class="inputtext formsize120" valid="required"
                        errmsg="物品名称不能为空！"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hidGoodId" />
                </td>
                <td width="15%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    <span class="errmsg">*</span>物品数量：
                </td>
                <td width="32%" align="left">
                    <asp:TextBox runat="server" ID="txtCount" class="inputtext formsize40" valid="required|isInt|range"
                        min="1" errmsg="物品数量不能为空！|数量格式不正确!|物品数量最小为1"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hidGoodCount" />
                    <asp:HiddenField runat="server" ID="hidGoodStock" />
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    物品单价：
                </td>
                <td height="28" align="left">
                    <asp:TextBox runat="server" ID="txtPrice" valid="isMoney" errmsg="物品单价格式不正确!" class="inputtext formsize120"></asp:TextBox>
                </td>
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    <span class="errmsg">*</span>入库时间：
                </td>
                <td align="left">
                    <asp:TextBox runat="server" ID="txtInTime" class="inputtext formsize120" valid="required"
                        errmsg="入库时间不能为空！" onfocus="WdatePicker()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    用途
                </td>
                <td height="28" colspan="3" align="left">
                    <asp:TextBox runat="server" ID="txtUse" class="inputtext formsize450"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="15%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    登 记 人：
                </td>
                <td height="28" colspan="3" align="left">
                    <asp:Label runat="server" ID="lbRecorder"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    备&nbsp;&nbsp;&nbsp; 注：
                </td>
                <td height="28" colspan="3" align="left">
                    <asp:TextBox runat="server" ID="txtRemark" class="inputtext formsize450"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <asp:PlaceHolder runat="server" ID="ph_Save"><a href="javascript:void(0);" hidefocus="true"
                id="btnSave"><s class="baochun"></s>保 存</a></asp:PlaceHolder>
            <a href="javascript:void(0);" onclick="PageJsData.CloseBox()" hidefocus="true"><s
                class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

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
                    var url = "/ManageCenter/ObjectManage/ObjAdd.aspx?";
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
