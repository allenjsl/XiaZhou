<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/M1.Master" AutoEventWireup="true"
    CodeBehind="ChuanZhi.aspx.cs" Inherits="EyouSoft.YlWeb.Youlun.ChuanZhi" %>
<%@ MasterType VirtualPath="~/MasterPage/M1.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <div class="step_mainbox">
        <div class="main fixed">
            <div class="C_left">
                <div class="left_sub">
                    <div class="left_T left_T02">
                        <h5>
                            <asp:Label ID="lblName" runat="server" Text=""></asp:Label></h5>
                    </div>
                    <ul class="xilie_menu xilie_menu02">
                        <asp:Repeater ID="rptxilies" runat="server">
                            <ItemTemplate>
                                <li><a href="javascript:;">
                                    <%# Eval("MingCheng")%></a>
                                    <%# getChuanzhiByXilie(Eval("CompanyId").ToString(), Eval("XiLieId").ToString())%>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="left_sub margin_T16">
                    <div class="left_T left_T02">
                        <h5>
                            热销排行榜</h5>
                    </div>
                    <div style="height: 500px; overflow: hidden;">
                        <ul id="i_ul_rexiao" class="list_paihang">
                            <asp:Repeater ID="rpt_Hot" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div class="L_img">
                                            <a href="/Hangqi/HaiYangInfo.aspx?id=<%# Eval("HangQiId")%>">
                                                <%#GetReXiaoImg(Eval("FuJians")) %>
                                            </a>
                                        </div>
                                        <dl>
                                            <dt><a href="/Hangqi/HaiYangInfo.aspx?id=<%# Eval("HangQiId")%>">
                                                <%# Eval("MingCheng")%></a></a></dt>
                                            <dd>
                                                <%#Eval("QiShiJiaGe","{0:C2}")%>/人</dd>
                                        </dl>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div class="caozuo_area left_sub">
                        <a id="caozuo_area_down" href="javascript:">
                            <img src="/images/y_jiantouB.png"></a><a id="caozuo_area_up" href="javascript:void(0);"
                                style="background: #D0D2D1;"><img src="/images/y_jiantouT.png"></a>
                    </div>

                    <script type="text/javascript">
                        $(document).ready(function() {

                            $("#caozuo_area_down").click(function() {
                                var ul = $("#i_ul_rexiao");
                                $(ul).stop();
                                ul.animate({ marginTop: "-100px" }, 1000, "swing", function() {
                                    $(this).css({ marginTop: "0px" }).find("li:first").appendTo(this);
                                });
                            });

                            $("#caozuo_area_up").click(function() {
                                var ul = $("#i_ul_rexiao");
                                $(ul).stop();
                                ul.find("li:last").prependTo(ul);
                                ul.css({ marginTop: "-100px" });
                                ul.animate({ marginTop: "0px" }, 1000, "swing", function() { });
                            });
                        });
                    </script>

                </div>
            </div>
            <div class="C_right">
                <div class="Bright_box">
                    <div class="right_T">
                        <asp:Label ID="lblCZ" runat="server" Text=""></asp:Label></div>
                    <div class="company_txt">
                        <p>
                            <asp:Literal ID="litInfo" runat="server"></asp:Literal>
                        </p>
                    </div>
                    <div class="company_txt2">
                        <asp:Label ID="lblimg" runat="server" Text=""></asp:Label>
                        <p>
                            <b class="font14">基本资料</b></p>
                        <p>
                            总吨位：<asp:Label ID="lblzongdunwei" runat="server" Text=""></asp:Label></p>
                        <p>
                            载客量：<asp:Label ID="lblzaikeliang" runat="server" Text=""></asp:Label></p>
                        <p>
                            首航日期：<asp:Label ID="lblshouhang" runat="server" Text=""></asp:Label></p>
                        <p>
                            长度：<asp:Label ID="lblchangdu" runat="server" Text=""></asp:Label></p>
                        <p>
                            宽度：<asp:Label ID="lblkuandu" runat="server" Text=""></asp:Label></p>
                        <p>
                            甲板楼层：<asp:Label ID="lbllouceng" runat="server" Text=""></asp:Label></p>
                        <p>
                            客房数量：<asp:Label ID="lblfangjian" runat="server" Text=""></asp:Label></p>
                        <p>
                            吃水：<asp:Label ID="lblchishui" runat="server" Text=""></asp:Label></p>
                        <p>
                            船速：<asp:Label ID="lblhangsu" runat="server" Text=""></asp:Label></p>
                    </div>
                </div>
                <div class="Bright_box margin_T16">
                    <div class="company_tab">
                        <ul>
                            <li><a href="#fangxing" class="current">房型介绍</a></li>
                            <li><a href="#meishi">美食介绍</a></li>
                            <li><a href="#sheshi">娱乐及服务设施</a></li>
                            <li><a href="#pingmian">平面图</a></li>
                            <li><a href="#youlun">游轮视频</a></li>
                            <li><a href="#hangxian">航线介绍</a></li>
                        </ul>
                    </div>
                    <div class="company_cont">
                        <a name="fangxing"></a>
                        <ul class="fangxinjs">
                            <asp:Repeater ID="rptfangxings" runat="server">
                                <ItemTemplate>
                                    <li style="height:auto; overflow:hidden;">
                                        <div class="fangxin_img" data-fangxingid="<%#Eval("FangXingId") %>">
                                            <a href="javascript:;">
                                                <img src="<%# EyouSoft.YlWeb.TuPian.F1(ErpFilepath+Eval("Filepath").ToString(),300,200)%>" /></a></div>
                                        <dl>
                                            <dt>
                                                <%#  Eval("MingCheng")%></dt>
                                            <dd>
                                                房间数量：
                                                <%#  Eval("ShuLiang")%>间</dd>
                                            <dd>
                                                房间面积：
                                                <%#  Eval("MianJi")%>㎡</dd>
                                            <dd>
                                                所在楼层：
                                                <%#  Eval("LouCeng")%><%#  Eval("LouCeng").ToString().IndexOf("楼")>-1?"":"楼"%></dd>
                                            <dd>
                                                房型结构：
                                                <%#  Eval("JieGou")%></dd>
                                            <dd>
                                                床位配置：
                                                <%#  Eval("ChuangWeiPeiZhi")%></dd>
                                            <dd>
                                                客房设施：
                                                <%#EyouSoft.Common.Utils.InputText(Eval("SheShi").ToString())%></dd>
                                            <dd>
                                                <!--<em>-->
                                                用品介绍：<!--</em><span>-->
                                                <%#  EyouSoft.Common.Utils.InputText(Eval("YongPin").ToString())%><!--</span>--></dd>
                                        </dl>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        &nbsp;
                    </div>
                    <div class="company_cont">
                        <a name="meishi"></a>
                        <div class="right_T right_T03">
                            美食介绍</div>
                        <div class="meishijs">
                            <ul>
                                <asp:Repeater ID="rptmeishis" runat="server">
                                    <ItemTemplate>
                                        <li><a href="javascript:;" class="fangxin_img" data-fangxingid="<%#Eval("meishiid") %>">
                                            <img src="<%# EyouSoft.YlWeb.TuPian.F1(ErpFilepath+Eval("Filepath").ToString(),263,190)%>" /><span class="name"><%# Eval("MingCheng")%></span><span
                                                class="title"><%# EyouSoft.Common.Utils.GetText(Eval("MiaoShu").ToString(), 30, true)%></span></a></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                    <div class="company_cont">
                        <a name="sheshi"></a>
                        <div class="right_T right_T03">
                            娱乐及服务设施</div>
                        <div class="meishijs">
                            <ul>
                                <asp:Repeater ID="rptsheshis" runat="server">
                                    <ItemTemplate>
                                        <li><a href="javascript:;" class="fangxin_img" data-fangxingid="<%#Eval("sheshiid") %>">
                                            <img src="<%# EyouSoft.YlWeb.TuPian.F1(ErpFilepath+Eval("Filepath").ToString(),263,190)%>" /><span class="name"><%# Eval("MingCheng")%></span><span
                                                class="title"><%# EyouSoft.Common.Utils.GetText(Eval("MiaoShu").ToString(), 30, true)%></span></a></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                    <div class="company_cont">
                        <a name="pingmian"></a>
                        <div class="right_T right_T03">
                            平面图</div>
                        <div class="pinmiantu hy_pinmiantu fixed">
                            <asp:Repeater ID="rptpingmians" runat="server">
                                <ItemTemplate>
                                    <p style="display:none"><img id="pingmiantu<%#Container.ItemIndex+1 %>" src="<%#  getImgPath(Eval("Filepath").ToString()) %>" /></p>
                                </ItemTemplate>
                            </asp:Repeater>
                            <%if (this.rptpingmians.Items.Count > 0) %>
                            <%{ %>
                            <div class="floor" style="position:fixed;top:auto;">
						   <ul>
						   <%for(var i=1;i<=this.rptpingmians.Items.Count;i++) %>
						   <%{ %>
						      <li><a href="#pingmiantu<%=i %>">第<%=i+1 %>层</a></li>
						      <%} %>
						   </ul>
						</div>
						<%} %>
                        </div>
                    </div>
                    <div class="company_cont">
                        <a name="youlun"></a>
                        <div class="right_T right_T03">
                            游轮视频</div>
                        <div class="youlunvideo fixed">
                            <asp:Literal ID="litShiPin" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="company_cont borderbot">
                        <a name="hangxian"></a>
                        <div class="right_T right_T03">
                            航线介绍</div>
                        <ul class="hangxianjs">
                            <asp:Repeater ID="rpthangxian" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div class="hanxian_img">
                                            <a href="javascript:;"><%# GetReXiaoImg1(Eval("FuJians"))%></a></div>
                                        <div class="hanxian_R">
                                            <div class="hanxian_T">
                                                <%# Eval("MingCheng")%></div>
                                            <ul class="hanxian_list">
                                                <li>
                                                    <label>
                                                        航线性质：</label><%# Eval("HangXianXingZhi")%></li>
                                                <li>
                                                    <label>
                                                        邮轮公司：</label><%# Eval("GongSiName")%></li>
                                                <li>
                                                    <label>
                                                        行程天数：</label><%# Eval("TianShu1")%>天<%# Eval("TianShu2")%>晚</li>
                                                <li>
                                                    <label>
                                                        船只名称：</label><%# Eval("ChuanZhiName")%></li>
                                                <li>
                                                    <label>
                                                        登船港口：</label><%# Eval("ChuFaGangKouMingCheng")%></li>
                                                <li>
                                                    <label>
                                                        下船港口：</label><%# Eval("DiDaGangKouMingCheng")%></li>
                                                <li class="width100">
                                                    <label>
                                                        途经城市：</label><%# Eval("TuJingChengShi")%></li>
                                            </ul>
                                            <div class="Rbox">
                                                <span>价格：<em><%# Eval("QiShiJiaGe","{0:C0}")%></em> 起</span> <a target="_blank" href="/Hangqi/HaiYangInfo.aspx?id=<%# Eval("HangQiId")%>">
                                                    立即预订</a>
                                            </div>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    

    <script type="text/javascript">  
            var iPage = {
                chuanzhiid:'<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>',
                _tabStatus: -1,
                _setTab: function(_$tab, _$tablis, _$tabli, _status, _top) {
                    if (_status == this._tabStatus) return;
                    if (_status == -1) {
                        _$tab.css({ "position": "static" });
                        _$tablis.siblings().find("a").removeClass("current");
                        _$tabli.find("a").addClass("current");
                    } else {
                        _$tablis.siblings().find("a").removeClass("current");
                        _$tabli.find("a").addClass("current");
                        if ($.browser.msie && $.browser.version == "6.0") {
                            _$tab.css({ "position": "absolute" });
                            _$tab.addClass("fixed-top");
                        }
                        else _$tab.css({ "position": "fixed", "top": 0 });
                    }

                    this._tabStatus = _status;
                },
                //设置平面图显示
                SetPianMianTu:function(o){
                    $(o).closest("div").find("p:not(:hidden)").hide();
                    $(o).closest("p").show();
                },
                iscroll: function() {
                    var _$tab = $(".company_tab");
                    var _$tablis = _$tab.find("li");
                    var _tabtop = _$tab.offset().top;
                    var _$infos = $(".company_cont");
                    var _infotop = [];
                    var _pingmiantutop=[];
                    
                    for(var i=0;i<$(".pinmiantu img").length;i++)_pingmiantutop.push($(".pinmiantu img").eq(i).offset().top - 70);

                    for (var i = 0; i < 6; i++) _infotop.push(_$infos.eq(i).offset().top - 70);
                    //if ($.browser.msie && $.browser.version == "6.0") return false;
                    var _self = this;
                    $(window).scroll(function() {
                        var _wtop = $(window).scrollTop();
                        var _top = _wtop - _tabtop;
                        if (_wtop > _infotop[5]) {
                            _self._setTab(_$tab, _$tablis, _$tablis.eq(5), 5, _top);
                        } else if (_wtop > _infotop[4]) {
                            _self._setTab(_$tab, _$tablis, _$tablis.eq(4), 4, _top);
                        } else if (_wtop > _infotop[3]) {
                            _self._setTab(_$tab, _$tablis, _$tablis.eq(3), 3, _top);
                        } else if (_wtop > _infotop[2]) {
                            _self._setTab(_$tab, _$tablis, _$tablis.eq(2), 2, _top);
                        } else if (_wtop > _infotop[1]) {
                            _self._setTab(_$tab, _$tablis, _$tablis.eq(1), 1, _top);
                        } else if (_wtop > _infotop[0]) {
                            _self._setTab(_$tab, _$tablis, _$tablis.eq(0), 0, _top);
                        } else if (_wtop < _infotop[0]) {
                            _self._setTab(_$tab, _$tablis, _$tablis.eq(0), -1, _top);
                        }
                        //平面图
                        if(_wtop > _infotop[3]){
                            $(".floor").css({"top":"100px"})
//                            for(var i=0;i<_pingmiantutop.length;i++){
//                                if(_wtop>_pingmiantutop[i]){
//                                    $(".floor_on").removeClass();
//                                    $(".floor ul li a").eq(i).addClass("floor_on");
//                                }
//                            }
                        }else{
                            $(".floor").css({"top":"auto"})
                        }
                    });
                }
            };

            $(document).ready(function() { 
                var index=0;//房型附件焦点图index
                var len=0;//房型附件个数
                var pagesize=6;//房型附件pagesize
                var pagecount=0;//房型附件pagecount
                var pageindex=1;//房型附件pageindex
                var spwidth=163;//小图width
                iPage.iscroll(); 
                $(".fangxin_img").click(function() {
                    $.ajax({
                            type: "get",
                            cache: true,
                            async:false,
                            url: '/YouLun/ChuanZhiCJ.aspx?do=getchuanzhifangxingfujian&fangxingid=' + $(this).attr("data-fangxingid"),
                            dataType: "html",
                            success: function(html) {
                                showZZ(html);
                                index=0;
                                len=$(".small_img ul li").length;
                                pagecount=Math.ceil(len/pagesize);
                                pageindex=1;
                                showPics(index);
                            },
                            error: function() {
                                tableToolbar._showMsg(tableToolbar.errorMsg);
                            }
                    });
                });
	            //上一页、下一页按钮透明度处理
	            $(".R_area div span").css("opacity",0.5).live("hover",function() {
		            $(this).animate({"opacity":"1.0"},300);
	            },function() {
		            $(this).animate({"opacity":"0.5"},300);
	            });
                //焦点图下一张
                $(".big_jtL").live("click",function(){
                    index-=1;
                    if(index==-1){index=len-1;}
                    showPics(index);
                });
                //焦点图上一张
                $(".big_jtR").live("click",function(){
                    index+=1;
                    if(index==len){index=0;}
                    showPics(index);
                });
                //小图下一页
                $(".small_jtL").live("click",function(){
                    pageindex-=1;
                    if(pageindex<=0){pageindex=pagecount;}
                    initposition((pageindex-1)*pagesize);
                });
                //小图上一页
                $(".small_jtR").live("click",function(){
                    pageindex+=1;
                    if(pageindex>pagecount){pageindex=1;}
                    initposition((pageindex-1)*pagesize);
                });
                //小图聚焦
                $(".small_img ul li").live("click",function(){
                    index=$(this).index();
                    showPics(index);
                })
	            //显示图片函数，根据接收的index值显示相应的内容
	            function showPics(i) { //普通切换
	                initposition(i);
	                var _li=$(".small_img ul li").eq(i);
		            $(".big_img a img").attr("src",_li.find("img").attr("src"));
		            $(".big_img a p").text(_li.find("p").text());
	            }
	            //初始化小图位置
	            function initposition(i){
	                if(len<=pagesize)return false;
		            var _nowLeft = -i*spwidth; //根据index值计算ul元素的left值
		            $(".small_img ul").animate({"margin-Left":_nowLeft},300); //通过animate()调整ul元素滚动到计算出的position
	            }
	            
	            //平面图楼层click
	            $(".floor ul li a").live("click",function(){
	                $(".floor_on").removeClass();
	                $(this).addClass("floor_on");
	                iPage.SetPianMianTu($(".pinmiantu img").eq($(this).closest("ul").find("li a").index(this)));
	            })
	            $(".floor ul li a").eq(0).trigger("click");
            })            
    </script>

    <style type="text/css">
        * html,* html body{background-image:url(about:blank);background-attachment:fixed;}
        * html .fixed-top{position:absolute;bottom:auto;top:expression(eval(document.documentElement.scrollTop));}
    </style>
</asp:Content>
