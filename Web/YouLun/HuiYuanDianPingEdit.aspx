<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HuiYuanDianPingEdit.aspx.cs" Inherits="EyouSoft.Web.YouLun.HuiYuanDianPingEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="/css/boxynew.css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
</head>
<body style="background: none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT" style="border: solid 1x #9AD2ED;">
                        产品名称：
                    </td>
                    <td height="28" align="left" bgcolor="#e0e9ef" style="border: solid 1x #9AD2ED;">
                        <asp:Literal ID="litmingcheng" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT" style="border: solid 1x #9AD2ED;">
                        点评分数：
                    </td>
                    <td height="28" align="left" bgcolor="#e0e9ef" style="border: solid 1x #9AD2ED;">
                        <label><input name="txt_dp_fen" value="1" type="radio" id="rdo1" runat="server"/>1分</label><label><input name="txt_dp_fen" value="2" type="radio" id="rdo2" runat="server"/>2分</label><label><input name="txt_dp_fen" value="3" type="radio" id="rdo3" runat="server"/>3分</label><label><input name="txt_dp_fen" value="4" type="radio" id="rdo4" runat="server"/>4分</label><label><input name="txt_dp_fen" value="5" type="radio" id="rdo5" runat="server"/>5分</label>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT" style="border: solid 1x #9AD2ED;">
                        点评标题：
                    </td>
                    <td height="28" align="left" bgcolor="#e0e9ef" style="border: solid 1x #9AD2ED;">
                        <asp:TextBox ID="txtbiaoti" Height="65" CssClass="inputtext formsize450" runat="server"
                            TextMode="MultiLine" valid="required" errmsg="点评标题不可为空！"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT" style="border: solid 1x #9AD2ED;">
                        点评内容：
                    </td>
                    <td height="28" align="left" bgcolor="#e0e9ef" style="border: solid 1x #9AD2ED;">
                        <asp:TextBox ID="txtHuiDa" Height="65" CssClass="inputtext formsize450" runat="server"
                            TextMode="MultiLine" valid="required" errmsg="点评内容不可为空！"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT" style="border: solid 1x #9AD2ED;">
                        是否审核：
                    </td>
                    <td height="28" align="left" bgcolor="#e0e9ef" style="border: solid 1x #9AD2ED;">
                        <select id="isshenhe" runat="server">
                        <option value="0">否</option>
                        <option value="1">是</option>
                        </select>
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
            Params: { sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>', hangqiid: '<%=EyouSoft.Common.Utils.GetQueryStringValue("hangqiid") %>' },
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
            }, Save: function() {
                pageDataJs.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/youlun/huiyuandianpingedit.aspx?save=save&" + $.param(pageDataJs.Params),
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
            }

        };
        $(function() {
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
