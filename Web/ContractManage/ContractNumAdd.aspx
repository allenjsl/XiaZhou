<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractNumAdd.aspx.cs"
    Inherits="Web.ContractManage.ContractNumAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登记合同号</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <style type="text/css">
        .errmsg
        {
            font-size: 12px;
            color: Red;
        }
    </style>
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table id="tbl" width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td width="22%" height="32" align="right" bgcolor="#b7e0f3">
                    <span class="alertboxTableT">合同类型：</span>
                </td>
                <td width="78%" height="32" align="left">
                    <span class="kuang2">
                    <asp:DropDownList ID="ddlConType" runat="server" valid="required|RegInteger" errmsg="请选择合同号类型|请选择合同号类型"></asp:DropDownList>
                    </span>
                </td>
            </tr>
            <tr>
                <td width="22%" height="32" align="right" bgcolor="#b7e0f3">
                    <span class="alertboxTableT">合同编码：</span>
                </td>
                <td width="78%" height="32" align="left">
                    <asp:TextBox ID="txtCode" runat="server" Class="formsize140" errmsg="请输入编码!" valid="required"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3">
                    <label><span class="alertboxTableT">合同号段：</span></label>
                </td>
                <td height="32" align="left">
                    <table width="377" border="0" style="margin-top: 5px; margin-bottom: 5px;" class="autoAdd">
                        <tr>
                            <td align="center" width="250" height="25" class="alertboxTableT">
                                合同号段
                            </td>
                            <td align="center" width="120" class="alertboxTableT">
                                操作
                            </td>
                        </tr>
                        <tr class="tempRow">
                            <td height="28" align="center" >
                                <input name="txtConStart" type="text" style="width: 100px;" valid="isNumber|required"
                                    errmsg="*开始数必须是数字!|请输入开始数" />
                                至
                                <input name="txtConEnd" type="text" style="width: 100px;" valid="isNumber|required"
                                    errmsg="*结束数必须是数字!|请输入结束数!" />
                            </td>
                            <td align="center">
                                <a href="javascript:void(0)" class="addbtn">
                                    <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                        class="delbtn">
                                        <img src="/images/delimg.gif" width="48" height="20" /></a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn" style="text-align: center;">
            <asp:LinkButton ID="btnSubmit" runat="server" hidefocus="true" OnClick="btnClick"><s class="baochun"></s>保 存</asp:LinkButton>
            <a hidefocus="true" href="javascript:void(0);"><s class="chongzhi"></s>重 置</a>
        </div>
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
    $(function() {

        $("#btnSubmit").click(function() {
            var form = $(this).closest("form").get(0);
            //点击按纽触发执行的验证函数
            //return ValiDatorForm.validator(form, "parent");
            if (ValiDatorForm.validator(form, "parent")) {
                var errorMsg = CheckConNum();
                if (errorMsg != "") {
                    parent.tableToolbar._showMsg(errorMsg); return false;
                }
                else {
                    return true;
                }
            }
            else {
                return false;
            }
        });
        //初始化表单元素失去焦点时的行为，当需验证的表单元素失去焦点时，验证其有效性。
        FV_onBlur.initValid($("#btnSubmit").closest("form").get(0));
    });
    
    //检查合同号是否规范
    function CheckConNum() {
        var datas = [];
        var datae = [];
        var errorMsg = "";
        $("input:text[name='txtConStart']").each(function() {
            datas.push($(this).val());
        });
        $("input:text[name='txtConEnd']").each(function() {
            datae.push($(this).val());
        });
        if (datas.length > 0 && datae.length > 0 && datas.length == datae.length) {
            for (var i = 0; i < datas.length; i++) {
                if (parseInt(datas[i]) >= parseInt(datae[i])) {
                    errorMsg = errorMsg + "第" + (i + 1) + "行开始号大于或等于结束号! &nbsp;";
                }
                else {
                    if (i > 0 && parseInt(datae[i - 1]) >= parseInt(datas[i])) {
                        errorMsg = errorMsg + "第" + (i + 1) + "行号码段与第" + i + "行号码段有重合或小于! &nbsp;";
                    }
                }
            }
        }
        return errorMsg;
    }
</script>

