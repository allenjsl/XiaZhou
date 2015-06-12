<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectPeoples.aspx.cs" Inherits="Web.CommonPage.SelectPeoples" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

</head>
<body>
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" bordercolor="#85C1DD" border="1"
            style="margin: 0 auto; border-collapse: collapse;">
            <tbody>
                <tr>
                    <td align="center">
                        <span style="font-size: 14px;">【选择销售员】</span>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#B7E0F3" class="bumenbox">
                        <b>所有部门</b>： <a href="#">计调部</a> | <a href="#">地联部</a> | <a href="#">网络部</a> | <a
                            href="#">总经理办公室</a> | <a href="#">营销部</a> | <a href="#">组团部</a> | <a href="#">西门店</a>
                        | <a href="#">导游部</a> | <a href="#">事业发展部</a> | <a href="#">财务部</a> | <a href="#">总经理室</a>
                        | <a href="#">同业部</a> | <a href="#">市场营销中心</a> | <a href="#">城区营业部</a> | <a href="#">
                            客户服务部</a> | <a href="#">百易会展公司</a> | <a href="#">呼叫中心</a> | <a href="#">县市营业部</a>
                        | <a href="#">形象店</a> | <a href="#">柳叶湖国旅</a> | <a href="#">临澧店</a> | <a href="#">市场部</a>
                        | <a href="#">东门店</a>
                    </td>
                </tr>
                <tr>
                    <td height="30">
                        <form id="form1" method="get">
                        姓名：
                        <input type="text" name="userName" class="formsize80 bk" name="textfield4" value='<%=Request.QueryString["userName"] %>' />
                        <input type="hidden" name="hideID" id="hideID" value='<%=Request.QueryString["hideID"] %>' />
                        <input type="hidden" name="showID" id="showID" value='<%=Request.QueryString["showID"] %>' />
                        <input type="hidden" name="sModel" id="sModel" value='<%=Request.QueryString["sModel"] %>' />
                        <button style="width: 64px; height: 24px; background: url(/images/cx.gif) no-repeat center center;
                            border: 0 none; margin-left: 5px;" type="submit">
                            查 询</button>
                        </form>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tblList" width="100%" cellspacing="0" cellpadding="0" bordercolor="#B8C5CE"
                            border="1" bgcolor="#FFFFFF" style="border-collapse: collapse; margin: 5px 0;">
                            <tbody>
                                <tr>
                                    <asp:Repeater ID="rptList" runat="server">
                                        <ItemTemplate>
                                            <td align="left">
                                                <input type="checkbox" value="<%#Eval("UserId")%>" />
                                                <span>
                                                    <%#Eval("ContactName")%></span>
                                                <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex, listCount, 4)%>
                                        </ItemTemplate>
                                    </asp:Repeater>
                            </tbody>
                        </table>
                        <div style="width: 100%; text-align: center; background-color: #ffffff">
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="position: relative; height: 20px;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="alertbox-btn">
            <a id="a_btn" hidefocus="true" href="javascript:void(0);"><s class="xuanzhe"></s>选 择</a></div>
    </div>

    <script type="text/javascript">
        $(function() {
            $("#a_btn").click(function() {

                OrderSellsPage.SetValue();
                var data = {
                    callBack: Boxy.queryString("callBack"),
                    hideID: Boxy.queryString("hideID"),
                    iframeID: '<%=Request.QueryString["iframeId"]%>',
                    pIframeID: '<%=Request.QueryString["pIframeId"]%>',
                    showID: Boxy.queryString("showID")
                }
                //根据父级是否为弹窗传值
                if (data.pIframeID) {
                    //定义父级弹窗

                    var boxyParent = window.parent.Boxy.getIframeDocument(data.pIframeID);
                    boxyParent.getElementById(data.hideID).value = $.trim(OrderSellsPage.selectValue);
                    boxyParent.getElementById(data.showID).value = $.trim(OrderSellsPage.selectTxt);
                    //定义回调
                }
                else {

                    parent.document.getElementById(data.hideID).value = $.trim(OrderSellsPage.selectValue);
                    parent.document.getElementById(data.showID).value = $.trim(OrderSellsPage.selectTxt);
                    //定义回调
                }
                parent.Boxy.getIframeDialog(data.iframeID).hide();
                return false;
            })

            $("#tblList").find("input[type='checkbox']").click(function() {
                if ($("#sModel").val() != "2") {
                    $("#tblList").find("input[type='checkbox']").attr("checked", "");
                    $(this).attr("checked", "checked");
                }
            })

            var OrderSellsPage = {
                selectValue: "",
                selectTxt: "",
                SetValue: function() {
                    var valueArray = new Array();
                    var txtArray = new Array();
                    $("#tblList").find("input[type='checkbox']:checked").each(function() {
                        valueArray.push($(this).val());
                        txtArray.push($(this).next().html());
                    })
                    OrderSellsPage.selectValue = valueArray.join(',');
                    OrderSellsPage.selectTxt = txtArray.join(',');
                }
            }
        })
    </script>

</body>
</html>