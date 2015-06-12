<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderInsurance.aspx.cs"
    Inherits="Web.SellCenter.Order.OrderInsurance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
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
                        <table width="99%" cellspacing="1" cellpadding="0" border="0" bgcolor="#FFFFFF" style="margin: 0 auto;
                            text-align: center" id="tblBaoXian">
                            <tbody>
                                <tr style="background: url(../images/y-formykinfo.gif) repeat-x center top;">
                                    <td height="23" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                        是否购买
                                    </td>
                                    <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                        所有游客批量购买此保险
                                    </td>
                                    <td bgcolor="#B7E0F3" class="alertboxTableT">
                                        保险名称
                                    </td>
                                    <td bgcolor="#B7E0F3" class="alertboxTableT">
                                        单价
                                    </td>
                                    <td bgcolor="#B7E0F3" class="alertboxTableT">
                                        购买份数
                                    </td>
                                    <td bgcolor="#B7E0F3" class="alertboxTableT">
                                        小计金额
                                    </td>
                                </tr>
                                <asp:Repeater ID="rptList" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td height="28" bgcolor="#E9F4F9" align="center" class="bottomline">
                                                <input type="checkbox" name="cbxID" value="<%#Eval("InsuranceId")%>" />
                                            </td>
                                            <td bgcolor="#E9F4F9" align="center" class="bottomline">
                                                <input type="checkbox" name="cbxIsAll" value="<%#Eval("InsuranceId")%>" />
                                            </td>
                                            <td bgcolor="#E9F4F9" class="bottomline">
                                                <span>
                                                    <%#Eval("InsuranceName")%></span>
                                            </td>
                                            <td bgcolor="#E9F4F9" class="bottomline">
                                                <input type="text" name="txtPrice" value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroString( Eval("UnitPrice").ToString())%>"
                                                    class="inputtext formsize40" />
                                            </td>
                                            <td bgcolor="#E9F4F9" class="bottomline">
                                                <input type="text" name="txtCount" class="inputtext formsize40" />
                                            </td>
                                            <td bgcolor="#E9F4F9" class="bottomline">
                                                <input type="text" name="txtSum" class="inputtext formsize40" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td height="28" bgcolor="#E9F4F9" align="right" class="bottomline" colspan="5">
                                        <strong>合计金额</strong>
                                    </td>
                                    <td bgcolor="#E9F4F9" align="right" class="bottomline">
                                        <input type="text" value="" id="txtSumPrice" class="inputtext formsize40" name="text" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:void(0);" id="a_OK"><s class="xuanzhe"></s>选 择</a></div>
    </div>

    <script type="text/javascript">
        $(function() {
            $("#a_OK").click(function() {
                var data = { id: [], name: [], price: [], count: [] }
                $("input[name='cbxID']:checked").each(function() {
                    data.id.push($(this).val());
                    data.name.push($(this).closest("tr").find("span").html());
                    data.price.push($(this).closest("tr").find("input[name='txtPrice']").val());
                    data.count.push($(this).closest("tr").find("input[name='txtCount']").val());
                })

                parent.TravelControl.SetHideValue('<%=Request.QueryString["a_id"] %>', data.id.join(","), data.name.join(","), data.price.join(","), data.count.join(","));


                var moreData = { id: [], name: [], price: [], count: [] }
                $("input[name='cbxIsAll']:checked").each(function() {
                    moreData.id.push($(this).val());
                    moreData.name.push($(this).closest("tr").find("span").html());
                    moreData.price.push($(this).closest("tr").find("input[name='txtPrice']").val());
                    moreData.count.push($(this).closest("tr").find("input[name='txtCount']").val());
                })
                if (moreData.id.length > 0) {
                    parent.TravelControl.SetAllValue(moreData);
                }
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeid"] %>').hide();

            })

            $("#tblBaoXian").find("input[type='checkbox']").click(function() {
                if (this.checked) {
                    if ($.trim($(this).closest("tr").find("input[name='txtCount']").val()) == "") {
                        $(this).closest("tr").find("input[name='txtCount']").val("1");
                        $(this).closest("tr").find("input[name='txtCount']").blur();
                    }
                }
            })

            DataSet();

            $("#tblBaoXian").find("input[type='text']").blur(function() {
                var tr = $(this).closest("tr");
                tr.find("input[name='txtSum']").val(tableToolbar.calculate(tr.find("input[name='txtPrice']").val(), tr.find("input[name='txtCount']").val(), "*"));
                var sumPrice = 0;
                $("#tblBaoXian").find("input[name='txtSum']").each(function() {
                    sumPrice = tableToolbar.calculate(sumPrice, $(this).val(), "+");
                })
                $("#txtSumPrice").val(sumPrice);
            })
        })

        function DataSet() {
            var id = '<%=Request.QueryString["id"] %>';
            var price = '<%=Request.QueryString["price"] %>';
            var count = '<%=Request.QueryString["count"] %>';
            if (id != "") {
                if (id.split(",").length > 1) {
                    var idList = id.split(",");
                    var priceList = price.split(",");
                    var countList = count.split(",");
                    for (var i = 0; i < idList.length; i++) {
                        $("input[name='cbxID']").each(function() {
                            if ($(this).val() == idList[i]) {
                                $(this).attr("checked", "checked");
                                $(this).closest("tr").find("input[name='txtPrice']").val(tableToolbar.getFloat(priceList[i]));
                                $(this).closest("tr").find("input[name='txtCount']").val(tableToolbar.getInt(countList[i]));
                                $(this).closest("tr").find("input[name='txtSum']").val(tableToolbar.calculate(priceList[i], countList[i], "*"));
                            }
                        })
                    }

                } else {
                    $("input[name='cbxID']").each(function() {
                        if ($(this).val() == id) {
                            $(this).attr("checked", "checked");
                            $(this).closest("tr").find("input[name='txtPrice']").val(tableToolbar.getFloat(price));
                            $(this).closest("tr").find("input[name='txtCount']").val(tableToolbar.getInt(count));
                            $(this).closest("tr").find("input[name='txtSum']").val(tableToolbar.calculate(price, count, "*"));
                        }
                    })
                }
            }

            var sumPrice = 0;
            $("#tblBaoXian").find("input[name='txtSum']").each(function() {
                sumPrice = tableToolbar.calculate(sumPrice, $(this).val(), "+");
            })
            $("#txtSumPrice").val(sumPrice);
        }
    </script>

    </form>
</body>
</html>
