<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CuisineSelect.aspx.cs"
    Inherits="EyouSoft.Web.CommonPage.CuisineSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

</head>
<body style="background:#e9f4f9;">
    <div class="alertbox-outbox02">
        <table width="99%" border="0" cellpadding="0" cellspacing="0" class="alertboxbk2"
            style="margin: 0 auto; border-collapse: collapse;">
            <tr>
                <td align="center">
                    <span style="font-size: 14px;">【请选择菜系】</span>
                    <input type="hidden" name="hideID" id="hideID" value='<%=Request.QueryString["hideID"] %>' />
                    <input type="hidden" name="showID" id="showID" value='<%=Request.QueryString["showID"] %>' />
                    <input type="hidden" name="sModel" id="sModel" value='<%=Request.QueryString["sModel"] %>' />
                    <input type="hidden" name="pIframeId" id="pIframeId" value='<%=Request.QueryString["pIframeId"] %>' />
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
                                            <label><input type="checkbox" name="contactID" value="<%#Eval("Value")%>" />
                                               <span><%#Eval("Text")%></span> </label>
                                            <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex, listCount, 4)%>
                                    </ItemTemplate>
                                </asp:Repeater>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
        <div style="position: relative; height: 10px;">
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0)" hidefocus="true" id="a_btn"><s class="xuanzhe"></s>选 择</a></div>
    </div>

    <script type="text/javascript">
        var SelectCuisinePage = {
            selectValue: "",
            selectTxt: "",
            aid: '<%=Request.QueryString["id"] %>',
            parentWindow: null, //要赋值的页面的window对象
            iframeID: '<%=Request.QueryString["iframeId"]%>', //当前弹窗ID
            pIframeID: '<%=Request.QueryString["pIframeId"]%>', //父级弹窗ID
            SetValue: function() {
                var valueArray = new Array();
                var txtArray = new Array();
                $("#tblList").find("input[type='checkbox']:checked").each(function() {
                    valueArray.push($.trim($(this).val()));
                    txtArray.push($.trim($(this).next().html()));
                })
                SelectCuisinePage.selectValue = valueArray.join(',');
                SelectCuisinePage.selectTxt = txtArray.join(',');
            },
            InitSetSelect: function() {
                if (SelectCuisinePage.aid) {
                    var oldValue = SelectCuisinePage.parentWindow.$('#' + SelectCuisinePage.aid).parent().find("input[type='hidden']");
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
            //判断是否为二级弹窗
            if (SelectCuisinePage.pIframeID) {
                SelectCuisinePage.parentWindow = window.parent.Boxy.getIframeWindow(SelectCuisinePage.pIframeID);
            }
            else {
                SelectCuisinePage.parentWindow = parent.window;
            }
            SelectCuisinePage.InitSetSelect();
            $("#a_btn").click(function() {
                SelectCuisinePage.SetValue();

                //根据父级是否为弹窗传值
                var data = { id: '<%=Request.QueryString["id"] %>', value: SelectCuisinePage.selectValue, text: SelectCuisinePage.selectTxt, hide: '<%=Request.QueryString["hide"] %>', show: '<%=Request.QueryString["show"] %>' };
                var callBackFun = '<%=Request.QueryString["callBackFun"] %>';
                if (callBackFun.indexOf('.') == -1) {
                    SelectCuisinePage.parentWindow[callBackFun](data);
                } else {
                    SelectCuisinePage.parentWindow[callBackFun.split('.')[0]][callBackFun.split('.')[1]](data);
                }
                parent.Boxy.getIframeDialog(SelectCuisinePage.iframeID).hide();
                return false;
            })
            //判断是否只能选中一项
            $("#tblList").find("input[type='checkbox']").click(function() {
                if ($("#sModel").val() != "2") {
                    $("#tblList").find("input[type='checkbox']").attr("checked", "");
                    $(this).attr("checked", "checked");
                }
            })
        })
    </script>

</body>
</html>
