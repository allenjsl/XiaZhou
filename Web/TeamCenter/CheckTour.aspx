<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckTour.aspx.cs" Inherits="Web.TeamCenter.CheckTour" %>

<%@ Register Src="../UserControl/PriceStand.ascx" TagName="PriceStand" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script type="text/javascript" src="/Js/ValiDatorForm.js"></script>

    <style>
        th.th-line
        {
            background-attachment: scroll;
            background-color: #B7E0F3;
            border: 1px solid #B8C5CE;
            height: 22px;
        }
    </style>
</head>
<body style="background: 0 none;">
    <form id="mf" runat="server">
    <div class="alertbox-outbox">
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 99%; overflow-x: auto;" id="divPriceStand">
            <uc1:PriceStand ID="UC_PriceStand" runat="server" />

            <script type="text/javascript">
                $("#divPriceStand").find("table").removeClass();
            </script>

        </div>
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 99%;">
            <table width="99%" cellspacing="0" cellpadding="0" border="0" align="center">
                <tbody>
                    <tr>
                        <td height="23" align="center">
                            分销商平台中查看到的发布人信息
                            <label>
                                <input type="radio" value="1" checked="checked" name="rdo_CheckTour" />
                                审核人</label>
                            <label>
                                <input type="radio" value="2" name="rdo_CheckTour" />
                                供应商</label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="alertbox-btn" style="position: fixed">
            <a id="btnSave" style="text-indent: 0" hidefocus="true" href="javascript:void(0);">审
                核</a><a hidefocus="true" href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"
                    id="btnClose"><s class="chongzhi"></s>关 闭</a></div>
    </div>
    <asp:HiddenField ID="hideAdultPrice" runat="server" />
    <asp:HiddenField ID="hideChildPrice" runat="server" />
    <asp:HiddenField ID="hidePriceLevelId" runat="server" />
    </form>

    <script type="text/javascript">
        var CheckTour = {
            Data: {
                sl: '<%=Request.QueryString["sl"] %>',
                type: '<%=Request.QueryString["type"] %>',
                id: '<%=Request.QueryString["id"] %>'
            },
            //按钮绑定事件
            BindBtn: function() {
                $("#btnSave").text("审核");
                $("#btnSave").click(function() {
                    CheckTour.FormSave();
                    return false;
                });
            },
            DisabledValue: "",
            FormSave: function() {
                PriceStand.Init();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/TeamCenter/CheckTour.aspx?dotype=shenhe&" + $.param(CheckTour.Data),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax回发提示
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                window.parent.location.href = window.parent.location.href;
                            });
                        }
                        CheckTour.BindBtn();
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        CheckTour.BindBtn();
                    }
                });
            },
            LoadData: function() {
                $("#divPriceStand").find("input[name='txt_PriceStand_Adult_" + $("#<%=hidePriceLevelId.ClientID %>").val() + "_0']").val($("#<%=hideAdultPrice.ClientID %>").val()).css("background-color", "#dadada").attr("readonly", "readonly");
                $("#divPriceStand").find("input[name='txt_PriceStand_Child_" + $("#<%=hidePriceLevelId.ClientID %>").val() + "_0']").val($("#<%=hideChildPrice.ClientID %>").val()).css("background-color", "#dadada").attr("readonly", "readonly");
            },
            AddCallBack: function() {
                var adultPrice = $("#divPriceStand").find("input[name='txt_PriceStand_Adult_" + $("#<%=hidePriceLevelId.ClientID %>").val() + "_0']").val($("#<%=hideAdultPrice.ClientID %>").val());
                var childPrice = $("#divPriceStand").find("input[name='txt_PriceStand_Child_" + $("#<%=hidePriceLevelId.ClientID %>").val() + "_0']").val($("#<%=hideChildPrice.ClientID %>").val());
                adultPrice.val(adultPrice.eq(0).val());
                childPrice.val(childPrice.eq(0).val());
            }
        }
        $(function() {
            //表单验证初始化
            FV_onBlur.initValid($("#btnSave").closest("form").get(0));

            CheckTour.LoadData();

            $("#divPriceStand").find("table").autoAdd({ addCallBack: CheckTour.AddCallBack });

            $("#btnSave").click(function() {
                if (ValiDatorForm.validator($("#btnSave").closest("form").get(0), "parent")) {
                    $("#btnSave").unbind("click");
                    CheckTour.FormSave();
                    return false;
                }
            });


        })
    </script>

</body>
</html>
