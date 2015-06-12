<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DanXiangYeWuGysQueRenDan.aspx.cs"
    Inherits="EyouSoft.Web.PrintPage.xz.DanXiangYeWuGysQueRenDan" MasterPageFile="~/MasterPage/Print.Master"
    Title="单项服务供应商确认件" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
    <div id="i_div_to">
        <!--FT S-->
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="borderbot_2 inputbot">
            <tr>
                <td width="50%" align="left">
                    <span class="font14">敬呈：<asp:TextBox ID="txtGysName" runat="server" CssClass="w220"></asp:TextBox>/<asp:TextBox
                        ID="txtGysLxr" runat="server" CssClass="w70"></asp:TextBox></span>
                </td>
                <td width="25%" align="left">
                    <span class="font14">电话：<asp:TextBox ID="txtGysTelephone" runat="server" CssClass="w120"></asp:TextBox></span>
                </td>
                <td width="25%" align="right">
                    <span class="font14">传真：<asp:TextBox ID="txtGysFax" runat="server" CssClass="w120"></asp:TextBox></span>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <span class="font14">发自：<asp:TextBox ID="txtCompanyName" runat="server" CssClass="w220"></asp:TextBox>/<asp:TextBox
                        ID="txtXiaoShouYuanName" runat="server" CssClass="w70"></asp:TextBox></span>
                </td>
                <td align="left">
                    <span class="font14">电话：<asp:TextBox ID="txtXiaoShouYuanTelephone" runat="server"
                        CssClass="w120"></asp:TextBox></span>
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
            <td align="left" style="width: 50%">
                <b class="font14">单项服务供应商确认件</b>
            </td>
            <td style="text-align: right;">
                <b class="font14">团号：<asp:Literal runat="server" ID="ltrTourCode"></asp:Literal></b>
            </td>
        </tr>
    </table>
    <table align="center" width="696" cellspacing="0" cellpadding="0" border="0">
        <tbody>
            <tr>
                <td align="left" height="40" class="font14">
                    您好！感谢贵景区长期对我社的友好合作，现将我社团队计划传真于您，请回传确认！
                </td>
            </tr>
        </tbody>
    </table>
    <table class="list_2" align="center" width="696" cellspacing="0" cellpadding="0"
        border="0">
        <tr>
            <th style="width:80px; text-align:right;">具体安排：</th>
            <td><asp:Literal runat="server" ID="ltrJuTiAnPai"></asp:Literal></td>
        </tr>
        <tr>
            <th style="text-align: right;">费用明细：</th>
            <td><asp:Literal runat="server" ID="ltrFeiYongMingXi"></asp:Literal></td>
        </tr>
        <tr>
             <th style="text-align: right;">数量：</th>
            <td><asp:Literal runat="server" ID="ltrShuLiang"></asp:Literal></td>
        </tr>
        <tr>
             <th style="text-align: right;">支付方式：</th>
            <td><asp:Literal runat="server" ID="ltrZhiFuFangShi"></asp:Literal></td>
        </tr>
        <tr>
             <th style="text-align: right;">结算金额：</th>
            <td><asp:Literal runat="server" ID="ltrJieSuanJinE"></asp:Literal></td>
        </tr>
    </table>
    
    <table class="list_2" align="center" width="696" cellspacing="0" cellpadding="0"
        border="0">
        <tr>
            <td height="120" align="center" style="width:50%">
                <div id="divImgCachet">峡州签章</div>
            </td>
            <td colspan="3" align="center">
                供应商签章
            </td>
        </tr>
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
            
        });
    </script>

</asp:Content>
