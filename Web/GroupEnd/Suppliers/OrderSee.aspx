<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderSee.aspx.cs" Inherits="EyouSoft.Web.GroupEnd.Suppliers.OrderSee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="text/html; charset=gb2312" http-equiv="Content-Type" />
    <title>������Ϣ</title>
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
            <!--<span class="formtableT formtableT02">��·��Ϣ</span>-->
            <form id="Form1" runat="server">
            <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9">
                <tbody>
                    <tr>
                        <td width="14%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            �����ţ�
                        </td>
                        <td width="26%" align="left">
                            <asp:Literal ID="LtOrderCode" runat="server"></asp:Literal>
                        </td>
                        <td width="12%" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                            ��������Ա��
                        </td>
                        <td width="18%">
                            <asp:Literal ID="LtSellerName" runat="server"></asp:Literal>
                        </td>
                        <td width="12%" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                            �µ��ˣ�
                        </td>
                        <td width="18%">
                            <asp:Literal ID="LtOperator" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            ������
                        </td>
                        <td align="left">
                            <img width="16" height="15" style="vertical-align: middle" src="/Images/chengren.gif" />����
                            <asp:Literal ID="LtAdults" runat="server"></asp:Literal>
                            &nbsp;
                            <img style="vertical-align: middle" src="/Images/child.gif" />
                            ��ͯ
                            <asp:Literal ID="LtChilds" runat="server"></asp:Literal>
                        </td>
                        <td height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                            �۸���ɣ�
                        </td>
                        <td height="28" colspan="3">
                            ����<span class="fontred"><asp:Literal ID="LtAdultPrice" runat="server"></asp:Literal></span>����ͯ<span
                                class="fontred"><asp:Literal ID="LtChildPrice" runat="server" /></span>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            ���ӷ��ã�
                        </td>
                        <td align="left">
                            <asp:Literal ID="LtSaleAddCost" runat="server"></asp:Literal>
                        </td>
                        <td height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                            ��ע��
                        </td>
                        <td height="28" colspan="3">
                            <asp:Literal ID="LtSaleAddCostRemark" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            ���ٷ��ã�
                        </td>
                        <td height="28" align="left">
                            <asp:Literal ID="LtSaleReduceCost" runat="server"></asp:Literal>
                        </td>
                        <td height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                            ��ע��
                        </td>
                        <td height="28" align="left" colspan="3">
                            <asp:Literal ID="LtSaleReduceCostRemark" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            �ϼƽ�
                        </td>
                        <td height="28" align="left">
                            <strong class="fontred">
                                <asp:Literal ID="LtSumPrice" runat="server"></asp:Literal></strong>
                        </td>
                        <td height="28" bgcolor="#B7E0F3" align="right">
                            �������գ�
                        </td>
                        <td height="28" align="left">
                            <strong class="fontblue">
                                <asp:Literal ID="LtGuideIncome" runat="server"></asp:Literal></strong>
                        </td>
                        <td height="28" bgcolor="#B7E0F3" align="right">
                            ����Ӧ�գ�
                        </td>
                        <td height="28" align="left">
                            <strong>
                                <asp:Literal ID="LtSalerIncome" runat="server"></asp:Literal></strong>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            ��ֹԤ��ʱ�䣺
                        </td>
                        <td height="28" align="left" colspan="5">
                            <asp:TextBox ID="txtSaveSeatDate" runat="server" class="inputtext formsize100" Width="160px"></asp:TextBox>
                            <a class="timesicon" href="javascript:void(0);" onclick="WdatePicker({el:'<%=txtSaveSeatDate.ClientID %>',dateFmt:'yyyy-MM-dd HH:mm',maxDate:'<%=this.MaxDateTime %>',minDate:'<%= DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm")%>'});">
                                Ԥ����ֹʱ��</a>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            ������ע��
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
                <span class="formtableT formtableT02">�ο���Ϣ</span>
                <table width="99%" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr style="background: url(../images/y-formykinfo.gif) repeat-x center top;">
                            <td width="30" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                ���
                            </td>
                            <td height="23" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                ����
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                ����
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                ֤������
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                ֤������
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                �Ա�
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                ��ϵ��ʽ
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                ����
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                ��ע
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                ����֪ͨ
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
                                        <%#Convert.ToBoolean(Eval("LNotice"))==true?"����":"" %>
                                        <%#Convert.ToBoolean(Eval("RNotice")) == true ? "����" : ""%>
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
                href="javascript:void(0)" id="_aNotSure">������</a> <a style="text-indent: 4px;" hidefocus="true"
                    href="javascript:void(0)" id="_aSaveSeat">�� λ</a> <a style="text-indent: 4px;" hidefocus="true"
                        href="javascript:void(0)" id="_aSure">�� ��</a> </asp:PlaceHolder>
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
                        tableToolbar._showMsg("������æ��");
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
                $("#_aNotSure").bind("click").html("������");
                $("#_aSure").bind("click").html("�� ��");
                $("#_aSaveSeat").bind("click").html("�� λ");
            },
            PageInit: function() {
                //��λʱ��
                //onfocus = "WdatePicker({dateFmt:'yyyy-MM-dd HH:mm',maxDate:'<%=this.MaxDateTime %>',minDate:'<%=this.MinDateTime %>'})"
                $("#<%=txtSaveSeatDate.ClientID %>").attr("onfocus", "WdatePicker({dateFmt:'yyyy-MM-dd HH:mm',maxDate:'<%=this.MaxDateTime %>',minDate:'<%=this.MinDateTime %>'})");
                //������
                $("#_aNotSure").click(function() {
                    $(this).html("�ύ��...");
                    OrderDetial.Update("<%=(int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.������%>", "");
                });

                //�ɽ�
                $("#_aSure").click(function() {
                    $(this).html("�ύ��...");
                    OrderDetial.Update("<%=(int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.�ѳɽ�%>", "");
                });

                //��λ
                $("#_aSaveSeat").click(function() {
                    if ($.trim($("#<%=txtSaveSeatDate.ClientID %>").val()) == "") {
                        tableToolbar._showMsg("������Ԥ����ֹʱ��!");
                        return false;
                    } else {
                        $(this).html("�ύ��...");
                        OrderDetial.Update("<%=(int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.����λ%>", $("#<%=txtSaveSeatDate.ClientID %>").val());
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
                            title: "������Ϣ",
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
