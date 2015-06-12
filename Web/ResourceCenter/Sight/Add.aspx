<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="EyouSoft.Web.ResourceCenter.Sight.Add" %>
<%@ Register Src="/UserControl/ControlType.ascx" TagName="ControlType" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/SupplierControl.ascx" TagName="SupplierControl" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>景点预控-添加-</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/js/jquery.boxy.js"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/bt.min.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" border="0" cellspacing="0" cellpadding="0" style="margin: 0 auto;">
            <tr>
                <td width="14%" height="34" align="right" bgcolor="#b7e0f3">
                    <span class="alertboxTableT">景点公司名称：</span>
                </td>
                <td width="41%">
                    <uc3:SupplierControl ID="SupplierControl1" runat="server" />
                </td>
                <td width="13%" align="right" bgcolor="#b7e0f3">
                    景点：
                </td>
                <td width="32%">
                    <select class="inputselect" name="ddl_carType" id="ddl_carType" valid="isNo" errmsg="请选择景点"
                        novalue="-1">
                        <option value="-1">-请选择-</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td height="34" align="right" bgcolor="#b7e0f3">
                    预控日期：
                </td>
                <td>
                    <asp:TextBox ID="txt_sueStartTime" runat="server" onfocus="WdatePicker();"
                        CssClass="inputtext formsize80" valid="required" errmsg="预控日期不能为空！"></asp:TextBox>
                    -
                    <asp:TextBox ID="txt_sueEndTime" runat="server" onfocus="WdatePicker();"
                        CssClass="inputtext formsize80" valid="required" errmsg="预控日期不能为空！"></asp:TextBox>
                </td>
                <td align="right" bgcolor="#b7e0f3">
                    预付金额：
                </td>
                <td>
                    <asp:TextBox ID="txt_day" CssClass="inputtext formsize80" runat="server" valid="required|isMoney"
                        errmsg="预付金额不能为空！|预付金额金额不正确!"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="34" align="right" bgcolor="#b7e0f3">
                    最后保留日期：
                </td>
                <td>
                    <asp:TextBox ID="txt_lastTime" onfocus="WdatePicker();" runat="server" CssClass="inputtext formsize80"
                        valid="required" errmsg="最后保留日期不能为空！"></asp:TextBox>
                </td>
                <td align="right" bgcolor="#b7e0f3">
                    门票数量：
                </td>
                <td>
                    <asp:TextBox ID="txt_quantity" runat="server" CssClass="inputtext formsize80" valid="required"
                        errmsg="门票数量不能为空！"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="34" align="right" bgcolor="#b7e0f3">
                    门票单价：
                </td>
                <td>
                    <asp:TextBox ID="txt_unitPrice" runat="server" CssClass="inputtext formsize80" valid="required"
                        errmsg="门票单价不能为空！"></asp:TextBox>
                </td>
                <td align="right" bgcolor="#b7e0f3">
                    门票总价：
                </td>
                <td>
                    <asp:TextBox ID="txt_totalPrice" runat="server" CssClass="inputtext formsize80" valid="required"
                        errmsg="门票总价不能为空！"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="34" align="right" bgcolor="#b7e0f3">
                    备注：
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txt_remark" runat="server" TextMode="MultiLine" CssClass="inputtext formsize450"
                        Height="70px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="34" align="right" bgcolor="#b7e0f3">
                    控票类型：
                </td>
                <td colspan="3">
                    <uc1:ControlType ID="ControlType1" runat="server" />
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <a id="a_Save" href="javascript:void(0);" hidefocus="true"><s class="baochun"></s>保
                存</a><a href="javascript:void(0);" onclick="window.parent.Boxy.getIframeDialog(Boxy.queryString('IframeId')).hide();return false;">
                    <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var AddPage = {
            WdatePickerCallBack: function() {
                var datas = new Date($("#<%=txt_sueStartTime.ClientID %>").val()).getTime();
                var datae = new Date($("#<%=txt_sueEndTime.ClientID %>").val()).getTime();
                $("#<%=txt_day.ClientID %>").val(((((datae - datas) / 1000 / 60 / 60 / 24)) + 1 || 0));
            },
            BindBtn: function() {
                /*绑定保存*/
                var that = this;
                /*绑定保存*/
                var a_Save = $("#a_Save");
                a_Save.unbind("click");
                a_Save.click(function() {
                    that.Save(this);
                    return false;
                })
                a_Save.css({ "background-position": "0 0", "text-decoration": "none" });
                a_Save.html("<s class=baochun></s>保 存");
            },
            SupplierControlCollBack: function(data) { /*酒店选用回调*/
                if (data) {
                    $("#<%=SupplierControl1.ClientText %>").val(data.name || "");
                    $("#<%=SupplierControl1.ClientValue %>").val(data.id || "");
                    this.GetCarType(data.id);
                }
            },
            GetCarType: function(id) {/*获取酒店房型*/
                var that = this;
                var obj = $("#ddl_carType");
                obj.html("<option value=-1>-正在获取....-</option>");
                obj.attr("disabled", "disabled");
                $.newAjax({
                    type: "get",
                    cache: false,
                    url: "/ResourceCenter/Sight/Add.aspx?" + $.param({ doType: "GetCarType", sourceId: id }),
                    data: {},
                    dataType: "html",
                    success: function(html) {
                        obj.html(html);
                        obj.removeAttr("disabled");
                        that.BindBtn();
                    },
                    error: function() {
                        obj.html("<option value=-1>-无可用景点-</option>");
                    }
                });

            },
            Form: null,
            //提交数据验证
            FormCheck: function() {
                this.Form = $("#a_Save").closest("form").get(0);
                FV_onBlur.initValid(this.Form);
                return ValiDatorForm.validator(this.Form, "parent");
            },
            Save: function(obj) {
                var that = this;
                if (that.FormCheck()) {
                    var url = '/ResourceCenter/Sight/Add.aspx?' + $.param({ sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', doType: "Save" });
                    obj = $(obj);
                    obj.unbind("click");
                    obj.css({ "background-position": "0 -57px", "text-decoration": "none" });
                    obj.html("<s class=baochun></s>提交中...");
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: url,
                        data: $(this.Form).serialize() + "&" + $.param($.extend(ControlType.Data, { CarTypeName: $.trim($("option[value]:selected").html()), CarTypeId: $("option[value]:selected").val() })),
                        dataType: "json", /*这个参数可随意,尽量使用json*/
                        success: function(ret) {
                            if (parseInt(ret.result) === 1) {
                                parent.tableToolbar._showMsg('添加成功!', function() {
                                    window.parent.Boxy.getIframeDialog(Boxy.queryString("IframeId")).hide();
                                    parent.location.href = parent.location.href;
                                });
                            }
                            else {
                                parent.tableToolbar._showMsg(ret.msg);
                                that.BindBtn();
                            }
                        },
                        error: function() {
                            parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                            that.BindBtn();
                        }
                    });
                }
            },
            PageInit: function() {
                FV_onBlur.initValid($("#a_Save").closest("form").get(0));
                this.BindBtn();
            }

        }
        $(function() {
            AddPage.PageInit();
        })
    </script>

</body>
</html>
