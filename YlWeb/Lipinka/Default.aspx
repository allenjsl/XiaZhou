<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EyouSoft.YlWeb.Lipinka.Default" %>

<%@ Register Src="../UserControl/Navhead.ascx" TagName="Navhead" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Navfoot.ascx" TagName="Navfoot" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>礼品卡</title>
    <link type="text/css" rel="stylesheet" href="/Css/style.css">

    <script type="text/javascript" src="/Js/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="/Js/foucs.js"></script>

    <script type="text/javascript">
        $(function() {
            $('#lpk_Slider').loopedSlider({
                autoStart: 5000
            });
            $('.validate_Slider').loopedSlider({
                autoStart: 5000
            });
        });
</script>

    <script type="text/javascript">
        function nTabs(tabObj, obj) {
            var tabList = document.getElementById(tabObj).getElementsByTagName("li");
            for (i = 0; i < tabList.length; i++) {
                if (tabList[i].id == obj.id) {
                    document.getElementById(tabObj + "_Title" + i).className = "active";
                    document.getElementById(tabObj + "_Content" + i).style.display = "block";
                } else {
                    document.getElementById(tabObj + "_Title" + i).className = "normal";
                    document.getElementById(tabObj + "_Content" + i).style.display = "none";
                }
            }
        }
    </script>

