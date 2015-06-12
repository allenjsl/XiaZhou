<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileApprove.aspx.cs" Inherits="Web.ManageCenter.File.FileApprove" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/js/table-toolbar.js"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    文件字号：
                </td>
                <td height="28" colspan="3" align="left">
                    <asp:Label runat="server" ID="lbFontSize"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="18%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    文件发布单位：
                </td>
                <td width="35%" height="28" align="left">
                    <asp:Label runat="server" ID="lbCompany"></asp:Label>
                </td>
                <td width="15%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    文件标题：
                </td>
                <td width="32%" align="left">
                    <asp:Label runat="server" ID="lbTitle"></asp:Label>
                </td>
            </tr>
            <asp:PlaceHolder runat="server" ID="ph_Flies">
                <tr>
                    <td width="18%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        附件下载：
                    </td>
                    <td colspan="3" height="28" align="left">
                        <asp:Label runat="server" ID="lbFiles"></asp:Label>
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
        <div class="hr_10">
        </div>
        <div class="tablelist-box">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <strong>审批/审阅人</strong>
                    </td>
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批/审阅时间
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批/审阅状态
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        审批/审阅意见
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        操作
                    </td>
                </tr>
                <asp:Repeater runat="server" ID="rpt_approve">
                    <ItemTemplate>
                        <tr class="tr_Approve" data-class='<%#Eval("ApproveState") %>' data-id='<%#Eval("ApproveID") %>'>
                            <td height="28" align="center" bgcolor="#FFFFFF">
                                <strong>
                                    <label data-class='name'>
                                        <%#Eval("ApproveName")%></label>
                                </strong>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <input type="text" name="txtTime" onfocus="WdatePicker()" />
                                <label data-class="time">
                                    <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("ApproveTime"), ProviderToDate)%></label>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <input type="checkbox" name="checkbox" checked="checked" />
                                <img src='../../Images/y-duihao.gif' width='13' height='9' />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <input type="text" name="txtView" />
                                <label data-class="view">
                                    <%#Eval("ApprovalViews")%></label>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a href="javascript:void(0)" class="a_Approve">审批</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:window.print();" hidefocus="true">打 印</a><a href="javascript:void(0)"
                onclick="PageJsData.Closewin()" hidefocus="true"><s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        var PageJsData = {
            Query: {/*URL参数对象*/
                sl: '<%=Request.QueryString["sl"] %>',
                userId: '<%=this.SiteUserInfo.UserId %>',
                id: '<%=Request.QueryString["id"] %>',
                iframeId: '<%=Request.QueryString["iframeId"] %>'
            },
            Closewin: function() {
                parent.Boxy.getIframeDialog(PageJsData.Query.iframeId).hide();
                parent.window.document.getElementById("btnSubmit").click();
            },
            Approve: function(obj) {
                var self = $(obj).closest("tr");
                var url = "/ManageCenter/File/FileApprove.aspx?";
                url += $.param({
                    save: "approve",
                    sl: this.Query.sl,
                    apptime: self.find("td input[name='txtTime']").val(),
                    appname: self.find("td label[data-class='name']").html(),
                    appview: self.find("td input[name='txtView']").val(),
                    appid: $.trim(self.attr("data-id")),
                    id: this.Query.id
                });
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function(result) {
                        if (result.result == "1") {
                            parent.tableToolbar._showMsg(result.msg, function() {
                                window.location.reload();
                            });
                        }
                        else { parent.tableToolbar._showMsg(result.msg, function() { PageJsData.BindBtn(); }); }
                        return false;
                    },
                    error: function() {
                        //ajax异常--你懂得
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg, function() { PageJsData.BindBtn(); });
                    }
                });
            },
            BindBtn: function() {
                $(".a_Approve").unbind("click");
                $(".a_Approve").click(function() {
                    PageJsData.Approve(this);
                    return false;
                })
            }
        }

        $(function() {
            PageJsData.BindBtn();
            //审批权限控制
            $(".tr_Approve").each(function() {
                var self = $(this);
                if (self.attr("data-id") != PageJsData.Query.userId) {
                    self.find("td a[class='a_Approve']").remove();
                    self.find("td").last().html("无权限");
                }
            })
            var obj = $("tr[data-class='待审核']");
            if (obj.length > 0) {
                //alert(obj.length);
                obj.find("td label[data-class='name']").css("color", "Red");
                obj.find("td label[data-class='time']").remove();
                obj.find("td label[data-class='view']").remove();
                obj.find("td img").hide();
            }
            var obj2 = $("tr[data-class='审核通过']");
            if (obj2.length > 0) {
                obj2.find("td input").hide();
                obj2.find("td a[class='a_Approve']").remove();
                obj2.find("td").last().html("已审核");
            }
        })
    </script>

</body>
</html>
