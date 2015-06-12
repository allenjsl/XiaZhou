<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="EyouSoft.YlWeb.Index" Title="首页" MasterPageFile="~/MasterPage/Boxy.Master"%>
<%@ OutputCache Duration="3600" VaryByParam="*" %>
<%@ MasterType VirtualPath="~/MasterPage/Boxy.Master" %>
<%@ Register Src="/UserControl/FLDH.ascx" TagName="FLDH" TagPrefix="uc3" %>
<asp:Content ID="ctHead" ContentPlaceHolderID="PageHead" runat="server">
    <!--[if IE]><script src="/js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->
    <script type="text/javascript" src="/Js/bt.min.js"></script>

<script type="text/javascript">
    function nTabs(tabObj, obj) {
        var tabList = document.getElementById(tabObj).getElementsByTagName("li");
        for (i = 0; i < tabList.length; i++) {
            if (tabList[i].id == obj.id) {
                $("#tabObj" + _Title + "i").addClass("active").removeClass("normal");
                //document.getElementById(tabObj+"_Title"+i).className = "active"; 
                document.getElementById(tabObj + "_Content" + i).style.display = "block";
            } else {
                $("#tabObj" + _Title + "i").addClass("normal").removeClass("active");
                //document.getElementById(tabObj+"_Title"+i).className = ""; 
                document.getElementById(tabObj + "_Content" + i).style.display = "none";
            }
        }
    }
    //左侧导航
    $(document).ready(function() {
        $('.L_side01box > li').mousemove(function() {
            $(this).find('.sub-menu').show();
            if ($.browser.msie) {
                if (parseFloat($.browser.version) <= 6) {
                    $(this).find('.sub-menu').bgiframe();
                }
            }
            $(this).addClass('curr');
        });
        $('.L_side01box > li').mouseleave(function() {
            $(this).find('.sub-menu').hide();
            $(this).removeClass('curr');
        });
    });
