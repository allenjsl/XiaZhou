<%@ Page Title="积分商城" Language="C#" AutoEventWireup="true" CodeBehind="JifenList.aspx.cs"
    Inherits="EyouSoft.YlWeb.Jifen.JifenList" MasterPageFile="~/MasterPage/Boxy.Master" %>

<%@ Register Src="../UserControl/JiFenHotList.ascx" TagName="JiFenHotList" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPage/Boxy.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">

<%--    <script type="text/javascript" src="../Js/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="../Js/foucs.js"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/slogin.js" type="text/javascript"></script>
--%>
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

        var pConfig = { pageSize: 15, pageIndex: 1, recordCount: 0, showPrev: true, showNext: true, showDisplayText: false, cssClassName: 'page_change' }
</script>

    <form id="form1" runat="server">
            <div class="step_mainbox" style="min-height:600px;">
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
                                        class="formsize200 input_style" maxlength="16"  /></li>
                                    <li class="password_txt"><a href="/reset.aspx">忘记密码？</a></li>
                                    <%--<li>验证码<input class="formsize100 input_style">
                                        <img src="../images/code.jpg"></li>--%>
                                    <%--<li>
                                        <div class="login_txt">
                                            <label>
                                                <input type="checkbox" checked="checked" value="">
                                                30天内自动登录</label></div>
                                    </li>--%>
                                    <li  class="login_txt">
                                            <a id="btnLogin" class="loginbtn" href="javascript:void(0);">登录</a>
                                    </li>
                                </ul>
                            </div>
                        </asp:PlaceHolder>
                        <uc2:JiFenHotList ID="JiFenHotList1" runat="server" />
                    </div>
                    <div class="jifen_rightbox">
                        <div id="lpk_Slider" class="lpk_focus">
                            <div class="lpk_imgArea">
                                <ul class="slides" >
                                    <asp:Literal runat="server" ID="ltr1"></asp:Literal>
                                </ul>
                                <div class="validate_Slider">
                                </div>
                                <ul class="pagination">
                                    <asp:Literal runat="server" ID="ltr2"></asp:Literal>
                                </ul>
                            </div>
                        </div>
                        <div class="jifen_product margin_T16">
                            <div class="basic_mainT_color basic_mainT_color02">
                                <h5>
                                    兑换商品</h5>
                            </div>
                            <div class="paixu_box paixu_box2">
                                <ul>
                                    <li><a href="javascript:;" id="i_a_jifen_up">积分<span></span></a></li>
                                    <%--<li><a class="down" href="javascript:;">礼品卡<span></span></a></li>--%>
                                    <li><a href="javascript:;" id="i_a_xianjin_up">信用卡<span></span></a></li>
                                </ul>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="tg_list margin_T16">
                                <ul>
                                    <asp:Repeater ID="rptList" runat="server">
                                        <ItemTemplate>
                                            <li <%#(Container.ItemIndex+1)%4==0?"class='marginR'":"" %>><a class="tg_img" href="<%#"/jifen/jifeninfo.aspx?id="+Eval("ShangPinId") %>">
                                                <img src="<%#EyouSoft.YlWeb.TuPian.F1(ErpFilepath+ImageView(Eval("FuJians")),214,157) %>"></a>
                                                <h5 class="tg_title">
                                                    <a href="<%#"/jifen/jifeninfo.aspx?id="+Eval("ShangPinId") %>">
                                                        <%#Eval("MingCheng")%></a></h5>
                                                <p>
                                                    <%#JifenHtml(Eval("FangShis"))%>
                                                </p>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                        <div>
                            <div id="page_change" style="width: 100%; text-align: right; margin: 0px auto 0px;
                                margin:0; clear: both">
                            </div>    
                        </div>
                    </div>
                </div>
            </div>
    

    <script type="text/javascript" src="/js/ajaxpagecontrols.js"></script>

    <script type="text/javascript">
        var erpFilepath = "<%=ErpFilepath %>";
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
                    tableToolbar._showMsg("登录成功，正进入系统....");
                    var s = '<%=Request.QueryString["rurl"] %>';
                    if (s.length > 0) window.location.href = s;
                    else window.location.href = window.location.href; 
                }
                , function(m) {//login error callback
                    tableToolbar._showMsg(m);
                    $("#btnLogin").click(function() { login(); return false; }).css("cursor", "pointer");
                });
        }

        $(document).ready(function() {
            setNav(6);
            $("#i_a_jifen_up").click(function() {
                window.location.href = "jifenlist.aspx?uptype=up&chatype=0";
                return false;
            });
            $("#i_a_xianjin_up").click(function() {
                window.location.href = "jifenlist.aspx?uptype=up&chatype=2";
                return false;
            });
            if ('<%=Request.QueryString["chatype"] %>' == "2") {
                $("#i_a_xianjin_up").addClass("up");
            } else {
                $("#i_a_jifen_up").addClass("up");
            }
            
            $(".i_jinfen_hot").eq(0).addClass("li_current");

            if (pConfig.recordCount > 0) {
                AjaxPageControls.replace("page_change", pConfig);
            }

            $("#btnLogin").click(function() { login(); });
            $('#u').keydown(function(e) { if (e.keyCode == 13) { $("#btnLogin").click(); } });
            $('#p').keydown(function(e) { if (e.keyCode == 13) { $("#btnLogin").click(); } });

            if ($("#lpk_Slider").find("ul.slides li").length > 1)
                $('#lpk_Slider').loopedSlider({ autoStart: 5000 });
            else { $("#lpk_Slider").find("ul.slides li").show(); $("#lpk_Slider").find("ul.pagination li").addClass("active"); }
        });
    </script>
    
    </form>

</asp:Content>
