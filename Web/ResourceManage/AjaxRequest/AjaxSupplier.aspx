<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxSupplier.aspx.cs" Inherits="Web.ResourceManage.AjaxRequest.AjaxSupplier" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!--paopao start-->

<script type="text/javascript">
    $(function() {
        $('#tblList').find("a[data-contact='contact']").bt({
            contentSelector: function() {
                return $(this).prev("span").html();
            },
            positions: ['right', 'left'],
            fill: '#FFF2B5',
            strokeStyle: '#D59228',
            noShadowOpts: { strokeStyle: "#D59228" },
            spikeLength: 5,
            spikeGirth: 15,
            width: 375,
            overlap: 0,
            centerPointY: 4,
            cornerRadius: 4,
            shadow: true,
            shadowColor: 'rgba(0,0,0,.5)',
            cssStyles: { color: '#00387E', 'line-height': '200%' }
        });
    });
</script>

<!--paopao end-->
<table width="100%" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF" align="center"
    id="tblList" style="margin: 0 auto">
    <tr>
        <asp:repeater runat="server" id="RepList">  
         <ItemTemplate>
            <td align="left" height="30px" width="25%">
        <input name="1" type="radio" value="<%#Eval("GysId") %>" data-show="<%#Eval("GysName")%>" data-contactname="<%#GetContactInfo(Eval("Lxrs"),"name")%>" data-fax="<%#GetContactInfo(Eval("Lxrs"),"fax")%>" data-tel="<%#GetContactInfo(Eval("Lxrs"),"tel")%>" data-jiudianqiantaitelephone="<%#Eval("JiuDianQianTaiTelephone") %>" <%#Eval("GysName")==Request.QueryString["name"]?"checked=checked":"" %> /> 
                <span style="cursor:pointer; display:none;" bt-xtitle="" title=""><%#GetContactInfo(Eval("Lxrs"), "list", Eval("GysId").ToString(), Eval("GysName").ToString(),Eval("JiuDianQianTaiTelephone"))%></span>
            <a href="javascript:void(0);" data-contact="contact"><%#Eval("GysName")%><%#EyouSoft.Common.UtilsCommons.GetCompanyRecommend(Eval("IsTuiJian"), Eval("IsQianDan"), Eval("IsFanLi"))%></a>
         <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex,recordCount,4) %>
        </ItemTemplate>  
        </asp:repeater>
        <tr>
            <td height="23" align="right" class="alertboxTableT" colspan="5">
                <div style="position: relative; height: 32px;">
                    <div class="pages" id="div_AjaxPage">
                        <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                    </div>
                </div>
            </td>
        </tr>
</table>
