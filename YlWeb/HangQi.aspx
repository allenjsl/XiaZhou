<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HangQi.aspx.cs" Inherits="EyouSoft.YlWeb.HangQi" MasterPageFile="~/MasterPage/Boxy.Master" Title="航期表" %>
<%@ MasterType VirtualPath="~/MasterPage/Boxy.Master" %>
<asp:Content ID="ctHead" ContentPlaceHolderID="PageHead" runat="server">
<!--[if IE 6]>
<script type="text/javascript" src="../js/PNG.js" ></script>
<script type="text/javascript">
DD_belatedPNG.fix('*,div,img,a,a:hover,ul,li,p');
</script>
<![endif]-->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
<form id="form1" runat="server">
    
      <div class="step_mainbox">
        
        <div class="basicT">您的位置：维诗达游轮 &gt; 航期表</div>
        
        <div class="basic_mainT"><h5>航期表</h5></div>
      <div class="hq_side_main">
        <div class="hq_side01">
          <div class="hq_box font18" style="color:#07236d; position:relative;"><div style="background:#fff;padding:0 10px;position:relative;z-index:100;width:108px;margin-left:50px;">查询月份航期</div><div style="border-bottom:1px solid #07236d; width:1198px;height:20px;position:absolute;top:0px;"></div></div>
          <div class="hq_box fixed">
              
              <div class="hq_month">
                <span class="font24 floatL"><%=DateTime.Now.Year %>年</span>
                  <ul>
                  <%for (var m = DateTime.Now; m <= DateTime.Now.AddMonths(11); m=m.AddMonths(1)) %>
                  <%{ %>
                    <%if (m.Year != DateTime.Now.Year && m.Month == 1) %>
                    <%{ %>
                    </ul>
                    <span class="font24 floatL"><%=m.Year %>年</span>
                    <ul>
                    <%} %>
                     <li><a href="javascript:;" data-year="<%=m.Year %>" data-month="<%=m.Month %>" class="<%=m.Year==Year&&m.Month==Month?"on":"" %>"><%=string.Format("{0:00}", m.Month)%>月</a></li>
                  <%} %>
                  </ul>
                  <div class="clear"></div>
              </div>
          </div>
            
          <div class="hq_box font18" style="color:#07236d; position:relative;"><div style="background:#fff;padding:0 10px;position:relative;z-index:100;width:108px;margin-left:50px;">查询游轮航线</div><div style="border-bottom:1px solid #07236d; width:1198px;height:20px;position:absolute;top:0px;"></div></div>
          <div class="hq_hangxian">
             <ul>
             <asp:Repeater ID="rptHangXian" runat="server">
             <ItemTemplate>
                <li><a href="javascript:;" data-hangxian="<%#Eval("XinXiId") %>" class="<%#(int)Eval("XinXiId")==HangXianId?"on":"" %>"><%#Eval("MingCheng")%></a></li>
             </ItemTemplate>
             </asp:Repeater>
             </ul>
             <div class="clear"></div>
          </div>
         </div>
      </div>
         <div id="divHtml"><%=GetHangQi() %></div>
         
      </div>
 
</form>
<script type="text/javascript">
$(function(){
    var _th = $(".top").height()+15+44+34;

    $(window).bind('scroll',function(){
      var st = $(document).scrollTop();
      var _h = $("#divHtml").height()+_th;
      if( st>_th && st < _h){
        $(".hq_side01").addClass("position_fixed");

        if($(".hq_side01").has(".hq_yellow").length){
          return;
        }else{
          $(".hq_yellow").clone(true).attr("id","clone_hq_yellow").insertAfter(".hq_hangxian");
        }
        if($(".hq_side01").has(".headbg").length){
          return;
        }else{
          $(".headbg").clone(true).insertAfter("#clone_hq_yellow");
        }
        
      }else{
        $(".hq_side01").removeClass("position_fixed");
        $(".hq_side01 .hq_yellow").remove();
        $(".hq_side01 .headbg").remove();
      }
    });
    $(".hq_hangxian a, .hq_month a").live("click", function(){
      $('html,body').animate({'scrollTop':_th},500);
    });
    $("a").click(function(){
        $(this).closest("div").find("a.on").removeClass();
        $(this).addClass("on");
        var _x=$(".hq_hangxian").find(".on").attr("data-hangxian");
        var _y=$(".hq_month").find(".on").attr("data-year");
        var _m=$(".hq_month").find(".on").attr("data-month");
        var _params = {};
        _params["x"]=_x;
        _params["y"]=_y;
        _params["m"]=_m;
        
        $.ajax({
            type: "get",
            cache: true,
            url: "/hangqi.aspx?doajax=gethangqi",
            data:_params,
            dataType: "text",
            async:false,
            success: function(response) {
                $("#divHtml").html(response);
            }
        });
    })
})
</script>
</asp:Content>
