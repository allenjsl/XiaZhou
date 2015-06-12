<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Print.Master"
    ValidateRequest="false" CodeBehind="tuanduijiesuandan.aspx.cs" Inherits="EyouSoft.Web.PrintPage.xz.tuanduijiesuandan" Title="结算单" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
    <div id="i_div_to">
    <!--FT S-->    
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="borderbot_2 inputbot">
        <tr>
            <td width="50%" align="left">
                <span class="font14">敬呈：<asp:TextBox ID="txtCompanyName" runat="server" CssClass="w220"></asp:TextBox>/<asp:TextBox
                    ID="txtCompanyContactName" runat="server" CssClass="w70"></asp:TextBox></span>
            </td>
            <td width="25%" align="left">
                <span class="font14">电话：<asp:TextBox ID="txtContact" runat="server" CssClass="w120"></asp:TextBox></span>
            </td>
            <td width="25%" align="right">
                <span class="font14">传真：<asp:TextBox ID="txtFax" runat="server" CssClass="w120"></asp:TextBox></span>
            </td>
        </tr>
        <tr>
            <td align="left">
                <span class="font14">发自：<asp:TextBox ID="txtSelfName" runat="server" CssClass="w220"></asp:TextBox>/<asp:TextBox
                    ID="txtSelfContactName" runat="server" CssClass="w70"></asp:TextBox></span>
            </td>
            <td align="left">
                <span class="font14">电话：<asp:TextBox ID="txtSelfContact" runat="server" CssClass="w120"></asp:TextBox></span>
            </td>
            <td align="right">
                <span class="font14">传真：<asp:TextBox ID="txtSelfFax" runat="server" CssClass="w120"></asp:TextBox></span>
            </td>
        </tr>
    </table>
    <!--FT E-->
    </div>
    
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="borderbot_2">
        <tr>
            <td align="left">
                <b class="font14"><asp:Literal runat="server" ID="ltr1" Text="团队结算单" /></b>
            </td>
            <td align="right">
                <b class="font16"><asp:Literal runat="server" ID="ltr2" Text=""></asp:Literal></b>
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" class="font14">
                <asp:Literal runat="server" ID="ltr3"> 您好！感谢贵公司长期对我社的友好合作，现将我社团队结算单传真于您，请回传确认！</asp:Literal>
            </td>
        </tr>
    </table>
    
    <!--行程 S-->
    <asp:PlaceHolder runat="server" ID="phXingChengTeSe" Visible="false">
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="borderline_2">
            <tr>
                <td class="small_title">
                    <b class="font16">
                        <input type="checkbox" name="checkbox" id="chk_xcts" />
                        行程特色</b>
                </td>
            </tr>
            <tr i_sh="tr_chk_xcts">
                <td class="td_text">
                    <asp:Literal ID="ltrXingChengTeSe" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="phXingCheng" Visible="false">
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="borderline_2">
            <tr>
                <td class="small_title">
                    <b class="font16">
                        <input type="checkbox" name="checkbox" id="chk_xcap" />
                        行程安排</b>
                </td>
            </tr>
        </table>
        <asp:Literal ID="ltrXingCheng" runat="server"></asp:Literal>
    </asp:PlaceHolder>
    <!--行程 E-->
    
    <!--服务标准 S-->
    <div runat="server" id="TPlanService" visible="false">    
        <div id="divTuanDuiFenXiangBaoJia" runat="server">
            <table width="696" cellspacing="0" cellpadding="0" border="0" align="center" class="list_2">
                <tbody>
                    <tr>
                        <td height="30" class="small_title">
                            <b class="font16">
                                <input type="checkbox" id="chk_FWBZ_ZT" name="checkbox_t" />
                                服务标准</b>
                        </td>
                    </tr>
                </tbody>
            </table>
            <table width="696" cellspacing="0" cellpadding="0" border="0" align="center" class="list_2"
                i_sh="table_chk_FWBZ_ZT">
                <tbody>
                    <asp:Literal ID="lboptionservice" runat="server"></asp:Literal>
                </tbody>
            </table>
        </div>
        
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" runat="server"
            id="TService" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" name="checkbox" id="chk_FWBZ" />
                        服务标准</b>
                </td>
            </tr>
            <tr i_sh="tr_chk_FWBZ">
                <td class="td_text">
                    <asp:Literal ID="lbService" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" runat="server"
            id="TNoService" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" name="checkbox" id="chk_FWBH" />
                        服务不含</b>
                </td>
            </tr>
            <tr i_sh="tr_chk_FWBH">
                <td class="td_text">
                    <asp:Literal ID="lbnoService" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" id="TShopping"
            runat="server" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" name="checkbox" id="chk_GWAP" />
                        购物安排</b>
                </td>
            </tr>
            <tr i_sh="tr_chk_GWAP">
                <td class="td_text">
                    <asp:Literal ID="lbshopping" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" id="TChildren"
            runat="server" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" name="checkbox" id="chk_ETAP" />
                        儿童安排</b>
                </td>
            </tr>
            <tr i_sh="tr_chk_ETAP">
                <td class="td_text">
                    <asp:Literal ID="lbchildren" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" id="TSelfProject"
            runat="server" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" name="checkbox" id="chk_ZFXM" />
                        自费项目</b>
                </td>
            </tr>
            <tr i_sh="tr_chk_ZFXM">
                <td class="td_text">
                    <asp:Literal ID="lbselfproject" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" id="TNeedAttention"
            runat="server" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" name="checkbox" id="chk_ZYSX" />
                        注意事项</b>
                </td>
            </tr>
            <tr i_sh="tr_chk_ZYSX">
                <td class="td_text">
                    <asp:Literal ID="lbneedattention" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" id="TWarmRemind"
            runat="server" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" name="checkbox" id="chk_WXTX" />
                        温馨提示</b>
                </td>
            </tr>
            <tr i_sh="tr_chk_WXTX">
                <td class="td_text">
                    <asp:Literal ID="lbwarmremind" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
    <!--服务标准 E-->
    
    <!--结算信息 S-->
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2">
        <tr>
            <th width="80" align="right">
                接团时间
            </th>
            <td colspan="2">
                <asp:Label runat="server" ID="lbLDate">
                </asp:Label>
            </td>
            <th align="right" style="width:80px;">
                人数
            </th>
            <td colspan="2" align="left">
                <asp:Label runat="server" ID="lbPersonNum">
                </asp:Label>
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="ph_Standard">
            <tr>
                <th align="right">
                    整团
                </th>
                <td colspan="5">
                    <asp:Label runat="server" ID="lbServiceStandard">
                    </asp:Label>
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="ph_Quote">
            <tr>
                <th rowspan="<%=Count1+1 %>" align="right">
                    分项
                </th>
                <th align="center" style="width:80px;">
                    项目
                </th>
                <th align="center" style="width:100px;">
                    单项报价
                </th>
                <th colspan="3" align="center">
                    服务标准
                </th>
            </tr>
            <asp:Repeater runat="server" ID="rpt_QuoteList">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Eval("ServiceType")%>
                        </td>
                        <td>
                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(EyouSoft.Common.Utils.GetDecimal(Eval("Quote").ToString()), ProviderToMoney)%>/<%#Eval("Unit")%>
                        </td>
                        <td colspan="3">
                            <%#EyouSoft.Common.Function.StringValidate.TextToHtml(Eval("ServiceStandard").ToString())%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </asp:PlaceHolder>
        <tr>
            <th align="right">
                价格
            </th>
            <td colspan="5">
                <b><asp:Literal runat="server" ID="ltrBaoJia"></asp:Literal></b>成人单价：<strong><asp:Label runat="server" ID="lbAdultPrice"></asp:Label></strong> * 成人数：<strong><asp:Label
                    runat="server" ID="lbAdultCount"></asp:Label></strong> + 儿童单价：<strong><asp:Label
                        runat="server" ID="lbChildPrice"></asp:Label>
                    </strong> * 儿童数：<asp:Label runat="server" ID="lbChildCount"></asp:Label>
                <asp:Label runat="server" ID="lbOtherCost"></asp:Label>
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="phZengJiaFeiYong" Visible="false">
        <tr>
            <th align="right">
                增加费用
            </th>
            <td>
                <asp:Label runat="server" ID="lbSumPriceAddCost">
                </asp:Label>
            </td>
            <th align="right">
                备注
            </th>
            <td colspan="3">
                <asp:Label runat="server" ID="lbSumPriceAddCostRemark">
                </asp:Label>
            </td>
        </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phJianShaoFeiYong" Visible="false">
        <tr>
            <th align="right">
                减少费用
            </th>
            <td>
                <asp:Label runat="server" ID="lbSumPriceReduceCost">
                </asp:Label>
            </td>
            <th align="right">
                备注
            </th>
            <td colspan="3">
                <asp:Label runat="server" ID="lbSumPriceReduceCostRemark">
                </asp:Label>
            </td>
        </tr>
        </asp:PlaceHolder>        
        <tr>
            <th align="right">
                合计金额
            </th>
            <td colspan="5">
                <asp:Label runat="server" ID="lbSumPrice">
                </asp:Label>
            </td>
        </tr>        
        <asp:PlaceHolder runat="server" ID="ph_BackList">
            <tr>
                <th rowspan="<%=Count2+1 %>" align="right">
                    退款信息
                </th>
                <th align="center">
                    退款人
                </th>
                <th align="center">
                    退款金额
                </th>
                <th align="center">
                    退款方式
                </th>
                <th width="100" align="left">
                    备注
                </th>
                <th align="center">
                    审核
                </th>
            </tr>
            <asp:Repeater runat="server" ID="rpt_BackList">
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            <%#Eval("CollectionRefundOperator")%>
                        </td>
                        <td align="center">
                            <%#EyouSoft.Common.UtilsCommons.GetMoneyString(EyouSoft.Common.Utils.GetDecimal(Eval("CollectionRefundAmount").ToString()), ProviderToMoney)%>
                        </td>
                        <td align="center">
                            <%#Eval("CollectionRefundModeName")%>
                        </td>
                        <td>
                            <%#Eval("Memo")%>
                        </td>
                        <td align="center">
                            <%#(bool)Eval("IsCheck")?"已审核":"未审核"%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </asp:PlaceHolder>        
        <asp:PlaceHolder runat="server" ID="phBianGengZengJiaFeiYong" Visible="false">
        <tr>
            <th align="right">
                变更增加
            </th>
            <td>
                <asp:Label runat="server" ID="lbChangeAddMoney">
                </asp:Label>
            </td>
            <th align="right">
                备注
            </th>
            <td colspan="3">
                <asp:Label runat="server" ID="lbChangeRemark">
                </asp:Label>
            </td>
        </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phBianGengJianShaoFeiYong" Visible="false">
        <tr>
            <th align="right">
                变更减少
            </th>
            <td>
                <asp:Label runat="server" ID="lbChangelessonMoney">
                </asp:Label>
            </td>
            <th align="right">
                备注
            </th>
            <td colspan="3">
                <asp:Label runat="server" ID="lbChangeRemarks">
                </asp:Label>
            </td>
        </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phQueRenJinE" Visible="false">
        <tr>
            <th align="right">
                确认金额
            </th>
            <td>
                <asp:Label runat="server" ID="lbComfirmMoney">
                </asp:Label>
            </td>
            <th align="right">
                已支付金额
            </th>
            <td>
                <asp:Label runat="server" ID="lbPayMoney">
                </asp:Label>
            </td>
            <th align="right">
                尚欠金额
            </th>
            <td>
                <asp:Label runat="server" ID="lbDebtMoney">
                </asp:Label>
            </td>
        </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phJinEBianGengShuoMing" Visible="false">
            <tr>
                <th align="right">
                    金额变更说明
                </th>
                <td colspan="5">
                    <asp:Literal runat="server" ID="ltrJinEBianGengShuoMing"></asp:Literal>
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>    
    <!--结算信息 E-->
    
    <!--游客 S-->
    <asp:PlaceHolder runat="server" ID="phYouKe" Visible="false">
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="borderline_2">
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
            <asp:Repeater runat="server" ID="rptsYouKe">
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
    </asp:PlaceHolder>
    <!--游客 E-->
    
    <asp:PlaceHolder runat="server" ID="phYinHangZhangHao" Visible="false">
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
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list_2" id="i_talbe_yinhangzhanghu">
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
    </asp:PlaceHolder>      
    
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30" align="right">
                签发日期：<asp:Label runat="server" ID="lbDate"></asp:Label>
            </td>
        </tr>
    </table>
    
    <script type="text/javascript">
        $(document).ready(function() {
            $("#chk_xcts").attr("checked", true);
            $("#chk_xcap").attr("checked", true);
            $("#chk_youke").attr("checked", true);
            $("#chk_FWBZ").attr("checked", true);
            $("#chk_FWBH").attr("checked", true);
            $("#chk_GWAP").attr("checked", true);
            $("#chk_ETAP").attr("checked", true);
            $("#chk_ZFXM").attr("checked", true);
            $("#chk_ZYSX").attr("checked", true);
            $("#chk_WXTX").attr("checked", true);
            $("#chk_FWBZ_ZT").attr("checked", true);
            
            PrintMaster.bindChkClick("#chk_xcts", "tr[i_sh='tr_chk_xcts']");
            PrintMaster.bindChkClick("#chk_xcap", "table[i_sh='table_chk_xcap']");
            PrintMaster.bindChkClick("#chk_youke", "#table_youke");

            PrintMaster.bindChkClick("#chk_FWBZ", "tr[i_sh='tr_chk_FWBZ']");
            PrintMaster.bindChkClick("#chk_FWBH", "tr[i_sh='tr_chk_FWBH']");
            PrintMaster.bindChkClick("#chk_GWAP", "tr[i_sh='tr_chk_GWAP']");
            PrintMaster.bindChkClick("#chk_ETAP", "tr[i_sh='tr_chk_ETAP']");
            PrintMaster.bindChkClick("#chk_ZFXM", "tr[i_sh='tr_chk_ZFXM']");
            PrintMaster.bindChkClick("#chk_ZYSX", "tr[i_sh='tr_chk_ZYSX']");
            PrintMaster.bindChkClick("#chk_WXTX", "tr[i_sh='tr_chk_WXTX']");
            PrintMaster.bindChkClick("#chk_FWBZ_ZT", "table[i_sh='table_chk_FWBZ_ZT']");

            $("#i_talbe_yinhangzhanghu").find("input[type='checkbox']").attr("checked", false);
            var _yinHangZhangHaoCount = $("#i_talbe_yinhangzhanghu").find("tr").length;
            for (var i = 0; i < _yinHangZhangHaoCount; i++) {
                $("#tr_yinhangzhanghao_" + i).addClass("unprint");
                PrintMaster.bindChkClickTr("#chk_yinhangzhanghao_" + i);
            }

            
        });
    </script>
</asp:Content>
