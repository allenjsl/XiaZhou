<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navhead.ascx.cs" Inherits="EyouSoft.YlWeb.UserControl.Navhead" %>
<link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
<script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>
<script src="/Js/jsonsql-0.1.js" type="text/javascript"></script>
<div class="top">
    <div class="topbox fixed">
        <div class="logo">
            <a href="../"><img width="283" height="131" src="/images/logo.png" alt="国内最大的三峡游轮长江游船的运营商"></a></div>
        <div class="top_R">
            <div class="top_R01">
                <ul>
                    <asp:PlaceHolder runat="server" ID="plnLogin">
                        <li id="i_li_dl"><span><a href="/login.aspx">登录</a></span></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" ID="plnRegister">
                        <li id="i_li_zc"><span><a href="/register.aspx">注册</a></span></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" ID="plnLoginOut" Visible="false">
                        <li id="i_li_huiyuan_info"><span><a href="/Huiyuan/MyInformation.aspx">您好，&nbsp;<asp:Literal runat="server" ID="ltrUserName"></asp:Literal></a></span></li>
                        <li><span><a id="Login_Out" href="/Login.aspx">退出</a></span></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" ID="plnOrder" Visible="false">
                        <li><span><a href="/HuiYuan/DingDan.aspx">我的订单</a></span></li>
                    </asp:PlaceHolder>
                    <li id="i_li_scj"><span><a id="Add_URL_Favorite" href="javascript:void(0)">收藏夹</a></span></li>
                    <li><span class="noborder"><a href="javascript:void(0)">帮助中心</a></span></li>
                </ul>
            </div>
            <div class="top_R02">
                <div class="searchbox">
                    <h3>
                        热门：<asp:Literal runat="server" ID="ltrReMen"></asp:Literal></h3>
                    <div class="searchbar">
                        <input type="text" value="请输入目的地或关键词" class="search_input" id="i_txt_gjz" onkeydown="Navhead.searchenter(event)">
                        <input type="button" value="搜 索" class="search_btn" id="i_btn_chaxun">
                    </div>
                </div>
                <div class="tel">
                    <img width="296" height="38" src="/images/tel.png" alt="三峡旅游邮轮旅行船票官方电话"></div>
            </div>
            <div class="menu">
                <ul>
                    <li class="basicbg"><a href="/" id="i_a_nav_0">首页</a></li>
                    <li class="basicbg"><a href="/default.aspx" id="i_a_nav_1">长江游轮</a></li>
                    <li class="basicbg"><a href="/haiyang.aspx" id="i_a_nav_2">海洋邮轮</a></li>
                    <li class="tuangou"><a href="/tuangou/tuangoupiao.aspx" id="i_a_nav_3">团购票</a></li>
                    <li class="basicbg"><a href="/huiyi/youlunhuiyi.aspx" id="i_a_nav_4">游轮会议</a></li>
                    <%--<li class="tuangou"><a href="/lipinka/default.aspx" id="i_a_nav_5">礼品卡</a></li>--%>
                    <li class="basicbg"><a href="/jifen/jifenlist.aspx" id="i_a_nav_6">积分商城</a></li>
                    <li class="basicbg"><a href="http://vst88.taobao.com" id="i_a_nav_7" target="_blank">淘宝店</a></li>
                    <li class="basicbg" style="display:none;"><a href="http://www.vst88.com/blog/" id="i_a_nav_7" target="_blank">豪华游轮博客</a></li>
                </ul>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    var Navhead={
        LeiXing:"<%=LeiXing.HasValue?(int)LeiXing:-1 %>",
        searchenter:function(event){
            var e = event || window.event || arguments.callee.caller.arguments[0];
             if(e && e.keyCode==13){ // enter 键
                 $("#i_btn_chaxun").trigger("click");
            }
        }
    }
    $(document).ready(function() {
        $("#Add_URL_Favorite").click(function() {
            var sURL = window.location.href;
            var sTitle = document.title;
            try {
                window.external.addFavorite(sURL, sTitle);
            }
            catch (e) {
                try {
                    window.sidebar.addPanel(sTitle, sURL, "");
                }
                catch (e) {
                    alert("加入收藏失败，请使用Ctrl+D进行添加");
                }
            }
        });
        $("#Login_Out").live("click",function() {

            $.ajax({
                type: "post",
                cache: false,
                url: "/LoginOut.aspx?r=1",
                dataType: "json",
                success: function(ret) {
                    if (ret.result == "1") {
                        window.location.href = window.location.href;
                    }
                    else {
                        window.location.href = window.location.href;
                    }
                },
                error: function() {
                    window.location.href = window.location.href;
                }
            });

            return false;
        });

        $(".i_rmgjz").click(function() {
            $("#i_txt_gjz").val($(this).text());
            $("#i_btn_chaxun").click();
            return false;
        });
        $("#i_txt_gjz").focus(function() {
            if ($.trim(this.value) == "请输入目的地或关键词") {
                this.value = "";
                $(this).css({ "color": "#333333" });
            }
        });
        $("#i_txt_gjz").blur(function() {
            if ($.trim(this.value) == "") {
                this.value = "请输入目的地或关键词";
                $(this).css({ "color": "#c1c1c1" });
            }
        });
        //jsonsql.query("select * from json.channel.items order by title desc",json);
        $("#i_txt_gjz").autocomplete("/ashx/handler.ashx?dotype=guanjianzisousuo",{
            width:300
//            ,formatItem: function(data) {
//                return data.MingCheng;
//            }
            }).result(function(e, data) {
                Navhead.LeiXing=data[1]
            });
        $("#i_btn_chaxun").click(function() {
            var gjz = $.trim($("#i_txt_gjz").val());
            if (gjz == "请输入目的地或关键词") gjz = "";
            window.location.href = "/hangqi/chaxun.aspx?lx="+Navhead.LeiXing+"&gjz=" + encodeURIComponent(gjz);
            return false;
        });
        var url = window.location.href;
        if(url != "http://www.vst88.com"){
            $(".foot_cont p").eq(3).hide();
        }
    });

    function setNav(v) {$("#i_a_nav_" + v).addClass("default");}
</script>
