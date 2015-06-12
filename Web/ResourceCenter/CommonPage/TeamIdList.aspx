<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamIdList.aspx.cs" Inherits="Web.ResourceCenter.CommonPage.TeamIdList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>团号-选择-</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/js/jquery.boxy.js"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <div style="margin: 0 auto; width: 99%; padding-bottom: 5px;">
            <table width="99%" border="1" cellpadding="0" cellspacing="0" bordercolor="#b8c5ce"
                style="margin: 0 auto; border-collapse: collapse;">
                <tr style="background: url(../images/y-formykinfo.gif) repeat-x center top;">
                    <td width="6%" height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        选择
                    </td>
                    <td width="94%" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        团号
                    </td>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td height="29" align="center">
                                <input type="checkbox" />
                            </td>
                            <td align="left">
                                <%#Eval("TourCode")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <asp:Label ID="lbl_msg" runat="server" Text=""></asp:Label>
            <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            <div class="alertbox-btn">
                <a href="javascript:;" hidefocus="true" id="a_Save" onclick="Save()"><s class="xuanzhe">
                </s>选 择</a><a href="#" hidefocus="true"><s class="chongzhi"></s>重 置</a>
            </div>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        function Save() {
            var data = {
                callBack: Boxy.queryString("callBack"),
                iframeID: Boxy.queryString("iframeId"),
                pIframeID: Boxy.queryString("pIframeId"),
                showID: Boxy.queryString("showID")
            }
            var TeamIds = "";
            $(":checkbox:checked").each(function() {
                TeamIds += $.trim($(this).parent().next("td").text()) + ",";
            })
            //根据父级是否为弹窗传值
            if (data.pIframeID != null && data.pIframeID.length > 0) {
                //定义父级弹窗
                var boxyParent = window.parent.Boxy.getIframeDocument(data.pIframeID);
                $(boxyParent.getElementById(data.showID)).val(TeamIds.substring(0, TeamIds.length - 1));
                //定义回调
            }
            else {
                $(parent.document.getElementById(data.showID)).val(TeamIds.substring(0, TeamIds.length - 1))
                //定义回调
            }
            parent.Boxy.getIframeDialog(data.iframeID).hide();
            return false;
        }
    </script>

</body>
</html>
