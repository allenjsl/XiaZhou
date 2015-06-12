<%@ Page Title="积分商品详细" Language="C#" AutoEventWireup="true" CodeBehind="JifenInfo.aspx.cs"
    Inherits="EyouSoft.YlWeb.Jifen.JifenInfo" MasterPageFile="~/MasterPage/Boxy.Master" %>


<%@ Register Src="../UserControl/JiFenHotList.ascx" TagName="JiFenHotList" TagPrefix="uc2" %>

<%@ MasterType VirtualPath="~/MasterPage/Boxy.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <script type="text/javascript">
        var show_king_id = 1;
        function show_king_list(e, k) {
            if (show_king_id == k) return true;
            o = document.getElementById("a" + show_king_id);
            if (o) {
                o.className = "";
                e.className = "li_current";
                show_king_id = k;
            }
        }
</script>

    <style>
/******礼品兑换多张图******/
.jfxx_box .lpk_focus{width:371px; height:358px; float:left; margin-right:14px; display:inline;}
.jfxx_box .lpk_focus .lpk_imgArea{width:371px; height:358px;}
.jfxx_box .lpk_focus .lpk_imgArea ul.slides { position:relative;width:371px; height:237px;}
.jfxx_box .lpk_focus .lpk_imgArea .slides li {width:371px; height:273px;}
.jfxx_box .lpk_focus .lpk_imgArea .slides li img{width:371px; height:273px;}
.jfxx_box .lpk_focus .lpk_imgArea #loopedSlider {width:371px;}
.jfxx_box .lpk_focus .lpk_imgArea ul.pagination {height:78px;position: absolute;left:0;top:280px; right:auto; bottom:auto;}
.jfxx_box .lpk_focus .lpk_imgArea ul.pagination li a{ background:#fff;width:99px; height:72px;border:#ccc solid 1px; padding:2px;}
.jfxx_box .lpk_focus ul.pagination img{width:99px; height:72px;}
.jfxx_box .lpk_focus ul.pagination li.active a,ul.pagination li a:hover {border:#f00 solid 1px;}

</style>
    <form id="form1" runat="server">
            <div class="step_mainbox">
                <div class="basicT">
                    您的位置：维诗达游轮 &gt; 积分商城</div>
                <div class="basic_mainT">
                    <h5>
                        积分商城</h5>
                </div>
                <div class="jifen_main margin_T16 fixed">
                    <div class="jifen_leftbox">
                        <asp:PlaceHolder ID="phLoginIn" runat="server" Visible="false">
                            <div class="loginbar loginbar02">
                                <h3>
                                    会员资料</h3>
                                <ul class="login_form">
                                    <li>可用积分：<b class="jf_score">
                                        <asp:Literal ID="ltr_jfNumber" runat="server"></asp:Literal>
                                    </b>分</li>
                                    <%--<li>可用礼品卡：<b class="jf_gprice"><asp:Literal ID="ltr_jfCar" runat="server"></asp:Literal></b>元</li>--%>
                                    <li class="topborder"><a href="/HuiYuan/JiFen.aspx">积分明细</a> | <a href="/HuiYuan/DingDan.aspx">兑换记录</a></li>
                                </ul>
                            </div>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="phlLoginY" runat="server">
                            <div class="loginbar">
                                <h3>
                                    会员登录</h3>
                                <div class="login_select">
                                    <%--<label>
                                        <input type="radio" checked="checked" value="" name="">普通登录</label><label><input
                                            type="radio" value="" name="">手机动态密码登录</label>--%></div>
                                <ul class="login_form">
                                    <li>登录名<input id="u" name="txtUserName" type="text" class="formsize200 input_style"
                                        maxlength="15" valid="required" errmsg="请填写用户名！" /></li>
                                    <li>密&#12288;码<input type="password" id="p" name="txtPassWord"
                                        class="formsize200 input_style" maxlength="16" /></li>
                                    <li class="password_txt"><a href="/reset.aspx">忘记密码？</a></li>
                                    <%--<li>验证码<input class="formsize100 input_style">
                                        <img src="../images/code.jpg"></li>--%>
                                    <%--<li>
                                        <div class="login_txt">
                                            <label>
                                                <input type="checkbox" checked="checked" value="">
                                                30天内自动登录</label></div>
                                    </li>--%>
                                    <li class="login_txt">
                                            <a id="btnLogin" class="loginbtn" href="javascript:void(0);">登录</a>
                                    </li>
                                </ul>
                            </div>
                        </asp:PlaceHolder>
                        <uc2:JiFenHotList ID="JiFenHotList1" runat="server" />
                    </div>
                    <div class="jifen_rightbox">
                        <div class="jfxx_box fixed">
                            <div id="jfSlider" class="lpk_focus">
                                <div class="lpk_imgArea">
                                    <ul class="slides" style="width: 1113px; left: 0px;">
                                        <asp:Repeater ID="rptList1" runat="server">
                                            <ItemTemplate>
                                                <%# ImageView(Container.ItemIndex+1,Eval("Filepath"))%>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                    <div class="validate_Slider">
                                    </div>
                                    <ul class="pagination">
                                        <asp:Repeater ID="rptList2" runat="server">
                                            <ItemTemplate>
                                                <li class="<%# Container.ItemIndex+1.ToString()=="1"?"active" :"" %>"><a href="javascript:;"
                                                    rel="<%#Container.ItemIndex+1 %>">
                                                    <img src="<%#EyouSoft.YlWeb.TuPian.F1( ErpFilepath+Eval("Filepath"),99,72) %>"></a></li>
                                                <%--<li><a href="#" rel="2">
                                                    <img src="../images/yd_xximg01.jpg"></a></li>
                                                <li class="active"><a href="#" rel="3">
                                                    <img src="../images/yd_xximg01.jpg"></a></li>--%>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </div>
                            <div class="jf_xxRbox">
                                <dl>
                                    <dt><span id="span_JfName" runat="server"></span></dt>
                                    <dd class="fixed">
                                        <label>
                                            兑换方式：</label>
                                        <ul class="lp_xuanzeList">
                                            <asp:Literal ID="ltr_function" runat="server"></asp:Literal>
                                        </ul>
                                    </dd>
                                    <dd>
                                        <label>
                                            价 格：</label><span id="ShowMoney"></span></dd>
                                    <dd>
                                        <label>
                                            配送方式：</label><span id="span_PS" runat="server">快递</span></dd>
                                    <dd>
                                        <label>
                                            剩余数量：</label><span id="span_Shenyu" runat="server"></span>件</dd>
                                    <dd>
                                        <label>
                                            兑换数量：</label><span class="dindan_num dindan_num02"><a id="jia" href="javascript:;">-</a><input
                                                id="GouMaiSum" name="GouMaiSum" type="text" value="1"><a id="jian" href="javascript:;">+</a></span></dd>
                                </dl>
                                <div class="tg_rightbtn">
                                    <a class="yudin" href="javascript:;">积分兑换</a> <a class="shoucang" href="javascript:;">
                                        加入收藏</a>
                                </div>
                            </div>
                        </div>
                        <div id="n4Tab7" class="n4Tab_lpk margin_T16">
                            <div class="lpk_T">
                                <ul>
                                    <li class="active" onclick="nTabs('n4Tab7',this);" id="n4Tab7_Title0"><a href="javascript:void(0);">
                                        商品说明</a></li>
                                    <li class="normal" onclick="nTabs('n4Tab7',this);" id="n4Tab7_Title1"><a href="javascript:void(0);">
                                        兑换须知</a></li>
                                </ul>
                            </div>
                            <div class="lpk_Content fixed">
                                <div id="n4Tab7_Content0">
                                    <asp:Literal ID="ltr_shuoming" runat="server"></asp:Literal>
                                </div>
                                <div class="none" id="n4Tab7_Content1">
                                    <asp:Literal ID="ltr_duihuan" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </form>

    <script type="text/javascript">
        var UserLogin = {
            
        };
        var Duihuan = {
            Bindth: function() {
                $("#jia").click(function() {
                    var sum = tableToolbar.calculate($("#GouMaiSum").val(), 1, "-");
                    if (sum <= 0) {
                        sum = 1;
                    }
                    $("#GouMaiSum").val(sum);
                });
                $(".yudin").click(function() {
                    Duihuan.Submit();
                });
                $(".shoucang").click(function() {
                    Duihuan.Collection();
                });
                $("#jian").click(function() {

                    $("#GouMaiSum").val(tableToolbar.calculate($("#GouMaiSum").val(), 1, "+"));
                });
                $(".lp_xuanzeList li").click(function() {
                    $(".lp_xuanzeList li a").attr("class", "");
                    $(this).find("a").attr("class", "card_select");
                    Duihuan.jifenShow($(this).find("a"));
                });
                var len = $(".lp_xuanzeList li").length;
                for (var i = 0; i < len; i++) {
                    var name = $(".lp_xuanzeList li").eq(i).find("a").attr("class");
                    if (name != null && name != "") {
                        Duihuan.jifenShow($(".lp_xuanzeList li").eq(i).find("a"));
                    }
                }
            },
            jifenShow: function(obj) {
                var htm = [];
                var jifen = $(obj).attr("data-JiFen");
                var jine = $(obj).attr("data-JinE");
                var type = $(obj).attr("data-id");
                htm.push("<font class='font20 jf_score'>" + jifen + "</font>分");
                if (type == '<%=(int)EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分礼品卡 %>') {
                    htm.push("+<font class='font20 jf_gprice'>" + jine + "</font>元礼品卡");
                }
                else if (type == '<%=(int)EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分现金 %>') {
                    htm.push("+<font class='font20 jf_gprice'>" + jine + "</font>元");
                }
                $("#ShowMoney").html(htm.join(''));
            },
            Submit: function() {
                var num = tableToolbar.getInt($("#GouMaiSum").val());
                var len = $(".lp_xuanzeList li").length;
                var fsid = "";
                for (var i = 0; i < len; i++) {
                    var name = $(".lp_xuanzeList li").eq(i).find("a").attr("class");
                    if (name != null && name != "") {
                        fsid = $(".lp_xuanzeList li").eq(i).find("a").attr("data-id");
                    }
                }
                if (num < 1) { alert("请填写产品兑换数量！"); return false; }
                if (fsid == "") { alert("请选择兑换方式！"); return false; }

                if (iLogin.getM().isLogin) {
                    window.location.href = "/Jifen/JifenAddress.aspx?jfid=" + '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>' + "&fsid=" + fsid + "&jfsum=" + num;
                } else {
                    tableToolbar._showMsg("请登录");
                }
            },
            Collection: function() {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/Ashx/Handler.ashx?dotype=collect&cid=" + '<%=EyouSoft.Common.Utils.GetQueryStringValue("id")  %>' + "&lxid=" + '<%=(int)EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing.积分兑换 %>',
                    dataType: "json",
                    data: null,
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg);
                        }
                        else {
                            tableToolbar._showMsg(ret.msg);
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            }
        };
        
        function login() {
            var u = $.trim($("#u").val()), p = $.trim($("#p").val()), ckcode = $.trim($("#vc").val());
            if (u == "") {
                tableToolbar._showMsg("请输入用户名!");
                $("#u").focus();
                return;
            }
            if (p == "") {
                tableToolbar._showMsg("请输入密码");
                return;
            }

            tableToolbar._showMsg("正在登录....");
            $("#btnLogin").unbind().css("cursor", "default");

            blogin5({ u: u, p: p, vc: ckcode }
                , function(h) {//login success callback
                    tableToolbar._showMsg("登录成功....");
                    var s = '<%=Request.QueryString["rurl"] %>';
                    if (s.length > 0) window.location.href = s;
                    else window.location.href = window.location.href;
                }
                , function(m) {//login error callback
                    tableToolbar._showMsg(m);
                    $("#btnLogin").click(function() { login(); return false; }).css("cursor", "pointer");
                });
        }
        
        $(function() {
            Duihuan.Bindth();
            setNav(6);
            $("#btnLogin").click(function() { login(); });
            $(".i_jinfen_hot").eq(0).addClass("li_current");

            $('#jfSlider').loopedSlider({});

            var licount = $(".slides").find("li").length;
            if (licount == 1) {
                $(".slides").find("li").attr("style", "position: absolute; left: 0px; display: block;");
            }

            $('#u').keydown(function(e) { if (e.keyCode == 13) { $("#btnLogin").click(); } });
            $('#p').keydown(function(e) { if (e.keyCode == 13) { $("#btnLogin").click(); } });
        });
    </script>
</asp:Content>