<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XiLieAdd.aspx.cs" Inherits="EyouSoft.Web.YouLun.XiLieAdd" ValidateRequest="false" %>

<%@ Register Src="~/YouLun/WUC/wucupload.ascx" TagName="wucupload" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/YLCompanySelect.ascx" TagName="YLCompanySelect"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
<body  style="background: none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        游轮公司:
                    </td>
                    <td width="35%" height="28" align="left">
                        <uc2:YLCompanySelect ID="YLCompanySelect1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        系列名称：
                    </td>
                    <td height="28" align="left">
                        <asp:TextBox ID="txtXiLie" CssClass=" inputtext formsize180" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <%--      <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        其他图片：
                    </td>
                    <td height="28" align="left" bgcolor="#e0e9ef">
                        <uc1:UploadControl runat="server" ID="upFilesQiTa" IsUploadMore="true" IsUploadSelf="true" />
                        <asp:Label runat="server" ID="Label1" class="labelFiles"></asp:Label>
                        <br />
                    </td>
                </tr>--%>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        简要介绍：
                    </td>
                    <td height="28" align="left" bgcolor="#e0e9ef">
                        <asp:TextBox runat="server" ID="txtJieShao" Height="65" CssClass="inputtext formsize450"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        详细介绍：
                    </td>
                    <td height="28" align="left" bgcolor="#e0e9ef">
                        <span id="spanPlanContent" style="display: inline-block;">
                            <asp:TextBox ID="txtXXJieShao" CssClass="inputtext" runat="server" TextMode="MultiLine"
                                Width="800" Height="55"></asp:TextBox></span>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT" bgcolor="#b7e0f3">
                        系列图片：
                    </td>
                    <td height="28" align="left" bgcolor="#e0e9ef">
                        <uc1:wucupload runat="server" ID="upload1" IsUploadMore="true" IsUploadSelf="true"
                            FileTypes="*.jpg;*.gif;*.jpeg;*.png" YangShi="1" />
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        排序值：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="txtPaiXuId" CssClass=" inputtext formsize180" runat="server"></asp:TextBox><span
                            style="color: #666">（默认0 按值升序排列）</span>
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
            Params: { sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>' },
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
                pageDataJs.UnBindBtn();
                KEditer.sync();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/youlun/XiLieAdd.aspx?save=save&" + $.param(pageDataJs.Params),
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