</script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PageIndex" runat="server">
<style type="text/css">
*{margin:0;padding:0;}
body{font-size:12px;color:#222;font-family:Verdana,Arial,Helvetica,sans-serif;background:#f0f0f0;overflow-x:hidden;}
.clearfix:after{content: ".";display: block;height: 0;clear: both;visibility: hidden;}
.clearfix{zoom:1;}
ul,li{list-style:none;}
img{border:0;}
.wrapper{width:800px;margin:0 auto;padding-bottom:50px;}
h1{height:50px;line-height:50px;font-size:22px;font-weight:normal;font-family:"Microsoft YaHei",SimHei;margin-bottom:20px;}
/* focus */
#focus{width:845px;height:430px;overflow:hidden;position:relative;}
#focus ul{height:430px;position:absolute;}
#focus ul li{float:left;width:845px;height:430px;overflow:hidden;position:relative;background:#000;}
#focus ul li div{position:absolute;overflow:hidden;}
#focus .btnBg{position:absolute;width:845px;height:31px;left:0;bottom:0;background:#000;}
#focus .btn{position:absolute;width:780px;height:15px;padding:5px 10px;right:0;bottom:0;text-align:right;}
#focus .btn span{display:inline-block;_display:inline;_zoom:1;width:25px;height:10px;_font-size:0;margin-left:5px;cursor:pointer;background:#fff;}
#focus .btn span.on{background:#fff;}
#focus .preNext{width:45px;height:100px;position:absolute;top:150px;background:url(images/sprite.png) no-repeat 0 0;cursor:pointer;}
#focus .pre{left:0;}
#focus .next{right:0;background-position:right top;}
</style>
<script type="text/javascript">
    $(function() {
        var sWidth = $("#focus").width(); //获取焦点图的宽度（显示面积）
        var len = $("#focus ul li").length; //获取焦点图个数
        var index = 0;
        var picTimer;

        //以下代码添加数字按钮和按钮后的半透明条，还有上一页、下一页两个按钮
        var btn = "<div class='btnBg'></div><div class='btn'>";
        for (var i = 0; i < len; i++) {
            btn += "<span></span>";
        }
        btn += "</div><div class='preNext pre'></div><div class='preNext next'></div>";
        $("#focus").append(btn);
        $("#focus .btnBg").css("opacity", 0.5);

        //为小按钮添加鼠标滑入事件，以显示相应的内容
        $("#focus .btn span").css("opacity", 0.4).mouseover(function() {
            index = $("#focus .btn span").index(this);
            showPics(index);
        }).eq(0).trigger("mouseover");

        //上一页、下一页按钮透明度处理
        $("#focus .preNext").css("opacity", 0.2).hover(function() {
            $(this).stop(true, false).animate({ "opacity": "0.5" }, 300);
        }, function() {
            $(this).stop(true, false).animate({ "opacity": "0.2" }, 300);
        });

        //上一页按钮
        $("#focus .pre").click(function() {
            index -= 1;
            if (index == -1) { index = len - 1; }
            showPics(index);
        });

        //下一页按钮
        $("#focus .next").click(function() {
            index += 1;
            if (index == len) { index = 0; }
            showPics(index);
        });

        //本例为左右滚动，即所有li元素都是在同一排向左浮动，所以这里需要计算出外围ul元素的宽度
        $("#focus ul").css("width", sWidth * (len));

        //鼠标滑上焦点图时停止自动播放，滑出时开始自动播放
        $("#focus").hover(function() {
            clearInterval(picTimer);
        }, function() {
            picTimer = setInterval(function() {
                showPics(index);
                index++;
                if (index == len) { index = 0; }
            }, 4000); //此4000代表自动播放的间隔，单位：毫秒
        }).trigger("mouseleave");

        //显示图片函数，根据接收的index值显示相应的内容
        function showPics(index) { //普通切换
            var nowLeft = -index * sWidth; //根据index值计算ul元素的left值
            $("#focus ul").stop(true, false).animate({ "left": nowLeft }, 300); //通过animate()调整ul元素滚动到计算出的position
            //$("#focus .btn span").removeClass("on").eq(index).addClass("on"); //为当前的按钮切换到选中的效果
            $("#focus .btn span").stop(true, false).animate({ "opacity": "0.4" }, 300).eq(index).stop(true, false).animate({ "opacity": "1" }, 300); //为当前的按钮切换到选中的效果
        }
    });

</script>
<div class="index-banner">
    <div class="index-bannerbg"></div>
    <img src="../images/index_benner.jpg" />
</div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <form id="form1" runat="server">
   
     <div class="mainbox">
     
       <div class="index-box fixed">
       
          <div class="index-fenlei floatL">
          
					<div class="i-n4Tab" id="n4Tab">
					  <div class="TabTitle">
						<ul>
						  <li id="n4Tab_Title0" onclick="nTabs('n4Tab',this);"><a href="javascript:void(0);">长江游轮</a></li>
						  <li id="n4Tab_Title1" onclick="nTabs('n4Tab',this);"><a href="javascript:void(0);">海洋游轮</a></li>
						</ul>
					  </div>
					  <div class="TabContent">
					  
						<div id="n4Tab_Content0">
						  <uc3:FLDH ID="FLDH1" runat="server" YouLunLeiXing="长江游轮" IsIndex="true" />
						</div>
						
						<div id="n4Tab_Content1" class="none">
				 			 <uc3:FLDH ID="FLDH2" runat="server" YouLunLeiXing="海洋邮轮" IsIndex="true" />
                        
						</div>
						
					  </div>
					</div>
          
          </div>
          
          <div id="focus" class="index-focus floatR">
			  <ul>
			  <asp:Repeater ID="rptGuangGao" runat="server">
			  <ItemTemplate>
				 <li>
				   <a href="<%#Eval("Url") %>" target="_blank">
				   <%--<div class="i-price">¥<i>2947</i></div>--%>
				   <img src="<%#EyouSoft.Common.Utils.GetErpFilepath() + Eval("Filepath") %>" alt="<%#Eval("XiangXiNeiRong") %>"/>
				   <div class="i-txt"><%#Eval("MingCheng")%></div>
				   </a>
				 </li>
			  </ItemTemplate>
			  </asp:Repeater>
			  </ul>
		  </div>
		  
       </div>
	   
	   <div class="index-box fixed">
	      <div class="jinxuanbox floatL">
		  
		  <%foreach (var e in EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing))) %>
		  <%{ %>
		     <div class="jx_box">
			    <div class="basic_mainT"><h5><%=e.Text %>精选</h5></div>
				<%=this.GetJingXuan((EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)EyouSoft.Common.Utils.GetInt(e.Value),EyouSoft.Model.EnumType.YlStructure.HangQiBiaoQian.热门推荐,1) %>
				
				<ul class="i-linelist">
				   <%=this.GetJingXuan((EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)EyouSoft.Common.Utils.GetInt(e.Value), EyouSoft.Model.EnumType.YlStructure.HangQiBiaoQian.热门, 2)%>
				</ul>
				
			 </div>
			 <%} %>
			 			 
		  </div>
		  
		  <div class="i-paihang floatR">
				  <div class="basic_mainT"><h5>热销排行榜</h5></div>
                <div style="height: 334px; overflow: hidden; position: relative;" id="divHotSellList">
                
                </div>
				  <div class="caozuo_area"><a id="caozuo_area_down" href="javascript:;"><img src="../images/y_jiantouB.png" /></a><a id="caozuo_area_up" href="javascript:void(0);" style="background:#D0D2D1;"><img src="../images/y_jiantouT.png" /></a></div>
		  </div>
		  
	   </div>

     </div>
        
    </form>
<script type="text/javascript">
    $(document).ready(function() {
        $("#n4Tab_Title0").addClass("active").removeClass("normal");
        setNav(0);
        $("#caozuo_area_down").click(function() {
            var ul = $("#i_ul_rexiao");
            $(ul).stop();
            ul.animate({ marginTop: "-165px" }, 1000, "swing", function() {
                $(this).css({ marginTop: "0px" }).find("li:first").appendTo(this);
            });
        });

        $("#caozuo_area_up").click(function() {
            var ul = $("#i_ul_rexiao");
            $(ul).stop();
            ul.find("li:last").prependTo(ul);
            ul.css({ marginTop: "-160px" });
            ul.animate({ marginTop: "0px" }, 1000, "swing", function() { });
        });

        $.ajax({
            type: "get",
            cache: true,
            url: "/Ashx/GetHotSell.ashx?isindex=true",
            dataType: "html",
            success: function(ret) {
                $("#divHotSellList").html(ret);
            },
            error: function() {
            }
        });
        $('.youhuixinxi').bt({ contentSelector: function() { return $(this).next().html(); }, positions: ['right'], fill: '#FFF2B5', strokeStyle: '#D59228', noShadowOpts: { strokeStyle: "#D59228" }, spikeLength: 10, spikeGirth: 15, width: 500, overlap: 0, centerPointY: 1, cornerRadius: 4, shadow: true, shadowColor: 'rgba(0,0,0,.5)', cssStyles: { color: '#00387E', 'line-height': '180%'} });
    })
</script>
</asp:Content>
