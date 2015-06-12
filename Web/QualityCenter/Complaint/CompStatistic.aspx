<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompStatistic.aspx.cs"
    Inherits="Web.QualityCenter.Complaint.CompStatistic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

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
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9">
            <tr>
                <td width="16%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    团号：
                </td>
                <td width="40%" height="28" align="left">
                    <asp:Label runat="server" ID="lbTourCode"></asp:Label>
                    <asp:HiddenField runat="server" ID="hidComplaintsId" />
                </td>
                <td width="16%" height="28" align="right" bgcolor="#B7E0F3">
                    投诉人：
                </td>
                <td width="28%" height="28" align="left">
                    <asp:Label runat="server" ID="lbComplaintsName"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    投诉时间：
                </td>
                <td height="28" align="left">
                    <asp:Label runat="server" ID="lbComplaintsTime"></asp:Label>
                </td>
                <td height="28" align="right" bgcolor="#B7E0F3">
                    投诉人身份：
                </td>
                <td height="28" align="left">
                    <asp:Label runat="server" ID="lbComplaintsIdentity"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    投诉人电话：
                </td>
                <td height="28" align="left">
                    <asp:Label runat="server" ID="lbComplaintsTel"></asp:Label>
                </td>
                <td height="28" align="right" bgcolor="#B7E0F3">
                    投诉类型：
                </td>
                <td height="28" align="left">
                    <asp:Label runat="server" ID="lbComplaintsType"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    投诉意见：
                </td>
                <td height="28" colspan="3" align="left">
                    <asp:Label runat="server" ID="lbComplaintsOpinion"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>处理人：
                </td>
                <td height="28" align="left">
                    <asp:Label runat="server" data-class="txtlable" ID="lbHandleName"></asp:Label>
                    <asp:TextBox runat="server" ID="txtHandleName" class="inputtext formsize100" valid="required"
                        errmsg="请输入投诉处理人!"></asp:TextBox>
                </td>
                <td height="28" align="right" bgcolor="#B7E0F3">
                    <span class="errmsg">*</span>处理时间：
                </td>
                <td height="28" align="left">
                    <asp:Label runat="server" data-class="txtlable" ID="lbHandleTime"></asp:Label>
                    <asp:TextBox runat="server" ID="txtHandleTime" onfocus="WdatePicker()" class="inputtext formsize100"
                        valid="required" errmsg="请输入投诉处理时间!"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>处理结果：
                </td>
                <td height="28" colspan="3" align="left">
                    <asp:Label runat="server" data-class="txtlable" ID="lbHandleResult"></asp:Label>
                    <asp:TextBox runat="server" ID="txtHandleResult" TextMode="MultiLine" class="inputtext formsize450"
                        valid="required" errmsg="请输入投诉处理结果!" Height="60"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    是否处理：
                </td>
                <td height="28" colspan="3" align="left">
                    <asp:Label runat="server" data-class="txtlable" ID="lbIsHandle"></asp:Label>
                    <asp:RadioButton runat="server" ID="handle" Text="是" GroupName="rbIsHandle" Checked="true" />
                    <asp:RadioButton runat="server" ID="nohandle" Text="否" GroupName="rbIsHandle" />
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <asp:PlaceHolder runat="server" ID="ph_save"><a href="javascript:void(0);" hidefocus="true"
                id="btnSave"><s class="baochun"></s>保 存</a></asp:PlaceHolder>
            <a href="javascript:void(0);" onclick="PageJsData.ResetForm()" hidefocus="true"><s
                class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var PageJsData = {
            Query: {/*URL参数对象*/
                sl: '<%=Request.QueryString["sl"] %>',
                isDeal: '<%=Request.QueryString["isDeal"] %>'
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
                var that = this;
                if (that.FormCheck()) {
                    $("#btnSave").unbind("click").addClass("alertbox-btn_a_active").html("<s class=\"baochun\"></s> 提交中...");
                    var url = "/QualityCenter/Complaint/CompStatistic.aspx?";
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
                $("#btnSave").click(function() {
                    PageJsData.Save();
                    return false;
                })
                $("#btnSave").attr("class", "").html("<s class=\"baochun\"></s>保 存");
            }
        }
        $(function() {
            PageJsData.BindBtn();
            if (PageJsData.Query.isDeal == 'True') {
                $(":input").hide();
                $("textarea").hide();
                $("label").hide();
                $(".alertbox-btn").hide();
            }
            if (PageJsData.Query.isDeal == 'False') {
                $("span[data-class='txtlable']").hide();
            }
        });
    </script>

</body>
</html>
