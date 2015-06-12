<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HrSelect.aspx.cs" Inherits="Web.CommonPage.HrSelect" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

</head>
<body style="background: #e9f4f9;">
    <div class="alertbox-outbox02">
        <table width="99%" border="0" cellpadding="0" cellspacing="0" class="alertboxbk2"
            style="margin: 0 auto; border-collapse: collapse;">
            <tr>
                <td align="center">
                    <span style="font-size: 14px;">【请选择<%=Request.QueryString["setTitle"]%>】</span>
                </td>
            </tr>
            <tr>
                <td height="30">
                    <form id="form2" method="get">
                    姓名：
                    <input type="text" id="userName" name="userName" class="inputtext formsize80" name="textfield4"
                        value='<%=Request.QueryString["userName"] %>' />
                    <input type="hidden" name="sModel" id="sModel" value='<%=Request.QueryString["sModel"] %>' />
                    <button id="btnSubmit" style="width: 64px; height: 24px; background: url(/images/cx.gif) no-repeat center center;
                        border: 0 none; margin-left: 5px;" type="button">
                        查 询</button>
                    <asp:PlaceHolder runat="server" ID="ph_checkbox">&nbsp;&nbsp;已选择： <span id="selectedCheck">
                    </span></asp:PlaceHolder>
                    </form>
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
                                            <input type="checkbox" id='select_<%#Container.ItemIndex+1%>' name="contactID" value="<%#Eval("ID")%>" />
                                            <input type="radio" id='select_<%#Container.ItemIndex+1%>' value="<%#Eval("ID")%>"
                                                name="contactID" />
                                            <label data-deptid='<%#Eval("DepartId") %>' data-deptname='<%#Eval("DepartName") %>'
                                                for='select_<%#Container.ItemIndex+1%>'>
                                                <%#Eval("Name")%></label>
                                            <input type="hidden" name="hideUserId" value="<%#Eval("UserId") %>" />
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
        var SelectSectionPage = {
            selectValue: "",
            selectTxt: "",
            selectDeptId: "",
            selectDeptName: "",
            selectUserId: "",
            aid: '<%=Request.QueryString["id"] %>',
            parentWindow: null, //要赋值的页面的window对象
            iframeID: '<%=Request.QueryString["iframeId"]%>', //当前弹窗ID
            pIframeID: '<%=Request.QueryString["pIframeId"]%>', //父级弹窗ID
            SetValue: function() {
                var valueArray = new Array();
                var txtArray = new Array();
                var deptIdArray = new Array();
                var deptNameArray = new Array();
                var hideUserID = [];

                if ($("#sModel").val() == "2") {
                    $("#selectedCheck").find("input[type='checkbox']:checked").each(function() {
                        var _self = $(this);
                        valueArray.push($.trim(_self.val()));
                        txtArray.push($.trim(_self.next().html()));
                        deptIdArray.push($.trim(_self.next().attr("data-deptid")));
                        deptNameArray.push($.trim(_self.next().attr("data-deptname")));
                        hideUserID.push(_self.parent("tr").find("input[name='hideUserId']").val())
                    })
                    SelectSectionPage.selectValue = valueArray.join(',');
                    SelectSectionPage.selectTxt = txtArray.join(',');
                    SelectSectionPage.selectDeptId = deptIdArray.join(',');
                    SelectSectionPage.selectDeptName = deptNameArray.join(',');
                    SelectSectionPage.selectUserId = hideUserID.join(',');
                }
                else {
                    $("#tblList").find("input[type='radio']:checked").each(function() {
                    	var _self = $(this);
                    	//debug();
                        valueArray.push($.trim($(this).val()));
                        txtArray.push($.trim($(this).next().html()));
                        deptIdArray.push($.trim($(this).next().attr("data-deptid")));
                        deptNameArray.push($.trim($(this).next().attr("data-deptname")));
                    	hideUserID.push(_self.parent().find("input[name='hideUserId']").val())
                    })
                    SelectSectionPage.selectValue = valueArray.join(',');
                    SelectSectionPage.selectTxt = txtArray.join(',');
                    SelectSectionPage.selectDeptId = deptIdArray.join(',');
                    SelectSectionPage.selectDeptName = deptNameArray.join(',');
                	SelectSectionPage.selectUserId = hideUserID.join(',');
                }
            },
            InitSetSelect: function() {
                if (SelectSectionPage.aid) {
                    var parentSpan = SelectSectionPage.parentWindow.$('#' + SelectSectionPage.aid).parent();
                    var oldValue = "";
                    if (parentSpan.find("input[type='hidden']").length > 0) {
                        oldValue = parentSpan.find("input[type='hidden']").val().split(',');
                    }
                    var oldText = "";
                    if (parentSpan.find("input[type='text']").length > 0) {
                        oldText = parentSpan.find("input[type='text']").val().split(',');
                    }
                    var deptId = "", deptName = "";
                    if (parentSpan.find("span[data-class='hideDeptInfo']").attr("data-deptid").length > 0) {
                        deptId = SelectSectionPage.parentWindow.$("#" + parentSpan.find("span[data-class='hideDeptInfo']").attr("data-deptid")).val().split(',');
                    }
                    if (parentSpan.find("span[data-class='hideDeptInfo']").attr("data-deptname").length > 0) {
                        deptName = SelectSectionPage.parentWindow.$("#" + parentSpan.find("span[data-class='hideDeptInfo']").attr("data-deptname")).val().split(',');
                    }
                    var selected = SelectSectionPage.parentWindow["selected"];
                    if (selected && '<%=Request.QueryString["sModel"]%>' != '1') {
                        this.AppendCheckbox(selected.a, selected.b, selected.c, selected.d);
                        return false;
                    }
                    this.AppendCheckbox(oldValue, oldText, deptId, deptName);
                }
            },
            SearchFun: function(key) {
                var data = { id: '<%=Request.QueryString["id"] %>', iframeId: '<%=Request.QueryString["iframeId"]%>', pIframeId: '<%=Request.QueryString["pIframeId"]%>', callBackFun: '<%=Request.QueryString["callBackFun"] %>', sModel: '<%=Request.QueryString["sModel"]%>', isShow: '<%=Request.QueryString["isShow"] %>', setTitle: '<%=Request.QueryString["setTitle"]%>', userName: '' };
                data.userName = key;
                window.location.href = "/CommonPage/HrSelect.aspx?" + $.param(data);
            },
            AppendCheckbox: function(value, text, deptId, deptName) {
                var str = "";
                var selected = { a: [], b: [], c: [], d: [] };
                if (value.length > 0 && value.length == text.length) {
                    for (var i = 0; i < value.length; i++) {
                        if (value[i] != "") {
                            selected.a.push(value[i]);
                            selected.b.push(text[i]);
                            $("input[name='contactID'][value='" + value[i] + "']").attr("checked", "checked");
                            if ('<%=Request.QueryString["sModel"]%>' != '1') {
                                if (deptId && deptName && deptId.length == deptName.length && deptId.length >= i) {
                                    selected.c.push(deptId[i]);
                                    selected.d.push(deptName[i]);
                                    str += '<input id=cbx_' + i + ' checked="checked" type="checkbox" value=' +
                                    value[i] + ' name="contactID"><label for="cbx_' + i + '" data-deptid="' +
                                    deptId[i] + '"data-deptname=' + deptName[i] + ' >' + text[i] + '</label>';
                                }
                                else {
                                    selected.c.push("");
                                    selected.d.push("");
                                    str += '<input id=cbx_' + i + ' checked="checked" type="checkbox" value=' +
                                    value[i] + ' name="contactID"><label for="cbx_' + i + '" data-deptid=""data-deptname="" >' +
                                    text[i] + '</label>';
                                }
                            }
                        }
                    }
                    SelectSectionPage.parentWindow["selected"] = selected;
                }
                if (str != "") {
                    $("#selectedCheck").append(str);
                    this.BindCheckBoxClick();
                }
            },
            RemoveCheckbox: function(val) {
                var selected = SelectSectionPage.parentWindow["selected"]
                if (selected) {
                    for (var i = 0; i < selected.a.length; i++) {
                        if (selected.a[i] == val) {
                            selected.a.splice(i, 1);
                            selected.b.splice(i, 1);
                            selected.c.splice(i, 1);
                            selected.d.splice(i, 1);
                        }
                    }
                    SelectSectionPage.parentWindow["selected"] = selected;
                }
                //移除checkbox后边的label 及本身
                var self = $("#selectedCheck").find("input[type='checkbox'][value='" + val + "']");
                self.next().remove();
                self.remove();
            },
            BindCheckBoxClick: function() {
                $("#selectedCheck").find("input[type='checkbox']").unbind("click").click(function() {
                    if (this.value != "") {
                        $("input[name='contactID'][value='" + this.value + "']").attr("checked", "");
                        SelectSectionPage.RemoveCheckbox(this.value);
                    }
                })
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
                SelectSectionPage.parentWindow = window.parent.Boxy.getIframeWindow(SelectSectionPage.pIframeID) || window.parent.Boxy.getIframeWindowByID(SelectSectionPage.pIframeID);
            }
            else {
                SelectSectionPage.parentWindow = parent.window;
            }
            SelectSectionPage.InitSetSelect();

            $("#a_btn").click(function() {
                SelectSectionPage.SetValue();
                //根据父级是否为弹窗传值
                var data = { id: '<%=Request.QueryString["id"] %>', value: SelectSectionPage.selectValue, text: SelectSectionPage.selectTxt, deptId: SelectSectionPage.selectDeptId, deptName: SelectSectionPage.selectDeptName, uid: SelectSectionPage.selectUserId };
                var callBackFun = '<%=Request.QueryString["callBackFun"] %>'
                if (callBackFun.indexOf('.') == -1) {
                    SelectSectionPage.parentWindow[callBackFun](data);
                } else {
                    SelectSectionPage.parentWindow[callBackFun.split('.')[0]][callBackFun.split('.')[1]](data);
                }
                parent.Boxy.getIframeDialog(SelectSectionPage.iframeID).hide();
                return false;
            })
            if ('<%=Request.QueryString["sModel"]%>' != '1') {
                $("input[name='contactID']").click(function() {
                    var selected = SelectSectionPage.parentWindow["selected"];
                    if (this.checked) {
                        var a = new Array(), b = new Array(), c = new Array(), d = new Array();
                        a.push($.trim($(this).val()));
                        b.push($.trim($(this).next().html()));
                        c.push($(this).next().attr("data-deptid"));
                        d.push($(this).next().attr("data-deptname"));
                        if (selected) {
                            selected.a.push($.trim($(this).val()));
                            selected.b.push($.trim($(this).next().html()));
                            selected.c.push($(this).next().attr("data-deptid"));
                            selected.d.push($(this).next().attr("data-deptname"));
                        }
                        SelectSectionPage.AppendCheckbox(a, b, c, d);
                        SelectSectionPage.parentWindow["selected"] = selected;
                    } else {
                        var _s = this;
                        SelectSectionPage.RemoveCheckbox(_s.value);
                    }
                })
            }
            $("#btnSubmit").click(function() {
                SelectSectionPage.SearchFun($("#userName").val());
                return false;
            })
            //回车事件
            $("input[type='text'][name='userName']").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                    SelectSectionPage.SearchFun($("#userName").val());
                    return false;
                }
            });
        })
    </script>

</body>
</html>
