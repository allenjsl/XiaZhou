<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PickUpPoint.aspx.cs" Inherits="EyouSoft.Web.CommonPage.PickUpPoint" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/table-toolbar.js"></script>

</head>
<body style="background: #e9f4f9;">
    <div class="alertbox-outbox03">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto" class="alertboxbk1">
            <tr>
                <td width="20%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    上车地点：
                </td>
                <td width="80%" align="left">
                    <input name="pickUpText" type="text" class="inputtext" id="pickUpText" value='<%=Request.QueryString["text"] %>' />
                    <button id="btnSearch" type="button" style="width: 64px; height: 24px; background: url(../images/cx.gif) no-repeat center center;
                        border: 0 none; margin-left: 5px;">
                        查 询</button>
                </td>
            </tr>
        </table>
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 99%;">
            已选择：<span id="spanSelectMore"></span>
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#ffffff"
                class="alertboxbk1" id="liststyle">
                <tr>
                    <th width="55" height="28" align="left" bgcolor="#b7e0f3" class="alertboxTableT">
                    </th>
                    <th width="240" height="28" align="left" bgcolor="#b7e0f3" class="alertboxTableT">
                        上车地点
                    </th>
                    <th width="60" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        接价
                    </th>
                    <th width="60" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        送价
                    </th>
                    <th width="250" align="left" bgcolor="#b7e0f3" class="alertboxTableT">
                        描述
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt_PickUpPoint">
                    <ItemTemplate>
                        <tr>
                            <td height="28" align="left" class="alertboxTableT">
                                <input type="checkbox" name="pickUpID" value='<%#Eval("CarLocationId") %>' />
                                <span data-text='<%#Eval("Location") %>' data-pricej='<%#Eval("OnPrice") %>' data-prices='<%#Eval("OffPrice") %>'
                                    data-desc='<%#Eval("Desc") %>'></span>
                                <%#Container.ItemIndex+1 %>
                            </td>
                            <td align="left" class="alertboxTableT">
                                <%#Eval("Location")%>
                            </td>
                            <td align="right">
                                <b class="fontblue">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("OnPrice"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("OffPrice"), ProviderToMoney)%></b>
                            </td>
                            <td align="left">
                                <%#Eval("Desc")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div style="position: relative; height: 20px;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" hidefocus="true" id="a_btn"><s class="xuanzhe"></s>选 择</a><a
                href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();"
                hidefocus="true"><s class="chongzhi"></s>关 闭</a></div>
    </div>

    <script type="text/javascript">
        var PickUpPointOjb = {
            selectId: "", //上车地点编号
            selectText: "", //上车地点名称
            selectPricej: "", //上车地点接价
            selectPrices: "", //上车地点送价
            selectDesc: "", //上车地点描述
            aid: '<%=Request.QueryString["id"] %>',
            parentWindow: null, //要赋值的页面的window对象
            iframeID: '<%=Request.QueryString["iframeId"]%>', //当前弹窗ID
            pIframeID: '<%=Request.QueryString["pIframeId"]%>', //父级弹窗ID
            SetValue: function() {//设置PickUpPointOjb对象的selectId selectText selectPricej selectPrices的值
                var idArr = [], textArr = [], priceJArr = [], priceSArr = [], descArr = [];
                $("#spanSelectMore").find("input[name='pickUpID']:checked").each(function() {
                    var self = $(this);
                    idArr.push($.trim(self.val()));
                    textArr.push($.trim(self.next().attr("data-text")));
                    priceJArr.push($.trim(self.next().attr("data-pricej")));
                    priceSArr.push($.trim(self.next().attr("data-prices")));
                    descArr.push($.trim(self.next().attr("data-desc")))
                })
                this.selectId = idArr.join(',');
                this.selectText = textArr.join(',');
                this.selectPricej = priceJArr.join(',');
                this.selectPrices = priceSArr.join(',');
                this.selectDesc = descArr.join('&&&');
            },
            InitSetSelect: function() {
                if (PickUpPointOjb.aid) {
                    var parentSpan = PickUpPointOjb.parentWindow.$('#' + PickUpPointOjb.aid).parent();
                    var id = [], text = [], pricej = [], prices = [], desc = [];
                    if (parentSpan.find("input[type='hidden'][data-class='pickupId']").length > 0) {
                        id = parentSpan.find("input[type='hidden'][data-class='pickupId']").val().split(',');
                    }
                    if (parentSpan.find("input[type='hidden'][data-class='pickupText']").length > 0) {
                        text = parentSpan.find("input[type='hidden'][data-class='pickupText']").val().split(',');
                    }
                    if (parentSpan.find("input[type='hidden'][data-class='pickupPriceJ']").length > 0) {
                        pricej = parentSpan.find("input[type='hidden'][data-class='pickupPriceJ']").val().split(',');
                    }
                    if (parentSpan.find("input[type='hidden'][data-class='pickupPriceS']").length > 0) {
                        prices = parentSpan.find("input[type='hidden'][data-class='pickupPriceS']").val().split(',');
                    }
                    if (parentSpan.find("input[type='hidden'][data-class='pickupDesc']").length > 0) {
                        desc = parentSpan.find("input[type='hidden'][data-class='pickupDesc']").val().split('&&&');
                    }
                    var pickUpData = PickUpPointOjb.parentWindow["pickUpData"];
                    this.AppendCheckbox(id, text, pricej, prices, desc);
                    return false;
                }
            },
            BindCheckBoxClick: function() {
                $("#spanSelectMore").find("input[type='checkbox']").unbind("click").click(function() {
                    if (this.value != "") {
                        $("input[name='pickUpID'][value='" + this.value + "']").attr("checked", "");
                        PickUpPointOjb.RemoveCheckbox(this.value);
                    }
                    $(this).parent().remove();
                })
            },
            AppendCheckbox: function(id, text, pricej, prices, desc) {
                var str = "";
                var pickUpData = { id: [], text: [], priceJ: [], priceS: [], desc: [] };
                if (id.length > 0 && id.length == text.length) {
                    for (var i = 0; i < id.length; i++) {
                        if (id[i] != "") {
                            pickUpData.id.push(id[i]);
                            pickUpData.text.push(text[i]);
                            $("input[name='pickUpID'][value='" + id[i] + "']").attr("checked", "checked");
                            if (pricej && prices && pricej.length >= i && prices.length >= i) {
                                pickUpData.priceJ.push(pricej[i]);
                                pickUpData.priceS.push(prices[i]);
                                pickUpData.desc.push(desc[i]);
                                str += '<label><input type="checkbox" checked="checked" name="pickUpID" value=' +
                                    id[i] + ' /><span data-text="' +
                                    text[i] + '" data-pricej="' + pricej[i] + '" data-prices="' +
                                    prices[i] + '" data-desc="' + desc[i] + '"></span>' + text[i] + '</label>';
                            }
                        }
                    }
                }
                this.parentWindow["pickUpData"] = pickUpData;
                if (str != "") {
                    $("#spanSelectMore").append(str);
                    this.BindCheckBoxClick();
                }
            },
            RemoveCheckbox: function(val) {
                var pickUpData = this.parentWindow["pickUpData"];
                if (pickUpData) {
                    for (var i = 0; i < pickUpData.id.length; i++) {
                        if (pickUpData.id[i] == val) {
                            pickUpData.id.splice(i, 1);
                            pickUpData.text.splice(i, 1);
                            pickUpData.priceJ.splice(i, 1);
                            pickUpData.priceS.splice(i, 1);
                            pickUpData.desc.splice(i, 1);
                        }
                    }
                    this.parentWindow["pickUpData"] = pickUpData;
                }
                $("#spanSelectMore").find("input[type='checkbox'][value='" + val + "']").parent().remove();
            },
            SearchFun: function(text) {
                var data = { id: '<%=Request.QueryString["id"] %>', iframeId: '<%=Request.QueryString["iframeId"]%>', pIframeId: '<%=Request.QueryString["pIframeId"]%>', callBackFun: '<%=Request.QueryString["callBackFun"] %>', sModel: '<%=Request.QueryString["sModel"]%>', text: '' };
                data.text = text;
                window.location.href = "/CommonPage/PickUpPoint.aspx?" + $.param(data);
            },
            BtnBind: function() {
                $("#a_btn").click(function() {
                    PickUpPointOjb.SetValue();
                    var data = { id: '<%=Request.QueryString["id"] %>', tourid: PickUpPointOjb.tourId, value: PickUpPointOjb.selectId, text: PickUpPointOjb.selectText, priceJ: PickUpPointOjb.selectPricej, priceS: PickUpPointOjb.selectPrices, desc: PickUpPointOjb.selectDesc };
                    //根据父级是否为弹窗传值
                    var func = '<%=Request.QueryString["callBackFun"] %>';
                    if (func.indexOf('.') == -1) {
                        PickUpPointOjb.parentWindow[func](data);
                    } else {
                        PickUpPointOjb.parentWindow[func.split('.')[0]][func.split('.')[1]](data);
                    }
                    parent.Boxy.getIframeDialog(PickUpPointOjb.iframeID).hide();
                    return false;
                })

                $("#btnSearch").click(function() {
                    PickUpPointOjb.SearchFun($("#pickUpText").val());
                })

                //回车事件
                $("input[type='text'][name='pickUpText']").bind("keypress", function(e) {
                    if (e.keyCode == 13) {
                        PickUpPointOjb.SearchFun($("#pickUpText").val());
                        return false;
                    }
                });
            }
        }

        $(function() {
            //获得需要赋值页面的window 对象
            if (PickUpPointOjb.pIframeID) {
                PickUpPointOjb.parentWindow = window.parent.Boxy.getIframeWindow(PickUpPointOjb.pIframeID) || window.parent.Boxy.getIframeWindowByID(PickUpPointOjb.pIframeID);
            }
            else {
                PickUpPointOjb.parentWindow = parent.window;
            }
            //初始化设置选中
            PickUpPointOjb.InitSetSelect();
            //初始化绑定事件
            PickUpPointOjb.BtnBind();
            //如果是多选
            if ('<%=Request.QueryString["sModel"]%>' != '1') {
                if ($("#liststyle").find("input[type='checkbox'][name='pickUpID']:checked").length - $("#liststyle").find("input[type='checkbox'][name='pickUpID']").length == 0) {
                    $("#liststyle").find("input[type='checkbox'][name='checkAll']").attr("checked", "checked");
                }
                $("input[name='pickUpID']").click(function() {
                    var pickUpData = PickUpPointOjb.parentWindow["pickUpData"];
                    if (this.checked) {
                        var idArr = [], textArr = [], priceJArr = [], priceSArr = [], descArr = [];
                        var self = $(this);
                        idArr.push($.trim(self.val()));
                        textArr.push($.trim(self.next().attr("data-text")));
                        priceJArr.push(self.next().attr("data-pricej"));
                        priceSArr.push(self.next().attr("data-prices"));
                        descArr.push(self.next().attr("data-desc"));
                        if (pickUpData) {
                            pickUpData.id.push($.trim(self.val()));
                            pickUpData.text.push($.trim(self.next().attr("data-text")));
                            pickUpData.priceJ.push(self.next().attr("data-pricej"));
                            pickUpData.priceS.push(self.next().attr("data-prices"));
                            pickUpData.desc.push(self.next().attr("data-desc"));
                        }
                        PickUpPointOjb.AppendCheckbox(idArr, textArr, priceJArr, priceSArr, descArr);
                        PickUpPointOjb.parentWindow["pickUpData"] = pickUpData;
                    } else {
                        var self = this;
                        PickUpPointOjb.RemoveCheckbox(self.value);
                    }
                })
            }
        }) 
    </script>

</body>
</html>
