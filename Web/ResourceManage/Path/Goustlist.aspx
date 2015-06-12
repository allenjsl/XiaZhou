<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Goustlist.aspx.cs" Inherits="Web.ResourceManage.Path.Goustlist" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源管理-线路库-收客数</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/JS/jquery-1.4.4.js"></script>

    <!--[if IE]><script src="/js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->

    <script type="text/javascript" src="/js/bt.min.js"></script>

    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/js/table-toolbar.js"></script>

    <script type="text/javascript">
$(function(){
	$('#alertbox').find("a[Data-Contact='ContactInfo']").bt({
                contentSelector: function() {
                   return $(this).prev("span").html();
                },
                positions: ['left', 'right','bottom'],
                fill: '#FFF2B5',
                strokeStyle:'#D59228',
				noShadowOpts:{strokeStyle:"#D59228" },
                spikeLength: 10,
                spikeGirth: 10,
                width: 120,
                overlap: 0,
                centerPointY: 1,
                cornerRadius: 4,
                shadow: true,
                shadowColor: 'rgba(0,0,0,.5)',
				cssStyles:{color:'#00387E','line-height':'140%'}
            });
			$('#alertbox').find("a[Data-UnitInfo='UnitInfo']").bt({
                contentSelector: function() {
                    return $(this).prev("span").html();
                },
                positions: ['left', 'right','bottom'],
                fill: '#FFF2B5',
                strokeStyle:'#D59228',
				noShadowOpts:{strokeStyle:"#D59228" },
                spikeLength: 10,
                spikeGirth: 15,
                width: 200,
                overlap: 0,
                centerPointY: 1,
                cornerRadius: 4,
                shadow: true,
                shadowColor: 'rgba(0,0,0,.5)',
				cssStyles:{color:'#00387E','line-height':'180%'}
            });
});
    </script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox" id="alertbox">
        <table width="98%" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF" align="center"
            id="liststyle" style="margin: 0 auto">
            <tbody>
                <tr class="odd">
                    <td height="23" bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        序号
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        报名时间
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        订单号
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        销售员
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        客源单位
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        人数
                    </td>
                </tr>
                <asp:Repeater ID="RptList" runat="server">
                    <ItemTemplate>
                        <tr class="">
                            <td align="center">
                                <%#Container.ItemIndex + 1 %>
                            </td>
                            <td height="28" align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("ApplyDate"),ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#Eval("OrderCode") %>
                            </td>
                            <td align="center">
                                <span style="display: none">
                                    <%#GetContactInfo(Eval("SellerModel"))%></span> <a data-contact="ContactInfo" href="javascript:void(0);"
                                        title="">
                                        <%# ((EyouSoft.Model.ComStructure.MComUser)Eval("SellerModel")).ContactName%>
                                    </a>
                            </td>
                            <td align="center">
                                <span style="display: none">
                                    <%#GetUnitInfo(Eval("BuyCompanyName"), Eval("ContactName"), Eval("ContactTel"))%></span> <a data-unitinfo="UnitInfo" href="javascript:void(0);"
                                        title="">
                                        <%#Eval("BuyCompanyName")%></a>
                            </td>
                            <td height="28" align="center">
                                <%#((EyouSoft.Model.SourceStructure.MPeopleCountModel)Eval("PeopleCount")).AdultCount%>+<%#((EyouSoft.Model.SourceStructure.MPeopleCountModel)Eval("PeopleCount")).ChildrenCount%>+<%#((EyouSoft.Model.SourceStructure.MPeopleCountModel)Eval("PeopleCount")).OtherCount%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                <tr class="odd">
                    <td height="23" bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        序号
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        报名时间
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        订单号
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        销售员
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        客源单位
                    </td>
                    <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                        人数
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="position: relative; height: 32px;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:void(0);" onclick="GoustList.close()">关 闭</a>
        </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
   var GoustList={
        close:function(){
            parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();
        }
    }
</script>
