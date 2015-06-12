<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ObjLendList.aspx.cs" Inherits="Web.ManageCenter.ObjectManage.ObjLendList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <style type="text/css">
        .errmsg
        {
            color: #f00;
            font-size: 12px;
        }
    </style>
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div style="width: 99%; margin: 0 auto; text-align: left">
            <asp:PlaceHolder runat="server" ID="ph_back">
                <button class="btn_lendBack" style="width: 64px; height: 24px; background: url(/images/cx.gif) no-repeat center center;
                    border: 0 none; margin-left: 5px;" type="submit">
                    归还</button></asp:PlaceHolder>
        </div>
        <div class="hr_5">
        </div>
        <table width="98%" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
            style="margin: 0 auto" id="liststyle">
            <tr>
                <td width="5%" height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    <input type="checkbox" name="checkbox" />
                </td>
                <td width="11%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    借阅时间
                </td>
                <td width="10%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    借阅部门
                </td>
                <td width="10%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    借阅人
                </td>
                <td width="9%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    数量
                </td>
                <td width="47%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    用途说明
                </td>
                <td width="7%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    状态
                </td>
            </tr>
            <asp:Repeater runat="server" ID="rpt_List">
                <ItemTemplate>
                    <tr data-class="dataTr" data-status='<%#Eval("ReturnTime") %>'>
                        <td align="center" height="28">
                            <input type="checkbox" data-class="checkbox" name="checkbox" id="checkbox" value='<%#Eval("UseId") %>' /><%#Container.ItemIndex+1 %>
                        </td>
                        <td align="center">
                            <%#Eval("Time","{0:yyyy-MM-dd}") %>
                        </td>
                        <td align="center">
                            <%#Eval("Dept")%>
                        </td>
                        <td align="center">
                            <%#Eval("UserName") %>
                        </td>
                        <td align="center">
                            <%#Eval("Number")%>
                        </td>
                        <td align="left">
                            <%#Eval("Use")%>
                        </td>
                        <td align="center">
                            <%#Eval("ReturnTime")==null?"借阅中":"已归还"%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div class="pages">
            <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
        </div>
    </div>

    <script type="text/javascript">
        var ObjLendListPage = {
            Data: {
                sl: '<%=Request.QueryString["sl"] %>'
            },
            GoAjax: function(url) {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function(result) {
                        if (result.result == "1") {
                            parent.tableToolbar._showMsg(result.msg, function() {
                                window.location.reload();
                            });
                        }
                        else { parent.tableToolbar._showMsg(result.msg, function() { window.location.reload(); }); }
                    },
                    error: function() {
                        //ajax异常--你懂得
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            },
            LendBack: function(objsArr) {
                var list = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                }
                var url = "/ManageCenter/ObjectManage/ObjLendList.aspx?" + $.param({
                    sl: this.Data.sl,
                    ids: list.join(','),
                    doType: "lendback"
                });
                this.GoAjax(url);
            }
        }

        $(function() {
            tableToolbar.init({
                tableContainerSelector: "#liststyle", //表格选择器
                objectName: "借阅物品",
                otherButtons:
                    [{ button_selector: ".btn_lendBack",
                        sucessRulr: 2,
                        msg: '未选中任何借阅物品！',
                        buttonCallBack: function(objsArr) {
                            ObjLendListPage.LendBack(objsArr); return false;
                        } }]
            })
            $("tr[data-class='dataTr']").each(function() {
                if ($(this).attr("data-status") != "") {
                    $(this).find(":checkbox[name='checkbox']").remove();
                }
            })
            if ($(":checkbox[data-class='checkbox']").length == 0) {
                $(":checkbox[name='checkbox']").remove(); //.attr("disabled", "disabled");
                $(".btn_lendBack").attr("disabled", "disabled");
            }
        })
    </script>

</body>
</html>
