<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="AlreadyList.aspx.cs" Inherits="Web.FinanceManage.OneTeamAdjustAccounts.AlreadyList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="SelectFrom" action="AlreadyList.aspx" accept="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    团号：<input type="text" name="txt_teamNumber" value="<%=Request.QueryString["txt_teamNumber"] %>"
                        class="formsize120" />
                    线路名称：<input type="text" name="txt_lineName" value="<%=Request.QueryString["txt_lineName"] %>"
                        class="formsize180" />
                    出团时间：<input name="txt_SDate" type="text" value="<%=Request.QueryString["txt_SDate"] %>"
                        style="width: 65px; padding-left: 2px;" />
                    至
                    <input type="text" name="txt_EDate" value="<%=Request.QueryString["txt_EDate"] %>"
                        style="width: 65px; padding-left: 2px;" />
                    <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
                    <input type="submit" class="search-btn" /></p>
            </span>
        </div>
        </form>
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a href="WaitList.aspx?sl=<%=Request.QueryString["sl"] %>"
                    hidefocus="true" class="ztorderform"><span>待核算</span></a></li>
                <li><s class="orderformicon"></s><a href="javascript:();" hidefocus="true" class="ztorderform  de-ztorderform">
                    <span>已核算</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th width="30" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th width="111" align="center" class="th-line">
                        团号
                    </th>
                    <th width="179" align="left" class="th-line">
                        线路名称
                    </th>
                    <th width="86" align="center" class="th-line">
                        出团时间
                    </th>
                    <th width="76" align="center" class="th-line">
                        人数
                    </th>
                    <th width="83" align="center" class="th-line">
                        操作员
                    </th>
                    <th width="81" align="center" class="th-line">
                        收入
                    </th>
                    <th width="77" align="center" class="th-line">
                        支出
                    </th>
                    <th width="70" align="center" class="th-line">
                        毛利
                    </th>
                    <th width="101" align="center" class="th-line">
                        利润分配
                    </th>
                    <th width="61" align="center" class="th-line">
                        纯利
                    </th>
                    <th width="111" align="center" class="th-line">
                        单据打印
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" />
                            </td>
                            <td align="center">
                                HHL5-0001-0011
                            </td>
                            <td align="left">
                                <a href="#">海口进出五天四晚休闲之旅</a>
                            </td>
                            <td align="center">
                                2006-04-05
                            </td>
                            <td align="center">
                                26+0+0
                            </td>
                            <td align="center">
                                张三
                            </td>
                            <td align="center">
                                <b class="fontbsize12">2500</b>
                            </td>
                            <td align="center">
                                <b class="fontgreen">500</b>
                            </td>
                            <td align="center">
                                24%
                            </td>
                            <td align="center">
                                5%
                            </td>
                            <td align="center">
                                10%
                            </td>
                            <td align="center">
                                <a href="#" class="fontblue">退回财务</a> | <a href="../组团团队/querendan-tuanduibj.htm"
                                    class="fontblue">核算单</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td align="center">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </td>
                    <td align="center">
                        HHL5-0001-0011
                    </td>
                    <td align="left">
                        <a href="#">海口进出五天四晚休闲之旅</a>
                    </td>
                    <td align="center">
                        2006-04-05
                    </td>
                    <td align="center">
                        26+0+0
                    </td>
                    <td align="center">
                        张三
                    </td>
                    <td align="center">
                        <b class="fontbsize12">2500</b>
                    </td>
                    <td align="center">
                        <b class="fontgreen">500</b>
                    </td>
                    <td align="center">
                        24%
                    </td>
                    <td align="center">
                        5%
                    </td>
                    <td align="center">
                        10%
                    </td>
                    <td align="center">
                        <a href="#" class="fontblue">退回财务</a> | <a href="../组团团队/querendan-tuanduibj.htm"
                            class="fontblue">核算单</a>
                    </td>
                </tr>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border-top: 0 none;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
            <form id="form1" runat="server" style="text-align: center">
            <asp:Label ID="lbl_msg" runat="server" Text=""></asp:Label>
            </form>
        </div>
    </div>
</asp:Content>
