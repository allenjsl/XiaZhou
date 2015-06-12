<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="OrderInfo.aspx.cs" Inherits="Web.SellCenter.OrderInfo" %>

<%@ Register Src="~/UserControl/TravelControl.ascx" TagName="TravelControl" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/TravelControlS.ascx" TagName="TravelControlS" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../../Js/ValiDatorForm.js" type="text/javascript"></script>

    <form id="form1" runat="server">
    <div class="wrap">
        <!--内容-->
        <div class="mainbox mainbox-whiteback">
            <div class="addContent-box">
                <table width="100%" cellpadding="0" cellspacing="0" class="firsttable">
                    <tr>
                        <td width="12%" class="addtableT">
                            订单号：
                        </td>
                        <td width="23%" class="kuang2">
                            <asp:Literal ID="litOrderCode" runat="server"></asp:Literal>
                        </td>
                        <td width="10%" class="addtableT">
                            <font class="fontbsize12">*</font> 客源单位：
                        </td>
                        <td width="25%" class="kuang2">
                            <asp:Literal ID="litBuyCompanyName" runat="server"></asp:Literal>
                        </td>
                        <td width="10%" class="addtableT">
                            联系人：
                        </td>
                        <td width="20%" class="kuang2">
                            <asp:Literal ID="litDContactName" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            联系电话：
                        </td>
                        <td class="kuang2">
                            <asp:Literal ID="litDContactTel" runat="server"></asp:Literal>
                        </td>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font> 订单销售员：
                        </td>
                        <td class="kuang2">
                            <asp:Literal ID="litSellerName" runat="server"></asp:Literal>
                        </td>
                        <td class="addtableT">
                            下单人：
                        </td>
                        <td class="kuang2">
                            <asp:Literal ID="litOperator" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font> 价格组成：
                        </td>
                        <td colspan="5" class="kuang2" width="88%">
                            成人单价：<strong><asp:Literal ID="litAdultPrice" runat="server"></asp:Literal></strong>
                            * 成人数：<strong><asp:Literal ID="litAdults" runat="server"></asp:Literal></strong>
                            + 儿童单价：<strong><asp:Literal ID="litChildPrice" runat="server"></asp:Literal></strong>
                            * 儿童数： <strong>
                                <asp:Literal ID="litChilds" runat="server"></asp:Literal></strong> 其他： <strong>
                                    <asp:Literal ID="litother" runat="server"></asp:Literal></strong>
                        </td>
                    </tr>
                    <td class="addtableT">
                        价格备注：
                    </td>
                    <td colspan="5" class="kuang2" width="88%">
                        <asp:Literal ID="ltrJiaGeBeiZhu" runat="server"></asp:Literal>
                    </td>
                    <tr>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            销售增加费用：
                        </td>
                        <td class="kuang2">
                            <asp:Literal ID="litSaleAddCost" runat="server"></asp:Literal>
                        </td>
                        <td class="addtableT">
                            销售增加费用备注：
                        </td>
                        <td colspan="3" class="kuang2" width="55%">
                            <asp:Literal ID="litSaleAddCostRemark" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            销售减少费用：
                        </td>
                        <td class="kuang2">
                            <asp:Literal ID="litSaleReduceCost" runat="server"></asp:Literal>
                        </td>
                        <td class="addtableT">
                            销售减少费用备注：
                        </td>
                        <td colspan="3" class="kuang2" width="55%">
                            <asp:Literal ID="litSaleReduceCostRemark" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="14" class="addtableT">
                            <font class="fontbsize12">*</font> 合计金额：
                        </td>
                        <td class="kuang2">
                            <asp:Literal ID="litSumPrice" runat="server"></asp:Literal>
                        </td>
                        <td class="addtableT">
                            导游现收：
                        </td>
                        <td class="kuang2">
                            <asp:Literal ID="litGuideIncome" runat="server"></asp:Literal>
                        </td>
                        <td class="addtableT">
                            销售应收
                        </td>
                        <td class="kuang2">
                            <asp:Literal ID="litSalerIncome" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            订单备注：
                        </td>
                        <td colspan="5" class="kuang2 pand4" width="88%">
                            <asp:Literal ID="litOrderRemark" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="tablelist-box " style="width: 98.5%">
                <uc1:TravelControl ID="TravelControl1" runat="server" />
                <br />
                <uc2:TravelControlS ID="TravelControlS1" runat="server" />
            </div>
            <div class="hr_10">
            </div>
            <div class="mainbox cunline">
                <ul>
                    <li class="cun-cy">
                        <asp:PlaceHolder ID="phdSave" runat="server"><a href="javascript:void(0)" id="btnsave">
                            保存</a></asp:PlaceHolder>
                    </li>
                    <li class="cun-cy">
                        <asp:PlaceHolder ID="phdCancel" runat="server"><a href="javascript:history.go(-1);"
                            id="btncancel">返回</a></asp:PlaceHolder>
                    </li>
                </ul>
                <div class="hr_10">
                </div>
            </div>
        </div>
        <!-- InstanceEndEditable -->
        <asp:HiddenField ID="hidorderID" runat="server" />
    </div>

    <script type="text/javascript">
        var OrderInfo={
                data:{
                    sl:'<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                    ReturnUrl: '<%=Request.QueryString["url"] %>'
                },
                CheckForm: function() {
                    var form = $("#btnsave").closest("form").get(0);
                    return ValiDatorForm.validator(form, "parent");
                },
                save:function(){
                    var orderId=$("#<%=hidorderID.ClientID %>").val();
                    if(OrderInfo.CheckForm()){
                        $.newAjax({
                            type: "post",
                            cache: false,
                            url: "OrderInfo.aspx?AjaxType=SaveData&OrderId="+orderId+"&sl="+OrderInfo.data.sl,
                            dataType: "json",
                            data:$("#btnsave").closest("form").serialize(),
                            async:false,
                            success: function(ret) {
                                if(ret.result)
                                {
                                    tableToolbar._showMsg(ret.msg,function(){
                                        if(OrderInfo.data.ReturnUrl!="")
                                            window.location.href=decodeURIComponent(OrderInfo.data.ReturnUrl);
                                        else
                                            window.history.go(-1);
                                    });
                                }
                            },
                            error: function() {
                                if(arguments[1]!=null)
                                    tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                                else
                                    tableToolbar._showMsg("服务器忙");
                         }
                    });
                }
             },
             Into:function(){
                $("#btnsave").click(function(){
                    OrderInfo.save();
                    return false;    
                })            
             }
        }
        
        $(function(){
            OrderInfo.Into();       
        })
        
    </script>

    </form>
</asp:Content>
