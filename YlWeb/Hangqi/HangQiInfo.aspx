<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HangQiInfo.aspx.cs" Inherits="EyouSoft.YlWeb.Hangqi.HangQiInfo"
    MasterPageFile="~/MasterPage/Boxy.Master" Title="长江游轮详细页" %>

<%@ MasterType VirtualPath="~/MasterPage/Boxy.Master" %>
<%@ Register Src="~/UserControl/HistoryView.ascx" TagName="HistoryView" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageBody" runat="server">

    <script src="/Js/Rili.js" type="text/javascript"></script>

    <script src="/Js/gscx.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <form id="form1" runat="server">
    <div class="mainbox" id="ph_Page1">
        <div class="basicT">
            您的位置：维诗达游轮 &gt; 长江游轮 &gt;
            <asp:Literal ID="txt_YL_Title" runat="server"></asp:Literal></div>
        <div class="yudin_box01 fixed">
            <div class="basic_mainT">
                <h5>
                    游轮信息</h5>
            </div>
            <dl class="yudin_box01_L">
                <dt>
                    <asp:Literal ID="txt_YL_Name" runat="server"></asp:Literal></dt>
                <dd style="text-align: center;">
                    <span class="s_price">价格：</span><span class="b_price"><asp:Literal ID="txt_YL_Jiage"
                        runat="server"></asp:Literal></span><span class="s_price">起</span><b class="font_gray" data-id="qijiashuoming">【起价说明】</b><span style="display:none;"><%=FromHangqi.QiShiJiaGeShuoMing%></span><b
                            style="padding-left: 20px;" class="font_black font14">【产品编号】<asp:Literal ID="txt_YL_ChanPinBianHao"
                                runat="server"></asp:Literal></b></dd>
                <dd>
                    <div class="yd_bg">
                        <a class="yd_btn" href="#ydclickto">我要预订</a><a id="shoucang" class="fav"
                            href="javascript:;">【加入收藏】</a></div>
                </dd>
                <dd>
                    <span class="dd_t">游轮系列：</span><asp:Literal ID="txt_YL_Xilie" runat="server"></asp:Literal></dd>
                <dd>
                    <span class="dd_t">游轮星级：</span>
                    <asp:Literal ID="txt_YL_XingJi" runat="server"></asp:Literal>
                </dd>
                <dd>
                    <span class="dd_t">游轮航线：</span><asp:Literal ID="txt_YL_Hangxian" runat="server"></asp:Literal></dd>
                <dd>
                    <span class="dd_t">产品特色：</span><span class="dd_content" data-id="chanpintese"><asp:Literal ID="txt_YL_ChanPinTeSe"
                        runat="server"></asp:Literal>
                    </span>
                </dd>
            </dl>
            <div class="yudin_box01_R">
                <div id="n4Tab1" class="n4Tab_msg">
                    <div class="msg_T">
                        <ul>
                            <li class="active" onmouseover="nTabs('n4Tab1',this);" id="n4Tab1_Title0"><a href="javascript:void(0);">
                                游轮介绍</a></li>
                            <li class="normal" onmouseover="nTabs('n4Tab1',this);" id="n4Tab1_Title1"><a href="javascript:void(0);">
                                房型介绍</a></li>
                            <li class="normal" onmouseover="nTabs('n4Tab1',this);" id="n4Tab1_Title2"><a href="javascript:void(0);">
                                美食介绍</a></li>
                            <li class="normal" onmouseover="nTabs('n4Tab1',this);" id="n4Tab1_Title3"><a href="javascript:void(0);">
                                娱乐及服务设施</a></li>
                            <li class="normal" onmouseover="nTabs('n4Tab1',this);" id="n4Tab1_Title4"><a href="javascript:void(0);">
                                平面图</a></li>
                            <li class="normal" onmouseover="nTabs('n4Tab1',this);" id="n4Tab1_Title5"><a href="javascript:void(0);">
                                游轮视频</a></li>
                        </ul>
                    </div>
                    <div class="msg_Content">
                        <div id="n4Tab1_Content0" style="display: block;">
                            <div class="yljs_img">
                                <a target="_blank" href="/Youlun/ChuanZhiCJ.aspx?id=<%=FromHangqi.ChuanZhiId %>"><asp:Literal ID="txt_YL_JieShaoImage" runat="server"></asp:Literal></a></div>
                            <div class="yljs_cont">
                                <h3><asp:Literal ID="txt_YL_JSName" runat="server"></asp:Literal></h3>
                                <p>
                                    <asp:Literal ID="txt_YL_JScontent" runat="server"></asp:Literal>
                                    <a target="_blank" href="/Youlun/ChuanZhiCJ.aspx?id=<%=FromHangqi.ChuanZhiId %>">【查看全部】</a></p>
                            </div>
                        </div>
                        <div class="none" id="n4Tab1_Content1" style="display: none;">
                            <div class="meishijs fxjs">
                                <ul>
                                    <asp:Literal ID="txt_YL_FangXing" runat="server"></asp:Literal>
                                </ul>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                        <div class="none" id="n4Tab1_Content2" style="display: none;">
                            <div class="meishijs msjs">
                                <ul>
                                    <asp:Literal ID="txt_YL_MeiShi" runat="server"></asp:Literal>
                                </ul>
                                <div class="clear">
                                </div>
                                <div class="chakan_more">
                                    <a target="_blank" href="/Youlun/ChuanZhiCJ.aspx?id= <%=FromHangqi.ChuanZhiId %>#meishi">
                                        查看更多 +</a></div>
                            </div>
                        </div>
                        <div class="none" id="n4Tab1_Content3" style="display: none;">
                            <div class="meishijs msjs">
                                <ul>
                                    <asp:Literal ID="txt_YL_SheShi" runat="server"></asp:Literal>
                                </ul>
                                <div class="clear">
                                </div>
                                <div class="chakan_more">
                                    <a target="_blank" href="/Youlun/ChuanZhiCJ.aspx?id= <%=FromHangqi.ChuanZhiId %>#sheshi">
                                        查看更多 +</a></div>
                            </div>
                        </div>
                        <div class="none" id="n4Tab1_Content4" style="display: none;">
                            <div class="pmt">
                                <a target="_blank" href="/Youlun/ChuanZhiCJ.aspx?id= <%=FromHangqi.ChuanZhiId %>#pingmian"><asp:Literal ID="txt_YL_PingMian" runat="server"></asp:Literal></a>
                                <div class="chakan_more">
                                    <a target="_blank" href="/Youlun/ChuanZhiCJ.aspx?id= <%=FromHangqi.ChuanZhiId %>#pingmian">
                                        查看更多 +</a></div>
                            </div>
                        </div>
                        <div class="none" id="n4Tab1_Content5" style="display: none;">
                            <div class="youlunvideo ylsp fixed">
                                <asp:Literal ID="txt_YL_ShiPin" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:PlaceHolder runat="server" ID="phYouHui">
        <div class="yudin_box02 margin_T16">
            <div class="basic_mainT">
                <h5>
                    优惠信息</h5>
            </div>
            <div class="youhui_cont">
                <asp:Literal ID="txt_YL_YouHui" runat="server"></asp:Literal>
            </div>
        </div>
        </asp:PlaceHolder>
        <div class="yudin_box03 margin_T16" id="ydclickto">
            <div class="basic_mainT">
                <h5>
                    预订产品</h5>
            </div>
            <div class="yd_step1">
                <h3>
                    请选择游轮航线</h3>
                <dl class="dropdown">
                    <select class="select_style_1" id="select_hangxian" name="select_hangxian">
                        <asp:Literal ID="txt_dpt_HangXina" runat="server"></asp:Literal>
                    </select>
                </dl>
            </div>
            <div class="yd_step2 fixed" id="showTable">
            </div>
            
            <div id="i_div_riqi_xx" style="height:30px; line-height:30px;"></div>
            
            <div class="yd_step3">
                <h3>
                    请您选择房型\国籍\人数：
                </h3>
                <div id="n4Tab2" class="n4Tab_fx showHtml">
                </div>
                <div class="total_box">
                    <span id="heji" data-m="0">合计：<em class="b_price">￥0</em></span> <a id="Page1" href="javascript:;">
                        我要预订</a>
                </div>
            </div>
        </div>
        <div class="yudin_box04 margin_T16">
            <div id="n4Tab3" class="n4Tab_xch">
                <div class="xch_T">
                    <ul>
                        <li class="active" onmouseover="nTabs('n4Tab3',this);" id="n4Tab3_Title0"><a href="javascript:void(0);">
                            行程安排</a></li>
                        <li class="normal" onmouseover="nTabs('n4Tab3',this);" id="n4Tab3_Title1"><a href="javascript:void(0);">
                            费用说明</a></li>
                        <li class="normal" onmouseover="nTabs('n4Tab3',this);" id="n4Tab3_Title2"><a href="javascript:void(0);">
                            预订须知</a></li>
                        <li class="normal" onmouseover="nTabs('n4Tab3',this);" id="n4Tab3_Title3"><a href="javascript:void(0);">
                            友情提示</a></li>
                        <li class="normal" onmouseover="nTabs('n4Tab3',this);" id="n4Tab3_Title4"><a href="javascript:void(0);">
                            游轮攻略</a></li>
                        <li class="normal" onmouseover="nTabs('n4Tab3',this);" id="n4Tab3_Title5"><a href="javascript:void(0);">
                            用户点评</a></li>
                        <li class="normal" onmouseover="nTabs('n4Tab3',this);" id="n4Tab3_Title6"><a href="javascript:void(0);">
                            咨询问答</a></li>
                    </ul>
                </div>
                <div class="xch_Content">
                    <div id="n4Tab3_Content0" style="display: block;">
                        <div class="xch_tip">
                            <h3>
                                标准行程</h3>
                            <h5>
                                选择航线</h5>
                            <dl class="dropdown">
                                <select class="select_style_1" id="select_hangxiandown" name="select_hangxiandown">
                                    <asp:Literal ID="txt_dpt_listdown" runat="server"></asp:Literal>
                                </select>
                            </dl>
                            <div class="R_txt">
                                ※ 重要提示：不同出发日期，产品行程或略有不同，请以最终合同版行程为准。<a href="/Print/XingChengDan.aspx?HangQiId=<%=this.HangQiId %>" target="_blank">打印行程单</a></div>
                        </div>
                        <div class="xch_box">
                            <ul>
                                <asp:Repeater ID="rptList_XingCheng" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <div class="xch_day">
                                                <em>第<%# Container.ItemIndex + 1%>天</em>
                                                <%#Eval("QuJian1") %></div>
                                            <div class="xch_list">
                                                <dl>
                                                    <dt>
                                                        <%# XingChengImage(Eval("Filepath"))%></dt>
                                                    <dd class="xingch">
                                                        行程：<%#Eval("NeiRong")%></dd>
                                                    <dd>
                                                        交通工具：<%#Eval("JiaoTongGongJu") %></dd>
                                                    <%--<dd>
                                                        班次号码：<%#Eval("BanCi") %></dd>--%>
                                                </dl>
                                                <ul class="xch_rightlist">
                                                    <li><em>早餐</em><span><%#Eval("Zao")%></span></li>
                                                    <li><em>午餐</em><span><%#Eval("Zhong")%></span></li>
                                                    <li><em>晚餐</em><span><%#Eval("Wan") %></span></li>
                                                    <li><em>住宿</em><span><%#Eval("ZhuSu") %></span></li>
                                                </ul>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                    <div class="none" id="n4Tab3_Content1" style="display: none;">
                        <div class="xch_box02">
                            <div class="icon_title feiyong_t">
                                费用说明</div>
                            <div class="xc_listbox">
                                <asp:Literal ID="txt_YL_FeiYong" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                    <div class="none" id="n4Tab3_Content2" style="display: none;">
                        <div class="xch_box02">
                            <div class="icon_title yudin_t">
                                预订须知</div>
                            <div class="xc_listbox">
                                <asp:Literal ID="txt_YL_YuDingXuZhi" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                    <div class="none" id="n4Tab3_Content3" style="display: none;">
                        <div class="xch_box02">
                            <div class="icon_title youq_t">
                                友情提示</div>
                            <div class="xc_listbox">
                                <asp:Literal ID="txt_YL_YouQing" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                    <div class="none" id="n4Tab3_Content4" style="display: none;">
                        <div class="xch_box02">
                            <div class="icon_title glue_t">
                                游轮攻略</div>
                            <div class="xc_listbox">
                                <asp:Literal ID="txt_YL_YouLunGongLue" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                    <div class="none" id="n4Tab3_Content5" style="display: none;">
                        <div class="xch_box02">
                            <div class="icon_title dianp_t">
                                用户点评</div>
                            <div class="xc_listbox">
                                <div class="xc-user-score">
                                    <asp:Literal ID="litYongHuDingPing" runat="server"></asp:Literal>
                                </div>
                                <ul id="dingping" class="xc-user-list">
                                    <asp:Repeater ID="rptListDianPing" runat="server">
                                        <ItemTemplate>
                                            <li>
                                                <h4>
                                                    <%#Eval("BiaoTi")%>
                                                    <%#DianPingFenShu(Eval("FenShu"))%></h4>
                                                <p>
                                                    <%#Eval("NeiRong")%></p>
                                                <p class="user-txt">
                                                    <%# HuiYuanMingCheng(Eval("OperatorId"), Eval("IsNiMing"))%>
                                                    <%#Convert.ToDateTime(Eval("IssueTime")).ToString("yyyy-MM-dd")%></p>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>                               
                                <div class="chakan_more">
                                    <a href="javascript:void(0)" onclick="PageSet.DianPingMore();" id="dingping_more">查看更多
                                        +</a></div>
                            </div>
                        </div>
                    </div>
                    <div class="none" id="n4Tab3_Content6" style="display: none;">
                        <div class="xch_box02">
                            <div class="icon_title wenda_t">
                                咨询问答</div>
                            <div class="xc_listbox">
                                <div class="xc-user-score">
                                    <p id="p_zxlist" class="question-fl">
                                        <span class="L_title">咨询分类：</span>
                                        <%=ZiXunLeiXing() %>
                                    </p>
                                    <p class="question-cont">
                                        <span class="L_title">咨询提问：</span></span><a class="quest-link" id="i_a_tiwen" href="javascript:;">我要提问</a></p>
                                </div>
                                <ul id="zixun" class="xc-user-list">
                                    <asp:Repeater ID="rptListZiXunWD" runat="server">
                                        <ItemTemplate>
                                            <li>
                                                <p class="question-txt">
                                                    <span class="icon_qtitle">咨询内容：</span><%#Eval("WenNeiRong")%><span class="font_gray">（<%#HuiYuanMingCheng(Eval("WenYongHuId"), Eval("IsNiMing"))%>&nbsp;&nbsp;<%#Convert.ToDateTime(Eval("WenShiJian")).ToString("G")%>）</span></p>
                                                <p class="answer-txt">
                                                    <span class="icon_atitle">客服回复：</span><%#Eval("DaNeiRong")%></p>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <div class="chakan_more">
                                    <a href="javascript:void(0)" onclick="PageSet.ZiXunMore();" id="zixun_more">查看更多 +</a></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <uc1:HistoryView ID="HistoryView" LeiXing="长江游轮" runat="server" />
        </div>
    </div>
    <div class="step_mainbox" id="ph_Page2" style="display: none">
        <div class="step_T">
            <div class="basicT floatL">
                您的位置：维诗达游轮 &gt; 长江游轮 &gt; 选择产品</div>
            <div class="step_Rimg">
                <img src="/images/online_book.jpg"></div>
            <div class="step_num">
                <ul>
                    <li>
                        <img src="/images/step01.jpg"><span class="bfontblue">选择产品</span></li>
                    <li>
                        <img src="/images/step02.jpg"><span>核对与填写信息</span></li>
                    <li>
                        <img src="/images/step03.jpg"><span style="text-indent: 79px;">客服审核</span></li>
                    <li>
                        <img src="/images/step04.jpg"><span style="text-indent: 96px;">订单支付</span></li>
                    <li>
                        <img src="/images/step05.jpg"><span style="text-indent: 107px;">预订成功</span></li>
                </ul>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="step_box1 margin_T16">
            <div class="basic_mainT">
                <h5>
                    订单详情</h5>
            </div>
            <div class="step_xc">
                <div class="step_xcT">
                    <h3>
                        行程信息</h3>
                    <span><a href="javascript:void(0)" id="i_a_yingcangmingxi">隐藏明细
                        </a><img src="/images/jt_t.png"></span></div>
                <div id="stepHtml">
                </div>
            </div>
        </div>
        <div class="step_box2 margin_T16">
            <div class="basic_mainT_color">
                <h5>
                    附加产品</h5>
            </div>
            <asp:Repeater ID="rptList_FuJian" runat="server" OnItemDataBound="InitFuJiaChanPinDetail">
                <ItemTemplate>
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tbody>
                            <tr>
                                <th colspan="5">
                                    <%# JiChuXinXi(Convert.ToInt32(Eval("LeiXingId")))%>
                                </th>
                            </tr>
                            <tr class="thead">
                                <td width="40%">
                                    项目名称
                                </td>
                                <td width="15%">
                                    卖价
                                </td>
                                <td width="15%">
                                    计价单位
                                </td>
                                <td width="15%">
                                    份数
                                </td>
                                <td width="15%" class="right">
                                    单项总价
                                </td>
                            </tr>
                            <asp:Repeater ID="rptList_FujiaDetail" runat="server">
                                <ItemTemplate>
                                    <tr class="i_trFuJiaChanPin">
                                        <td>
                                            <span data-class="fujiachanpin"><%#Eval("XiangMu")%></span>
                                            <span style="display:none;">
                                                <%#Eval("JieShao")%></span>
                                        </td>
                                        <td>
                                            <%#Eval("DanJia","{0:C2}")%>
                                        </td>
                                        <td>
                                            <%# Eval("DanWei") %>
                                        </td>
                                        <td>
                                            <span class="dindan_num"><a data-m="<%#Eval("DanJia") %>" class="page_jian" href="javascript:;">
                                                -</a><input type="text" data-dj="<%#Eval("DanJia")%>" data-lx="<%#Eval("LeiXingId") %>"
                                                    data-fjid="<%#Eval("FuJiaChanPinId") %>" value="0" readonly="readonly"><a data-m="<%#Eval("DanJia") %>"
                                                        class="page_jia" href="javascript:;">+</a></span>
                                        </td>
                                        <td class="right fontprice font14">
                                            <span data-m="" class="danxiangJe">￥0</span>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="step_box3 margin_T16">
            <div class="basic_mainT_color">
                <h5>
                    合计</h5>
            </div>
            <table border="0" cellspacing="0" cellpadding="0">
                <tbody>
                    <tr>
                        <th width="150">
                            总价：
                        </th>
                        <td class="right">
                            <span id="td_page1zong" date-m="">￥18110.00</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            人均：
                        </th>
                        <td class="right">
                            <span id="td_page1renjun" date-rj="">￥2470.00</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            人数：
                        </th>
                        <td class="right renshu">
                            <span id="td_page1renshu" date-rs="">6人</span>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="btn_box margin_T16">
            <a id="PageDown1_back" href="javascript:;">上一步</a> <a class="yellow_btn" id="PageDown1_up"
                href="javascript:;">下一步</a>
        </div>
    </div>
    <div class="step_mainbox" id="ph_Page3" style="display: none">
        <div class="step_T">
            <div class="basicT floatL">
                您的位置：维诗达游轮 &gt; 长江游轮 &gt; 核对与填写信息</div>
            <div class="step_Rimg">
                <img src="/images/online_book.jpg"></div>
            <div class="step_num">
                <ul>
                    <li>
                        <img src="/images/step01_1.jpg"><span class="bfontblue">选择产品</span></li>
                    <li>
                        <img src="/images/step02_1.jpg"><span class="bfontblue">核对与填写信息</span></li>
                    <li>
                        <img src="/images/step03.jpg"><span style="text-indent: 79px;">客服审核</span></li>
                    <li>
                        <img src="/images/step04.jpg"><span style="text-indent: 96px;">订单支付</span></li>
                    <li>
                        <img src="/images/step05.jpg"><span style="text-indent: 107px;">预订成功</span></li>
                </ul>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="step_basicbox margin_T16">
            <div class="basic_mainT_color">
                <h5>
                    核对订单</h5>
            </div>
            <div class="step_dindan">
                <div id="page3_dd_show">
                </div>
                <div id="page3_fj_show">
                </div>
            </div>
        </div>
        <div class="step_basicbox margin_T16">
            <div class="basic_mainT_color">
                <h5>
                    旅客信息</h5>
            </div>
            <div id="page3_bklist">
            </div>
        </div>
        <div class="step_basicbox margin_T16">
            <div class="basic_mainT_color">
                <h5>
                    预订信息<em>※ 为方便我们及时与您联系，请准确填写联系人信息（手机号码、电子邮件）</em></h5>
            </div>
            <div class="yudin_msgbox border_bot fixed">
                <div class="yudin_leftT">
                    预订人信息</div>
                <ul class="lvke_form" style="position: relative;">
                    <div class="kuhao">此处手机号码和联系电话至少填写一项</div>
                    <li>
                        <label>
                            <font class="font_star">*</font>姓名：</label><span><input type="text" id="txtYudingName"
                                valid="required" errmsg="请填写预订人姓名" name="txtYudingName" value="" class="formsize370 inputbk"></span></li>
                    <li>
                        <label>
                            电子邮件：</label><span><input type="text" value="" class="formsize370 inputbk"
                                valid="isEmail" errmsg="请正确填写预订人电子邮件!" id="txtYudingEmail"
                                name="txtYudingEmail"></span></li>
                    <li>
                        <label>
                            手机号码：</label><span><input id="txtYudingphone" valid="isMobile" errmsg="请正确填写预订人手机号码!"
                                name="txtYudingphone" type="text" value="" class="formsize370 inputbk"></span></li>
                    <li>
                        <label>
                            座机电话：</label><span><input id="txtYudingtell" name="txtYudingtell" type="text" value=""
                                class="formsize370 inputbk"></span></li>
                    <li><label>下单备注：</label><span><input id="txtXiaDanBeiZhu" name="txtXiaDanBeiZhu" type="text" maxlength="255" value="" class=" formsize570 inputbk"/></span></li>
                </ul>
            </div>
            <div class="yudin_msgbox fixed">
                <div class="yudin_leftT">
                    发票信息</div>
                <ul class="lvke_form">
                    <li style="padding-left: 26px;" class="font_999 font14">抵用券和礼品卡等消费券支付的金额不提供报销凭证</li>
                    <li>
                        <label>
                            是否需要发票：</label><span class="font14"><input id="checkN" type="radio" value="0" name="IscheckFaPiao"
                                checked="checked" /><label for="checkN" class="no_style">&nbsp;否</label>&nbsp;&nbsp;<input id="checkY" type="radio" name="IscheckFaPiao"
                                    value="1" /><label for="checkY" class="no_style">&nbsp;是</label></span></li>
                    <li>
                        <label>
                            发票抬头：</label><span><input id="txtFapiaoTitle" name="txtFapiaoTitle" type="text" value=""
                                class=" formsize370 inputbk"></span><span class="error">遵循税务局相关规定，发票抬头必须为个人姓名或公司名称</span></li>
                    <li>
                        <label>
                            发票明细：</label><span>
                                <select class="select_style_1" name="txt_fapiao_mxlx" id="txt_fapiao_mxlx">
                                    <option value="">请选择</option>
                                    <option value="1">旅游业-船票</option>
                                    <!--<option value="2">旅游业-综合服务费</option>-->
                                    <option value="3">旅游业-旅游费</option>
                                </select>
                            </span></li>
                </ul>
            </div>
            <div class="yudin_msgbox fixed" data-id="peisongfangshi" style="display:none;">
                <div class="yudin_leftT">
                    配送方式</div>
                <div class="peisong_box">
                    <h5>
                        <span>快递
                            <%=FromHangqi.FaPiaoKuaiDiJinE.ToString("F2") %></span></h5>
                    <h3>
                        选择常用地址<span id="Address"><a href="javascript:;">添加新地址</a></span></h3>
                    <div class="peisong_address">
                        <ul class="address_list" id="i_changyongdizhi">
                            <asp:Literal runat="server" ID="ltrChangYongDiZhi"></asp:Literal>
                        </ul>
                        <ul class="address_form" style="display: none">
                            <li>
                                <label>
                                    收件人：</label><span><input type="text" id="txtName" name="txtName" class="formsize370 inputbk"></span></li>
                            <li>
                                <label>
                                    联系电话：</label><span><input type="text" id="txtTell" name="txtTell" class="formsize370 inputbk"></span></li>
                            <li>
                                <label>
                                    邮寄地址：</label><span>
                                        <select id="sel_province" name="sel_province" class="select_style_1">
                                        </select>
                                    
                                        
                                            <select id="sel_city" name="sel_city" class="select_style_1">
                                            </select>
                                        
                                        
                                            <select id="sel_country" name="sel_country" class="select_style_1">
                                            </select>
                                        
                                        </br>
                                        <input type="text" id="txtAdress" name="txtAdress" class="formsize370 inputbk"/></span></li>
                            <li>
                                <label>
                                    邮政编码：</label><span><input type="text" id="txtZip" name="txtZip" value="" class=" formsize100 inputbk"></span></li>
                            <li style="text-align: right;"><a class="address_btn" href="javascript:;">保存地址</a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="jiesuan_box">
                <ul>
                    <li><span>产品金额</span><em id="Page3_Zong" class="price_yellow"></em></li>
                    <li><span>附加产品金额</span><em id="Page3_FuJia" class="price_yellow">¥400</em></li>
                    <span id="i_span_youhui"></span>
                    <li><span>使用积分：<input type="text" id="txtDingDanJiFen" value="0" valid="isInt" errmsg="请从新填写积分!"
                        name="txtDingDanJiFen" value="" class="formsize100">
                        点<font id="KeYongJiFen" style="color: #999;"></font>本产品最多可抵扣：<%=FromHangqi.KeDiKouJinFen.ToString("F2")%>分</span><em
                            id="JiFenJinE_DuiHuan">-¥0</em></li>
                    <li class="noborder"><span>快递费</span><em id="KuaiDiJinE" class="price_yellow">¥0</em></li>
                </ul>
            </div>
            <div class="jiesuan_yellbox">
                <div class="jiesuan_price">
                    <b class="font20 price_20">实付款：</b><font id="Page3_ShiFu" class="font34 price_yellow"></font></div>
                <div class="jiesuan_btn">
                    <div class="leftTxt">
                        <a id="pagedow2_back" href="javascript:;">
                            <img src="/images/left_jt.jpg">
                            返回上一步</a></div>
                    <a style="float: right;" id="btnSava" class="btn" href="javascript:;"><span>同意以下条款，去支付</span></a></div>
            </div>
        </div>
        <div class="step_basicbox margin_T16">
            <div class="basic_mainT_color">
                <h5>
                    合同条款</h5>
            </div>
            <div class="hetong_box">
                <div class="hetong_txt">
                    <asp:Literal runat="server" ID="ltrHeTongTiaoKuan">
                    <div>
                        亲爱的游客：<br>
                        您好！为了让您能更好的享受涉外豪华游船给您带来的舒适享受，也为了避免不必要的争端产生，请您仔细阅读以下旅游注意事项，谢谢支持！<br>
                        一、安全事项<br>
                        1、长江三峡旅游线路主要以船为载体，大部分时间都会在游船上面度过，因游船自身空间相对较小，上下楼梯狭窄，甲板湿滑，所以请所有游客在游船上面走动或观景的时候特别注意安全，同时不要翻越游船护栏照相、看景点，有小孩的游客请照看好自己的小孩，不要让他们在游船上面嬉戏追赶，随行父母应特别注意各处安全隐患，防范于未然<br>
                        2、游船装载人数较多，游船停靠码头上下船通过跳板或趸船的时候请不要拥挤，按次序依次下船，注意安全，以免落水，如您前面有老人和小孩，请发扬尊老爱幼的传统美德<br>
                        3、上船后请客人注意防火防盗并保管好自己的行李物品，以免丢失，如有丢失，请及时报警<br>
                        二、游轮费用<br>
                        &nbsp;&nbsp;1、船票费用包含乘船期间的住宿、用餐（不含登船当天的晚餐）、行程指定的上岸景点参观费用。不含船上一切个人消费，如：酒水、洗衣、电话、美容美发、购物、自费上岸参观景点等费用。<br>
                        &nbsp;&nbsp;2、游船上正常舱位为2人间，将以一人一个床位为单位进行销售，如团队男女比例产生自然单差，游船公司会提前同其他团队客人进行调配，如客人特殊需求可提前申请一人单独使用一个标准房间，价格另议！<br>
                        &nbsp;&nbsp;3、船票的收费与机票类似，按年龄段收取，一般规则如下：2岁以下的婴儿，不单独使用床位，按照全额船票的10%收费；2-12岁儿童根据客人要求是否占床将收取50-100%不等的费用（根据游船不同收费标准存在一定差异），12岁及以上按成人结算。如您带有小孩随行，请一定提前告知报名旅行社，切勿隐瞒，以免给您的旅途带来不便<br>
                        &nbsp;&nbsp;4、因国籍不同，船票的销售价格可能会出现差异，请出发前予以说明，切勿隐瞒，我司确认的船位仅供确认书中指定的国籍的客人使用。若乘船的客人非确认书中指定的国籍，我司将根据客人的实际国籍增收或退还船票差价。如客人故意隐瞒个人身份，在游船前台登记时被查出国籍不同，将按照游船公布价格在前台补齐差额（可能会比结算价格高）。<br>
                        三、购物与自费项目<br>
                        1、行程中如安排的有购物店，游客可根据个人喜好挑选物品。进店购物需要一定时间，未进店客人可于店外自行活动，自由活动期间司机、导游可能无法提供服务敬请客人谅解。<br>
                        2、行程中所列自选景点和自理项目均由导游推荐参加，自愿选择不强迫，行程所列自费景点将根据当天船上报名人数而定，人多则停靠，人少则不停靠。未报名参加的客人可在码头附近自由活动并按照规定的时间返回船上即可，自由活动期间，车船将停止工作，不便之处敬请谅解。<br>
                        3、景区内的小商小贩所兜售的物品请客人谨慎选择，不买请不要随便触摸和试用，购买请先谈好价格，以免给自己带来不必要的麻烦。<br>
                        4、游客在旅游期间，旅行社不提倡强制消费和其他加点，行程中导游提出加点游客可以直接拒绝（导游如有强制消费和强迫加点行为请立即反应到我社），游客的自主购物及各种自费活动均属个人行为，由此产生的纠纷或连带关系游客自负，即买卖自由，风险自担。<br>
                        四、取消船位的违约金<br>
                        • 如果想取消船位，要立即书面通知我司，我司会于收到书面通知日起才正式取消订位，但必须支付下列的取消违约金：&nbsp;<br>
                        • 1、在开航之日前10天取消船位，须支付30%全额船票的违约金。<br>
                        • 2、在开航之日前6天取消船位，须支付50%全额船票的违约金。&nbsp;<br>
                        • 3、在开航之日前3天内取消船位，须支付100%全额船票的违约金。&nbsp;&nbsp;&nbsp;&nbsp;<br>
                        • 4、取消船位包括取消全部或部分船位，变更船名或航期。&nbsp;<br>
                        • 5、黄金周期间为买断舱位，取消船位须支付100%全额船票的违约金。<br>
                        五、特别说明<br>
                        1、游船经停景点的游览都将视为一个整体，用车将有游船统一调度，非局部单团独立用车；客人下船游览的各个景点都将有景区内导游负责接待讲解，下船后请紧跟景区导游，先听取讲解，后景区内自由游览或购买个人物品<br>
                        2、涉外豪华游轮实行一票制，船票包含内容为包装特惠销售，团队中持有老年证等其他相关证件的特殊人群在景点不再享受其他优惠，行程中客人放弃景点游览或不用餐不退还任何费用<br>
                        3、游船航程期间可能会举行一些见面会及行程说明会等活动，以当天航次实际公布为准。<br>
                        4、出发前请提醒客人带好个人有效身份证件/户口簿（儿童），以便办理登船入住手续及临检。<br>
                        六、特殊情况的处理<br>
                        1、因气候、水位、临时禁航和航道管制等无法控制的原因，船公司将据实际情况调整游船的始发港和终到港，并用其它交通工具（大巴）将旅客接送至游船始发站和终点站；可能调整乘船期间上岸参观的景点；<br>
                        2、三峡坝区及双线五级船闸是国防保护重点工程，属军管区，受行政管制、安全隐患、船闸设施设备检修、冲沙、临时突发问题、天气原因（特大雾）等影响，为确保三峡河段航运安全，缓解坝区通航压力，三峡通航管理局可能临时采取关闭双向或单向船闸等措施。届时将由游船公司提供旅行车将全船游客接驳至紧邻三峡大坝上游的港口或下游的宜昌港上下船（1小时高速）。<br>
                        3、遇夏季洪峰或秋冬枯水季节，轮船吨位过大而水位偏高或过低时，为确保安全，游轮将统一安排旅行车接驳全船客人自重庆市区前往涪陵港或者丰都港登船或自涪陵港（丰都港）离船前往重庆市区（1.5小时车/3小时车）<br>
                        4、若游船发生临时机械故障，为确保安全航行，船公司将调整航期或由指定安排其它游船接转。我司不承担其它的责任或赔偿。&nbsp;<br>
                        5、如因不可抗拒的外力所扰，无法提供原本保证的服务项目时，我司不负担任何责任或赔偿，这种不寻常的情况是指：战争的威胁，恐怖份子活动、自然灾害、火灾、天灾、气候、瘟疫的威胁、传染疾病和所有类似不可抗力。<br>
                        七、其他事项<br>
                        &nbsp;长江是我们的母亲河，请在旅游途中文明自己的个人行为，不要将果皮及相应的生活垃圾丢入长江，保护好我们的母亲河（违者罚款）
                    </div>
                    </asp:Literal>
                </div>
            </div>
        </div>
    </div>
    <div style="display: none" id="i_div_hidden">
        <input id="Hangqi" runat="server" type="hidden" />
        <input id="hidIsFapiao" runat="server" type="hidden" />
        <input id="hidAddressId" runat="server" type="hidden" />
        <input id="AddressFree" runat="server" type="hidden" />
        <textarea id="hd_page1json" runat="server"></textarea>
        <textarea id="txtFeiHuiYuanInfo" name="txtFeiHuiYuanInfo"></textarea>
        <textarea id="txtYouHuiInfo" name="txtYouHuiInfo"></textarea>
    </div>
    </form>

    <script type="text/javascript">
    <%=ScriptJson %>
    </script>

    <script type="text/javascript">
        var iPage = {
            BinRiLi: function() {
                QGD.initCalendar({
                    containerId: "showTable", //放日历容器ID
                    currentDate: new Date(Date.parse('<%=string.Format("{0:yyyy\\/MM\\/dd}",RiQi1) %>')), //当前月
                    firstMonthDate: new Date(Date.parse('<%=string.Format("{0:yyyy\\/MM\\/dd}",RiQi1) %>')), //当前月
                    nextMonthDate: new Date(Date.parse('<%=string.Format("{0:yyyy\\/MM\\/dd}",RiQi1.AddMonths(1)) %>')), //下一个月
                    areatype: '0', //团队线路区域类型
                    AddOrder: true,
                    maxMonth: 23,//配置日历追加几个月份(显示月份数量=maxMonth+1)
                    minDate: new Date(Date.parse('<%=string.Format("{0:yyyy\\/MM\\/dd}",DateTime.Now) %>'))
                });
            },
            SetDateInfo: function() {
                var tourlist = Tour;
                for (var i = 0; i < tourlist.length; i++) {
                    var objdiv = $("#" + tourlist[i].RiQi);
                    objdiv.attr("data-id", tourlist[i].RiQiId);
                    objdiv.attr("date-hq", tourlist[i].HangQiId);
                    objdiv.click(function() { iPage.SelectDateFrom(this) });
                    var jg = '<%=QishiMoney %>';

                    if (tourlist[i].QiShiJiaGe2 > 0) {
                        var _jg = tableToolbar.getFloat(tourlist[i].QiShiJiaGe2) + "";
                        if (_jg.indexOf(".") < 0) _jg = _jg + ".00";
                        jg = "¥" + _jg;
                    }

                    objdiv.append("<br /><span class=\"price\">" + jg + "起</span>");

                    if (i == 0 && '<%=Request.QueryString["riqiid"] %>' == '') objdiv.click();
                }
                $("#showTable").find("td").each(function() {
                    if ($.trim($(this).attr("data-id")) == '<%=Request.QueryString["riqiid"] %>') { $(this).click(); return false; }
                })
            },
            SelectDateFrom: function(obj) {
                var tourid = $(obj).closest("td").attr("data-id");
                var hq = $(obj).closest("td").attr("date-hq");
                var dateTime = $(obj).closest("td").attr("id");
                var jiage = $(obj).closest("td").find("span.price").html();
                $("#showTable").find("div.rili_Dbox").find("td").attr("class", "");
                $(obj).closest("td").attr("class", "on");
                $("#select_hangxian").val(hq);
                $.ajax({
                    type: "post",
                    cache: false,
                    url: "/Hangqi/HangQiInfo.aspx?type=selectHtml&id=" + hq + "&date=" + tourid,
                    dataType: "json",
                    data: "",
                    beforeSend: function(){
                        $(".showHtml").html("<p style='text-align:center;font-size:20px;line-height:40px;'><img src='/images/lodding.gif' />请稍等，正在为您查询！</p>");
                    },
                    success: function(ret) {
                        if (ret.result == "1") {
                            var _html = $(ret.obj);
                            _html.find("a[data-id='guojimore']").click(function() { guojimore(this); });
                            _html.find("th[data-class='guoji_mc']").each(function() {
                                var _t = $(this).html();
                                var _l = _t.length;
                                var _s = [];
                                for (var i = 0; i < _l; i++) _s.push(_t.substr(i, 1));
                                $(this).html(_s.join('<br/>'));
                            });
                            $(".showHtml").html(_html);
//                            $(".showHtml").find("a.jia").eq(0).click();
                        }
                    }
                });

                $("#i_div_riqi_xx").html('出发日期：' + dateTime + '    价格' + jiage);
            },
            SelectChangeFrom: function() {
                var item = $("#select_hangxian").val();
                window.location.href = "/Hangqi/HangQiInfo.aspx?id=" + item;
            },
            Bindth: function() {
                $("#select_hangxian").change(function() {
                    iPage.SelectChangeFrom();
                });

                $("#select_hangxiandown").change(function() {
                    window.location.href = "/Hangqi/HangQiInfo.aspx?id=" + $(this).val();
                    return false;
                });
            }
        };
        var MrHangQi = {};
        var JinEJson = { DingDanJinE: 0, JinFenJinE: 0, FujianChanPinJInE: 0, YouHuiJinE: 0, KuaiDiJinE: 0 };
        var PageSet = {
            PageProductSel: function() {
                var date = ""; var dateid = ""; var datehq = "";
                $("#showTable").find("div.rili_Dbox").find("td").each(function() {
                    if ($(this).attr("class") == "on") {
                        date = $(this).attr("id");
                        dateid = $(this).attr("data-id");
                        datehq = $(this).attr("date-hq");
                    }
                });
                if (dateid == "" && date == "") {
                    tableToolbar._showMsg("请选择出发日期");
                    return;
                }
                var jsonStr = {};
                jsonStr["hangqiid"] = datehq;
                jsonStr["chufadate"] = date;
                jsonStr["chufaId"] = dateid;
                jsonStr["options"] = FangXIngJson();

                var _data = { jsonhtml: JSON.stringify(jsonStr) };

                $.ajax({
                    type: "post",
                    cache: false,
                    url: "/Hangqi/HangQiInfo.aspx?type=CheckPage",
                    dataType: "json",
                    data: _data,
                    success: function(ret) {
                        if (ret.result == "1") {
                            hideBanner();
                            $("#ph_Page1").hide();
                            $("#ph_Page2").show();
                            $("#stepHtml").html(ret.obj.html);
                            MrHangQi["HangQiId"] = ret.obj.HangQiId;
                            MrHangQi["DingdanRenShu"] = tableToolbar.getInt(ret.obj.DingdanRenShu);
                            MrHangQi["DingdanZong"] = tableToolbar.getFloat(ret.obj.DingdanZong);
                            MrHangQi["Dingdanrenjun"] = tableToolbar.getFloat(ret.obj.Dingdanrenjun);
                            MrHangQi["RiQiId"] = ret.obj.RiQiId;
                            MrHangQi["FangXing"] = ret.obj.FangXing;
                            $("#td_page1renshu").html(tableToolbar.getInt(ret.obj.DingdanRenShu) + "人");
                            $("#td_page1zong").html("￥" + tableToolbar.getFloat(ret.obj.DingdanZong));
                            $("#td_page1renjun").html("￥" + tableToolbar.getFloat(ret.obj.Dingdanrenjun));

                            $("#Page3_Zong").html("￥" + tableToolbar.getFloat(ret.obj.DingdanZong));
                            $("#page3_bklist").html(ret.obj.html3);
                            $("#page3_dd_show").html(ret.obj.html2);
                            PageSet.cleackHtml();
                            $("#KeYongJiFen").html("（可用" + ret.obj.KeYongJiFen + "点）");
                            //$("#<%=hd_page1json.ClientID%>").val(JSON.stringify(ret.obj));
                            JinEJson.DingDanJinE = tableToolbar.getFloat(ret.obj.DingdanZong);

                            $("input[name='bkzj']").change(function() { PageSet.getYouHui(); });
                            //$("input[name='bkbrithday']").change(function() { PageSet.getYouHui(); });
                            $("input[name='sr1']").change(function() { PageSet.getYouHui(); });
                            $("select[name='select_Sex']").change(function() { PageSet.getYouHui(); });
                            $("input[name='sr01']").change(function() { PageSet.getYouHui(); });

                            $("select[name='sel_zjtype']").change(function() { PageSet.changeZJ(this); PageSet.getYouHui(); });
                            PageSet.setYK();
                            $(document).scrollTop(0);
                        }
                        else if (ret.result == "0") {
                            tableToolbar._showMsg(ret.msg);
                        }
                    }
                });
            },
            selectToContact: function() {
                $(".savacontact").live("click", function() {
                    if ($(this).attr("checked") == true) {
                        $(this).closest("div").find("input").val("1");
                    }
                    else {
                        $(this).closest("div").find("input").val("0");
                    }
                });
            },
            clearInputValue: function() {
                $("a.clearInput").live("click", function() {
                    $(this).closest("div").prev("ul").find("input").val("");
                });

            },
            JiFenCheck: function() {
                var star = false;
                var _self = this;
                var jifen = $("#txtDingDanJiFen").val();
                var reg = /^[-\+]?\d+$/;
                if (reg.exec(jifen)) {
                    if (jifen > 0) {
                        star = true;
                    }
                }
                else {
                    $("#txtDingDanJiFen").val('0');
                }
                if (star) {

                    var _data = {
                        txtDingDanJiFen: jifen,
                        txtHangQiid: $("#<%=Hangqi.ClientID %>").val()
                    };
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: "/Hangqi/HangQiInfo.aspx?type=jifen",
                        dataType: "json",
                        data: _data,
                        success: function(ret) {
                            if (ret.result == "1") {
                                var jifenJinE = tableToolbar.getFloat(ret.msg);
                                JinEJson.JinFenJinE = jifenJinE;
                                _self.getJinE();
                                $("#JiFenJinE_DuiHuan").html("-￥" + jifenJinE);

                            }
                            else {
                                tableToolbar._showMsg(ret.msg);
                                $("#txtDingDanJiFen").val('0');
                                $("#JiFenJinE_DuiHuan").html("-￥0");
                                JinEJson.JinFenJinE = 0;
                                _self.getJinE();
                            }
                        }
                    });
                }
            },
            cleackHtml: function() {
                $("div.step_box2").find("input").val(0);
                $("div.step_box2").find(".danxiangJe").attr("data-m", "").html("￥0");
            },
            bkHtml: function() {
                var _self = this;
                var jsonStr = {};
                jsonStr["hangqiid"] = $("#<%=Hangqi.ClientID %>").val();
                jsonStr["options"] = FuJianChanPinJson();
                var _date = { jsonHtml: JSON.stringify(jsonStr) }
                $.ajax({
                    type: "post",
                    cache: false,
                    url: "/Hangqi/HangQiInfo.aspx?type=CheckBk",
                    dataType: "json",
                    data: _date,
                    success: function(ret) {
                        if (ret.result == "1") {
                            $("#page3_fj_show").html(ret.obj.html4);

                            MrHangQi["FuJiaChanPin"] = ret.obj.FuJiaChanPin;
                            $("#<%=hd_page1json.ClientID%>").val(JSON.stringify(MrHangQi));
                            var JinE = tableToolbar.calculate(FangXingJinE(), FuJianChanPinJinE(), "+");

                            $("#Page3_ShiFu").html("￥" + tableToolbar.getFloat(JinE));
                            $("#Page3_FuJia").html("￥" + tableToolbar.getFloat(FuJianChanPinJinE()));
                            $("#ph_Page1").hide();
                            $("#ph_Page2").hide();
                            $("#ph_Page3").show();
                            hideBanner();
                            JinEJson.FujianChanPinJInE = tableToolbar.getFloat(FuJianChanPinJinE());
                            _self.getYouHui();
                        }
                        else {
                            tableToolbar._showMsg(ret.msg);
                        }
                    }
                });
            },
            sava: function() {
                var _feiHuiYuanInfo = PageSet.getFeiHuiYuanInfo();
                $("#<%=hidIsFapiao.ClientID %>").val(PageSet.RadioListValue('IscheckFaPiao'));
                $("#<%=hidAddressId.ClientID %>").val(PageSet.RadioListValue('addressCk'));

                if (!PageSet.checkForm()) return;

                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/Hangqi/HangQiInfo.aspx?type=sava",
                    dataType: "json",
                    data: $("#btnSava").closest("form").serialize(),
                    success: function(ret) {
                        if (_feiHuiYuanInfo != null) {
                            _feiHuiYuanInfo.id = ret.obj.FeiHuiYuanId;
                            PageSet.setFeiHuiYuanInfo(_feiHuiYuanInfo);
                        }
                        if (ret.result == "1") {
                            if (ret.obj.DingDanStatus == "<%=(int)EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus.成交 %>") {
                                PageSet.goZhiFu(ret.obj.DingDanId);
                            }
                            else {
                                showZZ('<div class="tc-boxbg"><a class="close-btn" href="/huiyuan/dingdan.aspx"></a><div class="tc-cont"><div class="big-txt">您的订单已提交审核 !</div><div class="b_txt01">我们的工作人员将尽快核准您的订单 请您待审核后再进行支付操作</div><div class="tc-btn"><a href="/huiyuan/dingdan.aspx">好的，了解了</a></div><div class="b_txt01"><a href="/default.aspx">【返回长江游轮首页】</a></div></div></div>');
                            }
                        }
                        else {
                            tableToolbar._showMsg(ret.msg);
                        }
                    }
                });
            },
            RadioListValue: function(name) {
                var list = $("input[name='" + name + "']:checked").val();
                return list;
            },
            checkForm: function() {
                var vf = ValiDatorForm.validator($(".address_btn").closest("form").get(0), "alert");
                if (!vf) return vf;
                var _phone = 0;
                $("input[name='bkphone']").each(function() { if (RegExps.isMobile.test($(this).val())) _phone++; });
                if (_phone == 0) { tableToolbar._showMsg("请至少输入一位出行旅客的手机号码！"); return false; }

                if ($.trim($("#txtYudingphone").val()).length == 0 && $.trim($("#txtYudingtell").val()).length == 0) {
                    tableToolbar._showMsg("预订人手机号码和座机电话至少填写一项！"); return false;
                }

                if ($("#<%=hidIsFapiao.ClientID %>").val() == "1") {                    
                    if ($.trim($("#txtFapiaoTitle").val()).length == 0) {
                        tableToolbar._showMsg("-请填写发票抬头！");
                        return false;
                    }
                    if ($("#txt_fapiao_mxlx").val().length == 0) {
                        tableToolbar._showMsg("-请选择发票明细！");
                        return false;
                    }
                    if ($("#<%=hidAddressId.ClientID %>").val() == "") {
                        tableToolbar._showMsg("-请选择配送方式！");
                        return false;
                    }
                }

                return vf;
            },
            AddressSave: function() {
                var _self = this;
                if (PageSet.bindCk("in")) {
                    var _data = {};
                    _data["txtName"] = $("#txtName").val();
                    _data["txtTell"] = $("#txtTell").val();
                    _data["sel_province"] = $("#sel_province").val();
                    _data["sel_city"] = $("#sel_city").val();
                    _data["sel_country"] = $("#sel_country").val();
                    _data["txtAdress"] = $("#txtAdress").val();
                    _data["txtZip"] = $("#txtZip").val();
                    var _feiHuiYuanInfo = _self.getFeiHuiYuanInfo();
                    _data["txtFeiHuiYuanInfo"] = $("#txtFeiHuiYuanInfo").val();

                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: "/Hangqi/HangQiInfo.aspx?type=address",
                        dataType: "json",
                        data: _data,
                        success: function(ret) {
                            if (ret.result == "1") {
                                var html = "<li><label><input type=\"radio\" name=\"addressCk\" value=\"" + ret.msg + "\">";
                                html += $("#txtAdress").val() + "&nbsp;&nbsp;邮编：" + $("#txtZip").val() + "&nbsp;&nbsp;联系电话：" + $("#txtTell").val() + "</label></li>";
                                $(".address_list").append(html);
                                $(".address_form").find("input").val("");

                                $('input[name="addressCk"]:last').attr('checked', 'checked');
                            }
                            else {
                                tableToolbar._showMsg(ret.msg);
                            }

                            if (_feiHuiYuanInfo != null) {
                                _feiHuiYuanInfo.id = ret.obj.FeiHuiYuanId;
                                _self.setFeiHuiYuanInfo(_feiHuiYuanInfo);
                            }
                        }
                    });
                }
            },
            bindCk: function(obj) {
                var str = "";
                if (obj == "in") {
                    if ($("#txtName").val() == "") {
                        str += "-收件人不能为空！</br>";
                    }
                    if ($("#txtTell").val() == "") {
                        str += "-联系电话不能为空！</br>";
                    }
                    if ($("#sel_province").val() == "") {
                        str += "-请选择省份！</br>";
                    }
                    if ($("#sel_city").val() == "") {
                        str += "-请选择城市！</br>";
                    }
                    if ($("#sel_country").val() == "") {
                        str += "-请选择县区！</br>";
                    }
                    if ($("#txtZip").val() == "") {
                        str += "-邮编不能为空！</br>";
                    }
                    if ($("#txtAdress").val() == "") {
                        str += "-地址不能为空！</br>";
                    }
                    if (str != "") {
                        tableToolbar._showMsg(str);
                        return false;
                    }
                    else {
                        return true;
                    }
                }
                else
                    return false;
            },
            InitSelectFrom: function() {
                pcToobar.init({ pID: '#sel_province', cID: '#sel_city', comID: '<%=this.YuMingInfo.CompanyId %>', xID: "#sel_country" });
            },
            AddressShow: function() {
                $(".address_form").show();
            },
            PageBindth: function() {
                $("#Page1,.yd_btn").click(function() {
                    PageSet.PageProductSel();
                    //                    PageSet.F101();
                });
                $("#PageDown1_back").click(function() {
                    $("#ph_Page1").show();
                    $("#ph_Page2").hide();
                    showBanner();
                    $(document).scrollTop(0);
                });
                $("#PageDown1_up").click(function() {
                    PageSet.bkHtml();
                    $(document).scrollTop(0);
                });
                $("#pagedow2_back").click(function() {
                    var value = PageSet.RadioListValue('IscheckFaPiao');
                    var ZongJinE = tableToolbar.getFloat($("#Page3_ShiFu").attr("data-m"));
                    $("input[name=IscheckFaPiao]:eq(0)").attr("checked", 'checked');
                    $("#KuaiDiJinE").html("￥" + 0);

                    $("#ph_Page2").show();
                    $("#ph_Page3").hide();
                    $(document).scrollTop(0);
                });
                $("#btnSava").click(function() {
                    if (iLogin.getM().isLogin) {
                        PageSet.sava();
                    } else {
                        PageSet.F101();
                    }
                })
                PageSet.InitSelectFrom();
                $(".address_btn").click(function() {
                    //PageSet.bindCk('in');
                    PageSet.AddressSave();
                });
                $("#Address").click(function() {
                    if (iLogin.getM().isLogin) PageSet.AddressShow();
                    else PageSet.F101(PageSet.AddressShow);
                });
                PageSet.selectToContact();
                PageSet.clearInputValue();
                $("#txtDingDanJiFen").blur(function() {
                    PageSet.JiFenCheck();
                });
                $('input:radio[name="IscheckFaPiao"]').click(function() {
                    var value = PageSet.RadioListValue('IscheckFaPiao');
                    var ZongJinE = tableToolbar.getFloat($("#Page3_ShiFu").attr("data-m"));
                    var Free = tableToolbar.getFloat($("#<%=AddressFree.ClientID %>").val());
                    if (value == "1") {
                        $("#KuaiDiJinE").html("￥" + Free);
                        JinEJson.KuaiDiJinE = Free;
                    }
                    else {
                        $("#KuaiDiJinE").html("￥" + 0);
                        JinEJson.KuaiDiJinE = 0;
                    }
                    PageSet.getJinE();
                });
            },
            F101: function(callback) {

                if (typeof callback != "function") { callback = PageSet.sava; }
                var _self = this;
                var _isLogin = iLogin.getM().isLogin;
                //if (_isLogin) { _self.PageProductSel(); return; }
                if (_isLogin) { callback(); return; }
                var _feiHuiYuanInfo = _self.getFeiHuiYuanInfo();
                //if (_feiHuiYuanInfo != null) { _self.PageProductSel(); return }
                if (_feiHuiYuanInfo != null) { callback(); return }

                //showZZ('<div><p>已经注册账号</p><p>账号：<input type="text" id="i_u_0" /></p><p>密码：<input type="password" id="i_p_0" /></p><p><input type="button" value="登录预订" id="i_btn_0" /></p><p>不想注册账号</p><p>姓名：<input type="text" id="i_xm_0" /></p><p>手机：<input type="text" id="i_sj_0" /></p><p><input type="button" value="直接预订" id="i_btn_1" /></p><p><input type="button" value="关闭窗口" id="i_btn_2" /></p></div>');
                //showZZ('<div class="user_boxy"><a href="javascript:void(0)" id="i_btn_2" class="close_btn"><em>X</em>关闭</a><h3>已经注册账号</h3><ul class="login_form"><li><label>账号</label><input class="formsize270 input_style" type="text" id="i_u_0" /></li><li><label>密码</label><input class="formsize270 input_style" type="password" id="i_p_0" /></li><li class="login_txt"><input type="button" value="登录" id="i_btn_0" class="user_loginbtn" /></li></ul><h3>不想注册账号</h3><ul class="login_form"><li><label>姓名</label><input class="formsize270 input_style" type="text" id="i_xm_0" /></li><li><label>手机</label><input class="formsize270 input_style" type="text" id="i_sj_0" /></li><li class="login_txt"><input type="button" value="直接预订" id="i_btn_1" class="user_loginbtn" /></li></ul></div>');
                //                showZZ('<div class="user_boxy"><a href="javascript:void(0)" id="i_btn_2" class="close_btn"><em>X</em>关闭</a><h3>已经注册账号</h3><ul class="login_form"><li>账号</li><li><input class="formsize270 input_style" type="text" id="i_u_0" /></li><li>密码</li><li><input class="formsize270 input_style" type="password" id="i_p_0" /></li><li class="login_txt"><input type="button" value="登录" id="i_btn_0" class="user_loginbtn" /></li></ul><h3>不想注册账号</h3><ul class="login_form"><li>姓名</li><li><input class="formsize270 input_style" type="text" id="i_xm_0" /></li><li>手机</li><li><input class="formsize270 input_style" type="text" id="i_sj_0" /></li><li class="login_txt"><input type="button" value="直接预订" id="i_btn_1" class="user_loginbtn" /></li></ul></div>');
                showZZ('	<div class="user_boxy login_boxy">'
+ '		   <a class="close_btn" href="javascript:void(0)" id="i_btn_2"><em>X</em>关闭</a>'
+ '		   <div class="login_boxyL">'
+ '				<h3>会员登录<span><a href="/Register.aspx" target="_blank">免费注册</a></span></h3>'
+ '				<div class="tip margin_T16">可享积分换礼，预订返现，专属服务等优惠！</div>'
+ '				<div class="loginbar margin_T16">'
+ '	                     <div class="login_select"><label><input type="radio" checked="checked" value="" id="rdo1" name="rdo">普通登录</label><label><input type="radio" value="" id="rdo2" name="rdo">手机动态密码登录</label></div> '
+ '	                     <ul class="login_form" id="ul1">'
+ '	                        <li><label>登录名</label><input value="" class="formsize200 input_style" id="i_u_0"/>'
+ '	                        </li>'
+ '	                        <li><label>密&#12288;码</label><input class="formsize200 input_style" type="password" id="i_p_0"/><a class="forget_pwd" href="/reset.aspx" target="_blank">忘记密码?</a></li>'
+ '	                        <li class="login_txt"> <label class="no_style"><input type="checkbox" checked="checked" value="" id="c_1">30天内自动登录</label></li>'
+ '	                        <li class="login_txt"><a class="loginbtn" href="javascript:void(0)" id="i_btn_0">登录</a></li>'
+ '	                     </ul>'
+ '	                     <ul style="display:none;" class="login_form" id="ul2">'
+ '	                        <li><label>手机号</label><input value="" class="formsize200 input_style" id="i_sj_0"/>'
+ '	                        </li>'
+ '	                        <li><label>验证码</label><input class="formsize100 input_style" id="txtYanZhengMa"/><a href="javascript:void(0)">'
+ '                        <img style="cursor: pointer; margin-top: 0px" title="点击更换验证码" '
+ '                                    align="middle" width="88" height="30" id="imgCheckCode" src="/ashx/ValidateCode.ashx?ValidateCodeName=SYS_VC&t=<%=DateTime.Now.ToString("HHmmssffff") %>" />'
+ '</a></li>'
+ '	                        <li><label>密&#12288;码</label><input value="" class="formsize100 input_style" type="password" id="i_p_1"/> <a class="pwd_bg" href="javascript:void(0)" id="i_p">发送动态密码</a></li>'
+ '	                        <li class="login_txt"> <label class="no_style"><input type="checkbox" checked="checked" value="" id="c_2">30天内自动登录</label></li>'
+ '	                        <li class="login_txt"><a class="loginbtn" href="javascript:void(0)" id="i_btn_1">登录</a></li>'
+ '	                     </ul>'
                //+'						 <div class="login_cont margin_T16">	'
                //+'						    <div class="lg_Title">您还可以使用以下方式登录：</div>	'
                //+'							<div class="lg_icon">'
                //+'							   <a class="zfb" href="#">支付宝</a>'
                //+'							   <a class="qq" href="#">QQ</a>'
                //+'							   <a class="sina" href="#">新浪微博</a>'
                //+'							   <a class="qh360" href="#">360</a>'
                //+'							   <a class="wangyi" href="#">网易</a>'
                //+'							   <a class="renren" href="#">人人网</a>'
                //+'							   <a class="msn" href="#">MSN</a>'
                //+'							   <a class="baidu" href="#">百度</a>'
                //+'							</div>'
                //+'						 </div>	'
+ '	         </div>'
+ '		   </div>'
+ '		   <div class="login_boxyR">'
+ '		      <h5>免登录预订</h5>'
+ '			  <p>此预订方式将无法享受维诗达积分，会员优惠，礼品卡购买和领用</p>	'
+ '			  <a class="yudin_btn" href="javascript:void(0)" id="i_btn_3">免登录预订</a>	'
+ '		   </div>'
+ '	    </div>'
)

                function __initu() {
                    PageSet.initU();
                }

                //获取验证码
                function _getcode() {
                    var c = document.cookie, ckcode = "", tenName = "";
                    for (var i = 0; i < c.split(";").length; i++) {
                        tenName = c.split(";")[i].split("=")[0];
                        ckcode = c.split(";")[i].split("=")[1];
                        if ($.trim(tenName) == "SYS_VC") {
                            break;
                        } else {
                            ckcode = "";
                        }
                    }
                    return $.trim(ckcode);
                }

                function __login() {
                    var u = $.trim($("#i_u_0").val()), p = $.trim($("#i_p_0").val()), ckcode = '', is = $("#c_1").attr("checked");
                    if (u == "") { tableToolbar._showMsg("请输入用户名!"); $("#i_u_0").focus(); return; }
                    if (p == "") { tableToolbar._showMsg("请输入密码"); return; }

                    tableToolbar._showMsg("正在登录....");
                    $("#i_btn_0").unbind().css("cursor", "default");

                    blogin5({ u: u, p: p, vc: ckcode, is: is }
                        , function(h) { /*tableToolbar._showMsg("登录成功", $("#btnSava").unbind().bind("click", function() { PageSet.sava(); }));*/; tableToolbar._showMsg("登录成功"); _self.initDiZhi(); __initu(); closeZZ(); /*_self.PageProductSel();*/callback(); return; }
                        , function(m) { tableToolbar._showMsg(m); $("#i_btn_0").click(function() { __login(); return false; }).css("cursor", "pointer"); }
                    );
                }

                function __login1() {
                    var u = $.trim($("#i_sj_0").val()), p = $.trim($("#i_p_1").val()), ckcode = $.trim($("#txtYanZhengMa").val()), is = $("#c_2").attr("checked");
                    if (u == "") { tableToolbar._showMsg("请输入手机!"); $("#i_u_0").focus(); return; }
                    if (p == "") { tableToolbar._showMsg("请输入密码"); $("#i_p_1").focus(); return; }
                    if (ckcode == "") { tableToolbar._showMsg("请输入验证码"); $("#txtYanZhengMa").focus(); return; }

                    tableToolbar._showMsg("正在登录....");
                    $("#i_btn_1").unbind().css("cursor", "default");

                    blogin5({ u: u, p: p, vc: ckcode, is: is }
                        , function(h) { /*tableToolbar._showMsg("登录成功", $("#btnSava").unbind().bind("click", function() { PageSet.sava(); }));*/; tableToolbar._showMsg("登录成功"); _self.initDiZhi(); __initu(); closeZZ(); /*_self.PageProductSel();*/callback(); return; }
                        , function(m) { tableToolbar._showMsg(m); $("#i_btn_1").click(function() { __login1(); return false; }).css("cursor", "pointer"); }
                    );
                }

                function __nologin() {
                    //var _feiHuiYuanInfo = { sj: $.trim($("#i_sj_0").val()), xm: $.trim($("#i_xm_0").val()), id: "" };
                    var _feiHuiYuanInfo = { sj: "", xm: "", id: "" };
                    //                    if (_feiHuiYuanInfo.xm.length < 1) { tableToolbar._showMsg("请输入联系姓名!"); $("#i_xm_0").focus(); return; }
                    //                    if (_feiHuiYuanInfo.sj.length < 1) { tableToolbar._showMsg("请输入联系手机!"); $("#i_sj_0").focus(); return; }
                    _self.setFeiHuiYuanInfo(_feiHuiYuanInfo);
                    closeZZ();
                    //                    _self.PageProductSel();
                    //$("#btnSava").unbind().bind("click", function() { PageSet.sava(); })
                    callback();
                }

                $("#i_btn_0").click(function() { __login(); });
                $("#i_btn_1").click(function() { __login1(); });
                $("#i_btn_2").click(function() { closeZZ(); });
                $("#i_btn_3").click(function() { __nologin(); });
                //                $('#i_u_0').focus();
                $('#i_u_0').keydown(function(e) { if (e.keyCode == 13) { $("#i_btn_0").click(); } });
                $('#i_p_0').keydown(function(e) { if (e.keyCode == 13) { $("#i_btn_0").click(); } });
                $('#i_xm_0').keydown(function(e) { if (e.keyCode == 13) { $("#i_btn_1").click(); } });
                $('#i_sj_0').keydown(function(e) { if (e.keyCode == 13) { $("#i_btn_1").click(); } });
                //$('#i_u_0,#i_sj_0').focus(function() { if ($.trim($(this).val()) == "用户名/卡号/手机/邮箱") { $(this).val(""); } });
                //$('#i_u_0,#i_sj_0').blur(function() { if ($.trim($(this).val()) == "") { $(this).val("用户名/卡号/手机/邮箱"); } });
                $('#rdo1').click(function() { $('#ul1').css('display', ''); $('#ul2').css('display', 'none'); });
                $('#rdo2').click(function() { $('#ul1').css('display', 'none'); $('#ul2').css('display', ''); });
                $('#imgCheckCode').click(function() { $(this).attr("src", '/ashx/ValidateCode.ashx?ValidateCodeName=SYS_VC&id=' + Math.random()) });
                $("#i_p").click(function() {
                    if ($.trim($("#txtYanZhengMa").val()) != _getcode()) { tableToolbar._showMsg("请输入正确的验证码！"); return false; }
                    if (!RegExps.isMobile.test($.trim($("#i_sj_0").val()))) { tableToolbar._showMsg("请输入正确的手机号码！"); return false; }
                    $(this).unbind("click").css("cursor", "default");
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: "/Ashx/Handler.ashx?dotype=fasongmima&shouji=" + $.trim($("#i_sj_0").val()),
                        dataType: "json",
                        success: function(ret) {
                            tableToolbar._showMsg(ret.msg);
                        },
                        error: function() {
                            tableToolbar._showMsg(tableToolbar.errorMsg);
                        }
                    })
                    //$(this).bind("click").css("cursor", "pointer");
                })
            },
            getFeiHuiYuanInfo: function() {
                var _feiHuiYuanInfo = $("#i_div_hidden").data("FeiHuiYuanInfo");
                if (typeof (_feiHuiYuanInfo) != "undefined" && _feiHuiYuanInfo != null) { return _feiHuiYuanInfo; }
                return null;
            },
            setFeiHuiYuanInfo: function(info) {
                $("#i_div_hidden").data("FeiHuiYuanInfo", info);
                $("#txtFeiHuiYuanInfo").val(JSON.stringify(info));

                if (info.id.length > 0) PageSet.initU();
            },
            initDiZhi: function() {
                $.ajax({
                    type: "post", cache: false, url: "/Hangqi/HangQiInfo.aspx?type=getdizhi", dataType: "json",
                    success: function(response) {
                        $("#i_changyongdizhi").html(response.obj);
                    }
                });
            },
            getYouHui: function() {
                var _self = this;
                var _data = {};
                _data["txtHangQiId"] = MrHangQi.HangQiId;
                _data["txtRiQiId"] = MrHangQi.RiQiId;
                _data["txtRenShu"] = MrHangQi.DingdanRenShu;
                _data["txtJinE"] = MrHangQi.DingdanZong;
                _data["txtYouKe"] = "";

                var youKes = [];
                var zjhs = $("input[name='bkzj']");
                var xbs = $("select[name='select_Sex']");
                //var srs = $("input[name='bkbrithday']");
                var srs = $("input[name='sr1']");
                var zjlx = $("select[name='sel_zjtype']");
                var sr01s = $("input[name='sr01']");
                if (zjhs.length > 0) {
                    for (var i = 0; i < zjhs.length; i++) {
                        var youKe = {};
                        youKe["zjhm"] = zjhs.eq(i).val();
                        youKe["xb"] = xbs.eq(i).val();
                        youKe["sr"] = srs.eq(i).val();
                        youKe["zjlx"] = zjlx.eq(i).val();
                        youKe["sr01"] = sr01s.eq(i).val();
                        youKes.push(youKe);
                    }
                }
                _data["txtYouKe"] = JSON.stringify(youKes);

                $.ajax({
                    type: "POST", url: "/ashx/Handler.ashx?dotype=hangqiyouhui", data: _data, dataType: "json", cache: false, async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            _self.getYouHuiCallback(response.obj);
                        } else _self.clearYouHui();
                    }
                });
            },
            getYouHuiCallback: function(data) {
                var s = [];
                for (var i = 0; i < data.youhuis.length; i++) {
                    s.push('<li>');
                    s.push('<span>');
                    s.push(data.youhuis[i].miaoshu);
                    s.push('</span>');
                    s.push('<em>');
                    s.push('-¥' + data.youhuis[i].jine);
                    s.push('</em>')
                    s.push('</li>');
                }
                $("#txtYouHuiInfo").val(JSON.stringify(data));
                $("#i_span_youhui").html(s.join(''));
                JinEJson.YouHuiJinE = data.jine;
                this.getJinE();
            },
            clearYouHui: function() {
                $("#txtYouHuiInfo").val('');
                $("#i_span_youhui").html('');
                JinEJson.YouHuiJinE = 0;
                this.getJinE();
            },
            getJinE: function() {
                var DingDan = JinEJson.DingDanJinE + JinEJson.FujianChanPinJInE + JinEJson.KuaiDiJinE - JinEJson.JinFenJinE - JinEJson.YouHuiJinE;
                $("#Page3_ShiFu").html("￥" + tableToolbar.getFloat(DingDan));
            },
            goZhiFu: function(dingDanId) {
                var _u = "";
                var _m = iLogin.getM();
                if (_m.isLogin) _u = _m.token;
                else {
                    var _feiHuiYuan = this.getFeiHuiYuanInfo();
                    if (_feiHuiYuan != null) _u = _feiHuiYuan.id;
                }
                tableToolbar._showMsg("下单成功，正在跳转至支付窗口");

                window.location.href = "/Hangqi/OrderPay.aspx?dingdanleixing=<%=(int)EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing.航期订单 %>&dingdanid=" + dingDanId + "&token=" + _u;
            },
            DianPingMore: function() {
                if (DingPing_Index == -1) return;
                DingPing_Index++;
                var hagnqi = '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>';
                $.ajax({ type: "post", cache: false, url: "/Ashx/Handler.ashx?dotype=dianping&index=" + DingPing_Index + "&hangqiID=" + hagnqi, dataType: "json",
                    success: function(response) {
                        if (response.result == "0") {
                            DingPing_Index = -1;
                            $("#dingping_more").text("没有更多了");
                        }
                        else {
                            $("#dingping").append(response.obj);
                        }
                    }
                });

            },
            DianPingMore: function(pageIndex) {
                //if (DingPing_Index == -1) return;
                //pageIndex=DingPing_Index++;
                var hagnqi = '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>';
                $.ajax({ type: "post", cache: false, url: "/Ashx/Handler.ashx?dotype=dianping&index=" + pageIndex + "&hangqiID=" + hagnqi, dataType: "json",
                    success: function(response) {
                        if (response.result == "0") {
                            pageIndex = -1;
                            //$("#dingping_more").text("没有更多了");
                        }
                        else {
                            $("#dingping").append(response.obj);
                        }
                    }
                });

            },
            ZiXunMore: function() {
                if (ZiXun_Index == -1) return;
                ZiXun_Index++;
                var hagnqi = '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>';
                var lx = "";
                $("#p_zxlist").find("a").each(function() {
                    if ($(this).attr("class") == "select") {
                        lx = $(this).attr("data-lx");
                    }
                });
                var data = { txtHangQiId: hagnqi, txtLeiXing: lx };
                $.ajax({ type: "post", cache: false, url: "/Ashx/Handler.ashx?dotype=zixun&index=" + ZiXun_Index, dataType: "json", data: data,
                    success: function(response) {
                        if (response.result == "0") {
                            ZiXun_Index = -1;
                            $("#zixun_more").text("没有更多了");
                        }
                        else {
                            $("#zixun").append(response.obj);
                        }
                    }
                });

            },
            ShowFaPiao: function() {
                $("#checkY").closest("li").nextAll().show();
                $('div[data-id="peisongfangshi"]').show();
            },
            HideFaPiao: function() {
                $("#checkN").closest("li").nextAll().hide();
                $('div[data-id="peisongfangshi"]').hide();
            },
            changeZJ: function(obj) {
                var _ul = $(obj).closest("ul");
                var _v = $(obj).val();

                if (_v == "0") {
                    _ul.find('div[data-class="zjhm"]').hide();
                    _ul.find('div[data-class="sr01"]').hide();
                    _ul.find('li[data-class="zjyxq"]').hide();
                    _ul.find('li[data-class="sr"]').hide();
                    _ul.find('li[data-class="xb"]').hide();
                }

                if (_v == "1") {
                    _ul.find('div[data-class="zjhm"]').show();
                    _ul.find('div[data-class="sr01"]').hide();
                    _ul.find('li[data-class="zjyxq"]').hide();
                    _ul.find('li[data-class="sr"]').hide();
                    _ul.find('li[data-class="xb"]').hide();
                }

                if (_v == "2") {
                    _ul.find('div[data-class="zjhm"]').hide();
                    _ul.find('div[data-class="sr01"]').show();
                    _ul.find('li[data-class="zjyxq"]').hide();
                    _ul.find('li[data-class="sr"]').hide();
                    _ul.find('li[data-class="xb"]').hide();
                }

                if (_v == "3") {
                    _ul.find('div[data-class="zjhm"]').show();
                    _ul.find('div[data-class="sr01"]').hide();
                    _ul.find('li[data-class="zjyxq"]').show();
                    _ul.find('li[data-class="sr"]').show();
                    _ul.find('li[data-class="xb"]').show();
                }

                if (_v == "4") {
                    _ul.find('div[data-class="zjhm"]').hide();
                    _ul.find('div[data-class="sr01"]').hide();
                    _ul.find('li[data-class="zjyxq"]').hide();
                    _ul.find('li[data-class="sr"]').hide();
                    _ul.find('li[data-class="xb"]').hide();
                }
            },
            setYK: function() {
                $('li[data-class="zjyxq"]').hide();
                $('li[data-class="sr"]').hide();
                $('li[data-class="xb"]').hide();
                $('div[data-class="zjhm"]').hide();
                $('div[data-class="sr01"]').hide();

                $("input[name='bkzj']").focus(function() { if ($.trim(this.value) == "请输入证件号码") { this.value = ""; } });
                $("input[name='bkzj']").blur(function() { if ($.trim(this.value) == "") { this.value = "请输入证件号码"; } });

                $("input[name='yxq1']").focus(function() { if ($.trim(this.value) == "年份yyyy") { this.value = ""; } });
                $("input[name='yxq1']").blur(function() { if ($.trim(this.value) == "") { this.value = "年份yyyy"; } });

                $("input[name='yxq2']").focus(function() { if ($.trim(this.value) == "月份mm") { this.value = ""; } });
                $("input[name='yxq2']").blur(function() { if ($.trim(this.value) == "") { this.value = "月份mm"; } });

                $("input[name='yxq3']").focus(function() { if ($.trim(this.value) == "日期dd") { this.value = ""; } });
                $("input[name='yxq3']").blur(function() { if ($.trim(this.value) == "") { this.value = "日期dd"; } });

                $("input[name='sr1']").focus(function() { if ($.trim(this.value) == "出生年份") { this.value = ""; } });
                $("input[name='sr1']").blur(function() { if ($.trim(this.value) == "") { this.value = "出生年份"; } });

                $("input[name='sr2']").focus(function() { if ($.trim(this.value) == "出生月份") { this.value = ""; } });
                $("input[name='sr2']").blur(function() { if ($.trim(this.value) == "") { this.value = "出生月份"; } });

                $("input[name='sr3']").focus(function() { if ($.trim(this.value) == "出生日期") { this.value = ""; } });
                $("input[name='sr3']").blur(function() { if ($.trim(this.value) == "") { this.value = "出生日期"; } });

                $("input[name='sr01']").focus(function() { if ($.trim(this.value) == "出生年份") { this.value = ""; } });
                $("input[name='sr01']").blur(function() { if ($.trim(this.value) == "") { this.value = "出生年份"; } });

                $("input[name='sr02']").focus(function() { if ($.trim(this.value) == "出生月份") { this.value = ""; } });
                $("input[name='sr02']").blur(function() { if ($.trim(this.value) == "") { this.value = "出生月份"; } });

                $("input[name='sr03']").focus(function() { if ($.trim(this.value) == "出生日期") { this.value = ""; } });
                $("input[name='sr03']").blur(function() { if ($.trim(this.value) == "") { this.value = "出生日期"; } });

                $("input[name='bkphone']").val("请至少输入一位出行旅客的手机号码");
                $("input[name='bkphone']").focus(function() { if ($.trim(this.value) == "请至少输入一位出行旅客的手机号码") { this.value = ""; } });
                $("input[name='bkphone']").blur(function() { if ($.trim(this.value) == "") { this.value = "请至少输入一位出行旅客的手机号码"; } });
                $("span[data-class='yktxsm']").css({ "cursor": "pointer" });
                $("span[data-class='yktxsm']").bt({ contentSelector: function() { return "1. 乘客姓名必须与所持证件一致。<br/>2. 名字中含生僻字可直接输入拼音代替。例：“王鬳”可输入为“王yan”或者“王-yan”。<br/>3. 姓名中不可含有称谓等词语，如：小姐、先生、太太、夫人等。<br/>4. 中文姓名不可少于2个汉字，英文姓名不可少于2个英文单词。<br/>登机特别提示：<br/>1. 持护照登机，如使用中文姓名，请确保护照上有相应的中文姓名。<br/>2. 持护照登机的外宾，请以姓在前名在后的方式填写，例：Zhang（姓）/San（名），不区分大小写" }, positions: ['top'], fill: '#FFF2B5', strokeStyle: '#D59228', noShadowOpts: { strokeStyle: "#D59228" }, spikeLength: 10, spikeGirth: 15, width: 500, overlap: 0, centerPointY: 1, cornerRadius: 4, shadow: true, shadowColor: 'rgba(0,0,0,.5)', cssStyles: { color: '#00387E', 'line-height': '180%'} });
            },
            initU: function() {
                if ($("#i_li_huiyuan_info").length > 0) return;
                var _m = iLogin.getM();
                if (!_m.isLogin) return;
                var s = [];
                s.push('<li id="i_li_huiyuan_info"><span><a href="/Huiyuan/MyInformation.aspx">您好，&nbsp;' + _m.u + '</a></span></li>');
                s.push('<li><span><a id="Login_Out" href="/Login.aspx">退出</a></span></li>');
                s.push('<li><span><a href="/HuiYuan/DingDan.aspx">我的订单</a></span></li>');
                $("#i_li_dl,#i_li_zc").remove();
                $("#i_li_scj").before(s.join(''));
            }
        }
        var DingPing_Index = 1; //点评页面索引
        var ZiXun_Index = 1; //咨询问答页面索引
        $(function() {

            $("#i_a_tiwen").click(function() {
                var data = { txtHangQiId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>', txtLeiXing: "", txtNeiRong: "" };

                //showZZ('<div class="tiwen_boxy"><p>咨询分类：<select id="txt_tw_lx"><option value="0">交通</option><option value="1">住宿</option><option value="2">景点</option><option value="3">餐饮</option><option value="4">其他</option></select></p><p>咨询内容：</p><p><textarea id="txt_tw_nr" style="width: 300px; height: 100px"></textarea></p><p></p><p><input type="button" value="我要咨询" id="i_btn_tw_01" /><input type="button" value="关闭窗口" id="i_btn_tw_02" /></p><p></p></div>');
                showZZ('<div class="user_boxy tiwen_boxy"><a href="javascript:void(0)" id="i_btn_tw_02" class="close_btn"><em>X</em>关闭</a><ul class="login_form"><li>咨询分类</li><li><select id="txt_tw_lx" class="formsize270 input_style"><option value="0">交通</option><option value="1">住宿</option><option value="2">景点</option><option value="3">餐饮</option><option value="4">其他</option></select></li><li>咨询内容</li><li><textarea id="txt_tw_nr" class="formsize270 input_style" style="height: 100px"></textarea></li><li class="login_txt"><input type="button" value="我要咨询" id="i_btn_tw_01" class="user_loginbtn" /></li></ul></div>');

                function __tiwen() {
                    if ($.trim($("#txt_tw_nr").val()).length < 1) { tableToolbar._showMsg("请输入咨询内容"); return; }
                    data.txtLeiXing = $.trim($("#txt_tw_lx").val());
                    data.txtNeiRong = $.trim($("#txt_tw_nr").val());
                    $.ajax({
                        type: "POST", url: "/Ashx/Handler.ashx?dotype=wdtw", cache: false, async: false, dataType: "json", data: data,
                        success: function(response) {
                            if (response.result == "1") { tableToolbar._showMsg("提交成功！"); }
                            else { tableToolbar._showMsg("提交失败！"); }
                            closeZZ();
                        }
                    });
                }

                $("#i_btn_tw_01").click(function() { __tiwen(); });
                $("#i_btn_tw_02").click(function() { closeZZ(); });
            });
            $("#p_zxlist").find("a").live("click", function() {
                $(this).closest("p").find("a").attr("class", "");
                $(this).attr("class", "select");

                ZiXun_Index = 1;
                var data = { txtHangQiId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>', txtLeiXing: $(this).attr("data-lx") };
                $.ajax({
                    type: "POST", url: "/Ashx/Handler.ashx?dotype=zixun&index=" + ZiXun_Index, cache: false, async: false, dataType: "json", data: data,
                    success: function(response) {
                        if (response.result == "1") {
                            $("#zixun").html("");
                            $("#zixun").append(response.obj);
                            $("#zixun_more").text("查看更多了+");
                        }
                        else {
                            ZiXun_Index = -1;
                            $("#zixun").html("<li>暂无内容！</li>");
                            $("#zixun_more").text("没有更多了");
                        }
                    }
                });
            });
            PageSet.PageBindth();
            iPage.BinRiLi();
            iPage.SetDateInfo();
            iPage.Bindth();
            $("a.jian").live("click", function() {
                var num = tableToolbar.getInt($(this).closest("span").find("input").val()) - 1;
                if (num < 0) num = 0;
                $(this).closest("span").find("input").val(num);
                $("#heji").html("合计：<em  class=\"b_price\">￥" + FangXingJinE() + "</em>");
            })
            $("a.jia").live("click", function() {
                var num = tableToolbar.getInt($(this).closest("span").find("input").val()) + 1;
                $(this).closest("span").find("input").val(num);
                $("#heji").html("合计：<em  class=\"b_price\">￥" + FangXingJinE() + "</em>");
            })

            $("a.page_jia").live("click", function() {
                var num = tableToolbar.getInt($(this).closest("span").find("input").val()) + 1;

                $(this).closest("span").find("input").val(num);
                var ShiFuJinE = tableToolbar.calculate(FangXingJinE(), FuJianChanPinJinE(), "+");
                var AveJinE = tableToolbar.calculate(ShiFuJinE, MrHangQi.DingdanRenShu, "/");
                var JIen = tableToolbar.calculate(num, $(this).closest("span").find("input").attr("data-dj"), "*");
                $(this).closest("td").next("td").find("span").html("￥" + JIen);
                $("#td_page1zong").html("￥" + ShiFuJinE);

                $("#td_page1renjun").html("￥" + tableToolbar.getFloat(AveJinE));

            });
            $("a.page_jian").live("click", function() {
                var num = tableToolbar.getInt($(this).closest("span").find("input").val()) - 1;
                if (num < 0) num = 0;
                $(this).closest("span").find("input").val(num);
                var ShiFuJinE = tableToolbar.calculate(FangXingJinE(), FuJianChanPinJinE(), "+");
                var AveJinE = tableToolbar.calculate(ShiFuJinE, MrHangQi.DingdanRenShu, "/");
                var JIen = tableToolbar.calculate(num, $(this).closest("span").find("input").attr("data-dj"), "*");
                $(this).closest("td").next("td").find("span").html("￥" + JIen);
                $("#td_page1zong").html("￥" + tableToolbar.getFloat(ShiFuJinE));

                $("#td_page1renjun").html("￥" + tableToolbar.getFloat(AveJinE));

            });
            $(".i_trInput").find("input").live("blur", function() {
                var num = tableToolbar.getInt($(this).val());
                $(this).val(num);
                $("#heji").html("合计：<em  class=\"b_price\">￥" + FangXingJinE() + "</em>");
            });
            $(".i_trFuJiaChanPin").find("input").blur(function() {
                var num = tableToolbar.getInt($(this).val());
                $(this).val(num);
                var ShiFuJinE = tableToolbar.calculate(FangXingJinE(), FuJianChanPinJinE(), "+");
                var AveJinE = tableToolbar.calculate(ShiFuJinE, MrHangQi.DingdanRenShu, "/");
                $("#td_page1zong").html("￥" + ShiFuJinE);

                $("#td_page1renjun").html("￥" + tableToolbar.getFloat(AveJinE));
            });

            $("#shoucang").click(function() {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/Ashx/Handler.ashx?dotype=collect&cid=" + '<%=EyouSoft.Common.Utils.GetQueryStringValue("id")  %>' + "&lxid=" + '<%=(int)EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing.长江游轮 %>',
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
            });
            setNav(1);

            $("#i_a_yingcangmingxi").click(function() {
                var _display = $("#stepHtml").css("display");
                if (_display == "none") { $("#stepHtml").show(); $(this).text('隐藏明细'); return; }
                $("#stepHtml").hide();
                $(this).text('显示明细')
            });

            PageSet.HideFaPiao();

            $("#checkY").click(function() {
                PageSet.ShowFaPiao();
            })

            $("#checkN").click(function() {
                PageSet.HideFaPiao();
            })

            $('span[data-id="chanpintese"]').bt({ contentSelector: function() { return $(this).html(); }, positions: ['top'], fill: '#FFF2B5', strokeStyle: '#D59228', noShadowOpts: { strokeStyle: "#D59228" }, spikeLength: 10, spikeGirth: 15, width: 500, overlap: 0, centerPointY: 1, cornerRadius: 4, shadow: true, shadowColor: 'rgba(0,0,0,.5)', cssStyles: { color: '#00387E', 'line-height': '180%'} });
            $('span[data-class="fujiachanpin"]').bt({ contentSelector: function() { return $(this).next().html(); }, positions: ['top'], fill: '#FFF2B5', strokeStyle: '#D59228', noShadowOpts: { strokeStyle: "#D59228" }, spikeLength: 10, spikeGirth: 15, width: 500, overlap: 0, centerPointY: 1, cornerRadius: 4, shadow: true, shadowColor: 'rgba(0,0,0,.5)', cssStyles: { color: '#00387E', 'line-height': '180%'} });
            $('b[data-id="qijiashuoming"]').bt({ contentSelector: function() { return $(this).next().html(); }, positions: ['right'], fill: '#FFF2B5', strokeStyle: '#D59228', noShadowOpts: { strokeStyle: "#D59228" }, spikeLength: 10, spikeGirth: 15, width: 500, overlap: 0, centerPointY: 1, cornerRadius: 4, shadow: true, shadowColor: 'rgba(0,0,0,.5)', cssStyles: { color: '#00387E', 'line-height': '180%'} });
        });

        function FangXIngJson() {
            var item = [];
            $(".i_trInput").find("input").each(function() {
                var items = {};
                if (tableToolbar.getInt($(this).val()) > 0) {
                    items["fx"] = tableToolbar.getInt($(this).attr("data-fx"));
                    items["bk"] = $(this).attr("data-bk");
                    items["gj"] = $(this).attr("data-gj");
                    items["rs"] = tableToolbar.getInt($(this).val());
                    items["zw"] = tableToolbar.getInt($(this).attr("data-zw"));
                    items["jn"] = tableToolbar.calculate($(this).attr("data-m"), $(this).val(), "*");
                    //items["dj"] = tableToolbar.getFloat($(this).attr("data-dj"));
                    items["dj"] = tableToolbar.getFloat($(this).attr("data-m"));
                    items["zd"] = tableToolbar.getFloat($(this).attr("data-zd"));
                    item.push(items);
                }
            });
            return item;
        }
        function FangXingJinE() {
            var item = FangXIngJson();
            var JinE = 0;
            for (var i = 0; i < item.length; i++) {
                JinE = tableToolbar.calculate(item[i].jn, JinE, "+");
            }
            return JinE;
        }
        function FuJianChanPinJson() {
            var item = [];
            $(".i_trFuJiaChanPin").find("input").each(function() {
                var items = {};
                if (tableToolbar.getInt($(this).val()) > 0) {
                    items["fj"] = $(this).attr("data-fjid");
                    items["lx"] = $(this).attr("data-lx");
                    items["dj"] = tableToolbar.getFloat($(this).attr("data-dj"));
                    items["sl"] = tableToolbar.getInt($(this).val());
                    items["jn"] = tableToolbar.calculate($(this).attr("data-dj"), $(this).val(), "*");
                    item.push(items);
                }
            });
            return item;
        }
        function FuJianChanPinJinE() {
            var item = FuJianChanPinJson();
            var JinE = 0;
            for (var i = 0; i < item.length; i++) {
                JinE = tableToolbar.calculate(item[i].jn, JinE, "+");
            }
            return JinE;
        }

        function guojimore(obj) {
            var fs = $(obj).attr("data-fs");

            if (fs == "+") {
                var div = $(obj).closest("div[data-class='guoji_1']");
                div.find("table[data-class='guoji']").each(function() {
                    if ($(this).css("display") == "none") $(this).show().css({ "margin-top": "1px" });
                });
                $(obj).html('<font style="color:#cc3939;">-</font> 点击收起更多国籍</a>');
                $(obj).attr("data-fs", "-");
            }

            if (fs == "-") {
                var div = $(obj).closest("div[data-class='guoji_1']");
                div.find("table[data-class='guoji']").each(function(i) {
                    if (i > 0) $(this).hide();
                });
                $(obj).html('<font style="color:#cc3939;">+</font> 点击查看更多国籍</a>');
                $(obj).attr("data-fs", "+");
            }
        }
    </script>

    <!--[if IE]><script src="/js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->
    <script type="text/javascript" src="/Js/bt.min.js"></script>
</asp:Content>
