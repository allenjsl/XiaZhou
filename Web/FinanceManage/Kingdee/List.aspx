<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="List.aspx.cs" Inherits="Web.FinanceManage.Kingdee.List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    团号：<input type="text" class="formsize140" />
                    凭证日期：<input type="text" style="width: 65px; padding-left: 2px;" />
                    至
                    <input type="text" style="width: 65px; padding-left: 2px;" />
                    是否已导：<input name="yes" type="radio" value="" style="border: none;" />是
                    <input name="no" type="radio" value="" style="border: none;" />否
                    <button type="button" class="search-btn">
                        搜索</button></p>
            </span>
        </div>
        <div class="tablehead">
            <div class="pages">
                <span class="current">首页</span><span class="disabled">上页</span><a href="#">下一页</a><a
                    href="#">尾页</a><select name=""><option>1/8</option>
                    </select></div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th align="center" class="th-line">
                        团号
                    </th>
                    <th align="center" class="th-line">
                        凭证日期
                    </th>
                    <th align="center" class="th-line">
                        摘要
                    </th>
                    <th align="center" class="th-line">
                        科目
                    </th>
                    <th align="center" class="th-line">
                        借方
                    </th>
                    <th align="center" class="th-line">
                        贷方
                    </th>
                    <th align="center" class="th-line">
                        <span class="th-line h20">是否已导</span>
                    </th>
                </tr>
                <tr>
                    <td align="center">
                        HHL5-0001-0011
                    </td>
                    <td align="center">
                        2011-09-09
                    </td>
                    <td align="center">
                        20385
                    </td>
                    <td align="center">
                        5101-主营业务收入
                    </td>
                    <td align="right">
                        <b class="fontgreen">20,185.00</b>
                    </td>
                    <td align="right" bgcolor="#FFFFFF">
                        <b class="fontred">20,185.00</b>
                    </td>
                    <td align="center">
                        是
                    </td>
                </tr>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border-top: 0 none;">
            <div class="pages">
                <span class="current">首页</span><span class="disabled">上页</span><a href="#">下一页</a><a
                    href="#">尾页</a><select name=""><option>1/8</option>
                    </select></div>
        </div>
    </div>
    <!-- InstanceEndEditable -->
</asp:Content>
