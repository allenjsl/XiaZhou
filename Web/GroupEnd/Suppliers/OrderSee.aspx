<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderSee.aspx.cs" Inherits="EyouSoft.Web.GroupEnd.Suppliers.OrderSee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="text/html; charset=gb2312" http-equiv="Content-Type" />
    <title>订单信息</title>
    <link type="text/css" rel="stylesheet" href="/Css/style.css" />
    <link type="text/css" rel="stylesheet" href="/Css/boxynew.css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/js/table-toolbar.js"></script>

    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%;">
            <!--<span class="formtableT formtableT02">线路信息</span>-->
            <form id="Form1" runat="server">
            <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9">
                <tbody>
                    <tr>
                        <td width="14%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            订单号：
                        </td>
                        <td width="26%" align="left">
                            <asp:Literal ID="LtOrderCode" runat="server"></asp:Literal>
                        </td>
                        <td width="12%" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                            订单销售员：
                        </td>
                        <td width="18%">
                            <asp:Literal ID="LtSellerName" runat="server"></asp:Literal>
                        </td>
                        <td width="12%" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                            下单人：
                        </td>
                        <td width="18%">
                            <asp:Literal ID="LtOperator" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            人数：
                        </td>
                        <td align="left">
                            <img width="16" height="15" style="vertical-align: middle" src="/Images/chengren.gif" />成人
                            <asp:Literal ID="LtAdults" runat="server"></asp:Literal>
                            &nbsp;
                            <img style="vertical-align: middle" src="/Images/child.gif" />
                            儿童
                            <asp:Literal ID="LtChilds" runat="server"></asp:Literal>
                        </td>
                        <td height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                            价格组成：
                        </td>
                        <td height="28" colspan="3">
                            成人<span class="fontred"><asp:Literal ID="LtAdultPrice" runat="server"></asp:Literal></span>，儿童<span
                                class="fontred"><asp:Literal ID="LtChildPrice" runat="server" /></span>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            增加费用：
                        </td>
                        <td align="left">
                            <asp:Literal ID="LtSaleAddCost" runat="server"></asp:Literal>
                        </td>
                        <td height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                            备注：
                        </td>
                        <td height="28" colspan="3">
                            <asp:Literal ID="LtSaleAddCostRemark" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            减少费用：
                        </td>
                        <td height="28" align="left">
                            <asp:Literal ID="LtSaleReduceCost" runat="server"></asp:Literal>
                        </td>
                        <td height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                            备注：
                        </td>
                        <td height="28" align="left" colspan="3">
                            <asp:Literal ID="LtSaleReduceCostRemark" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            合计金额：
                        </td>
                        <td height="28" align="left">
                            <strong class="fontred">
                                <asp:Literal ID="LtSumPrice" runat="server"></asp:Literal></strong>
                        </td>
                        <td height="28" bgcolor="#B7E0F3" align="right">
                            导游现收：
                        </td>
                        <td height="28" align="left">
                            <strong class="fontblue">
                                <asp:Literal ID="LtGuideIncome" runat="server"></asp:Literal></strong>
                        </td>
                        <td height="28" bgcolor="#B7E0F3" align="right">
                            销售应收：
                        </td>
                        <td height="28" align="left">
                            <strong>
                                <asp:Literal ID="LtSalerIncome" runat="server"></asp:Literal></strong>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            截止预留时间：
                        </td>
                        <td height="28" align="left" colspan="5">
                            <asp:TextBox ID="txtSaveSeatDate" runat="server" class="inputtext formsize100" Width="160px"></asp:TextBox>
                            <a class="timesicon" href="javascript:void(0);" onclick="WdatePicker({el:'<%=txtSaveSeatDate.ClientID %>',dateFmt:'yyyy-MM-dd HH:mm',maxDate:'<%=this.MaxDateTime %>',minDate:'<%= DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm")%>'});">
                                预留截止时间</a>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            订单备注：
                        </td>
                        <td height="28" align="left" colspan="5">
                            <asp:Literal ID="LtOrderRemark" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:HiddenField ID="hfTourId" runat="server" />
            <asp:HiddenField ID="hfAdults" runat="server" />
            <asp:HiddenField ID="hfChilds" runat="server" />
            <asp:HiddenField ID="hfConfirmSettlementMoney" runat="server" />
            <asp:HiddenField ID="hfLDate" runat="server" />
            <asp:HiddenField ID="hfRDate" runat="server" />
            </form>
        </div>
        <div class="hr_10">
        </div>
        <asp:PlaceHolder runat="server" ID="phTraveller">
            <div style="margin: 0 auto; width: 99%;">
                <span class="formtableT formtableT02">游客信息</span>
                <table width="99%" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr style="background: url(../images/y-formykinfo.gif) repeat-x center top;">
                            <td width="30" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                序号
                            </td>
                            <td height="23" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                姓名
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                类型
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                证件类型
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                证件号码
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                性别
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                联系方式
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                保险
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                备注
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                短信通知
                            </td>
                        </tr>
                        <asp:Repeater runat="server" ID="RpTravller">
                            <ItemTemplate>
                                <tr>
                                    <td align="center">
                                        <%#Container.ItemIndex+1 %>
                                    </td>
                                    <td height="28" align="center">
                                        <%#Eval("CnName") == null ? Eval("EnName") : Eval("CnName")%>
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
                                        <%#Eval("Gender")%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("Contact")%>
                                    </td>
                                    <td align="center">
                                        <a class="link2" href="javascript:void(0);" data-type='<%#Eval("IsInsurance") %>'
                                            data-id='<%#Eval("TravellerId") %>'>
                                            <img width="13" height="9" src='<%#Convert.ToBoolean(Eval("IsInsurance")) == true ? "/Images/y-duihao.gif" : "/Images/y-cuohao.gif"%>' /></a>
                                    </td>
                                    <td align="center">
                                        <%#Eval("Remark")%>
                                    </td>
                                    <td align="center">
                                        <%#Convert.ToBoolean(Eval("LNotice"))==true?"出团":"" %>
                                        <%#Convert.ToBoolean(Eval("RNotice")) == true ? "回团" : ""%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </asp:PlaceHolder>
        <div class="alertbox-btn">
            <asp:PlaceHolder ID="PhDo" runat="server"><a style="text-indent: 4px;" hidefocus="true"
                href="javascript:void(0)" id="_aNotSure">不受理</a> <a style="text-indent: 4px;" hidefocus="true"
                    href="javascript:void(0)" id="_aSaveSeat">留 位</a> <a style="text-indent: 4px;" hidefocus="true"
                        href="javascript:void(0)" id="_aSure">成 交</a> </asp:PlaceHolder>
        </div>
    </div>

    <script type="text/javascript">
        var OrderDetial = {
            Data: {
                'sl': '<%=Request.QueryString["sl"] %>',
                'OrderId': '<%=Request.QueryString["OrderId"] %>'
            },
            Update: function(id, saveSeatDate) {
                OrderDetial.UnBindSave();
                var param = {
                    Type: "Do",
                    sl: OrderDetial.Data.sl,
                    TourId: $("#<%=this.hfTourId.ClientID %>").val(),
                    OrderId: OrderDetial.Data.OrderId,
                    ConfirmSettlementMoney: $("#<%=this.hfConfirmSettlementMoney.ClientID %>").val(),
                    Adults: $("#<%=this.hfAdults.ClientID %>").val(),
                    Childs: $("#<%=this.hfChilds.ClientID %>").val(),
                    LDate: $("#<%=this.hfLDate.ClientID %>").val(),
                    RDate: $("#<%=this.hfRDate.ClientID %>").val(),
                    Status: id,
                    SaveSeatDate: saveSeatDate
                };

                $.newAjax({
                    type: "post",
                    url: "/GroupEnd/Suppliers/OrderSee.aspx",
                    data: $.param(param),
                    dataType: "json",
                    success: function(data) {
                        if (data.result == "1") {
                            tableToolbar._showMsg(data.msg, function() {
                                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                                parent.window.location.reload();
                            });
                        }
                        else {
                            tableToolbar._showMsg(data.msg);
                            OrderDetial.Bind();
                        }

                    },
                    error: function() {
                        tableToolbar._showMsg("服务器忙！");
                        OrderDetial.Bind();
                    }
                });

            },
            UnBindSave: function() {
                $("#_aNotSure").unbind("click");
                $("#_aSure").unbind("click");
                $("#_aSaveSeat").unbind("click");
            },
            Bind: function() {
                $("#_aNotSure").bind("click").html("不受理");
                $("#_aSure").bind("click").html("成 交");
                $("#_aSaveSeat").bind("click").html("留 位");
            },
            PageInit: function() {
                //留位时间
                //onfocus = "WdatePicker({dateFmt:'yyyy-MM-dd HH:mm',maxDate:'<%=this.MaxDateTime %>',minDate:'<%=this.MinDateTime %>'})"
                $("#<%=txtSaveSeatDate.ClientID %>").attr("onfocus", "WdatePicker({dateFmt:'yyyy-MM-dd HH:mm',maxDate:'<%=this.MaxDateTime %>',minDate:'<%=this.MinDateTime %>'})");
                //不受理
                $("#_aNotSure").click(function() {
                    $(this).html("提交中...");
                    OrderDetial.Update("<%=(int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.不受理%>", "");
                });

                //成交
                $("#_aSure").click(function() {
                    $(this).html("提交中...");
                    OrderDetial.Update("<%=(int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.已成交%>", "");
                });

                //留位
                $("#_aSaveSeat").click(function() {
                    if ($.trim($("#<%=txtSaveSeatDate.ClientID %>").val()) == "") {
                        tableToolbar._showMsg("请输入预留截止时间!");
                        return false;
                    } else {
                        $(this).html("提交中...");
                        OrderDetial.Update("<%=(int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.已留位%>", $("#<%=txtSaveSeatDate.ClientID %>").val());
                    }

                });

                $(".link2").click(function() {
                    var IsInsurance = $(this).attr("data-type");
                    if (IsInsurance == "True") {
                        var id = $(this).attr("data-id");
                        var data = {
                            sl: OrderDetial.Data.sl,
                            TravellerId: id
                        };
                        var url = "/GroupEnd/Suppliers/TravellerInsurance.aspx?" + $.param(data);

                        Boxy.iframeDialog({
                            iframeUrl: url,
                            title: "保险信息",
                            modal: true,
                            width: "1000px",
                            height: "285px"
                        });
                        return false;

                    }
                });
            }
        };

        $(function() {
            OrderDetial.PageInit();
        });

     
    </script>

</body>
</html>
