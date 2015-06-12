<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderPay.aspx.cs" Inherits="EyouSoft.YlWeb.Hangqi.OrderPay"
    MasterPageFile="~/MasterPage/M1.Master" Title="订单支付" %>
<%@ MasterType VirtualPath="~/MasterPage/M1.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <div class="step_mainbox">
        <div class="step_T">
            <div class="basicT floatL">
                您的位置：维诗达游轮 > 订单支付</div>
            <div class="step_Rimg">
                <img src="../images/online_book.jpg" /></div>
            <div class="step_num">
                <ul>
                    <li>
                        <img src="../images/step01_1.jpg" /><span class="bfontblue">选择产品</span></li>
                    <li>
                        <img src="../images/step02_1.jpg" /><span class="bfontblue">核对与填写信息</span></li>
                    <li>
                        <img src="../images/step03_1.jpg" /><span class="bfontblue" style="text-indent: 79px;">客服审核</span></li>
                    <li>
                        <img src="../images/step04_1.jpg" /><span class="bfontblue" style="text-indent: 96px;">订单支付</span></li>
                    <li>
                        <img src="../images/step05.jpg" /><span style="text-indent: 107px;">预订成功</span></li>
                </ul>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="step_box1 margin_T16">
            <div class="basic_mainT_color">
                <h5>
                    订单支付</h5>
            </div>
            <div class="ddzf_yellbox">
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <th>
                            订单号
                        </th>
                        <th align="left">
                            产品
                        </th>
                        <th>
                            付款金额
                        </th>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Literal runat="server" ID="ltrJiaoYiHao"></asp:Literal>
                        </td>
                        <td align="left" valign="middle">
                            <asp:Literal runat="server" ID="ltrMingCheng"></asp:Literal>
                        </td>
                        <td align="center" valign="middle">
                            <font class="font34 price_yellow">
                                <asp:Literal runat="server" ID="ltrJinE"></asp:Literal></font>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="n4Tab_bank" id="n4Tab4">
                <div class="bank_T">
                    <ul>
                        <%--<li id="n4Tab4_Title0" onclick="nTabs('n4Tab4',this);" class="active"><a href="javascript:void(0);">
                            信用卡</a></li>--%>
                        <li id="n4Tab4_Title0" onclick="bank_fs(0);" class="active"><a href="javascript:void(0);">
                            网上银行</a></li>
                        <li id="n4Tab4_Title1" onclick="bank_fs(1);" class="normal"><a href="javascript:void(0);">
                            第三方支付</a></li>
                    </ul>
                </div>
                <div class="bank_Content">
                    <%--<div id="n4Tab4_Content0">
                        <ul>
                            <li><a href="#">
                                <input name="" type="radio" value="" checked="checked" /><label class="current"><img
                                    src="../images/zhg_bank.jpg" /></label></a></li>
                            <li><a href="#">
                                <input name="" type="radio" value="" /><label><img src="../images/zhg_bank.jpg" /></label></a></li>
                            <li><a href="#">
                                <input name="" type="radio" value="" /><label><img src="../images/zhg_bank.jpg" /></label></a></li>
                            <li><a href="#">
                                <input name="" type="radio" value="" /><label><img src="../images/zhg_bank.jpg" /></label></a></li>
                            <li><a href="#">
                                <input name="" type="radio" value="" /><label><img src="../images/zhg_bank.jpg" /></label></a></li>
                            <li><a href="#">
                                <input name="" type="radio" value="" /><label><img src="../images/zhg_bank.jpg" /></label></a></li>
                            <li><a href="#">
                                <input name="" type="radio" value="" /><label><img src="../images/zhg_bank.jpg" /></label></a></li>
                            <li><a href="#">
                                <input name="" type="radio" value="" /><label><img src="../images/zhg_bank.jpg" /></label></a></li>
                            <li><a href="#">
                                <input name="" type="radio" value="" /><label><img src="../images/zhg_bank.jpg" /></label></a></li>
                        </ul>
                        <div class="clear">
                        </div>
                        <div class="bank_qt">
                            <a href="select_bank.html">选择其他银行
                                <img src="../images/bot_jt.png" /></a></div>
                    </div>--%>
                    <div id="n4Tab4_Content0">
                        <ul id="bank_01">
                            
                        </ul>
                        <div class="clear">
                        </div>
                        <div class="bank_qt">
                            <a href="javascript:void(0)" id="a_bank_03">选择其他银行
                                <img src="../images/bot_jt.png" /></a></div>
                    </div>
                    <div id="n4Tab4_Content1" style="display:none;">
                        <ul id="bank_02">
                            
                        </ul>
                        <div class="clear">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="zhifu_btn">
            <div class="leftTxt">
                <%--<a href="step2.html">
                    <img src="../images/left_jt.jpg" />
                    返回上一步</a>--%></div>
            <a href="javascript:void(0)" class="btn" id="i_btn_zhifu">现在去支付</a></div>
        <div class="guild">
            <h5>
                支付安全承诺：</h5>
            <p>
                维诗达游轮网采用国际标准加密算法来保证支付信息安全传输，并承诺保障用户在维诗达游轮网的信息安全，如因维诗达安全问题造成资金损失，维诗达全额赔付。
            </p>
        </div>
    </div>
    
    <input type="hidden" id="txt_bank_id" value="" />
    <input type="hidden" id="txt_bank_fs" value="" />

    <script type="text/javascript">
        $(document).ready(function() {
            init_bank_01();
            init_bank_02();
            bank_fs(0);

            $("#a_bank_03").click(function() { init_bank_03(); })

            $("#i_btn_zhifu").click(function() {
                var data = { dingdanid: '<%=EyouSoft.Common.Utils.GetQueryStringValue("dingdanid") %>', dingdanleixing: '<%=EyouSoft.Common.Utils.GetQueryStringValue("dingdanleixing") %>', token: '<%=EyouSoft.Common.Utils.GetQueryStringValue("token") %>' };
                data["bank_fs"] = $("#txt_bank_fs").val();
                data["bank_id"] = $("#txt_bank_id").val();
                data["send"] = "send";
                var url = "orderpay.aspx?" + $.param(data);

                //showZZ('<div class="zhifu_boxy"><p>请您在新打开的页面进行支付，支付完成前请不要关闭该窗口。</p><p><a href="javascript:void(0)" id="i_zhifuboxy_0">已完成支付</a></p><p><a href="javascript:void(0)" id="i_zhifuboxy_1">支付遇到问题</a></p><p><a href="javascript:void(0)" id="i_zhifuboxy_2">返回重新选择支付方式</a></p><p><a href="javascript:void(0)"  id="i_zhifuboxy_3">关闭窗口</a></p></div>');
                showZZ('<div class="tc-boxbg"><a class="close-btn" href="javascript:void(0)" id="i_zhifuboxy_3"></a><div class="tc-cont"><div class="big-txt" style="font-size: 20px;">请您在新打开的页面进行支付，支付完成前请不要关闭该窗口。</div><div class="tc-btn"><a href="javascript:void(0)" id="i_zhifuboxy_0" style="margin-right: 10px;">已完成支付</a><a href="javascript:void(0)" id="i_zhifuboxy_1">支付遇到问题</a></div><div class="b_txt01"><a href="javascript:void(0)" id="i_zhifuboxy_2">【返回重新选择支付方式】</a></div></div></div>');

                var dingDanXxUrl = "dingdanxx.aspx?";

                if (data.dingdanleixing == "<%=(int)EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.兑换订单%>")
                    dingDanXxUrl = "jifendingdanxx.aspx?";

                $("#i_zhifuboxy_0").click(function() { window.location.href = dingDanXxUrl + $.param(data); return false; });
                $("#i_zhifuboxy_1").click(function() { window.location.href = "/huiyuan/dingdan.aspx"; return false; });
                $("#i_zhifuboxy_2").click(function() { closeZZ(); });
                $("#i_zhifuboxy_3").click(function() { window.location.href = "/huiyuan/dingdan.aspx"; return false; });

                window.open(url);
                return false;
            });
        });

        function bank_fs(fs) {
            if (fs == 0) {
                $("#n4Tab4_Title0").removeClass("normal").addClass("active");
                $("#n4Tab4_Title1").removeClass("active").addClass("normal");

                $("#n4Tab4_Content0").show();
                $("#n4Tab4_Content1").hide();
                $("#txt_bank_fs").val('0');
                $("#txt_bank_id").val($("#bank_01").find("input[name='txt_bank_01']:checked").val());
            }

            if (fs == 1) {
                $("#n4Tab4_Title1").removeClass("normal").addClass("active");
                $("#n4Tab4_Title0").removeClass("active").addClass("normal");

                $("#n4Tab4_Content1").show();
                $("#n4Tab4_Content0").hide();
                $("#txt_bank_fs").val('1');
                $("#txt_bank_id").val($("#bank_02").find("input[name='txt_bank_02']:checked").val());
            }
        }

        function init_bank_01() {
            var s = [];
            var j = 0;
            for (var i = 0; i < banks.length; i++) {
                if (j > 9) break;
                if (banks[i].fs == "0") {
                    s.push('<li title="' + banks[i].name + '"><input name="txt_bank_01" type="radio" value="' + banks[i].id + '" id="txt_bank_01_' + banks[i].id + '" /><label for="txt_bank_01_' + banks[i].id + '"><span class="current"><img src="' + banks[i].logo + '" /></span></label></li>');
                    j++;
                }
            }

            $("#bank_01").append(s.join(''));
            $("#bank_01").find("input[name='txt_bank_01']").click(function() { $("#bank_01").find("span").removeClass("current"); $(this).closest("li").find("span").addClass("current"); $("#txt_bank_id").val($(this).val()); });
            $("#bank_01").find("input[name='txt_bank_01']").eq(0).click();
        }

        function init_bank_02() {
            var s = [];
            for (var i = 0; i < banks.length; i++) {
                if (banks[i].fs == "1") {
                    s.push('<li  title="' + banks[i].name + '"><input name="txt_bank_02" type="radio" value="' + banks[i].id + '" id="txt_bank_02_' + banks[i].id + '" /><label for="txt_bank_02_' + banks[i].id + '"><span class="current"><img src="' + banks[i].logo + '" /></span></label></li>');
                    //s.push('<li  title="' + banks[i].name + '"><input name="txt_bank_02" type="radio" value="' + banks[i].id + '" id="txt_bank_02_' + banks[i].id + '" /><label for="txt_bank_02_' + banks[i].id + '"><span class="current">' + banks[i].name + '</span></label></li>');
                }
            }

            $("#bank_02").append(s.join(''));
            $("#bank_02").find("input[name='txt_bank_02']").click(function() { $("#bank_02").find("span").removeClass("current"); $(this).closest("li").find("span").addClass("current"); $("#txt_bank_id").val($(this).val()); });
            $("#bank_02").find("input[name='txt_bank_02']").eq(0).click();
        }

        function init_bank_03() {
            var s = [];
            s.push('<div class="bank_box"><h3>选择银行<a href="javascript:void(0)" id="bank_close_03"><em>X</em>关闭</a></h3><div class="bank_Content"><ul id="bank_03">')
            for (var i = 0; i < banks.length; i++) {
                if (banks[i].fs == "0") {
                    s.push('<li title="' + banks[i].name + '"><input name="txt_bank_03" type="radio" value="' + banks[i].id + '" id="txt_bank_03_' + banks[i].id + '" /><label for="txt_bank_03_' + banks[i].id + '"><span class="current"><img src="' + banks[i].logo + '" /></span></label></li>');
                }
            }
            s.push('</ul><div class="clear"></div></div><div class="alertbox-btn"><a href="javascript:void(0)" id="bank_zhifu_03">现在去支付</a></div></div>');

            var _bank01 = $("#bank_01").find("input[name='txt_bank_01']:checked").val();
            showZZ(s.join(''));

            $("#bank_03").find("input[name='txt_bank_03']").click(function() { $("#bank_03").find("span").removeClass("current"); $(this).closest("li").find("span").addClass("current"); $("#txt_bank_id").val($(this).val()); });
            $("#bank_03").find("input[name = 'txt_bank_03'][value='" + _bank01 + "']").click();

            $("#bank_close_03").click(function() { closeZZ(); $("#txt_bank_id").val(_bank01); });

            $("#bank_zhifu_03").click(function() {
                if ($("#bank_03").find("input[name='txt_bank_03']:checked").length == 0) { tableToolbar._showMsg("请选择银行"); return; }
                $("#i_btn_zhifu").click();
                $("#txt_bank_id").val(_bank01);
            });
        }
    </script>

    <form runat="server" id="form1"></form>
</asp:Content>
