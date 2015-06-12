<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JiFenDingDanXX.aspx.cs"
    Inherits="EyouSoft.YlWeb.Hangqi.JiFenDingDanXX" MasterPageFile="~/MasterPage/M1.Master"
    Title="订单信息" %>
<%@ MasterType VirtualPath="~/MasterPage/M1.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <div class="step_mainbox">
        <div class="step_T">
            <div class="basicT floatL">
                您的位置：维诗达游轮 > 在线预订</div>
            <div class="step_Rimg">
                <img src="../images/online_book.jpg" /></div>
            <div class="step_num">
                <ul>
                    <li>
                        <img src="../images/step01_1.jpg" /><span class="bfontblue">选择产品</span></li>
                    <li>
                        <img src="../images/step02_1.jpg" /><span class="bfontblue">核对与填写信息</span></li>
                    <li>
                        <img src="../images/step03_1.jpg" /><span class="bfontblue" style="text-indent: 79px;">客服审核</span></li>
                    <li>
                        <img src="../images/step04_1.jpg" /><span class="bfontblue" style="text-indent: 96px;">订单支付</span></li>
                    <li>
                        <img src="../images/step05_1.jpg" /><span class="bfontblue" style="text-indent: 107px;">预订成功</span></li>
                </ul>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="step_box1 margin_T16">
            <div class="basic_mainT_color">
                <h5>
                    预订成功</h5>
            </div>
            <div class="ddzf_yellbox yudin_ok">
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <th align="left">
                            订单号：<asp:Label ID="dingdanhao" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;状态：<font
                                class="zht_fontgreen"><asp:Label ID="dingdanzhuangtai" runat="server" Text=""></asp:Label></font>
                        </th>
                        <th colspan="2" align="right">
                            <asp:Literal runat="server" ID="ltr01"><a href="javascript:void(0)" class="print-btn">打印订单</a></asp:Literal>
                        </th>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;
                        </td>
                        <td width="150" align="left">
                            商品金额
                        </td>
                        <td width="150" align="right">
                            <asp:Label ID="shanpinjine" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;
                        </td>
                        <td align="left">
                            兑换方式
                        </td>
                        <td align="right">
                            <asp:Label ID="duihuanfangshi" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;
                        </td>
                        <td align="left">
                            兑换数量
                        </td>
                        <td align="right">
                            <asp:Label ID="duihuanshuliang" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;
                        </td>
                        <td align="left">
                            兑换积分
                        </td>
                        <td align="right">
                            <asp:Label ID="duihuanjifen" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;
                        </td>
                        <td align="left">
                            兑换金额
                        </td>
                        <td align="right">
                            <asp:Label ID="duihuanjine" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;
                        </td>
                        <td align="left">
                            快递费用
                        </td>
                        <td align="right">
                            <asp:Label ID="kuaidifei" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;
                        </td>
                        <td align="left">
                            实付金额
                        </td>
                        <td align="right">
                            <font class="font18 price_fontred">
                                <asp:Label ID="shifujine" runat="server" Text=""></asp:Label></font>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <!--<div class="yudin_ok_box">
            <div class="yudin_Title">
                预订人信息</div>
            <div class="yudin_cont fixed">
                <ul>
                    <li><b>姓名：</b><asp:Label ID="yudingren" runat="server" Text=""></asp:Label></li>
                    <li><b>电子邮件：</b><asp:Label ID="yudingyoujian" runat="server" Text=""></asp:Label>
                    </li>
                    <li><b>手机号码：</b><asp:Label ID="yudingshouji" runat="server" Text=""></asp:Label></li>
                </ul>
            </div>
        </div>-->
        <asp:PlaceHolder runat="server" ID="phFaPiao">
        <div class="yudin_ok_box">
            <div class="yudin_Title">
                发票信息</div>
            <div class="yudin_cont">
                <p>
                    <b>发票抬头:</b><asp:Label ID="fapiaotaitou" runat="server" Text=""></asp:Label></p>
                <p>
                    <b>发票明细:</b><asp:Label ID="fapiaomingxi" runat="server" Text=""></asp:Label></p>
            </div>
        </div>
        </asp:PlaceHolder>
        
        <asp:PlaceHolder runat="server" ID="phFaPiaoPeiSongFangShi">
        <div class="yudin_ok_box">
            <div class="yudin_Title">
                配送方式</div>
            <div class="yudin_cont">
                <p>
                    <asp:Label ID="peisongfangshi" runat="server" Text=""></asp:Label></p>
            </div>
        </div>
        </asp:PlaceHolder>
    </div>
</asp:Content>
