<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxCarSupplier.aspx.cs"
    Inherits="Web.ResourceManage.AjaxRequest.AjaxCarSupplier" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>

<script src="/Js/bt.min.js" type="text/javascript"></script>

<link href="/Css/style.css" rel="stylesheet" type="text/css" />
<link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
        $(function() {
             $('#tblList').find("a[data-contact='contact']").bt({
                contentSelector: function() {
                    return $(this).prev("span").html();
                },
                positions: ['bottom'],
                fill: '#FFF2B5',
                strokeStyle: '#D59228',
                noShadowOpts: { strokeStyle: "#D59228" },
                spikeLength: 5,
                spikeGirth: 15,
                width: 405,
                overlap: 0,
                centerPointY: 4,
                cornerRadius: 4,
                shadow: true,
                shadowColor: 'rgba(0,0,0,.5)',
                cssStyles: { color: '#00387E', 'line-height': '200%' }
            });
        });
</script>

<div id="tblList">
    <table width="100%" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF" align="center"
        id="liststyle" style="margin: 0 auto">
        <tbody>
            <tr>
                <td width="30" height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    &nbsp;
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    车队名称
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    车型
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    控车数量
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    预控日期
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    已使用数量
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    联系人
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    联系电话
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    联系传真
                </td>
            </tr>
            <asp:repeater id="RepList" runat="server">
        <ItemTemplate>
        
            <tr <%# Container.ItemIndex%2!=0? "":"bgcolor='#F3F3F3'" %> >
               <td height="28"  align="center">
                 <input type="radio" name="checkbox" id="Radio1" data-show="<%#Eval("SourceName")%>" value="<%#Eval("SourceId") %>" zyyk="<%#Eval("Id") %>" data-contactname='<%#GetContactInfo(Eval("SourceId"),"name") %>' data-fax='<%#GetContactInfo(Eval("SourceId"),"fax") %>' data-tel='<%#GetContactInfo(Eval("SourceId"),"tel") %>' <%#Eval("SourceName")==Request.QueryString["name"]?"checked=checked":"" %> data-controlcount='<%#Eval("ControlNum") %>' data-alreadnum='<%#Eval("AlreadyNum") %>'  />
                </td>
                <td align="left"><%#Eval("SourceName")%>
                </td>
                <td  align="left"><%#Eval("TypeName")%> 
                </td>
                <td  align="center"><%# Eval("ControlNum")%></td>
                <td  align="center">
                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("SueStartTime"), ProviderToDate)%>-<%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("SueEndTime"), ProviderToDate)%>
                </td>
                <td  align="center"><%# Eval("AlreadyNum")%></td> 
                <td  align="center">
                 <span style="display:none"><%#GetContactInfo(Eval("SourceId"),"list")%></span>
                                <a href="javascript:void(0)" data-contact="contact"><%#GetContactInfo(Eval("SourceId"),"name")%></a>
                <td  align="center"><%#GetContactInfo(Eval("SourceId"),"tel")%></td>
                <td  align="center"><%#GetContactInfo(Eval("SourceId"),"fax")%></td>
            </tr>
         </ItemTemplate> 
    </asp:repeater>
            <tr>
                <td height="30" colspan="9">
                    <div style="position: relative; height: 20px;">
                        <div class="pages" id="div_AjaxPage">
                            <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                        </div>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</div>
