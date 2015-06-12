<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="List.aspx.cs" Inherits="Web.FinanceManage.InvoiceManage.List" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="SelectFrom" action="List.aspx" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    团号：
                    <input type="text" name="txt_teamNumber" value="<%= Request.QueryString["txt_teamNumber"]%>"
                        class="inputtext formsize80" />
                    客户单位：
                    <uc2:CustomerUnitSelect ID="CustomerUnitSelect1" runat="server" selectfrist="false" />
                    销售员：
                    <uc1:SellsSelect ID="txt_Seller" runat="server" selectfrist="false" />
                    开票金额：
                    <input type="text" class="inputtext formsize50" name="billAmount" value="<%= Request.QueryString["billAmount"]%>" />
                    开票时间：
                    <input type="text" class="inputtext formsize80" onclick="WdatePicker()" name="billTimeS"
                        value="<%= Request.QueryString["billTimeS"]%>" />
                    -
                    <input type="text" class="inputtext formsize80" onclick="WdatePicker()" name="billTimeE"
                        value="<%= Request.QueryString["billTimeE"]%>" />
                    <input type="submit" class="search-btn" /></p>
            </span>
        </div>
        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
        </form>
        <div id="tablehead" class="tablehead">
            <ul class="fixed">
                <asp:PlaceHolder ID="phdShenhe" runat="server">
                    <li><s class="dengji"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_plshenpi">
                        <span>批量审核</span></a></li></asp:PlaceHolder>
                <li><s class="daochu"></s><a href="javascript:void(0);" id="i_a_toXls" hidefocus="true"
                    class="i_a_toxls"><span>导出列表</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th align="center" class="th-line">
                        团号
                    </th>
                    <th align="left" class="th-line">
                        客户单位
                    </th>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <th align="center" class="th-line">
                        <span class="th-line h20">金额</span>
                    </th>
                    <th align="center" class="th-line">
                        开票时间
                    </th>
                    <th align="center" class="th-line">
                        票据号
                    </th>
                    <th align="center" class="th-line">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr data-billid="<%#Eval("Id") %>" data-isapprove="<%#Eval("IsApprove").ToString().ToLower()%>"
                            data-billtime="<%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("BillTime"),ProviderToDate) %>"
                            data-billamountstr="<%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("BillAmount"),ProviderToMoney) %>"
                            data-billamount="<%#Eval("BillAmount") %>" data-billno="<%#Eval("BillNo") %>"
                            data-remark="<%#Eval("Remark") %>">
                            <td align="center">
                                <input type="checkbox" />
                            </td>
                            <td align="center">
                                <%#Eval("TourCode")%>
                            </td>
                            <td align="left">
                                <a href="javascript:void(0);" data-class="a_popo">
                                    <%#Eval("Customer")%></a><span style="display: none"><b><%#Eval("Customer")%></b><br />
                                        联系人：<%#Eval("ContactName")%><br />
                                        联系方式：<%#Eval("ContactPhone")%></span>
                            </td>
                            <td align="center">
                                <%#Eval("SellerName")%>
                            </td>
                            <td align="center">
                                <b class="fontbsize12">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("BillAmount"), ProviderToMoney)%></b>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("BillTime"),ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#Eval("BillNo")%>
                            </td>
                            <td align="center">
                                <%if (this.IsShenHePower)
                                  { %>
                                <a href="javascript:void(0);" data-class="a_ExamineV" class="shenhe">审核</a>
                                <%} %>
                                <a href="javascript:void(0);" data-class="a_Show" class="shenhe">查看</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="8">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <div id="tablehead_clone" style="border: 0pt none;" class="tablehead">

            <script type="text/javascript">
                document.write($("#tablehead").html());
            </script>

        </div>
    </div>

    <script type="text/javascript">
        var InvoiceManageList = {
            ShowBoxy: function(data) {/*显示弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            DataBoxy: function() {/*弹窗默认参数*/
                return {
                    url: "/FinanceManage/InvoiceManage",
                    title: "发票管理",
                    width: "700px",
                    height: "350px"
                }
            },
            PageInit: function() {
                var that = this;
                $("tr[data-isapprove]").each(function() {
                    var obj = $(this)
                    if (obj.attr("data-isapprove") == "true") {
                        obj.find("a[data-class='a_ExamineV']").remove();
                    }
                    else {
                        obj.find("a[data-class='a_Show']").remove();
                    }
                });
                $("a[data-class='a_ExamineV']").click(function() {
                    $("tr[isexaminev]").removeAttr("isexaminev");
                    $(this).closest("tr").attr("isexaminev", "0");
                    var data = that.DataBoxy();
                    data.url += "/ExamineV.aspx?";
                    data.url += $.param({
                        sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>'
                    })
                    data.title += EnglishToChanges.Ping("ExamineV");
                    that.ShowBoxy(data);
                    return false
                });
                $("a[data-class='a_popo']").bt({
                    contentSelector: function() {
                        return $(this).next("span");
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
                $("a[data-class='a_Show']").click(function() {
                    var data = that.DataBoxy();
                    data.url += "/Show.aspx?" + $.param({ billID: $(this).closest("tr").attr("data-billid") });
                    data.title += EnglishToChanges.Ping("Show");
                    data.height = "200px";
                    that.ShowBoxy(data);
                    return false
                });
                tableToolbar.init({
                    tableContainerSelector: "#liststyle",
                    objectName: "记录",
                    otherButtons: [
                    {
                        button_selector: '.toolbar_plshenpi',
                        sucessRulr: 2,
                        msg: '未选中任何记录 ',
                        buttonCallBack: function(objArr) {
                            $("tr[isexaminev]").removeAttr("isexaminev");
                            var obj, isapprove = true;
                            $(objArr).each(function() {
                                obj = $(this);
                                obj.attr("isexaminev", "0");
                                if (obj.attr("data-isapprove") == "true") {
                                    isapprove = false;
                                    tableToolbar._showMsg('勾选的列表中存在已审核数据!');
                                    return false;
                                }
                            })
                            if (isapprove) {
                                var data = that.DataBoxy();
                                data.url += '/ExamineV.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>'
                                data.title += ("-批量" + EnglishToChanges.Ping("ExamineV"));
                                that.ShowBoxy(data);
                            }
                            return false
                        } }]
                    });
                }
            }
            $(function() {
                InvoiceManageList.PageInit();
                //init toxls
                toXls.init({ "selector": "#i_a_toXls" });
            });
    </script>

</asp:Content>