</head>
<body>
    <uc1:Navhead ID="Navhead1" runat="server" />
    <div class="warp">
        <div class="mainbg">
            <div class="step_mainbox">
                <div class="basicT">
                    您的位置：维诗达游轮 > 礼品卡</div>
                <div class="basic_mainT">
                    <h5>
                        礼品卡</h5>
                </div>
                <div class="lpk_main margin_T16 fixed">
                    <div class="lpk_leftbox">
                        <div class="lpk_focus" id="lpk_Slider">
                            <div class="lpk_imgArea">
                                <ul class="slides">
                                    <asp:Literal runat="server" ID="ltr1"></asp:Literal>
                                </ul>
                                <div class="validate_Slider">
                                </div>
                                <ul class="pagination">
                                    <asp:Literal runat="server" ID="ltr2"></asp:Literal>
                                </ul>
                            </div>
                        </div>
                        <div class="n4Tab_lpk margin_T16" id="n4Tab6">
                            <div class="lpk_T">
                                <ul>
                                    <li id="n4Tab6_Title0" onclick="nTabs('n4Tab6',this);" class="active"><a href="javascript:void(0);">
                                        实体卡</a></li>
                                    <li id="n4Tab6_Title1" onclick="nTabs('n4Tab6',this);" class="normal"><a href="javascript:void(0);">
                                        电子卡</a></li>
                                </ul>
                            </div>
                            <div class="lpk_Content fixed">
                                <div class="lpk_box">
                                    <div id="n4Tab6_Content0">
                                        <div class="left_bar">
                                            <img src="" id="i_shitika_img" style="width:459px; height:257px;"/></div>
                                        <div class="right_bar">
                                            <ul class="num_list">
                                                <asp:Repeater runat="server" ID="rpt1">
                                                <ItemTemplate>
                                                <li i_filepath="<%#ErpFilepath+Eval("FengMian") %>" class="i_shitika_filepath">
                                                    <label>
                                                        ¥<i><%#Eval("JinE","{0:F2}") %></i></label><span class="dindan_num"><a href="javascript:void(0)">-</a><input type="text" value="0" /><a
                                                            href="javascript:void(0)">+</a></span></li>  
                                                </ItemTemplate>
                                                </asp:Repeater>                                              
                                            </ul>                                            
                                            <div class="heji_box">
                                                <span class="left-info"><b class="font14">合计：</b>¥<i>0</i></span> <span class="right-btn">
                                                    <a href="lpk_xx.html">立即购买</a></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="n4Tab6_Content1" class="none">
                                        <div class="left_bar">
                                            <img src=""  id="i_dianzika_img" style="width:459px; height:257px;"/></div>
                                        <div class="right_bar">
                                            <ul class="num_list">
                                                <asp:Repeater runat="server" ID="rpt2"><ItemTemplate>
                                                <li i_filepath="<%#ErpFilepath+Eval("FengMian") %>" class="i_dianzika_filepath">
                                                    <label>
                                                        ¥<i><%#Eval("JinE","{0:F2}") %></i></label><span class="dindan_num"><a href="javascript:void(0)">-</a><input type="text"
                                                            value="0" /><a href="javascript:void(0)">+</a></span></li>
                                                </ItemTemplate>
                                                </asp:Repeater>
                                            <div class="heji_box">
                                                <span class="left-info"><b class="font14">合计：</b>¥<i>0</i></span> <span class="right-btn">
                                                    <a href="lpk_xx.html">立即购买</a></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="lpk_side01 margin_T16">
                            <div class="lpk_side01-T">
                                <span>送礼</span><em>如何送礼</em></div>
                            <div class="lpk_Content fixed">
                                <div class="lpk_box">
                                    <div class="left_bar">
                                        <img src="" /></div>
                                    <div class="right_bar">
                                        <div class="lp_xuanze fixed">
                                            <div class="lp_xuanze_L">
                                                选择：</div>
                                            <ul class="lp_xuanzeList">
                                                <li><a href="#">
                                                    <img src="../images/history_01.jpg" /><span></span></a><p>
                                                        孝敬长辈</p>
                                                </li>
                                                <li><a href="#" class="card_select">
                                                    <img src="../images/history_01.jpg" /><span></span></a><p>
                                                        馈赠好友</p>
                                                </li>
                                                <li><a href="#">
                                                    <img src="../images/history_01.jpg" /><span></span></a><p>
                                                        亲密爱人</p>
                                                </li>
                                                <li><a href="#">
                                                    <img src="../images/history_01.jpg" /><span></span></a><p>
                                                        春节</p>
                                                </li>
                                                <li><a href="#">
                                                    <img src="../images/history_01.jpg" /><span></span></a><p>
                                                        圣诞节</p>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="lp_xuanzebox">
                                            <label>
                                                金额：</label><dl class="select_style">
                                                    <dt><a href="#"><span>500</span></a></dt>
                                                    <!--                  <dd>
                       <ul>
                          <li><a href="#"  class="flag">500</a></li>
                        </ul>
                     </dd>
    -->
                                                </dl>
                                        </div>
                                        <div class="lp_xuanzebox">
                                            <label>
                                                数量：</label><span class="dindan_num"><a href="#">-</a><input type="text" value="0" /><a
                                                    href="#">+</a></span></div>
                                        <div class="heji_box border-top">
                                            <span class="left-info"><b class="font14">合计：</b>¥<i>0</i></span> <span class="right-btn">
                                                <a href="lpk_xx.html">立即购买</a></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="lpk_rightbox">
                        <div class="lpk_Rside01">
                            <ul>
                                <li class="txt">已有礼品卡？</li>
                                <li class="btn02"><a href="#">领用礼品卡</a></li>
                                <li class="txt">忘记礼品卡余额和订单情况？</li>
                                <li class="btn03"><a href="#" style="margin-right: 10px;">查看礼品卡账户</a><a href="#">我的礼品卡订单</a></li>
                            </ul>
                        </div>
                        <div class="lpk_Rmenu margin_T16">
                            <ul>
                                <li><a href="#">关于礼品卡</a></li>
                                <li><a href="#">购买流程</a></li>
                                <li><a href="#">规则说明</a></li>
                                <li><a href="#">使用帮助</a></li>
                                <li><a href="#">常见问题</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc3:Navfoot ID="Navfoot1" runat="server" />
    
    <script type="text/javascript">
        $(document).ready(function() {
            $("#i_shitika_img").attr("src", $(".i_shitika_filepath").eq(0).attr("i_filepath"));
            $("#i_dianzika_img").attr("src", $(".i_dianzika_filepath").eq(0).attr("i_filepath"));
            
            $(".i_shitika_filepath").mousemove(function() {
                $("#i_shitika_img").attr("src", $(this).attr("i_filepath"));
            });
            
            $(".i_dianzika_filepath").mousemove(function() {
                $("#i_dianzika_img").attr("src", $(this).attr("i_filepath"));
            });
        });
    </script>
</body>
</html>
