<%@ Page Title="积分兑换" Language="C#" AutoEventWireup="true" CodeBehind="JifenAddress.aspx.cs"
    Inherits="EyouSoft.YlWeb.Jifen.JifenAddress" MasterPageFile="~/MasterPage/M1.Master" %>
<%@ MasterType VirtualPath="~/MasterPage/M1.Master" %>
<%@ Register Src="../UserControl/JiFenHotList.ascx" TagName="JiFenHotList" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/gscx.js" type="text/javascript"></script>

    <script src="../Js/table-toolbar.js" type="text/javascript"></script>

    <form id="form1" runat="server">
    <div class="step_mainbox">
        <div class="basicT">
            您的位置：维诗达游轮 &gt; 积分商城</div>
        <div class="basic_mainT">
            <h5>
                积分商城</h5>
        </div>
        <div class="lpk_main margin_T16 fixed">
            <div class="basic_mainT_color basic_mainT_color02">
                <h5>
                    确认配送方式</h5>
            </div>
            <%--<div class="yudin_msgbox fixed">
                <div class="yudin_leftT">
                    发票信息</div>
                <ul class="lvke_form">
                    <li style="padding-left: 50px;" class="font_999 font14">礼品卡等消费券支付的金额不提供报销凭证</li>
                    <li>
                        <label>
                            是否需要发票：</label><span class="font14">
                                <input id="checkN" checked="checked" type="radio" value="0" name="Ischeck" /><label
                                    for="checkN" class="no_style">&nbsp;否</label>&nbsp;&nbsp;
                                <input id="checkY" type="radio" name="Ischeck" value="1" /><label for="checkY" class="no_style">&nbsp;是</label>
                            </span></li>
                    <li>
                        <label>
                            发票抬头：</label><span><input type="text" id="txtFapTitle" name="txtFapTitle" value=""
                                class=" formsize370 inputbk"></span><span class="error">遵循税务局相关规定，发票抬头必须为个人姓名或公司名称</span></li>
                    <li>
                        <label>
                            发票明细：</label><span><input type="text" id="txtFapMingx" name="txtFapMingx" value=""
                                class=" formsize370 inputbk"></span></a></dt> </dl> </span></li>
                </ul>
            </div>--%>
            <div class="yudin_msgbox fixed" data-id="peisongfangshi" <%--style="display: none;"--%>>
                <div class="yudin_leftT">
                    配送方式</div>
                <div class="peisong_box">
                    <!--<h5>市内自取<span>快递 ￥15.00</span></h5>-->
                    <h3>
                        选择常用地址<span id="Address"><a href="javascript:;">添加新地址</a></span></h3>
                    <div class="peisong_address">
                        <ul class="address_list">
                            <asp:Repeater ID="rptList" runat="server">
                                <ItemTemplate>
                                    <%# AddressHtml(Eval("DiZhiId"),Eval("DiZhi"),Eval("YouBian"),Eval("DianHua"),Eval("IsMoRen")) %>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <ul class="address_form" style="display: none">
                            <li>
                                <label>
                                    收件人：</label><span><input type="text" id="txtName" name="txtName" class="formsize370 inputbk"></span></li>
                            <li>
                                <label>
                                    联系电话：</label><span><input type="text" id="txtTell" name="txtTell" class="formsize370 inputbk"></span></li>
                            <li>
                                <label>
                                    邮寄地址：</label><span><dl class="select_style">
                                        <select id="sel_province" name="sel_province" class="select_style_1">
                                        </select>
                                    </dl>
                                        <dl class="select_style">
                                            <select id="sel_city" name="sel_city" class="select_style_1">
                                            </select>
                                        </dl>
                                        <dl class="select_style">
                                            <select id="sel_country" name="sel_country" class="select_style_1">
                                            </select>
                                        </dl>
                                        <br>
                                        <input type="text" id="txtAdress" name="txtAdress" class="formsize370 inputbk"></span></li>
                            <li>
                                <label>
                                    邮政编码：</label><span><input type="text" id="txtZip" name="txtZip" value="" class=" formsize100 inputbk"></span></li>
                            <li style="text-align: right;"><a class="address_btn" href="javascript:;">保存地址</a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="lpk_queren margin_T16">
                <div class="basic_mainT_color basic_mainT_color02">
                    <h5>
                        确认订单信息</h5>
                </div>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tbody>
                        <tr>
                            <th width="25%" align="center">
                                名称
                            </th>
                            <th width="25%" align="center">
                                价格
                            </th>
                            <th width="25%" align="center">
                                数量
                            </th>
                            <th width="25%" align="center">
                                小计（元）
                            </th>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Literal ID="txtjfName" runat="server"></asp:Literal>
                            </td>
                            <td align="center">
                                <asp:Literal ID="txtJinE" runat="server"></asp:Literal>
                            </td>
                            <td align="center">
                                <asp:Literal ID="txtSum" runat="server"></asp:Literal>
                            </td>
                            <td align="center">
                                <b class="font_yellow font18">
                                    <asp:Literal ID="txtXiaoji" runat="server"></asp:Literal></b>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                            <td id="KuaiDiShow" align="center">
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="jiesuan_yellbox">
                <div class="jiesuan_price">
                    <b class="font20 price_20">实付款：</b><font id="ShiFuJinE" class="font34 price_yellow"><asp:Literal
                        ID="txtShiFu" runat="server"></asp:Literal></font></div>
                <div class="jiesuan_btn">
                    <div class="leftTxt">
                        <a href="javascript:history.go(-1);">
                            <img src="../images/left_jt.jpg">
                            返回上一步</a></div>
                    <a style="float: right;" class="btn" href="javascript:;"><span>提交订单</span></a></div>
            </div>
        </div>
    </div>
    <input id="hidIsFapiao" runat="server" type="hidden" />
    <input id="hidAddressId" runat="server" type="hidden" />
    <input id="HidKuaiDiJinE" runat="server" type="hidden" />
    <input id="DingDanJinE" runat="server" type="hidden" />
    </form>

    <script type="text/javascript">
        var InitSava = {
            InitSelectFrom: function() {
                pcToobar.init({ pID: '#sel_province', cID: '#sel_city', comID: '<%=this.YuMingInfo.CompanyId %>', xID: "#sel_country" });
            },
            AddressShow: function() {
                $(".address_form").show();
            },
            checkForm: function() {
                return ValiDatorForm.validator($(".address_btn").closest("form").get(0), "alert");
            },
            AddressSave: function() {
                if (this.checkForm()) {
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: "/Jifen/JifenAddress.aspx?dotype=address",
                        dataType: "json",
                        data: $(".address_btn").closest("form").serialize(),
                        success: function(ret) {
                            if (ret.result == "1") {
                                tableToolbar._showMsg(ret.msg, function() {
                                    var html = "<li><label><input type=\"radio\"  name=\"addressCk\" value=\"" + ret.obj.DiZhiid + "\">";
                                    html += $("#txtAdress").val() + "&nbsp;&nbsp;邮编：" + $("#txtZip").val() + "&nbsp;&nbsp;联系电话：" + $("#txtTell").val() + "</label></li>";
                                    $(".address_list").append(html);
                                    $(".address_form").find("input").val("");
                                    $('input[name="addressCk"]:last').attr('checked', 'checked');
                                });
                            }
                            else {
                                tableToolbar._showMsg(ret.msg);
                            }
                        }
                    });
                }
            },
            bindCk: function(obj) {
                if (obj == "in") {
                    $("#txtName").attr({ valid: "required", errmsg: "收件人不能为空！" });
                    $("#txtTell").attr({ valid: "required", errmsg: "联系电话不能为空！" });
                    $("#sel_province").attr({ valid: "required", errmsg: "请选择省份！" });
                    $("#sel_city").attr({ valid: "required", errmsg: "请选择城市！" });
                    $("#sel_country").attr({ valid: "required", errmsg: "请选择县区！" });
                    $("#txtZip").attr({ valid: "required", errmsg: "邮编不能为空！" });
                    $("#txtAdress").attr({ valid: "required", errmsg: "地址不能为空！" });
                }
            },
            Submit: function() {
                var _self = this;
                var data = {
                    dotype: 'jfsubmit',
                    jfid: '<%=EyouSoft.Common.Utils.GetQueryStringValue("jfid")%> ',
                    jfsum: '<%=EyouSoft.Common.Utils.GetQueryStringValue("jfsum")%>',
                    fsid: '<%=EyouSoft.Common.Utils.GetQueryStringValue("fsid")%>',
                    dizhi: InitSava.RadioListValue('addressCk')
                };
                $("#<%=hidAddressId.ClientID %>").val(InitSava.RadioListValue('addressCk'));
                $("#<%=hidIsFapiao.ClientID %>").val(InitSava.RadioListValue('Ischeck'));
                if($("#<%=hidAddressId.ClientID %>").val()==""){
                    tableToolbar._showMsg("请选择配送地址！");
                    return false;
                }
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/Jifen/JifenAddress.aspx?" + $.param(data),
                    dataType: "json",
                    data: $(".btn").closest("form").serialize(),
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg);
                            if (ret.obj.FuKuanState != "<%=(int)EyouSoft.Model.EnumType.YlStructure.FuKuanStatus.已付款 %>"&&$("#ShiFuJinE").text()!="0"&&$("#ShiFuJinE").text()!="￥0") {
                                _self.goZhiFu(ret.obj.DingDanId);
                            }
                            else {
                                showZZ('<div class="tc-boxbg"><a class="close-btn" href="/huiyuan/dingdan.aspx"></a><div class="tc-cont"><div class="big-txt">您已经成功兑换该商品 !</div><div class="b_txt01">我们的工作人员将尽快核准订单信息。</div><div class="tc-btn"><a href="/huiyuan/dingdan.aspx">好的，了解了</a></div><div class="b_txt01"><a href="/jifen/jifenlist.aspx">【返回商城首页】</a></div></div></div>');
                            }
                        }
                        else {
                            tableToolbar._showMsg(ret.msg);
                        }
                    }
                });
            },
            RadioListValue: function(name) {
                var list = $("input[name='" + name + "']:checked").val();
                return list;
            },
            bindBtn: function() {
                InitSava.InitSelectFrom();
                $(".address_btn").click(function() {
                    InitSava.bindCk('in');
                    InitSava.AddressSave();
                });
                $(".btn").click(function() {
                    InitSava.Submit();
                });
            },
            goZhiFu: function(dingDanId) {
                var _u = "";
                var _m = iLogin.getM();
                if (_m.isLogin) _u = _m.token;
                tableToolbar._showMsg("下单成功，正在跳转至支付窗口");

                window.location.href = "/Hangqi/OrderPay.aspx?dingdanleixing=<%=(int)EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.兑换订单 %>&dingdanid=" + dingDanId + "&token=" + _u;
            },
            ShowFaPiao: function() {
                $("#checkY").closest("li").nextAll().show();
                $('div[data-id="peisongfangshi"]').show();
            },
            HideFaPiao: function() {
                $("#checkN").closest("li").nextAll().hide();
                $('div[data-id="peisongfangshi"]').hide();
            }
        };
        $(function() {
            InitSava.bindBtn();
            $("#Address").click(function() {
                InitSava.AddressShow();
            });
            $('input:radio[name="Ischeck"]').click(function() {
                var value = InitSava.RadioListValue('Ischeck');
                var ShiFu = tableToolbar.getFloat($("#<%=DingDanJinE.ClientID%>").val());
                var KuaiDi = tableToolbar.getFloat($("#<%=HidKuaiDiJinE.ClientID %>").val());
                if (value == "1") {

                    var HeJiJinE = tableToolbar.calculate(ShiFu, KuaiDi, "+");
                    $("#ShiFuJinE").html("￥" + HeJiJinE);
                    var html = "<font class='font_999'>运送方式：快递，费用：</font><font class='font_yellow font18'>￥" + KuaiDi + "</font>";
                    $("#KuaiDiShow").html(html);

                }
                else {
                    $("#ShiFuJinE").html("￥" + ShiFu);
                    $("#KuaiDiShow").html("");
                }
            });
//            InitSava.HideFaPiao();
            
            $("#checkY").click(function(){
                InitSava.ShowFaPiao();
            })
            
            $("#checkN").click(function(){
                InitSava.HideFaPiao();
            })
        });
    </script>

</asp:Content>
