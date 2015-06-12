<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DingDanChaKan.aspx.cs"
    Inherits="EyouSoft.Web.YouLun.DingDanChaKan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="/css/boxynew.css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script type="text/javascript" src="/js/datepicker/wdatepicker.js"></script>

    <style type="text/css">
        table th, td
        {
            border: solid 1px #9AD2ED;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <th height="28" align="left" class="alertboxTableT" bgcolor="#b7e0f3" colspan="4">
                        游轮信息
                    </th>
                </tr>
                <tr>
                    <th width="150px" height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        供应商：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:Label ID="gongyingshang" runat="server" Text=""></asp:Label>
                    </td>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        游轮公司：
                    </th>
                    <td height="28" align="left">
                        <asp:Label ID="youlungongsi" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        游轮系列：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:Label ID="youlunxilie" runat="server" Text=""></asp:Label>
                    </td>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        游轮船只：
                    </th>
                    <td height="28" align="left">
                        <asp:Label ID="youlunchuanzhi" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        出发港口：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:Label ID="chufagangkou" runat="server" Text=""></asp:Label>
                    </td>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        抵达港口：
                    </th>
                    <td height="28" align="left">
                        <asp:Label ID="didagangkou" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        行程天数：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:Label ID="xingchengtianshu" runat="server" Text=""></asp:Label>
                    </td>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        产品名称：
                    </th>
                    <td height="28" align="left">
                        <asp:Label ID="chanpinmingcheng" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        产品编号：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:Label ID="chanpinbianhao" runat="server" Text=""></asp:Label>
                    </td>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        出发日期：
                    </th>
                    <td height="28" align="left">
                        <asp:Label ID="chufariqi" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="left" class="alertboxTableT" bgcolor="#b7e0f3" colspan="4">
                        价格信息
                    </th>
                </tr>
                <tr>
                    <td class="alertboxTableT" bgcolor="#b7e0f3" colspan="4">
                        <asp:Literal ID="jiagexingxi" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="left" class="alertboxTableT" bgcolor="#b7e0f3" colspan="4">
                        附加产品
                    </th>
                </tr>
                <tr>
                    <td class="alertboxTableT" bgcolor="#b7e0f3" colspan="4">
                        <table width="100%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
                            style="margin: 0 auto">
                            <tbody>
                                <tr>
                                    <td>
                                        产品类型
                                    </td>
                                    <td>
                                        产品名称
                                    </td>
                                    <td>
                                        单价
                                    </td>
                                    <td>
                                        预订数量
                                    </td>
                                    <td>
                                        小计
                                    </td>
                                </tr>
                                <asp:Repeater ID="rptfujia" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%# getGYSName( Eval("LeiXingId").ToString()) %>
                                            </td>
                                            <td>
                                                <%# Eval("XiangMu")%>
                                            </td>
                                            <td>
                                                <%# Eval("DanJia","{0:F2}")%>
                                            </td>
                                            <td>
                                                <%# Eval("ShuLiang")%>
                                            </td>
                                            <td>
                                                <%# getXJ( Eval("DanJia"), Eval("ShuLiang"))%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:PlaceHolder ID="PlaceHolder2" runat="server">
                                    <tr>
                                        <td align="center" colspan="10">
                                            暂无数据
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="left" class="alertboxTableT" bgcolor="#b7e0f3" colspan="4">
                        优惠信息
                    </th>
                </tr>
                <tr>
                    <td class="alertboxTableT" bgcolor="#b7e0f3" colspan="4">
                        <table width="100%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
                            style="margin: 0 auto">
                            <tbody>
                                <asp:Repeater ID="rptyouhuis" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%# Eval("MiaoShu")%>
                                            </td>
                                            <td>
                                                <%# Eval("JinE","{0:F2}")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="left" class="alertboxTableT" bgcolor="#b7e0f3" colspan="4">
                        积分抵扣
                    </th>
                </tr>
                <tr>
                    <td class="alertboxTableT" bgcolor="#b7e0f3" colspan="4">
                        <table width="100%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
                            style="margin: 0 auto">
                            <tbody>
                                <tr>
                                    <td>
                                        金额：<asp:Literal ID="litDiKouJinE" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="left" class="alertboxTableT" bgcolor="#b7e0f3" colspan="4">
                        游客信息 
                    </th>
                </tr>
                <tr>
                    <td class="alertboxTableT" bgcolor="#b7e0f3" colspan="4">
                        <table width="100%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
                            style="margin: 0 auto">
                            <tbody>
                                <tr>
                                    <td>
                                        姓名
                                    </td>
                                    <td>
                                        性别
                                    </td>
                                    <td>
                                        类型
                                    </td>
                                    <td>
                                        证件类型
                                    </td>
                                    <td>
                                        证件号码
                                    </td>
                                    <td>
                                        证件有效期
                                    </td>
                                    <td>
                                        出生日期
                                    </td>
                                    <td>
                                        联系手机
                                    </td>
                                </tr>
                                <asp:Repeater ID="rptyouke" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <input type="hidden" name="yk_youkeid" value="<%#Eval("YouKeId") %>" />
                                                <input type="text" name="yk_xingming" class="inputtext" value="<%# Eval("XingMing")%>" style="width:80px" maxlength="50" />                                                
                                            </td>
                                            <td>
                                                <select name="yk_xingbie" class="inputselect" data-v="<%# (int)Eval("XingBie")%>">
                                                    <%#EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender), new string[] { "2" }))%>
                                                </select>
                                            </td>
                                            <td>
                                                <%# getGYSName(Eval("LeiXingId").ToString())%>
                                            </td>
                                            <td>
                                                <select name="yk_zhengjianleixing" class="inputselect" data-v="<%# (int)Eval("ZhengJianLeiXing")%>">
                                                <%#EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing))) %>
                                                </select>
                                            </td>
                                            <td>
                                                <input type="text" name="yk_zhengjianhaoma" class="inputtext" value="<%# Eval("ZhengJianHaoMa")%>"
                                                    style="width: 120px" maxlength="50" />                                                
                                            </td>
                                            <td>
                                                <input type="text" name="yk_zhengjianyouxiaoqi" onfocus="WdatePicker()" class="inputtext"
                                                    value="<%#GetYK_ZJYXQ(Eval("ZhengJianLeiXing"),Eval("ZhengJianYouXiaoQi"),Eval("YXQ1"),Eval("YXQ2"),Eval("YXQ3"))%>"
                                                    style="width: 70px" />                                                
                                            </td>
                                            <td>
                                                <input type="text" name="yk_chushengriqi" onfocus="WdatePicker()" class="inputtext"
                                                    value="<%#GetYK_ZJYXQ(Eval("ZhengJianLeiXing"),Eval("ChuShengRiQi"),Eval("SR1"),Eval("SR2"),Eval("SR3"))%>"
                                                    style="width: 70px" />                                                
                                            </td>
                                            <td>
                                                <input type="text" name="yk_shouji" class="inputtext" value="<%# Eval("ShouJi")%>"
                                                    style="width: 90px" maxlength="50" />                                                
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:PlaceHolder ID="PlaceHolder3" runat="server">
                                    <tr>
                                        <td align="center" colspan="10">
                                            暂无数据
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                <td colspan="8" style="height:25px; text-align:left;">
                                    <input type="button" id="btnsaveyk" value="保存游客信息" /></td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="left" class="alertboxTableT" bgcolor="#b7e0f3" colspan="4">
                        预订信息
                    </th>
                </tr>
                <tr>
                    <td class="alertboxTableT" bgcolor="#b7e0f3" colspan="4">
                        <table width="100%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
                            style="margin: 0 auto">
                            <tbody>
                                <tr>
                                    <th>
                                        预订姓名
                                    </th>
                                    <td width="35%" height="28" align="left" colspan="3">
                                        <asp:Label ID="yudingxingming" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        预订时间
                                    </th>
                                    <td width="35%" height="28" align="left">
                                        <asp:Label ID="yudingshijian" runat="server" Text=""></asp:Label>
                                    </td>
                                    <th>
                                        联系电话
                                    </th>
                                    <td width="35%" height="28" align="left">
                                        <asp:Label ID="yudinglianxidianhua" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        联系手机
                                    </th>
                                    <td width="35%" height="28" align="left">
                                        <asp:Label ID="yudinglianxishouji" runat="server" Text=""></asp:Label>
                                    </td>
                                    <th>
                                        联系邮箱
                                    </th>
                                    <td width="35%" height="28" align="left">
                                        <asp:Label ID="yudinglianxiyouxiang" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        下单备注
                                    </th>
                                    <td width="90%" height="28" align="left" colspan="3">
                                        <asp:Label ID="xiadanbeizhu" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="left" class="alertboxTableT" bgcolor="#b7e0f3" colspan="4">
                        发票信息 <asp:Literal runat="server" ID="ltrShiFouXuYaoFaPiao"></asp:Literal>
                    </th>
                </tr>
                <asp:PlaceHolder runat="server" ID="phFaPiao1" Visible="false">
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        邮寄地址：
                    </th>
                    <td height="28" align="left" colspan="3">
                        <asp:Label ID="youjidizhi" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        收件人姓名：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:Label ID="shoujianrenxingming" runat="server" Text=""></asp:Label>
                    </td>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        收件人联系电话：
                    </th>
                    <td height="28" align="left">
                        <asp:Label ID="shoujianrendianhua" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        快递金额：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:Literal ID="litKuaiDiJinE" runat="server"></asp:Literal>
                    </td>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        发票抬头：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:Literal ID="ltrFaPiaoTaiTou" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        发票明细：
                    </th>
                    <td width="35%" height="28" align="left" colspan="3">
                        <asp:Literal ID="ltrFaPiaoMingXi" runat="server"></asp:Literal>
                    </td>
                </tr>
                </asp:PlaceHolder>
                
                <tr>
                    <th height="28" align="left" class="alertboxTableT" bgcolor="#b7e0f3" colspan="4">
                        支付信息
                    </th>
                </tr>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        订单总金额：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="litZongJinE" runat="server" CssClass="inputtext formsize80" valid="required|isMoney" errmsg="请输入金额！|请输入正确的金额！"></asp:TextBox>
                    </td>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        订单积分：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="txtDingDanJiFen" runat="server" CssClass="inputtext formsize80"></asp:TextBox>
                        <input type="button" id="btnSaveDingDanJiFen" value="保存订单积分" />
                    </td>
                </tr>                
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        订单状态：
                    </th>
                    <td width="35%" height="28" align="left">
                        <asp:Literal ID="selectDDL" runat="server"></asp:Literal>
                    </td>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        付款状态：
                    </th>
                    <td height="28" align="left">
                        <asp:Label ID="fukuanzhuangtai" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th height="28" align="right" class="alertboxTableT" bgcolor="#b7e0f3">
                        操作备注：
                    </th>
                    <td height="28" align="left" colspan="3">
                        <textarea cols="80" id="txtCaoZuoBeiZhu" runat="server"></textarea>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn" style="text-align: center;">
            <asp:Literal ID="ltrCaoZuo" runat="server"></asp:Literal>
            
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var pageDataJs = {
            Params: { sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>' },
            UnBindBtn: function() {
                $("#btnSave").html("<s class='baochun'></s>提交中...").unbind("click").css("background-position", "0-57px");
            },
            //按钮绑定事件
            BindBtn: function() {
                $("#btnSave").bind("click").css("background-position", "0-28px").html("<s class='baochun'></s>保 存");
                $("#btnSave").click(function() {
                    var form = $(this).closest("form").get(0);
                    if (ValiDatorForm.validator("form", "parent")) {
                        pageDataJs.Save();
                    } else {
                        return false;
                    }
                })
            },
            Save: function() {
                if (!confirm("你确定要修改订单信息吗？")) return;
                pageDataJs.UnBindBtn();
                $.ajax({
                    type: "post",
                    url: "/youlun/DingDanChaKan.aspx?save=save&" + $.param(pageDataJs.Params),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() { window.location.href = window.location.href });
                        } else {
                            tableToolbar._showMsg(ret.msg);
                            pageDataJs.BindBtn();
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg("操作失败，请稍后重试！");
                        pageDataJs.BindBtn();
                    }
                });
            },
            ykSave: function() {
                if (!confirm("你确定要修改游客信息吗？")) return;
                $.ajax({
                    type: "post",
                    url: window.location.href + "&dotype=baocunyk",
                    data: $("#btnsaveyk").closest("form").serialize(),
                    dataType: "json",
                    success: function(response) {
                        alert(response.msg);
                        window.location.href = window.location.href;
                    }
                });
            },
            SaveDingDanJiFen: function() {
                if (!confirm("你确定要修改订单积分吗？")) return;
                $.ajax({
                    type: "post",
                    url: window.location.href + "&dotype=SaveDingDanJiFen",
                    data: $("#btnSaveDingDanJiFen").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() { window.location.href = window.location.href });
                        } else {
                            tableToolbar._showMsg(ret.msg);
                        }
                    }
                });
            }
        }
        $(function() {
            if ($("#dingdanState").val() == "0") {
                $(this).attr("disabled", "disabled");
            }

            pageDataJs.BindBtn();

            $("select[name='yk_xingbie']").each(function() { $(this).val($(this).attr("data-v")) });
            $("select[name='yk_zhengjianleixing']").each(function() { $(this).val($(this).attr("data-v")) });

            $("#btnsaveyk").click(function() { pageDataJs.ykSave(); });
            $("#btnSaveDingDanJiFen").click(function() { pageDataJs.SaveDingDanJiFen(); });
        })
    </script>

</body>
</html>
