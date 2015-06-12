<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TravellerInsurance.aspx.cs"
    Inherits="EyouSoft.Web.GroupEnd.Suppliers.TravellerInsurance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="text/html; charset=gb2312" http-equiv="Content-Type" />
    <title>游客保险信息</title>
    <link type="text/css" rel="stylesheet" href="/Css/style.css" />

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>
    <style type="text/css">
        .bumenbox
        {
            line-height: 200%;
        }
        .bumenbox a
        {
            color: #000000;
        }
        .bumenbox a:hover
        {
            color: #FF0000;
        }
    </style>
</head>
<body style="background: 0 none;">
    <form id="Form1" runat="server">
    <asp:HiddenField runat="server" ID="TravellerId" />
    </form>
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" class="alertboxbk2" border="0"
            style="margin: 0 auto; border-collapse: collapse;">
            <tbody>
                <tr>
                    <td align="center">
                        <span style="font-size: 14px;">【购买保险操作】</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="99%" cellspacing="1" cellpadding="0" border="0" bgcolor="#FFFFFF" style="margin: 0 auto;">
                            <tbody>
                                <tr style="background: url(../images/y-formykinfo.gif) repeat-x center top;">
                                    <td height="23" width="20%" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                        是否购买
                                    </td>
                                    <td class="alertboxTableT" bgcolor="#B7E0F3" align="center">
                                        所有游客批量购买此保险
                                    </td>
                                    <td bgcolor="#B7E0F3" align="left" class="alertboxTableT">
                                        保险名称
                                    </td>
                                    <td bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                                        单价
                                    </td>
                                    <td bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                                        购买份数
                                    </td>
                                    <td bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                                        小计金额
                                    </td>
                                </tr>
                                <asp:Repeater ID="RpInsurance" runat="server">
                                    <ItemTemplate>
                                        <tr data-id="<%#Eval("InsuranceId") %>">
                                            <td height="28" bgcolor="#E9F4F9" align="center" class="bottomline">
                                                <input type="checkbox" id="checkbox" name="checkbox">
                                            </td>
                                            <td class="bottomline" bgcolor="#E9F4F9" align="center">
                                                <input id="checkbox1" type="checkbox" name="checkbox2">
                                            </td>
                                            <td bgcolor="#E9F4F9" align="left" class="bottomline">
                                                <%#Eval("InsuranceName")%>
                                            </td>
                                            <td bgcolor="#E9F4F9" align="right" class="bottomline">
                                                <input id="txtUnitPrice" type="text" class="formsize80 bk" value="<%#Eval("UnitPrice") %>"">
                                            </td>
                                            <td bgcolor="#E9F4F9" align="right" class="bottomline">
                                                <input id="txtBuyNum" type="text" class="formsize40 bk" value="0">
                                            </td>
                                            <td bgcolor="#E9F4F9" align="right" class="bottomline">
                                                <input id="txtSumUnitPrice" type="text" class="formsize80 bk" value="0">
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td height="28" bgcolor="#E9F4F9" align="right" class="bottomline" colspan="5">
                                        <strong>合计金额</strong>
                                    </td>
                                    <td bgcolor="#E9F4F9" align="right" class="bottomline">
                                        <input id="txtSumPrice" type="text" class="formsize80 bk" name="text" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

    <script type="text/javascript">
        $(function() {
            var id = $("#TravellerId").val();
            if (id) {
                var data = {
                    Type: "Do",
                    TravellerId: id
                };
                $.newAjax({
                    url: "/GroupEnd/Suppliers/TravellerInsurance.aspx?" + $.param(data),
                    type: "post",
                    dataType: "json",
                    success: function(back) {
                        if (back) {
                            var sum = 0;
                            $.each(back, function(i) {
                                $("#txtSumPrice").val(back[i].SumPrice);
                                var InsuranceId = back[i].InsuranceId;
                                $("input[name='checkbox']").each(function() {
                                    if ($(this).closest("tr").attr("data-id") == InsuranceId) {
                                        $(this).attr("checked", "checked");
                                        var tr = $(this).closest("tr");
                                        tr.find("#txtUnitPrice").val(back[i].UnitPrice);
                                        tr.find("#txtBuyNum").val(back[i].BuyNum);
                                        var sumUnitPrice = back[i].UnitPrice * back[i].BuyNum;
                                        tr.find("#txtSumUnitPrice").val(sumUnitPrice);
                                        sum += sumUnitPrice;
                                    }

                                });
                            });
                            $("#txtSumPrice").val(sum);
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg("服务器忙！");
                    }
                });
            }
        });
        
    </script>

</body>
</html>
