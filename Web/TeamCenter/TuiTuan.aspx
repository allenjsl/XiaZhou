<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TuiTuan.aspx.cs" Inherits="EyouSoft.Web.TeamCenter.TuiTuan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="../Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <div class="hr_10">
        </div>
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="16%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        <font class="fontbsize12">*</font>������
                    </td>
                    <td width="21%" bgcolor="#E9F4F9" align="left">
                        <asp:TextBox ID="txt_reBackName" runat="server" class=" inputtext formsize120" name="textfield42"
                            valid="required" errmsg="������������"></asp:TextBox>
                    </td>
                    <td width="15%" height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                        <font class="fontbsize12">*</font>�Ա�
                    </td>
                    <td width="48%" height="28">
                        <asp:RadioButton ID="rad_Man" runat="server" Style="border: none;" name="" GroupName="rad_Sex"
                            value="0" Text="��" />
                        <asp:RadioButton ID="rad_Women" runat="server" Style="border: none;" GroupName="rad_Sex"
                            value="1" Text="Ů" />
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        <font class="fontbsize12">*</font>�������ڣ�
                    </td>
                    <td width="21%" bgcolor="#E9F4F9" align="left">
                        <asp:TextBox ID="txt_reBackDate" runat="server" class=" inputtext formsize120" name="textfield42"
                            onclick="WdatePicker()" valid="required" errmsg="���������ڣ�"></asp:TextBox>
                    </td>
                    <td height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                        <font class="fontbsize12">*</font>�˿��
                    </td>
                    <td height="28">
                        <asp:TextBox ID="txt_reBackMoney" runat="server" class="inputtext formsize120" valid="required|isMoney"
                            errmsg="�������|����ʽ����ȷ!"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        <font class="fontbsize12">*</font>���˵����
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left" colspan="3">
                        <asp:TextBox ID="txt_reMarkMoney" Height="50px" runat="server" TextMode="MultiLine"
                            class="inputtext formsize350" valid="required" errmsg="���˵������Ϊ�գ�"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        <font class="fontbsize12">*</font>����ԭ��
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left" colspan="3">
                        <asp:TextBox ID="txt_ReBackResion" Height="50px" runat="server" TextMode="MultiLine"
                            class="inputtext formsize350" valid="required" errmsg="����ԭ����Ϊ�գ�"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn">
            <a id="reBack" hidefocus="true" href="javascript:void(0)">�� ��</a> <a hidefocus="true"
                href="#" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;">
                <s class="chongzhi"></s>�� ��</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var reBack = {
            Data: {
                sl: '<%=Request.QueryString["sl"] %>',
                name: '$(#<%=txt_reBackName.ClientID%>).val()',
                sex: '$("input:radio[name="rad_Sex"]:checked").val()',
                id: '<%=Request.QueryString["id"] %>',
                aid: '<%=Request.QueryString["aid"] %>',
                tourId: '<%=Request.QueryString["tourId"] %>',
                orderId: '<%=Request.QueryString["orderId"] %>'
            },
            FormCheck: function() {/*�ύ������֤*/

                FV_onBlur.initValid($("#reBack").closest("form").get(0));

                return ValiDatorForm.validator($("#reBack").closest("form").get(0), "parent");
            },
            Submit: function() {
                var that = this;
                if (this.FormCheck()) {
                    $("#reBack").unbind("click");

                    $("#reBack").html('<s class="tuituan"></s>�����ύ...');

                    $.newAjax({

                        type: "post",
                        url: "/TeamCenter/TuiTuan.aspx?dotype=tuituan&" + $.param(reBack.Data),
                        data: $("#reBack").closest("form").serialize(),
                        dataType: "json",
                        success: function(ret) {
                            if (ret.result == "1") {
                                $("#reBack").html('<s class="tuituan"></s>�� ��');
                                parent.tableToolbar._showMsg(ret.msg, function() {
                                    //                                    var callbackfun = '<%=EyouSoft.Common.Utils.GetQueryStringValue("callbackfun") %>'.split(".");
                                    //                                    if (parent.window[callbackfun[0]] && parent.window[callbackfun[0]][callbackfun[1]]) {
                                    //                                        parent.window[callbackfun[0]][callbackfun[1]](reBack.Data);
                                    //                                    }

                                    $("#btnClose").click(); parent.window.location.reload();
                                })
                            }
                            else {
                                parent.tableToolbar._showMsg(ret.msg); $("#reBack").html('<s class="tuituan"></s>�� ��'); $("#reBack").click(function() {
                                    reBack.Submit();
                                })
                            }

                        },
                        error: function() {
                            parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                            $("#reBack").html('<s class="tuituan"></s>�� ��');
                            $("#reBack").click(function() { reBack.Submit(); })
                        }
                    });
                }
            }
        }
        $(function() {
            $("#reBack").click(function() {
                reBack.Submit();
                return false;
            })
        })

    </script>

</body>
</html>
