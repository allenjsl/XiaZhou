<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ObjSendList.aspx.cs" Inherits="Web.ManageCenter.ObjectManage.ObjSendList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .errmsg
        {
            color: #f00;
            font-size: 12px;
        }
    </style>    

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <table width="98%" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
            style="margin: 0 auto" id="liststyle">
            <tr style="background: url(../images/y-formykinfo.gif) repeat-x center top;">
                <td width="5%" height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    序号
                </td>
                <td width="10%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    发放时间
                </td>
                <td width="9%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    签收部门
                </td>
                <td width="9%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    签收人
                </td>
                <td width="8%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    数量
                </td>
                <td width="9%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    单价
                </td>
                <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    总价
                </td>
                <td width="20%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    用途说明
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    操作
                </td>
            </tr>
            <asp:Repeater runat="server" ID="rpt_List">
                <ItemTemplate>
                    <tr i_lingyongid="<%#Eval("UseId") %>">
                        <td align="center">
                            <%#Container.ItemIndex+1 %>
                        </td>
                        <td align="center">
                            <%#Eval("Time","{0:yyyy-MM-dd}") %>
                        </td>
                        <td align="center">
                            <%#Eval("Dept")%>
                        </td>
                        <td align="center">
                            <%#Eval("UserName") %>
                        </td>
                        <td align="center">
                            <%#Eval("Number")%>
                        </td>
                        <td align="center">
                            <strong>
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(EyouSoft.Common.Utils.GetDecimal(Eval("Price").ToString()), ProviderToMoney)%></strong>
                        </td>
                        <td height="28" align="center">
                            <b class="fontred">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(EyouSoft.Common.Utils.GetDecimal(Eval("SumPrice").ToString()), ProviderToMoney)%></b>
                        </td>
                        <td align="center">
                            <%#Eval("Use")%>
                        </td>
                        <td align="center">
                            <a href="javascript:void(0)" class="i_delete">删除</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>            
        </table>
        <div class="pages">
            <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
        </div>
    </div>    

    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location;
            },
            del: function(obj) {
                if (!confirm("发放信息删除后不可恢复，你确定要删除吗？")) return false;

                var _$tr = $(obj).closest("tr");

                $.ajax({
                    type: "post", cache: false, url: "ObjGetList.aspx?sl=<%=SL %>&dotype=delete&lingyongid=" + _$tr.attr("i_lingyongid"), dataType: "json",
                    success: function(response) {
                        alert(response.msg);
                        iPage.reload();
                    }
                });
            }
        };

        $(document).ready(function() {
            $(".i_delete").click(function() { iPage.del(this); });
        });
    </script>
</body>
</html>
