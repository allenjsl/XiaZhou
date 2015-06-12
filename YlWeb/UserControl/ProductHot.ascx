<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductHot.ascx.cs"
    Inherits="EyouSoft.YlWeb.UserControl.ProductHot" %>
<script type="text/javascript">
    function $_(id) { return document.getElementById(id); }

    function addLoadEvent(func) {
        var oldonload = window.onload;
        if (typeof window.onload != 'function') {
            window.onload = func;
        } else {
            window.onload = function() {
                oldonload();
                func();
            }
        }
    }

    function moveElement(elementID, final_x, final_y, interval) {
        if (!document.getElementById) return false;
        if (!document.getElementById(elementID)) return false;
        var elem = document.getElementById(elementID);
        if (elem.movement) {
            clearTimeout(elem.movement);
        }
        if (!elem.style.left) {
            elem.style.left = "0px";
        }
        if (!elem.style.top) {
            elem.style.top = "0px";
        }
        var xpos = parseInt(elem.style.left);
        var ypos = parseInt(elem.style.top);
        if (xpos == final_x && ypos == final_y) {
            return true;
        }
        if (xpos < final_x) {
            var dist = Math.ceil((final_x - xpos) / 10);
            xpos = xpos + dist;
        }
        if (xpos > final_x) {
            var dist = Math.ceil((xpos - final_x) / 10);
            xpos = xpos - dist;
        }
        if (ypos < final_y) {
            var dist = Math.ceil((final_y - ypos) / 10);
            ypos = ypos + dist;
        }
        if (ypos > final_y) {
            var dist = Math.ceil((ypos - final_y) / 10);
            ypos = ypos - dist;
        }
        elem.style.left = xpos + "px";
        elem.style.top = ypos + "px";
        var repeat = "moveElement('" + elementID + "'," + final_x + "," + final_y + "," + interval + ")";
        elem.movement = setTimeout(repeat, interval);
    }

    function classNormal(iFocusBtnID, iFocusTxID) {
        var iFocusBtns = $_(iFocusBtnID).getElementsByTagName('li');
        //var iFocusTxs = $_(iFocusTxID).getElementsByTagName('li');
        for (var i = 0; i < iFocusBtns.length; i++) {
            iFocusBtns[i].className = 'normal';
            //iFocusTxs[i].className='normal';
        }
    }

    function classCurrent(iFocusBtnID, iFocusTxID, n) {
        var iFocusBtns = $_(iFocusBtnID).getElementsByTagName('li');
        //var iFocusTxs = $_(iFocusTxID).getElementsByTagName('li');
        iFocusBtns[n].className = 'current';
        //iFocusTxs[n].className='current';
        //gd_remen(n)
    }

    function iFocusChange() {
        if (!$_('ifocus')) return false;
        $_('ifocus').onmouseover = function() { atuokey = true };
        $_('ifocus').onmouseout = function() { atuokey = false };
        var iFocusBtns = $_('ifocus_btn').getElementsByTagName('li');
        var listLength = iFocusBtns.length;
        if (listLength == 0) return false;
        iFocusBtns[0].onmouseover = function() {
            moveElement('ifocus_piclist', 0, 0, 5);
            classNormal('ifocus_btn', 'ifocus_tx');
            classCurrent('ifocus_btn', 'ifocus_tx', 0);
        }
        if (listLength >= 2) {
            iFocusBtns[1].onmouseover = function() {
                moveElement('ifocus_piclist', 0, -387, 5);
                classNormal('ifocus_btn', 'ifocus_tx');
                classCurrent('ifocus_btn', 'ifocus_tx', 1);
            }
        }
        if (listLength >= 3) {
            iFocusBtns[2].onmouseover = function() {
                moveElement('ifocus_piclist', 0, -774, 5);
                classNormal('ifocus_btn', 'ifocus_tx');
                classCurrent('ifocus_btn', 'ifocus_tx', 2);
            }
        }
        if (listLength >= 4) {
            iFocusBtns[3].onmouseover = function() {
                moveElement('ifocus_piclist', 0, -1161, 5);
                classNormal('ifocus_btn', 'ifocus_tx');
                classCurrent('ifocus_btn', 'ifocus_tx', 3);
            }
        }
    }

    setInterval('autoiFocus()', 8000);
    var atuokey = false;
    function autoiFocus() {
        if (!$_('ifocus')) return false;
        if (atuokey) return false;
        var focusBtnList = $_('ifocus_btn').getElementsByTagName('li');
        var listLength = focusBtnList.length;
        for (var i = 0; i < listLength; i++) {
            if (focusBtnList[i].className == 'current') var currentNum = i;
        }
        if (currentNum == 0 && listLength != 1) {
            moveElement('ifocus_piclist', 0, -387, 5);
            classNormal('ifocus_btn', 'ifocus_tx');
            classCurrent('ifocus_btn', 'ifocus_tx', 1);
        }
        if (currentNum == 1 && listLength != 2) {
            moveElement('ifocus_piclist', 0, -774, 5);
            classNormal('ifocus_btn', 'ifocus_tx');
            classCurrent('ifocus_btn', 'ifocus_tx', 2);
        }
        if (currentNum == 2 && listLength != 3) {
            moveElement('ifocus_piclist', 0, -1161, 5);
            classNormal('ifocus_btn', 'ifocus_tx');
            classCurrent('ifocus_btn', 'ifocus_tx', 3);
        }
        if (currentNum == 3) {
            moveElement('ifocus_piclist', 0, 0, 5);
            classNormal('ifocus_btn', 'ifocus_tx');
            classCurrent('ifocus_btn', 'ifocus_tx', 0);
        }
        if (currentNum == 1 && listLength == 2) {
            moveElement('ifocus_piclist', 0, 0, 5);
            classNormal('ifocus_btn', 'ifocus_tx');
            classCurrent('ifocus_btn', 'ifocus_tx', 0);
        }
        if (currentNum == 2 && listLength == 3) {
            moveElement('ifocus_piclist', 0, 0, 5);
            classNormal('ifocus_btn', 'ifocus_tx');
            classCurrent('ifocus_btn', 'ifocus_tx', 0);
        }
    }
    //addLoadEvent(iFocusChange);

    $(document).ready(function() { iFocusChange(); });    
