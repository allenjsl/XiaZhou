<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Updata.aspx.cs" Inherits="Web.ResourceCenter.CommonPage.Updata" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源预控-车辆-预控变更-</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/js/jquery.boxy.js"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <asp:PlaceHolder ID="phdFr" runat="server"><span class="formtableT formtableT02">变更历史</span>
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" style="height: auto;
                zoom: 1; overflow: hidden;">
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        变更类型
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        数量
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        变更人
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        变更时间
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        变更备注
                    </td>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td height="28" align="center">
                                <%#((int)Eval("Plus")) > 0 ? "增加" : "减少"%>
                            </td>
                            <td align="center">
                                <%#((int)Eval("Plus")) > 0 ? Eval("Plus") : Eval("Cut")%>
                            </td>
                            <td align="center">
                                <%#Eval("Operator")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("IssueTime"),ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#Eval("Remark")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div class="hr_10">
            </div>
        </asp:PlaceHolder>
        <table width="99%" border="0" cellspacing="0" cellpadding="0" style="margin: 0 auto;">
            <tr>
                <td width="16%" height="28" align="right" bgcolor="#b7e0f3">
                    <span id="sp_nameTitle"></span>名称：
                </td>
                <td colspan="3" align="left">
                    <asp:Label ID="lbl_sourceName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3">
                    <span id="sp_typeTitle"></span>：
                </td>
                <td width="35%" align="left">
                    <asp:Label ID="lbl_typeName" runat="server"></asp:Label>
                </td>
                <td width="16%" align="right" bgcolor="#B7E0F3">
                    原控<span id="sp_numTitle"></span>数：
                </td>
                <td width="33%" align="left">
                    <strong>
                        <asp:Label ID="lbl_controlNum" runat="server"></asp:Label></strong>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3">
                    已使用数：
                </td>
                <td align="left">
                    <strong>
                        <asp:Label ID="lbl_alreadyNum" runat="server"></asp:Label></strong>
                </td>
                <td align="right" bgcolor="#B7E0F3">
                    剩余数量：
                </td>
                <td align="left">
                    <strong>
                        <asp:Label ID="lbl_surplus" runat="server" Text="0"></asp:Label></strong>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3">
                    类型：
                </td>
                <td align="left">
                    <select id="sel_PlusOrCut" class="inputselect">
                        <option value="0">请选择</option>
                        <option selected="selected" value="1">增加</option>
                        <option value="-1">减少</option>
                    </select>
                </td>
                <td align="right" bgcolor="#B7E0F3">
                    数量：
                </td>
                <td align="left">
                    <asp:TextBox ID="txt_num" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3">
                    备注：
                </td>
                <td colspan="3" align="left">
                    <asp:TextBox ID="txt_remark" runat="server" TextMode="MultiLine" Height="70px" CssClass="inputtext formsize450"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" id="a_Save"><s class="baochun"></s>保 存</a> <a href="javascript:void(0);"
                onclick="window.parent.Boxy.getIframeDialog(Boxy.queryString('IframeId')).hide();return false;">
                <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var UpdataPage = {
            Save: function(obj) {
                var that = this;
                var url = '/ResourceCenter/CommonPage/Updata.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>';
                var obj = $(obj);
                obj.unbind("click")
                obj.css({ "background-position": "0 -57px", "text-decoration": "none" })
                obj.html("<s class=baochun></s>&nbsp;&nbsp;&nbsp;保存中...");
                var data = {
                    doType: "Save",
                    remark: $.trim($("#<%=txt_remark.ClientID %>").html()),
                    plusOrCut: parseInt($("#sel_PlusOrCut").val()) || 0,
                    sourceId: '<%= EyouSoft.Common.Utils.GetQueryStringValue("sourceId") %>',
                    sourceControlCategory: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sourceControlCategory") %>',
                    num: $.trim($("#<%=txt_num.ClientID %>").val())
                }
                var msg = "";
                if (data.plusOrCut == 0) {
                    msg += "请选择增减类型!<br/>";
                }
                else if (data.plusOrCut > 0 && data.num <= 0) {
                    msg += "增加数量不能为空!<br/>";
                }
                else if (data.plusOrCut < 0 && data.num <= 0) {
                    msg += "减少数量不能为空!<br/>";
                }
                else if (data.plusOrCut < 0 && data.num > 0) {
                    var surplus = parseInt($("#<%=lbl_surplus.ClientID %>").text()) || 0;
                    if (data.num > surplus) {
                        msg += "减少数量必须小于等于剩余数量!<br/>";
                    }
                }
                if (msg.length > 0) {
                    parent.tableToolbar._showMsg(msg);
                    that.BindBtn();
                }
                else {
                    $.newAjax({
                        type: "post",
                        data: "&" + $.param(data),
                        cache: false,
                        url: url,
                        dataType: "json",
                        success: function(ret) {
                            if (parseInt(ret.result) === 1) {
                                parent.tableToolbar._showMsg('变更成功!', function() {
                                    window.parent.Boxy.getIframeDialog(Boxy.queryString("IframeId")).hide();
                                    parent.location.href = parent.location.href;
                                });
                            }
                            else {
                                parent.tableToolbar._showMsg(ret.msg);
                                that.BindBtn();
                            }
                        },
                        error: function() {
                            parent.tableToolbar._showMsg(parent.tableToolbar.errorMsg);
                            that.BindBtn();
                        }
                    });
                }
                return false;

            },
            BindBtn: function() {
                var that = this;
                var a_Save = $("#a_Save");
                a_Save.unbind("click");
                a_Save.css({ "background-position": "", "text-decoration": "" })
                a_Save.html("<s class=baochun></s>保 存")
                a_Save.click(function() {
                    that.Save(this);
                    return false;
                })
            },
            PageInit: function() {
                var query = {
                    sl: Boxy.queryString("sl"),
                    sourceControlCategory: parseInt(Boxy.queryString("sourceControlCategory")) - 1 || 0
                }
                if (query.sourceControlCategory >= 0) {
                    var arrTitle = [["酒店", "房型", "房"], ["车队", "车型", "车"], ["游船公司", "游船名称", "船"], ["景点公司", "景点名称", "景点"], ["其他公司", "其他支出", "支出"]]
                    $("#sp_nameTitle").html(arrTitle[query.sourceControlCategory]["0"]);
                    $("#sp_typeTitle").html(arrTitle[query.sourceControlCategory]["1"]);
                    $("#sp_numTitle").html(arrTitle[query.sourceControlCategory]["2"]);
                }
                else {
                    throw "预控变更缺少参数sourceControlCategory,该参数用于确定变更类型,酒店 = 1,车辆 = 2,游轮 = 3,景点 = 4,其他 = 5";
                }
                this.BindBtn();
            }
        }
        $(function() {
            UpdataPage.PageInit();
        })
    </script>

</body>
</html>
