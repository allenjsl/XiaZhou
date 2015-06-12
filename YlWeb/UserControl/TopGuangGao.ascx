<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopGuangGao.ascx.cs"
    Inherits="EyouSoft.YlWeb.UserControl.TopGuangGao" %>
<div class="banner_play" id="i_banner_div">
    <div class="play_img_con">
        <div class="bannerbg">
            <a href='javascript:void(0)' style='display: block; width: 100%; height: 100%; position: relative;' id="a_banner_1"></a>
        </div>
        <ul class="img_list">
            <asp:Literal runat="server" ID="ltr1"></asp:Literal>
        </ul>
    </div>
    <div class="play_but_con">
        <asp:Literal runat="server" ID="ltr2"></asp:Literal>
    </div>
</div>

<script type="text/javascript">
    function showBanner() {
        $("#i_banner_div").show();
    }

    function hideBanner() {
        $("#i_banner_div").hide();
    }
</script>

<%--<div class="banner_play">
    <div class="play_img_con">
        <div class="bannerbg">
        </div>
        <ul class="img_list">
            <li class="bg_img">
                <div class="banner_con" style="background-image: url(../images/cj_banner1.jpg);"
                    time="5000">
                </div>
            </li>
            <li class="bg_img">
                <div class="banner_con" style="background-image: url(../images/tgp_banner1.jpg);"
                    time="5000">
                </div>
            </li>
            <li class="bg_img">
                <div class="banner_con" style="background-image: url(../images/ylhy_banner1.jpg);"
                    time="5000">
                </div>
            </li>
            <li class="bg_img">
                <div class="banner_con" style="background-image: url(../images/tgp_banner1.jpg);"
                    time="5000">
                </div>
            </li>
            <li class="bg_img">
                <div class="banner_con" style="background-image: url(../images/ylhy_banner1.jpg);"
                    time="5000">
                </div>
            </li>
        </ul>
    </div>
    <div class="play_but_con">
        <span class="play_but_disc" rel="1"></span><span class="play_but_disc" rel="2"></span>
        <span class="play_but_disc" rel="3"></span><span class="play_but_disc" rel="4"></span>
        <span class="play_but_disc" rel="5"></span>
    </div>
</div>--%>

<script type="text/javascript">
    window.timer = "";
    window.piclen = $(".img_list").find("li").length;
    window.picPre = window.piclen;
    window.picDelay = [5000, 5000, 5000,5000,5000];

    window.onload = function() {
        for (var i = 0; i < piclen; i++) {
            var index = i + 1;
            var t = $(".bg_img:nth-child(" + index + ")").find(".banner_con").attr("time");
            window.picDelay[i] = parseInt(t);
        }
        if (window.piclen > 0) {
            initImgPlayer();
        }
    }

    function initImgPlayer() {
        showImg(1);
    }
    function showImg(i) {
        if (i > window.piclen)
            i = i % window.piclen;
        if (i == window.picPre&&i==1) {
            $(".img_list").find("li").eq(0).show();

            if ($.trim($(".img_list").find("li").eq(0).attr("data-src")).length > 0) {
                $("#a_banner_1").attr("href", $(".img_list").find("li").eq(0).attr("data-src")).attr("target", "_blank");
            } else {
                $("#a_banner_1").attr("href", "javascript:void(0)").removeAttr("target");
            }
            
            return;
        }
        if (i == window.picPre) return;
        if (i > 0) {
            var prev = window.picPre;
            var preObj = $(".img_list").find("li:nth-child(" + prev + ")");
            var curObj = $(".img_list").find("li:nth-child(" + i + ")");
            if ($.trim($(curObj).attr("data-src")).length > 0) {
                $("#a_banner_1").attr("href", $(curObj).attr("data-src")).attr("target", "_blank");
            } else {
                $("#a_banner_1").attr("href", "javascript:void(0)").removeAttr("target");
            }
            
            /*$(preObj).fadeOut("slow", function() {
                $(curObj).fadeIn("fast");
            });*/

            /*$(preObj).fadeOut("slow");
            $(curObj).fadeIn("slow");*/

            $(preObj).fadeOut(800);
            $(curObj).fadeIn(1000);
            
            $(".play_but_con span[class~='current']").removeClass("current");
            $(".play_but_con").find("span:nth-child(" + i + ")").addClass("current");
            window.picPre = i;
        }
        else if (i < 1) {
            window.picPre = 1;
        }
        window.timer = window.setTimeout(function() {
            showImg(parseInt(window.picPre) + 1);
        }, window.picDelay[parseInt(window.picPre) - 1]);
    }
    function stopAtImg(i) {
        window.clearTimeout(window.timer);
        showImg(i);
        window.clearTimeout(window.timer);
    }
    function continuePlayImg() {
        window.clearTimeout(window.timer);
        window.timer = window.setTimeout(function() { showImg(parseInt(window.picPre) + 1) }, window.picDelay[parseInt(window.picPre) - 1]);
    }
    $(".play_but_disc").click(function() {
        var index = $(this).attr("rel");
        stopAtImg(index);
    }).mouseout(function() {
        continuePlayImg();
    });
    $(".button_zone").mouseover(function() {
        window.clearTimeout(window.timer);
    }).mouseout(function() {
        continuePlayImg();
    });
</script>
