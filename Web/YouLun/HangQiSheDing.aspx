<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HangQiSheDing.aspx.cs"
    Inherits="EyouSoft.Web.YouLun.HangQiSheDing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择出团日期</title>
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache, must-revalidate" />
    <meta http-equiv="expires" content="0" />
    <style>
        body
        {
            margin: auto 0;
            padding: 0;
        }
        td
        {
            font-size: 12px;
            line-height: 120%;
        }
        table
        {
            border-collapse: collapse;
            width: 100%;
            background-color: #FFFFFF;
            border-collapse: collapse;
        }
        img
        {
            border: none;
        }
        .hui
        {
            color: #aaaaaa;
        }
        .calendarTable td
        {
            border-collapse: collapse;
            border: solid 1px #999999;
        }
        /*input
        {
            display: none;
        }*/
        
        .Xdays { text-align:center;}
    </style>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/tankkuang.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="ContactBox">
    <%=stroldHangqis %>
    </form>
    <div id="div1" style="height: 400px;">
    </div>
    <div id="divTourCodeHTML" style="width: 780px; margin-top: 10px;">
    </div>
    <div class="alertbox-btn" style="position: fixed;">
        <%--<a hidefocus="true" href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;">
            <s class="chongzhi"></s>关 闭</a>--%>
        <a href="javascript:void(0)" id="i_sheding"><s class="baochun"></s>&nbsp;设定航期</a>
    </div>

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script type="text/javascript" src="/js/YLgroupdate.js?v=0.1"></script>

    <script type="text/javascript">
        var pageDataJS = {
            Params: { sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>', IframeId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("IframeId") %>' },
            UnBindBtn: function() {
                $("#btnSave").html("<s class='baochun'></s>提交中...").unbind("click").css("background-position", "0-57px");
            },
            //按钮绑定事件
            BindBtn: function() {
                $("#btnSelect").bind("click").css("background-position", "0-28px").html("<s class='baochun'></s>保 存");
                $("#btnSelect").click(function() {
                    var form = $(this).closest("form").get(0);
                    if (ValiDatorForm.validator("form", "parent")) {
                        pageDataJs.Save();
                    } else {
                        return false;
                    }
                });
            },
            CreateBoxy: function(data) {
                top.Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            callBack: function() { rili_callback(); }
        }

        $(function() {
            $(".YuKong").live("click", function() {
                var aid = $(this).attr("id");
                var data = { url: 'HangQiYuKong.aspx?aid=' + aid + '&sl=' + pageDataJS.Params.sl + '&pIframeId=' + pageDataJS.Params.IframeId + '&id=' + pageDataJS.Params.id, title: '设置人数', width: '300px', height: '100px' }
                pageDataJS.CreateBoxy(data);
            })//
            $(".xiugai").live("click", function() {
                var aid = $(this).attr("data-riqi");
                var data = { url: 'HangQiYuKong.aspx?dotype=xg&aid=' + aid + '&sl=' + pageDataJS.Params.sl + '&pIframeId=' + pageDataJS.Params.IframeId + '&id=' + pageDataJS.Params.id + '&riqiid=' + $("#ContactBox").find("div[id=" + $(this).attr("data-riqi") + "]").attr("data-id"), title: '设置人数', width: '300px', height: '100px' }
                pageDataJS.CreateBoxy(data);
            })//
            $(".quxiao").live("click", function() {
                if(!confirm("原先已设定的航期和价格信息确定要删除吗？"))return false;
                var data = {
                    hangqiid: pageDataJS.Params.id,
                    riqiid: $("#ContactBox").find("div[id=" + $(this).attr("data-riqi") + "]").attr("data-id"),
                    sl: pageDataJS.Params.sl,
                    riqi: $(this).attr("data-riqi")
                }
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/youlun/HangQiSheDing.aspx?save=save&" + $.param(data),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() { window.location.href = window.location.href });
                        } else {
                            parent.tableToolbar._showMsg(ret.msg);
                            pageDataJS.BindBtn();
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg("操作失败，请稍后重试！");
                        pageDataJS.BindBtn();
                    }
                });
            })//

        })
    </script>

    <script type="text/javascript">

        $(document).ready(function() {
            QGD.initCalendar({
                containerId: "div1",
                currentDate: <%=CurrentDate %>,
                firstMonthDate: <%=CurrentDate %>,
                nextMonthDate: <%=NextDate %>,
                listcontainer: "divTourCodeHTML",
                arrOldLeaveDate: '<% =OldDate %>'
            });
            

            /*$("#div1").find("a").each(function() {
                if ($("#ContactBox").find("#" + $(this).attr("id")).length > 0) {
                    $(this).closest("td").attr("class", "Xdays on");
                    $(this).attr("class", "quxiao").html("取消");
                    $(this).closest("td").append("&nbsp;<span class='price'><a id="+$(this).attr("id")+" href='javascript:;' class='xiugai' >修改</a></span>");
                }
            });*/

            rili_callback();
        });
    </script>
    
    <script type="text/javascript">
        function rili_callback() {
            $("#div1").find('input[type="checkbox"]').each(function() {
                $(this).unbind("click").bind("click", function() { setRiQi(this); });

                if ($("#ContactBox").find("#" + $(this).attr("value")).length == 0) return;
                $(this).closest("span").hide();

                var s = [];

                s.push('<span><a href="javascript:void(0)" class="quxiao" data-riqi="' + $(this).attr("value") + '">取消</a></span>');
                s.push('&nbsp;')
                s.push('<span><a href="javascript:void(0)" class="xiugai" data-riqi="' + $(this).attr("value") + '">修改</a></span>');

                $(this).closest("td").append(s.join(''));
            });
        }

        function setRiQi(obj) {
            var riqi = $(obj).attr("value");
            var data = getRiQi();
            var ischeck = $(obj).attr("checked");

            if (ischeck) data.push(riqi);
            else data.splice($.inArray(riqi, data), 1);

            $("div").data("data-riqi", data);
        }

        function getRiQi() {
            var data = $("div").data("data-riqi");
            if (data == null || data == "undefined") data = [];
            return data;
        }

        $("#i_sheding").click(function() {
            var riqi = getRiQi();
            if (riqi.length < 1) { alert("至少要选择一个日期"); return false; }

            var data = { url: 'HangQiYuKong.aspx?piliang=1&aid=&sl=' + pageDataJS.Params.sl + '&pIframeId=' + pageDataJS.Params.IframeId + '&id=' + pageDataJS.Params.id, title: '设置人数', width: '300px', height: '100px' }
            pageDataJS.CreateBoxy(data);
        });
    </script>

</body>
</html>
