<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YuSuanBiao.aspx.cs" Inherits="EyouSoft.Web.TongJi.YuSuanBiao"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/DuoXuanTourStatus.ascx" TagName="DuoXuanTourStatus"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form id="form1" method="get">
            <input type="hidden" name="sl" value='<%= SL %>' />
            <span class="searchT">
                <p>
                    出团时间：
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtChuTuanSRiQi"
                        name="txtChuTuanSRiQi" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtChuTuanSRiQi") %>" />
                    -
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize100" id="txtChuTuanERiQi"
                        name="txtChuTuanERiQi" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtChuTuanERiQi") %>" />
                    销售员部门：<uc1:selectsection id="txtXiaoShouYuanBuMen" runat="server" readonly="true" settitle="部门"
                        smodel="2" />
                    销售员：
                    <uc1:sellsselect id="txtXiaoShouYuan" runat="server" settitle="选择销售员" selectfrist="false" />
                    团队状态：<uc1:DuoXuanTourStatus runat="server" id="txtTourStatus"></uc1:DuoXuanTourStatus><br />
                    团号：
                    <input type="text" class="inputtext formsize100" name="txtTourCode" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtTourCode") %>" />
                    线路名称：<input type="text" class="inputtext formsize120" name="txtXianLuName" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtXianLuName") %>" />
                    <input type="submit" class="search-btn" />
                </p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                <li><s class="daochu"></s><a href="javascript:void(0);" onclick="toXls1(); return false;"
                    hidefocus="true" class="toolbar_daochu"><span>导出列表</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr class="odd">
                    <th class="th-line" style="text-align:right; width:40px;">
                        序号
                    </th>
                    <th class="th-line" style="text-align: left;">
                        团号
                    </th>
                    <th class="th-line" style="text-align: left;">
                        线路名称
                    </th>
                    <th class="th-line" style="text-align: left; width: 7%;">
                        出团日期
                    </th>
                    <th class="th-line" style="text-align: left; width: 7%;">
                        回团日期
                    </th>                                   
                    <th class="th-line" style="text-align: left; width: 7%;">
                        销售员
                    </th>
                    <th class="th-line" style="text-align: left; width: 7%;">
                        计调员
                    </th>
                    <th class="th-line" style="text-align: left; width: 7%;">
                        导游
                    </th>
                    <th class="th-line" style="text-align: left; width: 7%;">
                        团队状态
                    </th>
                    <th class="th-line" style="text-align: right; width: 7%;">
                        人数&nbsp;
                    </th>
                    <th class="th-line" style="text-align: right; width: 8%;">
                        收入&nbsp;
                    </th>
                    <th class="th-line" style="text-align: right; width: 8%;">
                        支出&nbsp;
                    </th>
                    <th class="th-line" style="text-align: right;width:8%;">
                        毛利&nbsp;
                    </th>
                </tr>
                <asp:Repeater ID="rpt" runat="server">
                <ItemTemplate>
                <tr class="odd" i_tourid="<%#Eval("TourId") %>">
                    <td class="thinputbg" style="text-align:right;">
                        <%# Container.ItemIndex + 1+( this.pageIndex - 1) * this.pageSize%>
                    </td>
                    <td class="th-line">
                        <%#Eval("TourCode") %>
                    </td>
                    <td class="th-line">
                        <%#Eval("XianLuName") %>
                    </td>
                    <td class="th-line">
                        <%#Eval("ChuTuanRiQi", "{0:yyyy-MM-dd}")%>
                    </td>
                    <td class="th-line">
                        <%#Eval("HuiTuanRiQi","{0:yyyy-MM-dd}") %>
                    </td>
                    <td class="th-line">
                        <%#Eval("XiaoShouYuanName") %>
                    </td>
                    <td class="th-line">
                        <%#Eval("JiDiaoYuanName") %>
                    </td>
                    <td class="th-line">
                        <%#Eval("DaoYouName") %>
                    </td>
                    <td class="th-line">
                        <%# GetTourStatus(Eval("TourType"),Eval("TourStatus"))%>
                    </td>
                    <td class="th-line" style="text-align:right;">
                        <%#Eval("ShiShouRenShu") %>&nbsp;
                    </td>
                    <td align="right" class="th-line">
                        <!--<a href="javascript:void(0)" class="i_maoli">--><%#Eval("ShouRuJinE","{0:F2}") %><!--</a>-->&nbsp;
                    </td>
                    <td align="right" class="th-line">
                        <!--<a href="javascript:void(0)" class="i_maoli">--><%#Eval("ZhiChuJinE", "{0:F2}")%><!--</a>-->&nbsp;
                    </td>
                    <td align="right" class="th-line">
                        <a href="javascript:void(0)" class="i_maoli"><%#Eval("MaoLi", "{0:F2}")%></a>&nbsp;
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phHeJi">
                <tr>
                    <td colspan="9" align="right">
                        合计：
                    </td>
                    <td align="right">
                        <asp:Literal runat="server" ID="ltrShiShouRenShu"></asp:Literal>&nbsp;
                    </td>
                    <td align="right">
                        <asp:Literal runat="server" ID="ltrShouRuJinE"></asp:Literal>&nbsp;
                    </td>
                    <td align="right">
                        <asp:Literal runat="server" ID="ltrZhiChuJinE"></asp:Literal>&nbsp;
                    </td>
                    <td align="right">
                        <asp:Literal runat="server" ID="ltrMaoLi"></asp:Literal>&nbsp;
                    </td>
                </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                <tr>
                    <td colspan="50" align="center">
                        暂无数据。
                    </td>
                </tr>
                </asp:PlaceHolder>
                
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border-top: 0 none;" id="select_Toolbar_Paging_2">
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#select_Toolbar_Paging_1").children().clone(true).prependTo("#select_Toolbar_Paging_2");
            
            window["<%=txtTourStatus.ClientID %>"].setValue({ value: "<%=EyouSoft.Common.Utils.GetQueryStringValue(txtTourStatus.ValueClientID) %>", text: "<%=EyouSoft.Common.Utils.GetQueryStringValue(txtTourStatus.TextClientID) %>" });
            
            /*$(".i_shouru").click(function() {
                var url = "yusuanbiaoshouru.aspx?sl=" + '<%=SL %>&';
                var _$tr = $(this).closest("tr");
                var _data = { tourid: _$tr.attr("i_tourid") };
                Boxy.iframeDialog({ iframeUrl: url, title: "收入明细", modal: true, data: _data, width: "900px", height: "525px" });
                return false;
            });
            
            $(".i_zhichu").click(function() {
                var url = "yusuanbiaozhichu.aspx?sl=" + '<%=SL %>&';
                var _$tr = $(this).closest("tr");
                var _data = { tourid: _$tr.attr("i_tourid") };
                Boxy.iframeDialog({ iframeUrl: url, title: "支出明细", modal: true, data: _data, width: "900px", height: "525px" });
                return false;
            });*/
            
            $(".i_maoli").click(function() {
                var url = "yusuanbiaomaoli.aspx?sl=" + '<%=SL %>&';
                var _$tr = $(this).closest("tr");
                var _data = { tourid: _$tr.attr("i_tourid") };
                Boxy.iframeDialog({ iframeUrl: url, title: "毛利明细", modal: true, data: _data, width: "900px", height: "525px" });
                return false;
            });
        });
    </script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
</asp:Content>
