<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZiYuanAdd.aspx.cs" Inherits="EyouSoft.Web.YouLun.ZiYuanAdd" ValidateRequest="false" %>

<%@ Register Src="~/YouLun/WUC/wucupload.ascx" TagName="wucupload" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="/css/boxynew.css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>

    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

</head>
<body style="background: none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        公司名称：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="txtMingCheng" CssClass=" inputtext formsize180" runat="server" errmsg="请输入公司名称!"
                            valid="required"></asp:TextBox><font class="fontbsize12">*</font>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        公司类型：
                    </th>
                    <td height="28" align="left">
                        <asp:DropDownList ID="ddlGSType" runat="server" CssClass="inputselect">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT" bgcolor="#b7e0f3">
                        公司logo：
                    </th>
                    <td height="28" align="left" bgcolor="#e0e9ef">
                        <uc1:wucupload runat="server" ID="upload1" IsUploadMore="false" IsUploadSelf="true"
                            FileTypes="*.jpg;*.gif;*.jpeg;*.png" YangShi="1" />
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT" bgcolor="#b7e0f3">
                        公司图片：
                    </th>
                    <td height="28" align="left" bgcolor="#e0e9ef">
                        <uc1:wucupload runat="server" ID="upload2" IsUploadMore="true" IsUploadSelf="true"
                            FileTypes="*.jpg;*.gif;*.jpeg;*.png" YangShi="1" />
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        简要介绍：
                    </th>
                    <td height="28" align="left">
                        <asp:TextBox runat="server" ID="txtJieShao" Height="65" CssClass="inputtext formsize450"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        详细介绍：
                    </th>
                    <td height="28" align="left">
                        <span id="spanPlanContent" style="display: inline-block;">
                            <asp:TextBox ID="txtXXJieShao" CssClass="inputtext formsize450" runat="server" TextMode="MultiLine"
                                Height="65"></asp:TextBox></span>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        公司优势：
                    </th>
                    <td height="28" align="left">
                        <span id="spanPlanContent" style="display: inline-block;">
                            <asp:TextBox ID="txtYouShi" CssClass="inputtext formsize450" runat="server" TextMode="MultiLine"
                                Height="65"></asp:TextBox></span>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        公司荣誉：
                    </th>
                    <td height="28" align="left">
                        <span id="spanPlanContent" style="display: inline-block;">
                            <asp:TextBox ID="txtRongYu" CssClass="inputtext formsize450" runat="server" TextMode="MultiLine"
                                Height="65"></asp:TextBox></span>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        排序值：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="txtPaiXuId" CssClass=" inputtext formsize180" runat="server"></asp:TextBox><span style="color:#666">（默认0 按值升序排列）</span>
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="text-align: center;" class="alertbox-btn">
            <a href="javascript:;" id="btnSave"><s class="baochun"></s>保 存</a> <a hidefocus="true"
                href="javascript:" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide(); return false;">
                <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var pageDataJs = {
            Params: { sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', type: '<%=EyouSoft.Common.Utils.GetQueryStringValue("type") %>', id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>' },
            UnBindBtn: function() {
                $("#btnSave").html("<s class='baochun'></s>提交中...").unbind("click").css("background-position", "0-57px");
            },
            //按钮绑定事件
            BindBtn: function() {
                $("#btnSave").bind("click").css("background-position", "0-28px").html("<s class='baochun'></s>保 存");
                $("#btnSave").click(function() {
                    var form = $(this).closest("form").get(0);
                    if (ValiDatorForm.validator("form", "parent")) {
                        pageDataJs.Save();
                    } else {
                        return false;
                    }
                });
            },
            Save: function() {
                KEditer.sync();
                pageDataJs.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/youlun/ZiYuanAdd.aspx?save=save&" + $.param(pageDataJs.Params),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() { parent.window.location.href = parent.window.location.href });
                        } else {
                            parent.tableToolbar._showMsg(ret.msg);
                            pageDataJs.BindBtn();
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg("操作失败，请稍后重试！");
                        pageDataJs.BindBtn();
                    }
                });
            },
            CreateEdit: function() {
                var _keoptions = { items: keSimpleYL, height: "120px", width: "800px" }
                //创建行程编辑器
                KEditer.init('<%=txtXXJieShao.ClientID %>', _keoptions);

                KEditer.init('<%=txtYouShi.ClientID %>', _keoptions);
                KEditer.init('<%=txtRongYu.ClientID %>', _keoptions);
            },
            //删除附件
            RemoveVisaFile: function(obj) {
                $(obj).parent().remove();
            }
        };
        $(function() {
            KEditer["UPLOAD_P1"] = "YL";
            pageDataJs.CreateEdit();
            $("#btnSave").click(function() {
                var form = $(this).closest("form").get(0);
                if (ValiDatorForm.validator(form, "parent")) {
                    pageDataJs.Save();
                } else {
                    return false;
                }
            });
        });
    </script>

</body>
</html>
