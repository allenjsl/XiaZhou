<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderBaoZhang.aspx.cs"
    Inherits="Web.CommonPage.OrderBaoZhang" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/boxynew.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: 0 none;">
    <form id="form1" method="post" runat="server">
    <div class="alertbox-outbox">
        <asp:HiddenField ID="hidorderID" runat="server" />
        <asp:HiddenField ID="hidTourID" runat="server" />
        <asp:HiddenField ID="hidTourCode" runat="server" />
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    订单号：
                </td>
                <td width="20%" align="left">
                    <asp:Literal ID="litOrderCode" runat="server"></asp:Literal>
                </td>
                <td width="10%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    客源单位：
                </td>
                <td width="20%">
                    <asp:Literal ID="litBuyCompanyName" runat="server"></asp:Literal>
                </td>
                <td width="10%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    联系人：
                </td>
                <td width="19%">
                    <asp:Literal ID="litDContactName" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    订单销售员：
                </td>
                <td align="left" bgcolor="#e0e9ef">
                    <asp:Literal ID="litSellerName" runat="server"></asp:Literal>
                </td>
                <td height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    下单人：
                </td>
                <td height="28" colspan="3" bgcolor="#e0e9ef">
                    <asp:Literal ID="litOperator" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    价格组成：
                </td>
                <td colspan="5">
                    成人单价 <strong>
                        <asp:Literal ID="litAdultPrice" runat="server"></asp:Literal></strong> * 成人数
                    <strong>
                        <asp:Literal ID="litAdults" runat="server"></asp:Literal></strong> + 儿童单价 <strong>
                            <asp:Literal ID="litChildPrice" runat="server"></asp:Literal></strong> *
                    儿童数 <strong>
                        <asp:Literal ID="litChilds" runat="server"></asp:Literal></strong>
                </td>
            </tr>            
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    增加费用：
                </td>
                <td colspan="5" bgcolor="#e0e9ef">
                    <asp:Literal ID="litSaleAddCost" runat="server"></asp:Literal><span style="margin-left: 80px;">备注：
                        <asp:Literal ID="litSaleAddCostRemark" runat="server"></asp:Literal>
                    </span>
                </td>
            </tr>
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    减少费用：
                </td>
                <td colspan="5">
                    <asp:Literal ID="litSaleReduceCost" runat="server"></asp:Literal><span style="margin-left: 80px;">备注：
                        <asp:Literal ID="litSaleReduceCostRemark" runat="server"></asp:Literal>
                    </span>
                </td>
            </tr>
            <tr bgcolor="#E0E9EF">
                <td width="11%" height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    合计金额：
                </td>
                <td colspan="5">
                    <asp:Literal ID="litSumPrice" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr bgcolor="#E0E9EF">                
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT" style="height:28px;">
                    订单备注：
                </td>
                <td colspan="5">
                    <asp:Literal ID="ltrDingDanBeiZhu" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr bgcolor="#E0E9EF">                
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT" style="height:28px;">
                    内部信息：
                </td>
                <td colspan="5">
                    <asp:Literal ID="ltrNeiBuXinXi" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    变更增加：
                </td>
                <td colspan="5" bgcolor="#e0e9ef">
                    <asp:Literal ID="litAddCost" runat="server"></asp:Literal><span style="margin-left: 80px;">备注：
                        <asp:Literal ID="litAddCostRemark" runat="server"></asp:Literal>
                    </span>
                </td>
            </tr>
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    变更减少：
                </td>
                <td colspan="5">
                    <asp:Literal ID="litReduceCost" runat="server"></asp:Literal><span style="margin-left: 80px;">备注：
                        <asp:Literal ID="litReduceCostRemark" runat="server"></asp:Literal>
                    </span>
                </td>
            </tr>
            <tr>
                <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    订单确认金额：
                </td>
                <td colspan="5">
                    <asp:Literal ID="litConfirmMoney" runat="server"></asp:Literal>
                    <asp:HiddenField ID="hidConfirmMoney" runat="server" />
                    <asp:HiddenField ID="hidConfirmMoneyStatus" Value="F" runat="server" />
                </td>
            </tr>
        </table>
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02">游客信息</span>
            <table width="99%" border="0" cellspacing="0" cellpadding="0" style="height: auto;
                zoom: 1;">
                <tr style="background: url(../images/y-formykinfo.gif) repeat-x center top;">
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        姓名
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        类型
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        证件类型
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        证件号码
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        性别
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        联系方式
                    </td>
                    <asp:PlaceHolder ID="PHchujingtitle" runat="server" Visible="false">
                        <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                            有效期
                        </td>
                        <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                            办理
                        </td>
                        <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                            签证状态
                        </td>
                    </asp:PlaceHolder>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        备注
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        短信通知
                    </td>
                </tr>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td height="28" align="center">
                                <%#Eval("CnName")%><br />
                                <%#Eval("EnName")%>
                            </td>
                            <td align="center">
                                <%#Eval("VisitorType")%>
                            </td>
                            <td align="center">
                                <%#Eval("CardType") %>
                            </td>
                            <td align="center">
                                <%#Eval("CardNumber")%>
                            </td>
                            <td align="center">
                                <%#Eval("Gender") %>
                            </td>
                            <td align="center">
                                <%#Eval("Contact")%>
                            </td>
                            <asp:PlaceHolder ID="PHchujing" runat="server" Visible="<%#IsShow %>">
                                <td align="center">
                                    <%#Eval("CardValidDate") %>
                                </td>
                                <td align="center">
                                    <%#(bool)Eval("IsCardTransact")?"是":"否" %>
                                </td>
                                <td align="center">
                                    <%#Eval("VisaStatus")%>
                                </td>
                            </asp:PlaceHolder>
                            <td align="center">
                                <%#Eval("Remark")%>
                            </td>
                            <td align="center">
                                <%#(bool)Eval("LNotice") == true ? "出团" : ""%>&nbsp&nbsp
                                <%#(bool)Eval("RNotice") == true ? "回团" : ""%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <asp:PlaceHolder ID="tabOrderView" runat="server" Visible="true">
            <div style="margin: 0 auto; width: 99%;">
                <span class="formtableT formtableT02">订单结算信息</span>
                <table width="99%" border="0" cellspacing="0" cellpadding="0" style="height: auto;
                    zoom: 1; overflow: hidden;">
                    <tr>
                        <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                            订单结算价：
                        </td>
                        <td width="79%" bgcolor="#e0e9ef">
                            成人单价
                            <asp:TextBox ID="txtAdultPrice" runat="server" class="formsize50" Enabled="false"></asp:TextBox>
                            * 成人数
                            <asp:TextBox ID="txtAdults" runat="server" class="formsize40" Enabled="false"></asp:TextBox>
                            + 儿童单价
                            <asp:TextBox ID="txtChildPrice" runat="server" class="formsize50" Enabled="false"></asp:TextBox>
                            * 儿童数
                            <asp:TextBox ID="txtChilds" runat="server" class="formsize40" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                            增加费用：
                        </td>
                        <td width="79%" bgcolor="#e0e9ef">
                            <asp:TextBox ID="txtAddCost" runat="server" class="formsize80" valid="required|isMoney"
                                errmsg="增加费用不能为空!|增加费用格式不正确!" onchange="getData();"></asp:TextBox>
                            <span style="margin-left: 80px;">备注：
                                <asp:TextBox ID="txtAddCostRemark" runat="server" class="inputtext formsize450"></asp:TextBox>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                            减少费用：
                        </td>
                        <td>
                            <asp:TextBox ID="txtReduceCost" runat="server" class="formsize80" valid="required|isMoney"
                                errmsg="减少费用不能为空!|减少费用格式不正确!" onchange="getData();"></asp:TextBox>
                            <span style="margin-left: 80px;">备注：
                                <asp:TextBox ID="txtReduceCostRemark" runat="server" class="inputtext formsize450"></asp:TextBox>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td width="11%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                            结算金额：
                        </td>
                        <td>
                            <asp:TextBox ID="txtSettlementMoney" runat="server" class="formsize80" valid="required|IsDecimalTwo"
                                errmsg="结算金额不能为空!|结算金额格式不正确!" ReadOnly="true" BackColor="#dadada" onchange="getDatas();"></asp:TextBox>
                            元&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; 订单利润：
                            <asp:TextBox ID="txtProfit" runat="server" class="formsize80" Enabled="false"></asp:TextBox>
                            元
                        </td>
                    </tr>
                    <tr>
                        <td height="28" align="left"  colspan="2">
                            <asp:Literal runat="server" ID="ltrJieSuanXinXi"><span style="color:red">该订单结算信息暂未提交。</span></asp:Literal></td>
                    </tr>
                </table>
            </div>
        </asp:PlaceHolder>
        
        <div class="alertbox-btn">
            <asp:Literal runat="server" ID="ltrCaoZuoTiShi"></asp:Literal>
            <asp:PlaceHolder ID="phdSave" runat="server">
            <a href="javascript:void(0);" hidefocus="true" id="btnSave"><s class="baochun"></s>保存</a> 
            </asp:PlaceHolder>
            <a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();" hidefocus="true"><s class="chongzhi"></s>关 闭</a>
        </div>
        
    </div>
    </form>

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script type="text/javascript">
        var OrderBaoZhang = {
            Query: {/*URL参数对象*/
                sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                tourType: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourType") %>',
                OrderId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("OrderId") %>'
            },
            ResetForm: function() {
                document.getElementById('form1').reset();
            },
            Form: null,
            FormCheck: function() {
                if ($("#hidConfirmMoneyStatus").val() == "F") {
                    tableToolbar._showMsg("合同金额未确认！");
                    return false;
                }
                this.Form = $("#btnSave").closest("form").get(0);
                FV_onBlur.initValid(this.Form);
                return ValiDatorForm.validator(this.Form, "alert");
            },
            Save: function() {
                var that = this;
                if (that.FormCheck()) {
                    $("#btnSave").unbind("click");
                    $("#btnSave").addClass("alertbox-btn_a_active");
                    $("#btnSave").html("<s class=\"baochun\"></s> 提交中...");
                    var url = "/CommonPage/OrderBaoZhang.aspx?";
                    url += $.param({
                        doType: 'save',
                        OrderId: this.Query.OrderId,
                        sl: this.Query.sl
                    });
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: url,
                        data: $(that.Form).serialize().replace(),
                        dataType: "json",
                        success: function(result) {
                            if (result.result == 'True') {
                                tableToolbar._showMsg(result.msg, function() {
                                    parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                                    parent.window.location.reload();
                                });
                            }
                            else {
                                $("#btnSave").bind("click", function() {
                                    OrderBaoZhang.Save();
                                });
                                $("#btnSave").attr("class", "");
                                $("#btnSave").html("<s class=\"baochun\"></s>保 存");
                                tableToolbar._showMsg(result.msg);
                            }
                        },
                        error: function() {
                            //ajax异常--你懂得
                            tableToolbar._showMsg("服务器忙！");
                            OrderBaoZhang.BindBtn();
                        }
                    });
                }
            },
            BindBtn: function() {
                $("#btnSave").click(function() {
                    OrderBaoZhang.Save();
                    return false;
                })
                $("#btnSave").attr("class", "");
                $("#btnSave").html("<s class=\"baochun\"></s>保 存");
            }
        }
        $(function() {
            OrderBaoZhang.BindBtn();
        })

        //验证
        function CheckAddReduce() {
            var errorMsg = "";
            var add = $("#txtAddCost").val().replace(/\s+/g, "");
            var cost = $("#txtReduceCost").val().replace(/\s+/g, "");
            if (add == "" && cost == "") {
                errorMsg = "增加费用或减少费用不能同时为空！";
            }
            return errorMsg;
        }

        //自动计算
        function getData() {
            var AdultPrice = parseFloat($("#txtAdultPrice").val()) || 0;
            var Adults = parseInt($("#txtAdults").val()) || 0;
            var ChildPrice = parseFloat($("#txtChildPrice").val()) || 0;
            var Childs = parseInt($("#txtChilds").val()) || 0;
            var AddCost = parseFloat($("#txtAddCost").val()) || 0;
            var ReduceCost = parseFloat($("#txtReduceCost").val()) || 0;
            var ConfirmMoney = parseFloat($("#hidConfirmMoney").val()) || 0;
            var SettlementMoney = parseFloat(AdultPrice) * parseInt(Adults) + parseFloat(ChildPrice) * parseInt(Childs) + parseFloat(AddCost) - parseFloat(ReduceCost);
            var Profit = parseFloat(ConfirmMoney) - parseFloat(SettlementMoney);
            $("#txtSettlementMoney").val(SettlementMoney);
            $("#txtProfit").val(Profit);
        }
        //自动计算
        function getDatas() {
            var ConfirmMoney = parseFloat($("#hidConfirmMoney").val()) || 0;
            var SettlementMoney = parseFloat($("#txtSettlementMoney").val()) || 0;
            var Profit = parseFloat(ConfirmMoney) - parseFloat(SettlementMoney);
            $("#txtProfit").val(Profit);
        }
    </script>

</body>
</html>
