<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChaXun.aspx.cs" Inherits="EyouSoft.YlWeb.Hangqi.ChaXun"
    MasterPageFile="~/MasterPage/Boxy.Master" Title="游轮航期" %>
<%@ MasterType VirtualPath="~/MasterPage/Boxy.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <input type="hidden" name="lx" id="lx" value="<%=LeiXing1 %>" />
    <div class="step_mainbox">
        <div class="basicT">您的位置：维诗达游轮 &gt; <%=LeiXing%>搜索</div>
        <div class="main fixed">
           <div class="leftside s_left">
                <div class="basic_leftT"><h5>分类搜索</h5></div>
                <div class="search-info fixed">
                   <span class="search-result"><i><label id="lblrecordCount"></label></i>条符合条件的线路</span>
                   <dl>
                      <dt>您已选择：</dt>
                      <dd class="empty"><a href="javascript:;">清空所有条件</a></dd>
                   </dl>
                </div>
                
		  		    <div id="youlunleixing" class="tg_nav1 fixed" style="padding:11px">
                      <label>游轮类型</label>
                      <dl>
                        <dt><a href="javascript:;" data-v="-1" class="<%="-1"==EyouSoft.Common.Utils.GetQueryStringValue("lx")?"on":"" %>">不限</a></dt>
                        <%foreach (var e in Enum.GetValues(typeof(EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing))) %>
                        <%{ %>
                                <dd><a href="javascript:;" data-v="<%=(int)e %>" class="<%=((int)e).ToString()==EyouSoft.Common.Utils.GetQueryStringValue("lx")?"on":"" %>"><%=e %></a></dd>
                                <%} %>
                      </dl>
                    </div>
		  		<div class="search_box1">
                    <div class="tg_nav1 fixed" data-cx="1" data-fl="hx" data-dx="0">
                      <label><%=S1%>航线</label>
                      <dl>
                        <dt data-v=""><a href="javascript:;" class="on">不限</a></dt>
                        <asp:Repeater runat="server" ID="rptHX">
                            <ItemTemplate>
                                <dd data-v="<%#Eval("XinXiId") %>"><a href="javascript:;"><%#Eval("MingCheng") %></a></dd>
                            </ItemTemplate>
                        </asp:Repeater>
                      </dl>

                    </div>
                    
                    <asp:PlaceHolder runat="server" ID="phxl">
                    <div class="tg_nav1 fixed" data-cx="1" data-fl="xl" data-dx="0">
                      <label><%=S1 %>系列</label>
                      <dl>
                        <dt data-v=""><a href="javascript:;" class="on">不限</a></dt>
                        <asp:Repeater runat="server" ID="rptXL">
                            <ItemTemplate>
                                <dd data-v="<%#Eval("XiLieId") %>"><a href="javascript:;"><%#Eval("MingCheng") %></a></dd>
                            </ItemTemplate>
                        </asp:Repeater>
                      </dl>
                    </div>
                    </asp:PlaceHolder>
                    
                    <asp:PlaceHolder runat="server" ID="phgs">
                    <div class="tg_nav1 fixed" data-cx="1" data-fl="gs" data-dx="0">
                      <label><%=S1 %>公司</label>
                      <dl>
                        <dt data-v=""><a href="javascript:;" class="on">不限</a></dt>
                        <asp:Repeater runat="server" ID="rptGS">
                            <ItemTemplate>
                                <dd data-v="<%#Eval("GongSiId") %>"><a href="javascript:;"><%#Eval("MingCheng") %></a></dd>
                            </ItemTemplate>
                        </asp:Repeater>
                      </dl>
                    </div>  
                    </asp:PlaceHolder>                  
                    
                    <div class="tg_nav1 fixed" data-cx="1" data-fl="yf" data-dx="0">
                      <label>出发月份</label>			  
                      <dl>
                        <dt data-v=""><a href="javascript:;" class="on">不限</a></dt>
                        <asp:Literal runat="server" ID="ltrYF"></asp:Literal>
                      </dl>
                    </div>
                    
                    <div class="tg_nav1 fixed" data-cx="1" data-fl="sj">
                      <label>出发时间</label>
                      <dl>
                        <dt data-v=""><a href="javascript:;" class="on">不限</a></dt>
                        <dd><input type="text" value="YY-MM-DD" class="inputbk formsize100" onfocus="WdatePicker(<%--{onpicked:chaXun()}--%>)"
                                <%--onchange="chaXun()" --%>style="margin-right:10px">~<input type="text" value="YY-MM-DD" class="inputbk formsize100" onfocus="WdatePicker(<%--{onpicked:chaXun()}--%>)"
                                <%--onchange="chaXun()"--%>><button class="search-btn" onclick="chaXun()">确 定</button><button class="search-btn" onclick="$(this).closest('dl').find('dt a').trigger('click')">清 空</button>
                        </dd>
                      </dl>
                    </div>
                    
                    <div class="tg_nav1 fixed" data-cx="1" data-fl="gk" data-dx="0">
                      <label><%=S2%></label>
                      <dl>
                        <dt data-v=""><a href="javascript:;" class="on">不限</a></dt>
                        <asp:Repeater runat="server" ID="rptGK">
                            <ItemTemplate>
                                <dd data-v="<%#Eval("XinXiId") %>"><a href="javascript:;"><%#Eval("MingCheng") %></a></dd>
                            </ItemTemplate>
                        </asp:Repeater>
                      </dl>
                    </div>
                    
                    <div class="tg_nav1 fixed" data-cx="1" data-fl="ts" data-dx="0">
                      <label>行程天数</label>			  
                      <dl>
                        <dt data-v=""><a href="javascript:;" class="on">不限</a></dt>
                        <dd data-v="1"><a href="javascript:;">1-3天</a></dd>
                        <dd data-v="2"><a href="javascript:;">4-5天</a></dd>
                        <dd data-v="3"><a href="javascript:;">6-7天</a></dd>
                        <dd data-v="4"><a href="javascript:;">8-9天</a></dd>
                      </dl>
                    </div>
                    
                    <div class="tg_nav1 fixed" data-cx="1" data-fl="jg" data-dx="0">
                      <label>价格区间</label>			   
                      <dl>
                        <dt data-v=""><a href="javascript:;" class="on">不限</a></dt>
                        <dd data-v="1"><a href="javascript:;">1000元以下</a></dd>
                        <dd data-v="2"><a href="javascript:;">1000-2000元</a></dd>
                        <dd data-v="3"><a href="javascript:;">2000-3000元</a></dd>
                        <dd data-v="4"><a href="javascript:;">3000-4000元</a></dd>
                        <dd data-v="5"><a href="javascript:;">4000-5000元</a></dd>
                        <dd data-v="6"><a href="javascript:;">5000元以上</a></dd>
                      </dl>
                    </div>
                    
		  		</div>
                
                
                
		  		<div class="S-paixu_box fixed margin_T16">
                <ul data-id="paixu" class="S-paixu_list">
                    <li data-v="2" data-s="1"><span>综合</span> | </li>
                    <li data-v="2" data-s="0"><span>销量<span></span></span></li>
                    <li data-v="3" data-s="0"><span class="on_up">价格<span></span></span></li>
                    <li data-v="4" data-s="0"><span>价格<span></span></span></li>
                </ul>
			  
                <div class="user_page_1" style="float: right; padding-top: 0px; height:22px;">
                    <div id="page_change1" style="width: 100%; text-align: right; margin: 0px auto 0px;
                        margin: 0; clear: both">
                    </div>
                </div>
                  
		  		</div>
                
                
		  		<div class="S_listL">
                    <ul class="S_box">
                    </ul>

                    <div style="padding-top: 20px;">
                        <div id="page_change" style="width: 100%; text-align: right; margin: 0px auto 0px;
                            margin: 0; clear: both">
                        </div>
                    </div>
				
			</div>                
                
                
           </div>
           <div class="rightside s_right">
                <%if(this.LeiXing==EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮) %>
                <%{ %>
                 <div class="basic_rightT"><h5>游轮航期</h5></div>
                 <div class="hq_riliboxR">
                    <ul>
                    <%for (var m = DateTime.Now; m <= DateTime.Now.AddMonths(11); m=m.AddMonths(1)) %>
                    <%{ %>
                       <li><a href="/HangQi.aspx?y=<%=m.Year %>&m=<%=m.Month %>" class="<%=DateTime.Now.Month==m.Month?"on":"" %>" onmouseover="<%=DateTime.Now.Month==m.Month?"":"$(this).addClass('on')" %>" onmouseout="<%=DateTime.Now.Month==m.Month?"":"$(this).removeClass()" %>"><%=string.Format("{0:00}",m.Month) %>月</a></li>
                    <%} %>
                    </ul>
                    <div class="clear"></div>
                 </div>
                 
                 <div class="basic_rightT margin_T16"><h5>热销排行榜</h5></div>
                 <%} %>
                 <%else %>
                 <%{ %>
                 <div class="basic_rightT"><h5>热销排行榜</h5></div>
                 <%} %>
                 
                 <div class="s-paihang margin_T16">
                   <ul id="ul_rx">
                   </ul>
                 </div>
                 
                 
           </div>
        
    </div>
    </div>

    <script type="text/javascript" src="/js/datepicker/wdatepicker.js"></script>
    <script type="text/javascript" src="/js/utilsuri.js"></script>
    <script type="text/javascript">
        function chaXun(pageIndex) {
            $(".S_box").html('<table width="100%" style="text-align:center"><tr><td><img src="../images/loadingnew.gif" /></td></tr></div>');
            var _params = {};
            _params["lx"] = $("#lx").val();

            $("div[data-cx='1']").each(function() {
                var _fl = $(this).attr("data-fl");
                var _s = [];
                var _dx = $(this).attr("data-dx");

                $(this).find("dt[data-s='1'],dd[data-s='1']").each(function() {
                    if ($(this).attr("data-v") != "")
                        _s.push($(this).attr("data-v"));
                });

                _params[_fl] = _s.join(",");
                if (_fl != "sj") _params[_fl + "1"] = _dx;
                else  {_params[_fl] = $(this).find("input").val();_params["sj1"] = $(this).find("input").eq(1).val();}
            });

            _params["px"] = $("ul[data-id='paixu']").find("li[data-s='1']").attr("data-v");

            if (_params["sj"] == "YY-MM-DD") _params["sj"] = "";
            if (_params["sj1"] == "YY-MM-DD") _params["sj1"] = "";
            if(_params["sj"]!=""||_params["sj1"]!="")$("input").closest("dl").find(".on").removeClass();
            _params["Page"]=pageIndex;
            _params["cz"] = $(".empty").closest("dl").find("dd[data-fl='cz']").attr("data-v");//'<%=EyouSoft.Common.Utils.GetQueryStringValue("cz") %>';
            _params["gjz"] = $(".empty").closest("dl").find("dd[data-fl='gjz']").attr("data-v");//'<%=EyouSoft.Common.Utils.GetQueryStringValue("gjz") %>';
            
            $.ajax({
                type: "get",
                cache: true,
                url: "/commonpage/sousuo.aspx",
                data:_params,
                dataType: "html",
                async:false,
                success: function(response) {
                    $(".S_box").html(response);
                    $("#lblrecordCount").text(pConfig.recordCount)
                }
            });
            
//            initList($("ul.S_box li").eq(0));

            return false;
        }

        function initChaXun() {
            var _params = utilsUri.getUrlParams([]);
            var _s=[];
            var _fl,_v,_title,_text,_div;
            
            for(var key in _params){
                _fl=key;
                _v=_params[key];
                _div=$(".search_box1 div[data-fl='"+_fl+"']");

                if($(_div).length>0){
                    _title=$(_div).find("label").text();
                    _text=$(_div).find("dl dd[data-v='"+_v+"']").text();
                    _s.push(getselect(_fl,_v,_title,_text));
                }
                //关键字
                if($.trim(_fl)=="gjz"&&$.trim(_v)!=""){
                    _s.push(getselect(_fl,_v,"关键字",decodeURIComponent(_v)));                    
                }
                //船只
                if($.trim(_fl)=="cz"&&$.trim(_v)!=""){
                    _s.push(getselect(_fl,_v,"船只",decodeURIComponent(_params["cn"])));                    
                }
            }

            $(_s.join("")).insertBefore(".empty");

            $("div[data-cx='1']").each(function() {
                var _fl = $(this).attr("data-fl");
                if (_params[_fl + "1"] == "1") {
                    $(this).attr("data-dx", "1");
                    $(this).find("a[data-dx='1']").text("单选-");
                }

                var _v = $.trim(_params[_fl]);
                if (_v.lenght == 0) return true;
                $(this).find("dt[data-v=''] a").removeClass("on")
                var _v1 = _v.split(',');
                for (var i = 0; i < _v1.length; i++) {
                    $(this).find("dt[data-v='" + _v1[i] + "']").attr("data-s", "1");
                    $(this).find("dt[data-v='" + _v1[i] + "'] a").addClass("on");
                    $(this).find("dd[data-v='" + _v1[i] + "']").attr("data-s", "1");
                    $(this).find("dd[data-v='" + _v1[i] + "'] a").addClass("on");
                }

                if (_fl == "sj") {
                    $(this).find("input").val(_params[_fl]);
                }
            });

            $('ul[data-id="paixu"] li').each(function() {
                if (typeof _params["px"] == "undefined") _params["px"] = "0";
                if ($(this).attr("data-v") == _params["px"]) {
                    $(this).closest("ul").find("li").attr("data-s", "0");
                    $(this).attr("data-s", "1");
                    $(this).css({ "font-weight": "bold" });
                    return false;
                }
            });
        }

        function initEvent() {
            $("a[data-dx='1']").live("click",function() {
                var _div = $(this).closest("div");
                if (_div.attr("data-dx") == "0") {
                    _div.attr("data-dx", "1");
                    $(this).text("单选-");
                } else {
                    _div.attr("data-dx", "0");
                    $(this).text("多选+");
                }
            });

            $("div[data-cx='1'] dl dt a,div[data-cx='1'] dl dd a").live("click",function() {
                var _div = $(this).closest("div");
                if (_div.attr("data-dx") == "0") {
                    _div.find("dt,dd").attr("data-s", "0");
                }
                if ($(this).closest("dt,dd").attr("data-v") == "") {
                    _div.find("dt,dd").attr("data-s", "0");
                    _div.find("input").val('');
                }
                $(this).closest("dt,dd").attr("data-s", 1);
                chaXun();
            });

            $('ul[data-id="paixu"] a').live("click",function() {
                var _ul = $(this).closest("ul");
                _ul.find("li").attr("data-s", "0");
                $(this).closest("li").attr("data-s", "1");
                chaXun();
            });
            
//            $("ul.S_box li").live("mouseover",function(){
//                $("ul.S_box").find("li").each(function(){
//                    var div0=$(this).find("div:first");
//                    var div1=$(this).find("div:last");
//                    
//                    $(this).removeClass();
//                    $(div0).hide();
//                    $(div1).show();
//                })
//                initList($(this));
//            })
            
            $(".search_box1 dd a").live("click",function(){
                if($(this).closest("div").attr("data-dx")=="0"){
                    $(this).closest("dl").find("dd a").removeClass();                    
                    $(".empty").closest("dl").find("dd[data-fl='"+$(this).closest("div").attr("data-fl")+"']").remove();
                }
                $(this).closest("dl").find("dt a").removeClass();
                $(this).addClass("on");
                $(getselect($(this).closest("div").attr("data-fl"),$(this).closest("dd").attr("data-v"),$(this).closest("div").find("label").text(),$(this).text())).insertBefore(".empty");
            })
            
            $(".search_box1 dt a").live("click",function(){
                $(this).closest("dl").find("dd a").each(function(){
                    $(this).removeClass();
                });
                $(this).addClass("on");
                $(".empty").closest("dl").find("dd[data-fl='"+$(this).closest("div").attr("data-fl")+"']").remove();
            })
            
            $(".S-paixu_list li a").live("click",function(){
                $(this).closest("ul").find("li").each(function(){
                    $(this).css({ "font-weight": "lighter" });
                });
                $(this).closest("li").css({ "font-weight": "bold" });
            })
            
            $(".empty").live("click",function(){
                $(this).closest("dl").find("dd").not(this).remove();
                $(".search_box1 dd").each(function(){
                    $(this).attr("data-s","0").find("a").removeClass();
                })
                $(".search_box1 dt").each(function(){
                    $(this).attr("data-s","1").find("a").addClass("on");
                })
                chaXun();
            })
            
            $("#youlunleixing").find("a").live("click",function(){
                var _params = utilsUri.getUrlParams("lx");
                _params["lx"]=$(this).attr("data-v");
                window.location.href=utilsUri.createUri("/hangqi/chaxun.aspx",_params);
            })
        }
        
        function getselect(fl,v,title,text){
            var _dd=$(".search-info dl dd[data-fl='"+fl+"'][data-v='"+v+"']");
            if($(_dd).length>0){
                return "";
            }
            else{
                return "<dd data-fl=\""+fl+"\" data-v=\""+v+"\" onclick=\"delselect(this)\"><a href=\"javascript:;\"><span>"+title+"：</span><strong>"+text+"</strong></a></dd>";
            }
        }
        
        function delselect(o){
            var _dd=$("div[data-fl='"+$(o).attr("data-fl")+"']").find("dd[data-v='"+$(o).attr("data-v")+"']");
            $(_dd).attr("data-s","0").find("a").removeClass();
            $(o).remove();
            if($(_dd).closest("dl").find("dd[data-s='1']").length==0){
                $(_dd).closest("dl").find("dt").attr("data-s","1").find("a").addClass("on");
            }
            chaXun();
        }

        function initRX() {
            $.ajax({
                type: "get",
                cache: true,
                url: "/commonpage/rexiao.aspx?lx=<%=LeiXing1 %>",
                dataType: "html",
                success: function(response) {
                    $("#ul_rx").html(response);
                }
            });
        }
        
        function initList(li){
            var div0=$(li).find("div:first");
            var div1=$(li).find("div:last");
            
            $(li).attr("class","current");
            $(div0).show();
            $(div1).hide();
        }

        $(document).ready(function() {
            initChaXun(); initEvent();

            setNav("<%=int.Parse(LeiXing1)+1 %>");
            
            initRX();
            chaXun();
        });
    </script>
    

    <script type="text/javascript">
        var pConfig = { pageSize: 15, pageIndex: 1, recordCount: 0, showPrev: true, showNext: true, showDisplayText: false, cssClassName: 'page_change',gotoPageFunctionName:'chaXun' }
    </script>

    <script type="text/javascript" src="/js/ajaxpagecontrols.js"></script>
    <form runat="server" id="form1">
    </form>
</asp:Content>
