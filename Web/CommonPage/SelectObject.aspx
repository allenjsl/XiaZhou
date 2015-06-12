<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectObject.aspx.cs" Inherits="Web.CommonPage.SelectObject" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
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

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox02">
        <table width="99%" border="0" cellpadding="0" cellspacing="0" class="alertboxbk2"
            style="margin: 0 auto; border-collapse: collapse;">
            <tr>
                <td align="center">
                    <input type="hidden" name="sModel" id="sModel" value='<%=Request.QueryString["sModel"] %>' />
                </td>
            </tr>
            <tr>
                <td>
                    <table id="tblList" width="100%" cellspacing="0" cellpadding="0" class="alertboxbk1"
                        border="0" bgcolor="#FFFFFF" style="border-collapse: collapse; margin: 5px 0;">
                        <tbody>
                            <tr>
                                <asp:Repeater ID="RepList" runat="server">
                                    <ItemTemplate>
                                        <td align="left">
                                            <input type="checkbox" id='select_<%#Container.ItemIndex+1 %>' name="contactID" value="<%#Eval("GoodId")%>"
                                                data-nums="<%# Eval("Stock") %>" />
                                            <input type="radio" id='select_<%#Container.ItemIndex+1 %>' value="<%#Eval("GoodId")%>"
                                                data-price='<%#Eval("Price") %>' name="contactID" data-nums="<%# Eval("Stock") %>" />
                                            <input id="hidPrices" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("Price").ToString())%>"
                                                name="hidPrices" type="hidden" />
                                            <label for='select_<%#Container.ItemIndex+1 %>'>
                                                <%#Eval("Name")%></label>
                                            <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex, listCount, 4)%>
                                    </ItemTemplate>
                                </asp:Repeater>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
        <div style="position: relative; height: 20px;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0)" hidefocus="true" id="a_btn"><s class="xuanzhe"></s>选 择</a></div>
    </div>

    <script type="text/javascript">
        var SelectObjectPage = {
            selectValue: "",
            selectTxt: "",
            selectPrice: "",
            selectNums: "",
            aid: '<%=Request.QueryString["id"] %>',
            parentWindow: null, //要赋值的页面的window对象
            iframeID: '<%=Request.QueryString["iframeId"]%>', //当前弹窗ID
            pIframeID: '<%=Request.QueryString["pIframeId"]%>', //父级弹窗ID
            SetValue: function() {
                var valueArray = new Array();
                var txtArray = new Array();
                var priceArray = new Array();
                var numsArray = new Array();
                $("#tblList").find("input[type='checkbox']:checked,input[type='radio']:checked").each(function() {
                    valueArray.push($.trim($(this).val()));
                    txtArray.push($.trim($(this).parent().find("label").html()));
                    priceArray.push($.trim($(this).parent().find("input[type='hidden'][name='hidPrices']").val()));
                    numsArray.push($.trim($(this).attr("data-Nums")));
                })
                SelectObjectPage.selectValue = valueArray.join(',');
                SelectObjectPage.selectTxt = txtArray.join(',');
                SelectObjectPage.selectPrice = priceArray.join(',');
                SelectObjectPage.selectNums = numsArray.join(',');
            },
            InitSetSelect: function() {
                if (SelectObjectPage.aid) {
                    var oldValue = SelectObjectPage.parentWindow.$('#' + SelectObjectPage.aid).parent().find("input[type='hidden']");
                    if (oldValue.length > 0) {
                        var list = oldValue.val().split(',');
                        for (var i = 0; i < list.length; i++) {
                            $("input[name='contactID'][value='" + list[i] + "']").attr("checked", "checked");
                        }
                    }
                }
            }
        }
        $(function() {
            if ($("#sModel").val() == "2") {
                $("#tblList").find("input[type='radio']").remove();
            }
            else {
                $("#tblList").find("input[type='checkbox']").remove();
            }
            //判断是否为二级弹窗            
            if (SelectObjectPage.pIframeID) {
                SelectObjectPage.parentWindow = window.parent.Boxy.getIframeWindow(SelectObjectPage.pIframeID) || window.parent.Boxy.getIframeWindowByID(SelectObjectPage.pIframeID);
            }
            else {
                SelectObjectPage.parentWindow = parent.window;
            }
            SelectObjectPage.InitSetSelect();
            $("#a_btn").click(function() {
                SelectObjectPage.SetValue();

                //根据父级是否为弹窗传值

                var data = { id: '<%=Request.QueryString["id"] %>', value: SelectObjectPage.selectValue, text: SelectObjectPage.selectTxt, price: SelectObjectPage.selectPrice, nums: SelectObjectPage.selectNums };
                var callBackFun = '<%=Request.QueryString["callBackFun"] %>'
                if (callBackFun.indexOf('.') == -1) {
                    SelectObjectPage.parentWindow[callBackFun](data);
                } else {
                    SelectObjectPage.parentWindow[callBackFun.split('.')[0]][callBackFun.split('.')[1]](data);
                }
                parent.Boxy.getIframeDialog(SelectObjectPage.iframeID).hide();
                return false;
            })
        })
     
    </script>

</body>
</html>
