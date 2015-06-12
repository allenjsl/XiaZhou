<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JiChuXinXiAdd.aspx.cs"
    Inherits="EyouSoft.Web.YouLun.JiChuXinXiAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加基础信息</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="/css/boxynew.css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

</head>
<body style="background: none">
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="80" height="28" align="right" class="alertboxTableT">
                        名称：
                    </td>
                    <td height="28" align="left">
                        <asp:TextBox ID="txtText" CssClass=" inputtext formsize180" runat="server" valid="required"
                            errmsg="请填写名称"></asp:TextBox>
                    </td>
                </tr>
                <asp:PlaceHolder ID="phdBieMing" runat="server" Visible="false">
                <tr>
                    <td width="80" height="28" align="right" class="alertboxTableT">
                        别名：
                    </td>
                    <td height="28" align="left">
                        <asp:TextBox ID="txtBieMing" CssClass=" inputtext formsize180" runat="server" valid="required"
                            errmsg="请填写别名"></asp:TextBox>
                    </td>
                </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                    <tr>
                        <td height="28" align="right" class="alertboxTableT">
                            游轮类型：
                        </td>
                        <td height="28" align="left">
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="inputselect">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phdGangKouBieMing" runat="server" Visible="false">
                    <tr>
                        <td height="28" align="right" class="alertboxTableT">
                            港口别名：
                        </td>
                        <td height="28" align="left">
                            <select id="ddlGangKouBieMing" name="ddlGangKouBieMing">
                                <%=this.GetGangKouBieMingDDL(this.YouLunLeiXing) %>
                            </select>
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="PlaceHolder2" runat="server" Visible=false>
                <tr>
                    <td height="28" align="right" class="alertboxTableT">
                        排序值：
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtPaiXuId" class="inputtext" style="width: 80px;" /><span
                            style="color: #666">（默认0 按值升序排列）</span>
                    </td>
                </tr>
                </asp:PlaceHolder>
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
                pageDataJs.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/youlun/JiChuXinXiAdd.aspx?save=save&" + $.param(pageDataJs.Params),
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
            $("#<%=this.ddlType.UniqueID %>").change(function(){
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/youlun/JiChuXinXiAdd.aspx?doajax=GetGangKouBieMingDDL&youlunleixing="+$(this).val()+"&" + $.param(pageDataJs.Params),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "text",
                    success: function(ret) {
                        $("#ddlGangKouBieMing").html(ret);
                    },
                    error: function() {
                        $("#ddlGangKouBieMing").html("<option value=\"0\">请选择</option>");
                    }
                });
            })
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
