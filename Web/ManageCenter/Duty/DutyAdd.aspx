<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DutyAdd.aspx.cs" Inherits="Web.ManageCenter.Duty.DutyAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <style type="text/css">
        .errmsg
        {
            font-size: 12px;
            color: Red;
        }
    </style>
</head>
<body style="background: 0 none;">
    <form id="form1" method="post" runat="server">
    <div class="alertbox-outbox02">
        <table width="100%" height="64" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td width="15%" height="32" align="right">
                    <span class="errmsg">*</span>职位名称：
                </td>
                <td width="85%" height="32" align="left">
                    <asp:TextBox runat="server" ID="txtDutyName" CssClass="inputtext formsize200" valid="required|custom"
                        MaxLength="12" custom="PageJsData.CheckDutyName" errmsg="职位名称不能为空！|对不起，该职位名已存在！"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hidDutyId" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <span class="errmsg">*</span>职位说明书：
                </td>
                <td height="60" align="left">
                    <asp:TextBox runat="server" ID="txtContent" CssClass="inputtext formsize600" Style="height: 180px;"
                        valid="required" errmsg="职位说明不能为空！" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <asp:PlaceHolder runat="server" ID="ph_Save"><a href="javascript:void(0);" hidefocus="true"
                id="btnSave"><s class="baochun"></s>保 存</a> </asp:PlaceHolder>
            <a href="javascript:void(0);" onclick="PageJsData.CloseForm()" hidefocus="true"><s
                class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script type="text/javascript">
        var PageJsData = {
            Query: {/*URL参数对象*/
                sl: '<%=Request.QueryString["sl"] %>'
            },
            CloseForm: function() {
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
            },
            Form: null,
            FormCheck: function() {
                this.Form = $("#btnSave").closest("form").get(0)
                FV_onBlur.initValid(this.Form);
                return ValiDatorForm.validator(this.Form, "parent");
            }, //检查职务名是否存在
            CheckDutyName: function(obj) {
                var s = "";
                var url = "DutyAdd.aspx?";
                url += $.param({
                    sl: this.Query.sl,
                    doType: "checkDutyName",
                    name: $(obj).val(),
                    id: '<%=Request.QueryString["id"] %>'
                })
                $.newAjax({
                    type: "get",
                    url: url,
                    success: function(data) {
                        s = data;
                    },
                    async: false
                })
                return s != "1" ? true : false;
            },
            Save: function() {
                var that = this;
                if (that.FormCheck()) {
                    $("#btnSave").unbind("click").addClass("alertbox-btn_a_active").html("<s class=\"baochun\"></s> 提交中...");
                    var url = "/ManageCenter/Duty/DutyAdd.aspx?";
                    url += $.param({
                        doType: '<%=Request.QueryString["doType"] %>',
                        save: "save",
                        sl: this.Query.sl,
                        id: $("#<%=hidDutyId.ClientID %>").val()
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
            }
        }
        $(function() {
            PageJsData.BindBtn();
        })
    </script>

</body>
</html>
