<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="Web.FinanceManage.FixedAssets.Add" %>

<%@ Register Src="/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>固定资产</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form runat="server">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="20%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <font class="fontbsize12">* </font>资产编号：
                </td>
                <td width="30%" align="left">
                    <asp:TextBox ID="txt_Id" runat="server" CssClass="inputtext formsize120"></asp:TextBox>
                </td>
                <td width="20%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <font class="fontbsize12">* </font>资产名称：
                </td>
                <td width="30%" align="left">
                    <asp:TextBox ID="txt_Name" runat="server" CssClass="inputtext formsize120"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <font class="fontbsize12">* </font>购买时间：
                </td>
                <td width="25%" align="left">
                    <asp:TextBox ID="txt_purchaseDate" runat="server" onfocus="WdatePicker();" CssClass="inputtext formsize80"></asp:TextBox>
                </td>
                <td width="15%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <font class="fontbsize12">* </font>原始价值：
                </td>
                <td width="45%" align="left">
                    <asp:TextBox ID="txt_cost" runat="server" valid="required|IsDecimalTwo" errmsg="原始价值不能为空！|请输入正确的原始价值!"
                        CssClass="inputtext formsize80"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <font class="fontbsize12">* </font>折旧年限：
                </td>
                <td width="25%" align="left">
                    <asp:TextBox ID="txt_depreciationDateY" valid="required|RegInteger" errmsg="折旧年限不能为空！|请输入正确的折旧年限!"
                        runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                </td>
                <td width="15%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <font class="fontbsize12">* </font>使用部门：
                </td>
                <td width="45%" align="left">
                    <uc1:SelectSection ID="SelectSection1" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <font class="fontbsize12">* </font>管理责任人：
                </td>
                <td colspan="3" align="left">
                    <uc1:SellsSelect ID="SellsSelect1" runat="server" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    备注：
                </td>
                <td colspan="3" align="left">
                    <asp:TextBox ID="txt_Remark" runat="server" CssClass="inputtext formsize450" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" hidefocus="true" id="a_Save"><s class="baochun"></s>保
                存</a><a onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"
                    hidefocus="true" style="cursor: pointer"><s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var FixedAssets = {
            Form: null,
            //数据验证
            FormCheck: function(obj) {
                this.Form = $(obj).closest("form").get(0)
                FV_onBlur.initValid(this.Form);
                return ValiDatorForm.validator(this.Form, "parent");
            },
            AdminDeptID: "",
            GetAdminVal: function(data) {
                if (data) {
                    newToobar.backFun(data);
                    this.AdminDeptID = data["deptID"] || "";
                }
            },
            Save: function(obj) {
                var that = this;
                if (that.FormCheck(obj)) {
                    var obj = $(obj);
                    obj.unbind("click")
                    obj.css({ "background-position": "0 -57px", "text-decoration": "none" })
                    obj.html("<s class=baochun></s> 提交中...");
                    $.newAjax({
                        type: "post",
                        data: $(that.Form).serialize() + "&" + $.param({
                            doType: "Save",
                            id: '<%=EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("id")) %>',
                            AdminDeptID: that.AdminDeptID
                        }),
                        cache: false,
                        url: "/FinanceManage/FixedAssets/Add.aspx?" + $.param({ sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>' }),
                        dataType: "json",
                        success: function(ret) {
                            if (parseInt(ret.result) === 1) {
                                parent.tableToolbar._showMsg('提交成功!', function() {
                                    window.parent.Boxy.getIframeDialog(Boxy.queryString("IframeId")).hide();
                                    parent.location.href = parent.location.href;
                                });

                            }
                            else {
                                parent.tableToolbar._showMsg(ret.msg);
                                that.PageInit();
                            }
                        },
                        error: function() {
                            //ajax异常--你懂得
                            parent.tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                            that.PageInit();
                        }
                    })
                }
            },
            PageInit: function() {
                var that = this;
                var obj = $("#a_Save");
                obj.css({ "background-position": "", "text-decoration": "none" })
                obj.html("<s class=baochun></s> 保 存");
                obj.click(function() {
                    that.Save(this);
                    return false;
                })
            }

        }
        $(function() {
            FixedAssets.PageInit();

        })
    </script>

</body>
</html>
