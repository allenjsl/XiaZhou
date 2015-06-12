<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Control.aspx.cs" Inherits="Web.ResourceCenter.CommonPage.Control" %>

<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>总控</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/js/jquery.boxy.js"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <div>
        <form id="form1" runat="server">
        <div class="alertbox-outbox02">
            <div style="margin: 0 auto; width: 99%; padding-bottom: 5px;">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr id="tr_resourceDate">
                        <td align="left" width="27%">
                            预控日期：
                            <input type="text" id="txt_resourceDate" class="inputtext formsize80" onfocus="WdatePicker();"
                                name="txt_resourceDate">
                        </td>
                    </tr>
                    <tr>
                        <td width="27%" align="left">
                            <span class="sp_radio" value="共享">
                                <input type="radio" name="radio" id="radio_share" />
                                共享</span><span class="sp_radio" value="不共享">
                                    <input type="radio" name="radio" id="radio_NoShare" />
                                    不共享 </span><span id="sp_SellsSelect1" style="display: none;">
                                        <asp:TextBox ID="txt_houseMan" runat="server" class="inputtext formsize120" Enabled="false"></asp:TextBox>
                                        <input type="hidden" id="hd_houseManId" />
                                        <a id="a_showMan" class="xuanyong" data-width="650" data-height="430" href="/CommonPage/OrderSells.aspx">
                                        </a></span>
                        </td>
                    </tr>
                </table>
                <div class="alertbox-btn" style="text-align: center;">
                    <a href="javascript:void(0);" id="a_Save" hidefocus="true"><s class="xuanzhe"></s>选
                        择</a><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;" hidefocus="true"><s class="chongzhi"></s>关 闭</a>
                </div>
            </div>
        </div>
        </form>
    </div>

    <script type="text/javascript">

        var BoxyPageJsObj = {
            ParentWindow: null,
            ShowOrHide: function(thiss) {
                if ($(thiss).attr("id") == "radio_share") {
                    $("#sp_SellsSelect1").css("display", "none")
                }
                else {
                    $("#sp_SellsSelect1").css("display", "inline")
                }
            },
            GetVal: function() {
                var ret = {}
                /*总控类型*/
                ret.Type = $("[name='radio']:checked").closest(".sp_radio").attr("value");
                /*不共享类型*/
                if (ret.Type == "不共享") {
                    /*用**人名称*/
                    ret.Names = $("#<%=txt_houseMan.ClientID %>").val();

                    /*用**人Id*/
                    ret.Ids = $("#hd_houseManId").val();
                }
                /*酒店预控情况下*/
                if (parseInt(Boxy.queryString("sourceControlCategory")) == 1) {
                    /*预控日期*/
                    ret.Date = $("#txt_resourceDate").val();
                }
                return ret;
            },
            Save: function() {
                var data = {
                    pIframeID: Boxy.queryString("pIframeID") || "", //父级弹窗IframeID
                    iframeID: Boxy.queryString("iframeID") || "" //本身IframeID
                }
                if (data.pIframeID.length > 0) {
                    window.parent.Boxy.getIframeWindow(data.pIframeID)["ControlType"]["ZKCallBackFun"](this.GetVal());
                }
                parent.Boxy.getIframeDialog(data.iframeID).hide();
                return false;

            },
            Init: function() {/*初始化*/
                var that = this;
                that.ParentWindow = window.parent.Boxy.getIframeWindow(Boxy.queryString("pIframeID")) || parent.window;
                /*初始化 预控日期*/
                var intSourceControlCategory = parseInt(Boxy.queryString("sourceControlCategory")) || -1;
                /*除了酒店预控其余均无预控日期*/
                if (intSourceControlCategory != 1) { /*酒店预控  1为酒店预控的枚举*/
                    $("#tr_resourceDate").remove();
                }
                else {

                    $("#txt_resourceDate").val(that.ParentWindow.ControlType.Data.Date)
                }
                /*初始化 单选钮事件*/
                $(":radio").click(function() {
                    BoxyPageJsObj.ShowOrHide(this);
                })
                $(".sp_radio").click(function() {
                    var obj = $(this);
                    if (obj.attr("value") === "共享") {
                        $("#sp_SellsSelect1").css("display", "none")
                    }
                    else {
                        $("#sp_SellsSelect1").css("display", "inline");
                    }
                    obj.children(":radio").attr("checked", "checked");
                    $(".sp_txt").css("display", "none")
                    $("#" + obj.attr("id") + "_txt").css("display", "inline");

                })
                /*初始化 不共享状态的人员选用*/
                var titleKey = ["房", "车", "船"];
                newToobar.init(
                {
                    box: "#sp_SellsSelect1",
                    className: "xuanyong",
                    title: "总控-不共享-用" + titleKey[intSourceControlCategory - 1] + "人选择-",

                    para:
                        {
                            pIframeId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>'
                        }
                });
                /*选择按钮*/
                $("#a_Save").click(function() {
                    that.Save();
                    return false;
                })
                /*初始化页面数据*/
                var ControlTypeData = that.ParentWindow.ControlType.Data;
                switch (parseInt(ControlTypeData.ShareType)) {
                    case 1:
                        $("#radio_share").attr("checked", "checked");
                        break;
                    case 2:
                        $("#radio_NoShare").attr("checked", "checked");
                        var txt_sumControl = that.ParentWindow.$("#txt_sumControl").val()
                        $("#sp_SellsSelect1").css("display", "inline");
                        $("#hd_houseManId").val(ControlTypeData.Ids)
                        $("#<%=txt_houseMan.ClientID %>").val(ControlTypeData.Names)
                        break;
                }

            }
        }
        $(function() {
            BoxyPageJsObj.Init();
        })
    </script>

</body>
</html>
