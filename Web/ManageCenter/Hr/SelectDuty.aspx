<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectDuty.aspx.cs" Inherits="Web.ManageCenter.Hr.SelectDuty" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox02">
        <table width="99%" border="0" cellpadding="0" cellspacing="0" class="alertboxbk2"
            style="margin: 0 auto; border-collapse: collapse;">
            <tr>
                <td align="center">
                    <span style="font-size: 14px;">【请选择<%=Request.QueryString["SetTitle"]%>】</span>
                    <input type="hidden" name="hideID" id="hideID" value='<%=Request.QueryString["hideID"] %>' />
                    <input type="hidden" name="showID" id="showID" value='<%=Request.QueryString["showID"] %>' />
                    <input type="hidden" name="sModel" id="sModel" value='<%=Request.QueryString["sModel"] %>' />
                    <input type="hidden" name="pIframeId" id="pIframeId" value='<%=Request.QueryString["pIframeId"] %>' />
                </td>
            </tr>
            <tr>
                <td align="left">
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="alertboxbk1"
                        id="tblList" bgcolor="#FFFFFF" style="border-collapse: collapse; margin: 5px 0;">
                        <tr>
                            <td>
                                <ul style="width: 100%;">
                                    <asp:Repeater ID="RepList" runat="server">
                                        <ItemTemplate>
                                            <li style="float: left; width: 25%; text-align: left; line-height: 20px;">
                                                <input id="select_<%#Container.ItemIndex+1 %>" type="checkbox" value="<%#Eval("PositionId")%>"
                                                    name="Operator3" />
                                                <input type="radio" id='select_<%#Container.ItemIndex+1%>' value="<%#Eval("PositionId")%>"
                                                    name="Operator3" />
                                                <label for="select_<%#Container.ItemIndex+1 %>">
                                                    <%#Eval("Title")%></label>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="position: relative; height: 20px;">
        </div>
        <div class="alertbox-btn" style="text-align: center;">
            <a href="javascript:void(0)" hidefocus="true" id="a_btn"><s class="xuanzhe"></s>选 择</a></div>
    </div>

    <script type="text/javascript">
        var SelectSectionPage = {
            selectValue: "",
            selectTxt: "",
            aid: '<%=Request.QueryString["id"] %>',
            parentWindow: null, //要赋值的页面的window对象
            iframeID: '<%=Request.QueryString["iframeId"]%>', //当前弹窗ID
            pIframeID: '<%=Request.QueryString["pIframeId"]%>', //父级弹窗ID
            SetValue: function() {
                var valueArray = new Array();
                var txtArray = new Array();
                $("#tblList").find("input[type='checkbox']:checked,input[type='radio']:checked").each(function() {
                    valueArray.push($.trim($(this).val()));
                    txtArray.push($.trim($(this).next().html()));
                })
                SelectSectionPage.selectValue = valueArray.join(',');
                SelectSectionPage.selectTxt = txtArray.join(',');
            },
            InitSetSelect: function() {
                var oldValue = SelectSectionPage.parentWindow.$('#' + SelectSectionPage.aid).parent().find("input[type='hidden']");
                if (oldValue.length > 0) {
                    var list = oldValue.val().split(',');
                    for (var i = 0; i < list.length; i++) {
                        $("input[name='Operator3'][value='" + list[i] + "']").attr("checked", "checked");
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
            if (SelectSectionPage.pIframeID) {
                SelectSectionPage.parentWindow = window.parent.Boxy.getIframeWindow(SelectSectionPage.pIframeID);
            }
            else {
                SelectSectionPage.parentWindow = parent.window;
            }
            SelectSectionPage.InitSetSelect();
            $("#a_btn").click(function() {
                SelectSectionPage.SetValue();

                var data = { id: '<%=Request.QueryString["id"] %>', value: SelectSectionPage.selectValue, text: SelectSectionPage.selectTxt, deptId: SelectSectionPage.selectDeptId, deptName: SelectSectionPage.selectDeptName, hide: '<%=Request.QueryString["hide"] %>', show: '<%=Request.QueryString["show"] %>' };
                var callBackFun = '<%=Request.QueryString["callBackFun"] %>'
                if (callBackFun.indexOf('.') == -1) {
                    SelectSectionPage.parentWindow[callBackFun](data);
                } else {
                    SelectSectionPage.parentWindow[callBackFun.split('.')[0]][callBackFun.split('.')[1]](data);
                }
                parent.Boxy.getIframeDialog(SelectSectionPage.iframeID).hide();
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
