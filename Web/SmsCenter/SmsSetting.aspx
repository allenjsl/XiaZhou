<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/UserCenter.Master"
    CodeBehind="SmsSetting.aspx.cs" Inherits="Web.SysCenter.SmsSetting" %>

<%@ Register Src="~/UserControl/SysTop.ascx" TagName="SysTop" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="gerenzx-mainbox ">
        <form id="form1" runat="server">
        <uc1:SysTop ID="SmsTop" runat="Server"></uc1:SysTop>
        <div class="sms-addbox">
            <span class="formtableT" style="margin-left: 11px; *margin-left: 10px;">出团通知</span>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="alertboxbk3">
                <tr>
                    <th width="100" align="right" class="addtableT">
                        发送内容：
                    </th>
                    <td bgcolor="#FFFFFF">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    插入标签：<asp:DropDownList ID="ddlSelCTLabel" runat="server" CssClass=" inputselect">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCTContent" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"
                                        Height="80px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="addtableT">
                        发送时间：
                    </th>
                    <td bgcolor="#FFFFFF">
                        出团前
                        <asp:TextBox ID="txtCTDay" runat="server" MaxLength="3" CssClass="inputtext formsize40"></asp:TextBox>
                        天
                        <asp:TextBox ID="txtCTHour" runat="server" MaxLength="2" CssClass="inputtext formsize40"></asp:TextBox>
                        时发送
                    </td>
                </tr>
                <tr>
                    <th align="right" class="addtableT">
                        是否启用：
                    </th>
                    <td bgcolor="#FFFFFF">
                        <asp:CheckBox ID="chkCTState" runat="server" Text="启用" />
                    </td>
                </tr>
            </table>
            <table width="90%" border="0" cellspacing="0" cellpadding="0" style="margin: 5px auto;">
                <tr>
                    <td align="center">
                        <div class="cun-cy">
                            <a href="javascript:" id="btnCTSave">保 存</a></div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="sms-addbox">
            <span class="formtableT" style="margin-left: 11px; *margin-left: 10px;">回团通知</span>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="alertboxbk3">
                <tr>
                    <th width="100" align="right" class="addtableT">
                        发送内容：
                    </th>
                    <td bgcolor="#FFFFFF">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    插入标签：<asp:DropDownList ID="ddlSelHTLabel" runat="server" CssClass=" inputselect">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtHTContent" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"
                                        Height="80px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="addtableT">
                        发送时间：
                    </th>
                    <td bgcolor="#FFFFFF">
                        回团前
                        <asp:TextBox ID="txtHTDay" runat="server" MaxLength="3" CssClass="inputtext formsize40"></asp:TextBox>
                        天
                        <asp:TextBox ID="txtHTHour" runat="server" MaxLength="2" CssClass="inputtext formsize40"></asp:TextBox>
                        时发送
                    </td>
                </tr>
                <tr>
                    <th align="right" class="addtableT">
                        是否启用：
                    </th>
                    <td bgcolor="#FFFFFF">
                        <asp:CheckBox ID="chkHTState" runat="server" Text="启用" />
                    </td>
                </tr>
            </table>
            <table width="90%" border="0" cellspacing="0" cellpadding="0" style="margin: 5px auto;">
                <tr>
                    <td align="center">
                        <div class="cun-cy">
                            <a href="javascript:" id="btnHTSave">保 存</a></div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="sms-addbox">
            <span class="formtableT" style="margin-left: 11px; *margin-left: 10px;">生日提醒</span>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="alertboxbk3">
                <tr>
                    <th width="100" align="right" class="addtableT">
                        发送内容：
                    </th>
                    <td bgcolor="#FFFFFF">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    插入标签：<asp:DropDownList ID="ddlSelSRLabel" runat="server" CssClass=" inputselect">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtSRContent" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"
                                        Height="80px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="addtableT">
                        发送时间：
                    </th>
                    <td bgcolor="#FFFFFF">
                        生日前
                        <asp:TextBox ID="txtSRDay" runat="server" MaxLength="3" CssClass="inputtext formsize40"></asp:TextBox>
                        天
                        <asp:TextBox ID="txtSRHour" runat="server" MaxLength="2" CssClass="inputtext formsize40"></asp:TextBox>
                        时发送
                    </td>
                </tr>
                <tr>
                    <th align="right" class="addtableT">
                        是否启用：
                    </th>
                    <td bgcolor="#FFFFFF">
                        <asp:CheckBox ID="chkSRState" runat="server" Text="启用" />
                    </td>
                </tr>
            </table>
            <table width="90%" border="0" cellspacing="0" cellpadding="0" style="margin: 5px auto;">
                <tr>
                    <td align="center">
                        <div class="cun-cy">
                            <a href="javascript:" id="btnSRSave">保 存</a></div>
                    </td>
                </tr>
            </table>
        </div>
        </form>
    </div>

    <script type="text/javascript">
        var SmsSetting = {
            Data: {
                sl: '<%=Request.QueryString["sl"] %>'
            },
            UnBindBtn: function() {
                $("#btnCTSave").text("提交中...");
                $("#btnHTSave").text("提交中...");
                $("#btnSRSave").text("提交中...");
                $("#btnCTSave").unbind("click");
                $("#btnHTSave").unbind("click");
                $("#btnSRSave").unbind("click");
                $("#btnCTSave").attr("class", "cun-cyactive");
                $("#btnHTSave").attr("class", "cun-cyactive");
                $("#btnSRSave").attr("class", "cun-cyactive");
            },
            //按钮绑定事件
            BindBtn: function() {
                $("#btnCTSave").bind("click");
                $("#btnHTSave").bind("click");
                $("#btnSRSave").bind("click");
                $("#btnCTSave").text("保存");
                $("#btnHTSave").text("保存");
                $("#btnSRSave").text("保存");
                $("#btnCTSave").click(function() {
                    var msg = "";
                    if ($("#<%=txtCTContent.ClientID%>").val() == "") {
                        msg += "发送内容不能为空!<br/>";
                    }
                    if ($("#<%=txtCTDay.ClientID%>").val() == "") {
                        msg += "发送时间天数不能为空!<br/>";
                    }
                    if ($("#<%=txtCTHour.ClientID%>").val() == "") {
                        msg += "发送时间小时不能为空!<br/>";
                    }
                    if (!/^[1-9]([0-9])*|0$/.test($("#<%=txtCTDay.ClientID%>").val())) {
                        msg += "请输入有效天数!<br/>";
                    }
                    if (!/^2[0-3]|1\d|0$/.test($("#<%=txtCTHour.ClientID%>").val())) {
                        msg += "请输入有效小时数!<br/>";
                    }
                    if (msg != "") {
                        tableToolbar._showMsg(msg);
                        return false;
                    }
                    SmsSetting.Save("1");
                });
                $("#btnHTSave").click(function() {
                    var msg = "";
                    if ($("#<%=txtHTContent.ClientID%>").val() == "") {
                        msg += "发送内容不能为空!<br/>";
                    }
                    if ($("#<%=txtHTDay.ClientID%>").val() == "") {
                        msg += "发送时间天数不能为空!<br/>";
                    }
                    if ($("#<%=txtHTHour.ClientID%>").val() == "") {
                        msg += "发送时间小时不能为空!<br/>";
                    }
                    if (!/^[1-9]([0-9])*|0$/.test($("#<%=txtHTDay.ClientID%>").val())) {
                        msg += "请输入有效天数!<br/>";
                    }
                    if (!/^2[0-3]|1\d|0$/.test($("#<%=txtHTHour.ClientID%>").val())) {
                        msg += "请输入有效小时数!<br/>";
                    }
                    if (msg != "") {
                        tableToolbar._showMsg(msg);
                        return false;
                    }
                    SmsSetting.Save("2");
                });
                $("#btnSRSave").click(function() {
                    var msg = "";
                    if ($("#<%=txtSRContent.ClientID%>").val() == "") {
                        msg += "发送内容不能为空!<br/>";
                    }
                    if ($("#<%=txtSRDay.ClientID%>").val() == "") {
                        msg += "发送时间天数不能为空!<br/>";
                    }
                    if ($("#<%=txtSRHour.ClientID%>").val() == "") {
                        msg += "发送时间小时不能为空!<br/>";
                    }
                    if (!/^[1-9]([0-9])*|0$/.test($("#<%=txtSRDay.ClientID%>").val())) {
                        msg += "请输入有效天数!<br/>";
                    }
                    if (!/^2[0-3]|1\d|0$/.test($("#<%=txtSRHour.ClientID%>").val())) {
                        msg += "请输入有效小时数!<br/>";
                    }
                    if (msg != "") {
                        tableToolbar._showMsg(msg);
                        return false;
                    }
                    SmsSetting.Save("3");
                });
            },
            //插入标签到当前光标处
            AddOnPos: function(myField, myValue) {
                //IE support    
                if (document.selection) {
                    myField.focus();
                    sel = document.selection.createRange();
                    myValue = "" + myValue + "";
                    sel.text = myValue;
                    sel.select();
                }
                //MOZILLA/NETSCAPE support    
                else if (myField.selectionStart || myField.selectionStart == '0') {
                    var startPos = myField.selectionStart;
                    var endPos = myField.selectionEnd;
                    // save scrollTop before insert    
                    var restoreTop = myField.scrollTop;
                    myValue = "" + myValue + "";
                    myField.value = myField.value.substring(0, startPos) + myValue + myField.value.substring(endPos, myField.value.length);
                    if (restoreTop > 0) {
                        // restore previous scrollTop    
                        myField.scrollTop = restoreTop;
                    }
                    myField.focus();
                    myField.selectionStart = startPos + myValue.length;
                    myField.selectionEnd = startPos + myValue.length;
                } else {
                    myField.value += myValue;
                    myField.focus();
                }
            },
            //提交表单
            Save: function(SaveType) {
                SmsSetting.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/SmsCenter/SmsSetting.aspx?dotype=save&saveType=" + SaveType + "&" + $.param(SmsSetting.Data),
                    data: $("#btnCTSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        tableToolbar._showMsg(ret.msg);
                        SmsSetting.BindBtn();
                    },
                    error: function() {
                        tableToolbar._showMsg("服务器忙，请稍后再试!");
                        SmsSetting.BindBtn();
                    }
                });
            }
        };

        $(function() {
            SmsSetting.BindBtn();
            $("#<%=ddlSelCTLabel.ClientID%>").change(function() {
                var insertText = "[" + $(this).find("option:selected").text() + "]";
                var obj = $("#<%=txtCTContent.ClientID%>").get(0);
                SmsSetting.AddOnPos(obj, insertText);
            });
            $("#<%=ddlSelHTLabel.ClientID%>").change(function() {
                var insertText = "[" + $(this).find("option:selected").text() + "]";
                var obj = $("#<%=txtHTContent.ClientID%>").get(0);
                SmsSetting.AddOnPos(obj, insertText);
            });
            $("#<%=ddlSelSRLabel.ClientID%>").change(function() {
                var insertText = "[" + $(this).find("option:selected").text() + "]";
                var obj = $("#<%=txtSRContent.ClientID%>").get(0);
                SmsSetting.AddOnPos(obj, insertText);
            });
        });
    </script>

</asp:Content>
