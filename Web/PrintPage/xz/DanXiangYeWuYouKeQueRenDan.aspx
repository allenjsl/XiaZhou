<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DanXiangYeWuYouKeQueRenDan.aspx.cs"
    Inherits="EyouSoft.Web.PrintPage.xz.DanXiangYeWuYouKeQueRenDan" MasterPageFile="~/MasterPage/Print.Master"
    Title="单项服务委托预定单" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">   
    <div id="i_div_to">
    <!--FT S-->
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="borderbot_2 inputbot">
        <tr>
            <td width="50%" align="left">
                <span class="font14">敬呈：<asp:TextBox ID="txtKeHuName" runat="server" CssClass="w220"></asp:TextBox>/<asp:TextBox
                    ID="txtKeHuLxr" runat="server" CssClass="w70"></asp:TextBox></span>
            </td>
            <td width="25%" align="left">
                <span class="font14">电话：<asp:TextBox ID="txtKeHuTelephone" runat="server" CssClass="w120"></asp:TextBox></span>
            </td>
            <td width="25%" align="right">
                <span class="font14">传真：<asp:TextBox ID="txtKeHuFax" runat="server" CssClass="w120"></asp:TextBox></span>
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="font14">发自：<asp:TextBox ID="txtCompanyName" runat="server" CssClass="w220"></asp:TextBox>/<asp:TextBox
                    ID="txtXiaoShouYuanName" runat="server" CssClass="w70"></asp:TextBox></span>
            </td>
            <td align="left">
                <span class="font14">电话：<asp:TextBox ID="txtXiaoShouYuanTelephone" runat="server" CssClass="w120"></asp:TextBox></span>
            </td>
            <td align="right">
                <span class="font14">传真：<asp:TextBox ID="txtXiaoShouYuanFax" runat="server" CssClass="w120"></asp:TextBox></span>
            </td>
        </tr>
    </table>
    <!--FT E-->    
    </div>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="borderbot_2">
        <tr>
            <td align="left" style="width:50%">
                <b class="font14">单项服务委托预定单</b>
            </td>
            <td style="text-align:right;"><b class="font14">订单号：<asp:Literal runat="server" ID="ltrOrderCode"></asp:Literal></b></td>
        </tr>
    </table>
    
    <table class="list_2" align="center" width="696" cellspacing="0" cellpadding="0"
        border="0">
        <tr>
            <th style="width:70px">委托类型</th>
            <th>具体要求</th>
            <th style="width:50px; text-align:right;">小计</th>
        </tr>        
        <asp:Repeater runat="server" ID="rptJuTiYaoQiu">
        <ItemTemplate>
        <tr>
            <td rowspan="2"><%#Eval("ServiceType")%></td>
            <td><%#EyouSoft.Common.Utils.TextareaToHTML(Eval("ServiceStandard"))%>&nbsp;</td>
            <td rowspan="2" style="text-align:right;"><%#Eval("Quote","{0:C2}") %></td>
        </tr>        
        <tr>
            <td>备注：<%#Eval("Remark")%></td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>
        <tr>
            <th>&nbsp;</th>
            <th style="text-align: right;">合同金额：</th>
            <th style="text-align: right;"><asp:Literal runat="server" ID="ltrHeTongJinE"></asp:Literal></th>
        </tr>     
    </table>
    
    <!--游客 S-->
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
        <tr>
            <td class="small_title">
                <b class="font16">
                    <input type="checkbox" name="checkbox" id="chk_youke" />
                    游客信息</b>
            </td>
        </tr>
    </table>
    <table width="696" align="center" cellspacing="0" cellpadding="0" border="0" class="list_2"
        id="table_youke">
        <tr>
            <th align="center">
                序号
            </th>
            <th align="center">
                姓名
            </th>
            <th align="center">
                性别
            </th>
            <th align="center">
                类型
            </th>
            <th align="center">
                证件类型
            </th>
            <th align="center">
                证件号
            </th>
            <th align="center">
                联系手机
            </th>
            <th align="center">
                备注
            </th>
        </tr>
        <asp:Repeater runat="server" ID="rptYouKe">
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <%# Container.ItemIndex + 1%>
                    </td>
                    <td align="center">
                        <%#Eval("CnName")%>
                    </td>
                    <td align="center">
                        <%#Eval("Gender")%>
                    </td>
                    <td align="center">
                        <%#Eval("VisitorType")%>
                    </td>
                    <td align="center">
                        <%#Eval("CardType")%>
                    </td>
                    <td align="center">
                        <%#Eval("CardNumber")%>
                    </td>
                    <td align="center">
                        <%#Eval("Contact")%>
                    </td>
                    <td align="center">
                        <%#Eval("Remark")%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <!--游客 E-->
    
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="40" align="left" class="font14 inputbor">
                该团接待无服务质量问题，该团欠款请于
                <asp:TextBox runat="server" ID="txtYear" Width="50"></asp:TextBox>
                年
                <asp:TextBox runat="server" ID="txtMonth" Width="50"></asp:TextBox>
                月
                <asp:TextBox runat="server" ID="txtDate" Width="50"></asp:TextBox>
                日前一次性付清。
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2"
        id="i_talbe_yinhangzhanghu">
        <tr>
            <th width="47" style="text-align: center" class="unprint">
                &nbsp;
            </th>
            <th width="200" align="center">
                开户行
            </th>
            <th width="92" align="center">
                户名
            </th>
            <th align="center">
                卡号
            </th>
        </tr>
        <asp:Repeater runat="server" ID="rptYinHangZhangHu">
            <ItemTemplate>
                <tr id="tr_yinhangzhanghao_<%#Container.ItemIndex %>">
                    <td style="text-align: center;" class="unprint">
                        <input type="checkbox" id="chk_yinhangzhanghao_<%#Container.ItemIndex %>" />
                    </td>
                    <td align="center">
                        <%# Eval("BankName")%>
                    </td>
                    <td align="center">
                        <%# Eval("AccountName")%>
                    </td>
                    <td align="center">
                        <%# Eval("BankNo")%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30" align="right">
                签发日期：<asp:Literal runat="server" ID="ltrQianFaRiQi"></asp:Literal>
            </td>
        </tr>
    </table>
    

    <script type="text/javascript">
        $(document).ready(function() {
            $("#chk_youke").attr("checked", true);
            PrintMaster.bindChkClick("#chk_youke", "#table_youke");

            $("#i_talbe_yinhangzhanghu").find("input[type='checkbox']").attr("checked", false);
            var _yinHangZhangHaoCount = $("#i_talbe_yinhangzhanghu").find("tr").length;
            for (var i = 0; i < _yinHangZhangHaoCount; i++) {
                $("#tr_yinhangzhanghao_" + i).addClass("unprint");
                PrintMaster.bindChkClickTr("#chk_yinhangzhanghao_" + i);
            }
        });
    </script>
</asp:content>
