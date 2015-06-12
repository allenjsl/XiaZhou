<%@ Page Title="团购票" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Boxy.Master"
    CodeBehind="TuanGouPiao.aspx.cs" Inherits="EyouSoft.YlWeb.TuanGou.TuanGouPiao" %>

<%@ Register Src="/UserControl/NavLeft.ascx" TagName="NavLeft" TagPrefix="uc3" %>
<%@ MasterType VirtualPath="~/MasterPage/Boxy.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
            <div class="mainbox" style="min-height:550px;">
                <form id="form_cx">
                <div class="basicT">
                    您的位置：维诗达游轮 > 团购票</div>
                <div class="tg_box1">
                    <div class="tg_nav fixed">
                        <label>
                            类 型：</label>
                        <input type="hidden" name='youlunleixing' id='youlunleixing' value='' />
                        <ul>
                            <li><a href="javascript:;" id="youlunleixing_">全部</a> </li>
                            <li><a href="javascript:;" id="youlunleixing_0">长江游轮</a></li>
                            <li><a href="javascript:;" id="youlunleixing_1">海洋邮轮</a></li>
                        </ul>
                    </div>
                    <div class="tg_nav fixed">
                        <a href="#" class="Rmore">更多</a>
                        <label>
                            航 线：</label>
                        <input type="hidden" name='hangxian' id='hangxian' value='' />
                        <ul>
                            <li><a href="javascript:;" id="hangxian_">全部</a> </li>
                            <asp:Repeater ID="rptList_HangXian" runat="server">
                                <ItemTemplate>
                                    <li><a href="javascript:;" id="hangxian_<%# Eval("XinXiId") %>">
                                        <%# Eval("MingCheng")%></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div class="tg_nav fixed">
                        <label>
                            出发地：</label>
                        <input type="hidden" name='chufadi' id='chufadi' value='' />
                        <ul>
                            <li><a href="javascript:;" id="chufadi_">全部</a> </li>
                            <asp:Repeater ID="rptList_DiZhi" runat="server">
                                <ItemTemplate>
                                    <li><a href="javascript:;" id="chufadi_<%# Eval("XinXiId") %>">
                                        <%# Eval("MingCheng")%></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div class="tg_nav fixed">
                        <label>
                            行 程：</label>
                        <input type="hidden" name='xingcheng' id='xingcheng' value='' />
                        <ul>
                            <li><a href="javascript:;" id="xingcheng_">全部</a></li>
                            <li><a href="javascript:;" id="xingcheng_1-3">1-3天</a></li>
                            <li><a href="javascript:;" id="xingcheng_4-5">4-5天</a></li>
                            <li><a href="javascript:;" id="xingcheng_6-7">6-7天</a></li>
                            <li><a href="javascript:;" id="xingcheng_8-9">8-9天</a></li>
                        </ul>
                    </div>
                    <div class="tg_nav fixed">
                        <label>
                            价 格：</label>
                        <input type="hidden" name='jiage' id='jiage' value='' />
                        <ul>
                            <li><a href="javascript:;" id="jiage_">全部</a></li>
                            <li><a href="javascript:;" id="jiage_1000">1000元以下</a> </li>
                            <li><a href="javascript:;" id="jiage_1000-2000">1000-2000元</a></li>
                            <li><a href="javascript:;" id="jiage_2000-3000">2000-3000元</a></li>
                            <li><a href="javascript:;" id="jiage_3000-4000">3000-4000元</a></li>
                            <li><a href="javascript:;" id="jiage_4000-5000">4000-5000元</a></li>
                            <li><a href="javascript:;" id="jiage_5000">5000元以上</a></li>
                        </ul>
                    </div>
                    <div class="paixu_box fixed">
                        <input type="hidden" name='sort' id='sort' value='xiaoliang' />
                        <input type="hidden" name='desc' id='desc' value='asc' />
                        <ul>
                            <li class="paixu_T">排序：</li>
                            <li><a href="javascript:;" id="sort_xiaoliang" class="on_up">销量<span></span></a></li>
                            <li><a href="javascript:;" id="sort_jiage">价格<span></span></a></li>
                            <li><a href="javascript:;" id="sort_fabushijian">发布时间<span></span></a></li>
                        </ul>
                    </div>
                </div>
                <div class="tg_list margin_T16">
                    <ul>
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <li style="width:383px;"><a target="_blank" href="TuanGouXiangQing.aspx?TuanGouId=<%# Eval("TuanGouId") %>" class="tg_img"><img src="<%#EyouSoft.YlWeb.TuPian.F1(ErpFilepath+Eval("FengMian"),390,370) %>" /></a>
                                    <h5 class="tg_title"><a target="_blank" href="TuanGouXiangQing.aspx?TuanGouId=<%# Eval("TuanGouId") %>">【<%# ((DateTime)Eval("RiQi")).ToString("MM月dd日") %>】<%# Eval("MingCheng")%></a></h5>
                                    <p>
                                        <span class="sales"><em>
                                            <%# Eval("TuanGouShu")%></em> 人已预订</span><span class="rate-info"><cite>原价：<s>¥<%# ((decimal)Eval("YuanJia")).ToString("F2") %></s></cite>
                                                <i>¥<%# ((decimal)Eval("XianJia")).ToString("F2") %></i></span></p>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="clear">
                    </div>
                </div>
                <div style="padding-top: 20px;">
                    <div id="page_change" style="width: 100%; text-align: right; margin: 0px auto 0px;
                        margin: 0; clear: both">
                    </div>
                </div>
                </form>
            </div>
          
    <script type="text/javascript">
        var Forms = {
            Load: function() {
                var str = location.search.substring(1);
                if (str == '') {
                    $('.tg_nav a[id$=\\_]').addClass('all'); return;
                }
                for (var i = 0; i < str.split('&').length; i++) {
                    var name = str.split('&')[i].split('=')[0];
                    var value = str.split('&')[i].split('=')[1];
                    var ele = document.getElementById(name + '_' + value);
                    var hiddenField = document.getElementById(name);
                    if (hiddenField == null) continue;
                    
                    hiddenField.value = value;

                    if ($(ele).parents('.tg_nav').length > 0) {
                        $(ele).addClass('all');
                        $(ele).css('width', 'auto');
                        $(ele).parents('.tg_nav').find('a').not(ele).removeClass('all');
                    }
                    if (name == 'desc') {
                        var ce = '#sort_' + location.search.match('sort=(\\w+)&?')[1]; ///
                        value == 'desc' ? $(ce).addClass('on_up') : $(ce).removeClass('on_up');
                    }
                }
            },
            Search: function(e) {
                var o = e.target;
                if (o.tagName != 'A') {
                    o = $(o).parents('a:first')[0];
                }
                var name = o.id.split('_')[0];
                var value = o.id.split('_')[1];
                var hiddenField = document.getElementById(name); //隐藏域
                hiddenField.value = value; //设置隐藏域
                if (o.id.indexOf('sort_') == 0) {
                    document.getElementById('desc').value = ($(o).is('.on_up') ? 'asc' : 'desc');
                }
                var params = $('#form_cx').serialize();
                location.href = '/TuanGou/TuanGouPiao.aspx?' + params;
            }
        };

        $(function() {
            Forms.Load();
            $('.tg_nav a,.paixu_box a').click(Forms.Search);

            setNav(3);

            if (pConfig.recordCount > 0) { AjaxPageControls.replace("page_change", pConfig); }
        });
    </script>
    

    <script type="text/javascript">
        var pConfig = { pageSize: 15, pageIndex: 1, recordCount: 0, showPrev: true, showNext: true, showDisplayText: false, cssClassName: 'page_change' }
    </script>
    <script type="text/javascript" src="/js/ajaxpagecontrols.js"></script>

    <form runat="server" id="form1">
    </form>

</asp:Content>
