<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxCruiseSupplier.aspx.cs"
    Inherits="Web.ResourceManage.AjaxRequest.AjaxCruiseSupplier" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>

<script src="/Js/bt.min.js" type="text/javascript"></script>

<link href="/Css/style.css" rel="stylesheet" type="text/css" />
<link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
        $(function() {
             $('#liststyle').find("a[data-contact='contact']").bt({
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
                <td width="30" height="23" bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                    &nbsp;
                </td>
                <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                    游船公司名称
                </td>
                <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                    游船名称
                </td>
                <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                    控船数量
                </td>
                <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                    登船日期
                </td>
                <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                    已使用数量
                </td>
                <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                    联系人
                </td>
                <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                    联系电话
                </td>
                <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
                    联系传真
                </td>
            </tr>
            <asp:repeater id="RepList" runat="server">
                <ItemTemplate >
                         <tr <%# Container.ItemIndex%2!=0? "":"bgcolor='#F3F3F3'" %>>
                                <td height="28" align="center">
                                     <input type="radio" name="checkbox" id="Radio1" data-show="<%#Eval("ShipCompany")%>" value="<%#Eval("SourceId") %>" zyyk="<%#Eval("Id") %>" data-contactname='<%#GetContactInfo(Eval("SourceId"),"name") %>' data-fax='<%#GetContactInfo(Eval("SourceId"),"fax") %>' data-tel='<%#GetContactInfo(Eval("SourceId"),"tel") %>' <%#Eval("SourceName")==Request.QueryString["name"]?"checked=checked":"" %>  data-controlcount='<%#Eval("ControlNum")%>' data-alreadnum='<%#Eval("AlreadyNum")%>' />
                                </td>
                                <td align="center">
                                     <%#Eval("ShipCompany")%>
                                </td>
                                <td align="center">
                                    <%#Eval("ShipName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("ControlNum")%>
                                </td>
                                <td align="center">
                                    <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("GoShipTime"), ProviderToDate)%>
                                </td>
                                <td align="center">
                                    <%#Eval("AlreadyNum")%>
                                </td>
                                <td align="center">
                                    <span style="display:none"><%#GetContactInfo(Eval("SourceId"),"list")%></span>
                                    <a href="javascript:void(0)" data-contact="contact"><%#GetContactInfo(Eval("SourceId"),"name")%></a>
                                </td>
                                <td align="center">
                                    <span><%#GetContactInfo(Eval("SourceId"),"tel")%></span>
                                </td>
                                <td align="center">
                                    <span><%#GetContactInfo(Eval("SourceId"),"fax")%></span>
                                </td>
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
