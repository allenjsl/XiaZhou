<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractCodeCollar.aspx.cs"
    Inherits="Web.ContractManage.ContractCodeCollar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/UserControl/HrSelect.ascx" TagName="HrSelect" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>��ͬ����-���ú�ͬ</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td width="18%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    ��ͬ�ţ�
                </td>
                <td width="82%" align="left" style="line-height: 20px;">
                    <%=EyouSoft.Common.Utils.GetQueryStringValue("codes")%>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="fontred">*</span><label>�� �� �ˣ�</label>
                </td>
                <td height="32" align="left">
                    <uc1:HrSelect runat="server" ID="HrSelect1" ReadOnly="true" />
                    &nbsp;&nbsp;
                    <asp:HiddenField ID="hideDeptID" runat="server" />
                    <asp:TextBox runat="server" ID="DeptName" class="formsize120" value="���������ڲ���"  ReadOnly="true" BackColor="#dadada"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="32" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    ����ʱ�䣺
                </td>
                <td height="32" align="left">
                    <asp:TextBox runat="server" ID="txtCollarTime" class="formsize80" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" hidefocus="true" id="btnSave"><s class="baochun"></s>��
                ��</a> <a href="javascript:void(0);" onclick="ContractCodeCollar.ResetForm()" hidefocus="true">
                    <s class="chongzhi"></s>�� ��</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var ContractCodeCollar = {
            Query: {/*URL��������*/
                sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                type: '<%=EyouSoft.Common.Utils.GetQueryStringValue("type") %>',
                ids: '<%=EyouSoft.Common.Utils.GetQueryStringValue("ids") %>',
                codes: '<%=EyouSoft.Common.Utils.GetQueryStringValue("codes") %>'
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
                    $("#btnSave").html("<s class=\"baochun\"></s> �ύ��...");
                    var url = "/ContractManage/ContractCodeCollar.aspx?";
                    url += $.param({
                        doType: 'save',
                        type: this.Query.type,
                        sl: this.Query.sl,
                        ids: this.Query.ids,
                        codes: this.Query.codes
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
                                    ContractCodeCollar.Save();
                                });
                                $("#btnSave").attr("class", "");
                                $("#btnSave").html("<s class=\"baochun\"></s>�� ��");
                                parent.tableToolbar._showMsg(result.msg);
                            }
                        },
                        error: function() {
                            //ajax�쳣--�㶮��
                            parent.tableToolbar._showMsg("������æ��");
                            ContractCodeCollar.BindBtn();
                        }
                    });
                }
            },
            BindBtn: function() {
                $("#btnSave").click(function() {
                    ContractCodeCollar.Save();
                    return false;
                })
                $("#btnSave").attr("class", "");
                $("#btnSave").html("<s class=\"baochun\"></s>�� ��");
            },
            CallBackFun: function(data) {
                $("#<%=HrSelect1.HrSelectIDClient%>").val(data.value);
                $("#<%=HrSelect1.HrSelectNameClient%>").val(data.text);
                $("#<%=hideDeptID.ClientID%>").val(data.deptId);
                $("#<%=DeptName.ClientID%>").val(data.deptName);
            }
        }
        $(function() {
            ContractCodeCollar.BindBtn();
        })
    </script>

</body>
</html>