</script>


<style type="text/css">
.remen_xiaotu_ul a{ display:block; border:1px solid #ccc;padding:1px;}
.remen_xiaotu_ul a:hover{  border:1px solid #70BAEB;}
.remen_xiaotu_ul a.dq{ border:1px solid #70BAEB;}

.remen_kz{ background-image:url('/images/un_icon_arrow.png');background-position: 500px 500px;background-repeat: no-repeat;}
.remen_prev{height:278px;width:30%;left:0;top:0; position:absolute; border:0px;}
.remen_next{height: 278px; width: 60%; right: 0; top:0; position: absolute;border:0px;}
.remen_prev:hover{ background-position:-46px 50%}
.remen_next:hover{ background-position:250px 50%}
</style>

<div class="R_basicbox margin_T16">
    <div class="basic_rightT">
        <h5>
            热门船票</h5>
    </div>
    <div id="ifocus">
        <div id="ifocus_box">
            <ul style="top: 0px; left: 0px;" id="ifocus_piclist">
                <asp:Repeater ID="rptList_tj_top" runat="server">
                    <ItemTemplate>
                        <li>
                            <div class="R_side_01 fixed">
                                <dl>
                                    <dt><span>特别推荐</span><a target="_blank" href="<%=HQURL %><%# Eval("HangQiId")%>.html" target="_blank"><strong><%# Eval("MingCheng")%></strong></a></dt>
                                    <dd class="date">
                                        <span>出发时间：</span><%# ChuGangTimeHtml(Eval("RiQis"),"top")%></dd>
                                    <dd>
                                        <a href="<%=HQURL %><%# Eval("HangQiId")%>.html" class="yudin_pic" target="_blank">
                                            &nbsp;<%#Eval("QiShiJiaGe","{0:C2}")%><em>起</em></a></dd>
                                    <dd class="person">
                                        <span>
                                            <%# EyouSoft.Common.Utils.GetInt(Eval("XiaoLiang1").ToString()) > 0 ? Eval("XiaoLiang1") : "0"%>
                                        </span> 人已经预订，下手要快哦</dd>
                                </dl>
                                <div class="R_area">
                                    <div class="big_img" style="position: relative">
                                        <ul class="img_ul">
                                            <%# FujianHtml(Eval("FuJians"),"top",Eval("HangQiId"))%>
                                        </ul>
                                        <a href="javascript:void(0)" class="remen_kz remen_prev"></a>
                                        <a href="javascript:void(0)" class="remen_kz remen_next"></a>
                                    </div>
                                    <div class="small_img" style="overflow: hidden">
                                        <span class="jiantou_L i_remen_left" style="cursor: pointer;"></span><span class="jiantou_R i_remen_right"
                                            style="cursor: pointer;"></span>
                                        <div style="width: 560px;">
                                        <ul style="width: auto;" i_hangqiid="<%#Eval("HangQiId") %>" class="remen_xiaotu_ul">
                                            <%# FujianHtml(Eval("FuJians"), "down", Eval("HangQiId"))%>
                                        </ul>
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div id="ifocus_btn">
            <ul>
                <asp:Literal ID="txtHangQiName" runat="server"></asp:Literal>
            </ul>
        </div>
    </div>
    <div class="R_side_02">
        <ul>
            <asp:Repeater ID="rptList_tj_down" runat="server">
                <ItemTemplate>
                    <li>
                        <div class="line_leftT">
                            <a target="_blank" href="<%=HQURL %><%# Eval("HangQiId")%>.html" title="<%# Eval("MingCheng")%>">
                                <%# Eval("MingCheng")%></a><p>
                                    出发时间：<em><%# ChuGangTimeHtml(Eval("RiQis"), "down")%></em></p>
                        </div>
                        <div class="line_rightbtn">
                            <span>
                                <%#Eval("QiShiJiaGe","{0:C2}")%>
                            </span><a target="_blank" href="<%=HQURL %><%# Eval("HangQiId")%>.html">立即预订</a></div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        <div class="chakan_more">
            <a target="_blank" href="/HangQi/chaxun.aspx?lx=<%=(int)LeiXing %>">查看更多 +</a></div>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function() {
        $(".i_remen_left").click(function() {
            var ul = $(this).closest("div").find("ul");
            $(ul).stop();
            ul.animate({ marginLeft: "-110px" }, 1000, "swing", function() {
                $(this).css({ marginLeft: "0px" }).find("li:first").appendTo(this);
            });
        });

        $(".i_remen_right").click(function() {
            var ul = $(this).closest("div").find("ul");
            $(ul).stop();
            ul.find("li:last").prependTo(ul);
            ul.css({ marginLeft: "-110px" });
            ul.animate({ marginLeft: "0px" }, 1000, "swing", function() { });
        });

        $(".i_remen_img_2").click(function() {
            var _self = this;
            var _img = $("#img_remen_" + $(_self).closest("ul").attr("i_hangqiid"));
            _img.fadeOut("fast", function() { _img.attr("src", $(_self).attr("data-src")); _img.fadeIn("slow"); });
            $(_self).closest("ul").find("a").removeClass("dq");
            $(_self).closest("a").addClass("dq");
        });

        //gd_remen(0)
        $(".remen_xiaotu_ul").each(function() { $(this).find("a").eq(0).addClass("dq"); });
        $(".remen_prev").click(function() { prev_remen(this); });
        $(".remen_next").click(function() { next_remen(this); });
    });

    var timer_remen = null;

    function gd_remen(index) {
        clearInterval(timer_remen);
        var li = $("#ifocus_piclist").children("li").eq(index);
        var ul = li.find("ul").eq(1);

        var count = ul.find("a").length;
        if (count == 0) return;

        if (ul.find("a.dq").length == 0) {
            ul.find("li").eq(0).find("a").addClass("dq");
            ul.find("li").eq(0).find("img").click();
        }

        timer_tejia = setInterval(function() {
            var dq = ul.find("a.dq");
            var dqindex = ul.find("a").index(dq);
            var li1 = dq.closest("li").next();

            if (count <= 4 && dqindex == count - 1) li1 = ul.find("li").eq(0);

            dq.removeClass("dq");
            li1.find("img").click();

            if (count > 4 && dqindex == 2) {
                ul.animate({ marginLeft: "-110px" }, 1000, "swing", function() {
                    $(this).css({ marginLeft: "0px" }).find("li:first").appendTo(this);
                });
            }

        }, 3000);
    }

    function prev_remen(obj) {
        var li = $(obj).closest("li");
        var ul = li.find("ul").eq(1);

        var count = ul.find("a");
        var dq = ul.find("a.dq");
        var dqindex = ul.find("a").index(dq);
        if (dqindex == 0) {
            ul.stop();
            ul.find("li:last").prependTo(ul);
            ul.css({ marginLeft: "-110px" });
            ul.animate({ marginLeft: "0px" }, 1000, "swing", function() { });
        }

        var li1 = dq.closest("li").prev();

        dq.removeClass("dq");
        li1.find("img").click();
    }

    function next_remen(obj) {
        var li = $(obj).closest("li");
        var ul = li.find("ul").eq(1);

        var count = ul.find("a").length;
        var dq = ul.find("a.dq");
        var dqindex = ul.find("a").index(dq);

        var li1 = dq.closest("li").next();
        if (count <= 4 && dqindex == count - 1) li1 = ul.find("li").eq(0);

        if (dqindex < 0) li1 = ul.find("li").eq(0);

        if (count > 4 && dqindex >= 2) {
            ul.stop();
            ul.animate({ marginLeft: "-110px" }, 1000, "swing", function() {
                $(this).css({ marginLeft: "0px" }).find("li:first").appendTo(this); dq.removeClass("dq");
                li1.find("img").click();
            });
        } else {
            dq.removeClass("dq");
            li1.find("img").click();
        }
    }
</script>

