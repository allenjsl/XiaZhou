<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KingDeeItem.aspx.cs" Inherits="EyouSoft.Web.CommonPage.KingDeeItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>无标题文档</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%;">
            <table width="100%" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                style="margin: 0 auto" id="liststyle">
                <tr>
                    <td width="30" height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                        选择
                    </td>
                    <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                        <span class="th-line">科目类型</span>
                    </td>
                    <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                        <span class="th-line">科目代码</span>
                    </td>
                    <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                        <span class="th-line">助记码</span>
                    </td>
                    <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                        <span class="th-line">核算项目名称</span>
                    </td>
                    <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                        <span class="th-line">科目类别</span>
                    </td>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td height="28" align="center">
                                <input name="1" type="checkbox" value="" style="border: none" data-chkid="<%#Eval("ChkId") %>"
                                    data-chknm="<%#Eval("ChkNm") %>" />
                            </td>
                            <td align="left">
                                <%#Eval("PreChkNm")%>
                            </td>
                            <td align="center">
                                <%#Eval("ChkCd")%>
                            </td>
                            <td align="center">
                                <%#Eval("MnemonicCd")%>
                            </td>
                            <td align="center">
                                <%#Eval("ChkNm")%>
                            </td>
                            <td align="center">
                                <%#Eval("ChkCate")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_msg" runat="server">
                    <tr>
                        <td colspan="6" align="center">
                            暂无数据！
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" hidefocus="true" id="a_btn"><s class="xuanzhe"></s>选 择</a><a
                href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;" hidefocus="true"><s class="chongzhi"></s>关 闭</a>
        </div>
    </div>

    <script type="text/javascript">
        var PayStyle = {
            _parentWindow: null,
            PageInit: function() {
                var that = this;
                that._parentWindow = Boxy.queryString("pIframeID") ? window.parent.Boxy.getIframeWindow(Boxy.queryString("pIframeID")) : parent;
                $("#a_btn").click(function() {
                    var data = {
                        ids: [],
                        names: []
                    };

                    $(":checkbox:checked").each(function() {
                        var obj = $(this);
                        data.ids.push(obj.attr("data-chkid"));
                        data.names.push(obj.attr("data-chknm"));
                    })
                    data.ids = data.ids.join(",");
                    data.names = data.names.join(",");
                    var parents = that._parentWindow;
                    if (parents["SubjectManageEdit"] && parents["SubjectManageEdit"]["ChkItemCallBack"]) {

                        parents["SubjectManageEdit"]["ChkItemCallBack"](data);
                    }
                    parent.Boxy.getIframeDialog(Boxy.queryString("iframeId")).hide();
                })
            },
            SetValue: function() {
                var ids = '<%=Request.QueryString["ids"] %>'.split(',');
                if (ids.length > 0) {
                    for (var i = 0; i < ids.length; i++) {
                        $("#liststyle").find("input[data-chkid='" + ids[i] + "']").attr("checked", "checked");
                    }
                }
            }
        }
        $(function() {
            PayStyle.PageInit();
            PayStyle.SetValue();
        })
    </script>

</body>
</html>
