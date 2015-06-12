<%@ Page Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="PriceList.aspx.cs" Inherits="Web.TeamCenter.PriceList" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="../UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrap">
        <div class="mainbox">
            <form id="form1" method="get">
            <input type="hidden" name="type" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("type") %>" />
            <input type="hidden" size="8" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
            <div class="searchbox fixed">
                <span class="searchT">
                    <p>
                        ��·����<select class="inputselect" name="ddlArea">
                            <%=EyouSoft.Common.UtilsCommons.GetAreaLineForSelect(EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("ddlArea")),SiteUserInfo.CompanyId) %>
                        </select>
                        &nbsp;��·����:
                        <input type="text" name="txtRouteName" class="inputtext formsize150" size="28" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtRouteName") %>" />
                        &nbsp;ѯ�۵�λ: &nbsp;<uc1:CustomerUnitSelect ID="CustomerUnitSelect1" runat="server"
                            SelectFrist="false" />
                        &nbsp;����Ա:
                        <uc2:SellsSelect ID="SellsSelect1" runat="server" SetTitle="����Ա" SelectFrist="false" />
                        &nbsp;����Ա:&nbsp;
                        <uc2:SellsSelect ID="SellsSelect2" runat="server" SetTitle="����Ա" SelectFrist="false" />
                        <input type="hidden" name="hd_sellerId" />
                        <br />
                        ����״̬��<select class="inputselect" name="sltStatus">
                            <option value="">-��ѡ��-</option>
                            <asp:Literal ID="litStatue" runat="server"></asp:Literal>
                        </select>
                        <input type="submit" class="search-btn" />
                    </p>
                </span>
            </div>
            </form>
            <div class="tablehead">
                <ul class="fixed" id="btnAction">
                    <asp:PlaceHolder ID="phForAdd" runat="server">
                        <li><s class="addicon"></s><a class="toolbar_add" hidefocus="true" href="AddPrice.aspx?type=<%=type %>&sl=<%=sl %>&act=add">
                            <span>����</span></a> </li>
                        <li class="line"></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phForCopy" runat="server">
                        <li><s class="copyicon"></s><a class="toolbar_copy" hidefocus="true" href="AddPrice.aspx?sl=1">
                            <span>����</span></a></li>
                        <li class="line"></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phForDelete" runat="server">
                        <li><a class="toolbar_delete" hidefocus="true" href="javascript:void(0)"><s class="delicon">
                        </s><span>ɾ��</span></a></li>
                        <li class="line"></li>
                    </asp:PlaceHolder>
                </ul>
                <div class="pages">
                    <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
                </div>
            </div>
            <!--�б���-->
            <div class="tablelist-box">
                <table width="100%" id="liststyle">
                    <tbody>
                        <tr class="odd">
                            <th class="thinputbg">
                                <input name="checkbox" id="checkbox1" type="checkbox" />
                            </th>
                            <th class="th-line" align="center">
                                ��·����
                            </th>
                            <th class="th-line" align="center">
                                ��·����
                            </th>
                            <th class="th-line" align="center">
                                ѯ�۵�λ
                            </th>
                            <th class="th-line" align="center">
                                �۸�
                            </th>
                            <th class="th-line" align="center">
                                ����
                            </th>
                            <th class="th-line" align="center">
                                ����
                            </th>
                            <th class="th-line" align="center">
                                ����Ա
                            </th>
                            <th class="th-line" align="center">
                                ����Ա
                            </th>
                            <th class="th-line" align="center">
                                ��ѯʱ��
                            </th>
                            <th class="th-line" align="center">
                                ����״̬
                            </th>
                            <%if (EyouSoft.Common.Utils.GetQueryStringValue("type") == "3")
                              { %>
                            <th class="th-line" align="center">
                                ǩ֤״̬
                            </th>
                            <%} %>
                            <th class="th-line" align="center">
                                ѯ��״̬
                            </th>
                            <th class="th-line" align="center">
                                ��ϸ
                            </th>
                        </tr>
                        <asp:Repeater ID="rptList" runat="server">
                            <ItemTemplate>
                                <tr <%#Container.ItemIndex%2==0?" class=\"odd\" ":""  %>>
                                    <td align="center">
                                        <input name="checkbox" value="<%#Eval("QuoteId") %>" id="checkbox" type="checkbox">
                                    </td>
                                    <td align="center">
                                        <%#Eval("AreaName")%>
                                    </td>
                                    <td align="left">
                                        <a href='<%#PrintPageZt %>?quoteid=<%#Eval("QuoteId") %>' target="_blank">
                                            <%#Eval("RouteName")%></a>
                                    </td>
                                    <td align="center">
                                        <a class="a_info" href="javascript:void(0);">
                                            <%#Eval("BuyCompanyName")%>
                                        </a><span style="display: none;"><b>
                                            <%#Eval("BuyCompanyName")%></b><br />
                                            ��ϵ�ˣ�<%#Eval("Contact")%><br />
                                            ��ϵ��ʽ��<%#Eval("Phone")%></span>
                                    </td>
                                    <td align="right">
                                        <b class="fontblue">
                                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("AdultPrice"),this.ProviderToMoney)%></b>
                                    </td>
                                    <td align="center">
                                        <%#Eval("Days")%>
                                    </td>
                                    <td align="center">
                                        <a href='<%#GetPrintUrl(Eval("MTourQuoteTourInfo"),Eval("QuoteState"))%>'>
                                            <%#Eval("Adults")%></a><sup class="fontred">+<%#Eval("Childs")%></sup>
                                    </td>
                                    <td align="center">
                                        <%#((EyouSoft.Model.TourStructure.MSaleInfo)Eval("SaleInfo")).Name%>
                                        <input type="hidden" name="ItemUserID" value="<%# Eval("SaleInfo")==null ?"": ((EyouSoft.Model.TourStructure.MSaleInfo)Eval("SaleInfo")).SellerId%>" />
                                    </td>
                                    <td align="center">
                                        <span data-class="OperatorInfo">
                                            <%#((EyouSoft.Model.TourStructure.MOperatorInfo)Eval("OperatorInfo")).Name%></span>
                                    </td>
                                    <td align="center">
                                        <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("InquiryTime"),ProviderToDate)%>
                                    </td>
                                    <td align="center">
                                        <%#GetHtmlByState(Eval("TimeCount").ToString(), (EyouSoft.Model.EnumType.TourStructure.QuoteState)(int)Eval("QuoteState"), Eval("CancelReason").ToString())%>
                                    </td>
                                    <%if (EyouSoft.Common.Utils.GetQueryStringValue("type") == "3")
                                      { %>
                                    <td align="center">
                                        <a href="javascript:void(0);" data-id="<%#Eval("QuoteId") %>" data-class="visaFiles">
                                            �鿴����</a>
                                    </td>
                                    <%} %>
                                    <td align="center">
                                        <%#GetQuoteStateFormOper(Eval("PlanerId").ToString(), (bool)Eval("IsPlanerQuote"))%>
                                    </td>
                                    <td align="center">
                                        <a href="AddPrice.aspx?type=<%=type %>&sl=<%=sl %>&id=<%#Eval("QuoteId") %>&act=update"
                                            class="check-btn" title="�鿴"></a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Literal ID="litMsg" runat="server" Text="<tr><td align='center' colspan='13'>���ޱ�����Ϣ!</td></tr>"></asp:Literal>
                    </tbody>
                </table>
            </div>
            <!--�б����-->
            <div style="border-top: 0 none;" class="tablehead">
                <ul class="fixed">

                    <script type="text/javascript">
                        document.write($("#btnAction").html());
                    </script>

                </ul>
                <div class="pages">
                    <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                </div>
            </div>
        </div>
        <!-- InstanceEndEditable -->
    </div>

    <script type="text/javascript">
        var PriceList = {
            type: '<%=Request.QueryString["type"] %>',
            sl: '<%=Request.QueryString["sl"] %>',
            //ʹ��ͨ�ð�ť�»�ȡ���ݲ�ִ���첽ɾ��
            //ɾ��(����)
            DelAll: function(objArr) {
                var list = new Array();
                var msgList = new Array();
                var state = "";
                //������ť�����������
                for (var i = 0; i < objArr.length; i++) {
                    //������������ҵ��������ڣ������浽���������
                    if (objArr[i].find("input[type='checkbox']").val() != "on") {
                        state = objArr[i].find("span[data-class='QuoteState']").attr("data-state");
                        if (state == "<%=(int)EyouSoft.Model.EnumType.TourStructure.QuoteState.���۳ɹ� %>") {
                            msgList.push("��ǰѡ�����е�" + (i + 1) + "���ѳɹ�����,�޷�ɾ��!");
                        } if (state == "<%=(int)EyouSoft.Model.EnumType.TourStructure.QuoteState.�渶������� %>") {
                            msgList.push("��ǰѡ�����е�" + (i + 1) + "�����������,�޷�ɾ��!");
                        } else {
                            list.push(objArr[i].find("input[type='checkbox']").val());
                        }
                    }
                }
                if (msgList.length > 0) {
                    tableToolbar._showMsg(msgList.join("<br />"));
                    return false;
                }
                //ִ��
                $.newAjax({
                    type: "get",
                    cache: false,
                    url: "/TeamCenter/PriceList.aspx?dotype=delete&ids=" + list.join(',') + "&sl=" + PriceList.sl,
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = window.location.href;
                            });
                        } else {
                            tableToolbar._showMsg(ret.msg);
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });

            }
        }

        $(function() {
            tableToolbar.init({
                objectName: "����",
                deleteCallBack: function(objsArr) {
                    //ɾ��(����)
                    PriceList.DelAll(objsArr);
                },
                copyCallBack: function(objsArr) {
                    location.href = "AddPrice.aspx?type=" + PriceList.type + "&sl=" + PriceList.sl + "&id=" + objsArr[0].find(":checkbox").val() + "&act=copy";
                }
            });

            $("#liststyle").find("a[data-class='visaFiles']").click(function() {
                Boxy.iframeDialog({
                    iframeUrl: "/TeamCenter/VisaFileList.aspx?quoteId=" + $(this).attr("data-id") + "&sl=" + $.param(PriceList.sl),
                    title: "�鿴ǩ֤����",
                    modal: true,
                    width: "650px",
                    height: "420px"
                });
                return false;
            })

            $('.a_info').bt({
                contentSelector: function() {
                    return $(this).next().html();
                },
                positions: ['left', 'right', 'bottom'],
                fill: '#FFF2B5',
                strokeStyle: '#D59228',
                noShadowOpts: { strokeStyle: "#D59228" },
                spikeLength: 10,
                spikeGirth: 15,
                width: 200,
                overlap: 0,
                centerPointY: 1,
                cornerRadius: 4,
                shadow: true,
                shadowColor: 'rgba(0,0,0,.5)',
                cssStyles: { color: '#00387E', 'line-height': '180%' }
            });

            $("#liststyle").find("a[data-class='cancelReason']").each(function() {
                if ($.trim($(this).next().html()) != "") {
                    $(this).bt({
                        contentSelector: function() {
                            return $(this).next().html();
                        },
                        positions: ['left', 'right', 'bottom'],
                        fill: '#FFF2B5',
                        strokeStyle: '#D59228',
                        noShadowOpts: { strokeStyle: "#D59228" },
                        spikeLength: 10,
                        spikeGirth: 15,
                        width: 200,
                        overlap: 0,
                        centerPointY: 1,
                        cornerRadius: 4,
                        shadow: true,
                        shadowColor: 'rgba(0,0,0,.5)',
                        cssStyles: { color: '#00387E', 'line-height': '180%' }
                    });
                }
            })

        });
    </script>

</asp:Content>
